﻿using System;
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
        public static bool SavePro(string projname,string str)
        {
            string _oldDataStr = System.IO.File.ReadAllText(System.IO.Directory.GetCurrentDirectory() + "\\ProjectTest\\" + projname + "\\Forms\\form1.txt", Encoding.Default);
            Type _listFormsType = typeof(List<Forms>);
            Type _formsType = typeof(Forms);
            var _listFormsTypeProperties = _listFormsType.GetProperties();
            var _formsTypeProperties = _formsType.GetProperties();
            List<Forms> _inputData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forms>>(str);
            List<Forms> _oldData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Forms>>(_oldDataStr);

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
            var ja = JArray.Parse(_fixDataStr);
            using (FileStream fs = new FileStream(@"D:\ProjectFormTemplet\test0921.txt", FileMode.Append, FileAccess.Write))
            {

                fs.Lock(0, fs.Length);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(ja);
                sw.Flush();
            }
            return true;
        }
        
    }
}