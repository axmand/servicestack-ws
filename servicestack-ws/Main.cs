using inventory_server.Helper;
using System;
using System.Windows.Forms;

namespace servicestack_ws
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            const string _url = "http://*:1338/";
            InventoryHelper.Start(_url);
            //最小化
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;  //不显示在系统任务栏
            notifyIcon.Visible = true;  //托盘图标可见
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
