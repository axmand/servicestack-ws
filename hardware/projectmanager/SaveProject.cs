using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hardware.projectmanager;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static hardware.projectmanager.ImportProject;
using System.Reflection;

namespace hardware.projectmanager
{
    public class SaveProject
    {
        /// <summary>
        /// 接收到前端返回的修改数据进行判断修改并保存
        /// </summary>
        /// <param name="str">接收到的前端返回值</param>
        /// <returns>true or false</returns>
        public static bool SavePro(string str)
        {
            try
            {
                string _oldDataStr = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + _importProjectName + "\\Forms\\all.txt", Encoding.Default);
                Type _listFormsType = typeof(List<Forms>);
                Type _formsType = typeof(List<Forms>);
                var _listFormsTypeProperties = _listFormsType.GetProperties();
                var _formsTypeProperties = _formsType.GetProperties();
                List<Forms> _oldData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forms>>(_oldDataStr);
                List<Forms> _inputData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forms>>(str);


                for (int i = 0; i < _inputData.Count; i++)
                {
                    Forms _inputForms = _inputData[i];
                    Forms _oldForms = _oldData[i];
                    Type t1 = _inputForms.GetType();
                    PropertyInfo[] p = t1.GetProperties();

                    for (int j = 0; j < p.Length; j++)
                    {
                        string name1 = p[j].Name;
                        object value1 = p[j].GetValue(_inputForms, null);
                        object value2 = p[j].GetValue(_oldForms, null);
                        if (value1 != value2)
                        {
                            p[j].SetValue(_oldForms, value1);
                        }
                    }
                    _oldData[i] = _oldForms;
                }
                string _fixDataStr = Newtonsoft.Json.JsonConvert.SerializeObject(_oldData);

                using (FileStream fs = new FileStream(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + _importProjectName + "\\Forms\\all.txt", FileMode.Create, FileAccess.Write))//保存的新数据库的地址
                {

                    fs.Lock(0, fs.Length);
                    StreamWriter sw = new StreamWriter(fs,Encoding.GetEncoding("gb2312"));
                    sw.Write(_fixDataStr);
                    sw.Flush();
                }
                return true;
            }
            catch (Exception ex) {
                var msg = ex;
                return false;
            }
        }

        
    }
}
