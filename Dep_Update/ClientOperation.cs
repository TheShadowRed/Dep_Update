using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dep_Update
{
    public partial class ClientOperation : Form
    {
        public ClientOperation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);

                    textBox4.Text = fbd.SelectedPath;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox4.Text == "")
            {
                //mesaj introduce datele
            }
            else
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);
                config.AppSettings.Settings["UserName"].Value = textBox1.Text;
                config.AppSettings.Settings["Password"].Value = textBox2.Text;
                config.AppSettings.Settings["ServerName"].Value = textBox3.Text;
                config.AppSettings.Settings["FTPLocalHost"].Value = textBox4.Text;
                config.AppSettings.Settings["ConfigDone"].Value = "DA";
                config.Save(ConfigurationSaveMode.Minimal);
                ConfigurationManager.RefreshSection("appSettings");
                this.Close();
            }
        }
    }
}
