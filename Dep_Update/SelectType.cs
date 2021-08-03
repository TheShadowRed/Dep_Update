using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dep_Update
{
    public partial class SelectType : Form
    {

        public SelectType()
        {
            InitializeComponent();
            //Kill Dep
            foreach (var process in Process.GetProcessesByName("Dep"))
            {
                process.Kill();
            }
            //Check Config
            string operationDecide = ConfigurationManager.AppSettings.Get("ConfigDone");
            if (operationDecide=="DA")
            {
                this.Hide();
                var Loadingo = new Loading();
                Loadingo.Closed += (s, args) => this.Close();
                Loadingo.Show();
            }
        }
       
        
        private void button1_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["SelectType"].Value = "Server";
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
            //this is server
            this.Hide();
            var svOperation = new ServerOperation();
            svOperation.Closed += (s, args) => this.Close();
            svOperation.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
            config.AppSettings.Settings["SelectType"].Value = "Client";
            config.Save(ConfigurationSaveMode.Minimal);
            ConfigurationManager.RefreshSection("appSettings");
            //this is client
            this.Hide();
            var clOperation = new ClientOperation();
            clOperation.Closed += (s, args) => this.Close();
            clOperation.Show();
        }
    }
}
