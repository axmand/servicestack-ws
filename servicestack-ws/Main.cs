using inventory_server;
using inventory_server.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace servicestack_ws
{
    public partial class Main : Form
    {

        const string _url = "http://localhost:1337/";

        public Main()
        {
            InitializeComponent();

            InventoryHelper.Start(_url);

        }
    }
}
