using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dep_Update
{
    public partial class Loading : Form
    {
        string ftpUsername;
        string ftpPassword;
        public Loading()
        {
            //server route
            InitializeComponent();
            String serveropertaionasd = ConfigurationManager.AppSettings.Get("SelectType");

            //if separator
            if (serveropertaionasd == "Server")
            {


                this.Show();
                ftpUsername = ConfigurationManager.AppSettings.Get("UserName");
                ftpPassword = ConfigurationManager.AppSettings.Get("Password");
                string urlDonlowad = ConfigurationManager.AppSettings.Get("ServerName");
                //check old update and delete
                if (File.Exists(@"C:\dep\dep.zip"))
                {
                    Console.WriteLine("The file exists.");
                }
                //DonwloadFTP
                DownloadFile(urlDonlowad, @"C:\dep\dep.zip");
                //Dezarhivare
                // DecompressFileLZMA(@"E:\poze\Update.7z", @"E:\poze\dep");
                ExtractFile(@"C:\dep\dep.zip", @"C:\Dep\Update\dezarhivat");
                //Copy Over Dep
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(@"C:\Dep\Update\dezarhivat", "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(@"C:\Dep\Update\dezarhivat", @"C:\dep\"));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@"C:\Dep\Update\dezarhivat", "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(@"C:\Dep\Update\dezarhivat", @"C:\dep\"), true);
                //ftp Check - Download
                //copy to homeftpServer
                string HomeFTPServer = ConfigurationManager.AppSettings.Get("FTPLocalHost");


                foreach (string dirPath in Directory.GetDirectories(HomeFTPServer, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(HomeFTPServer, @"C:\dep\"));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(HomeFTPServer, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(HomeFTPServer, @"C:\dep\"), true);
                //open dep
                Process.Start("C:\\dep\\dep.exe");
                this.Close();
            }
            else
            {

                //client route


                this.Show();
                ftpUsername = ConfigurationManager.AppSettings.Get("UserName");
                ftpPassword = ConfigurationManager.AppSettings.Get("Password");
                string urlDonlowad = ConfigurationManager.AppSettings.Get("ServerName");
                //check old update and delete
                if (File.Exists(@"C:\dep\dep.zip"))
                {
                    Console.WriteLine("The file exists.");
                }
                //DonwloadFTP
                string HomeFTPServer = ConfigurationManager.AppSettings.Get("FTPLocalHost");
                DownloadFile(HomeFTPServer, @"C:\dep\dep.zip");
                //Dezarhivare
                // DecompressFileLZMA(@"E:\poze\Update.7z", @"E:\poze\dep");
                ExtractFile(@"C:\dep\dep.zip", @"C:\Dep\Update\dezarhivat");
                //Copy Over Dep
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(@"C:\Dep\Update\dezarhivat", "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(@"C:\Dep\Update\dezarhivat", @"C:\dep\"));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(@"C:\Dep\Update\dezarhivat", "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(@"C:\Dep\Update\dezarhivat", @"C:\dep\"), true);
                //ftp Check - Download
                //copy to homeftpServer
                
                //open dep
                Process.Start("C:\\dep\\dep.exe");
                this.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        public void ExtractFile(string sourceArchive, string destination)
        {
            string zPath = "7za.exe"; //add to proj and set CopyToOuputDir
            try
            {
                ProcessStartInfo pro = new ProcessStartInfo();
                pro.WindowStyle = ProcessWindowStyle.Hidden;
                pro.FileName = zPath;
                pro.Arguments = string.Format("x \"{0}\" -y -o\"{1}\"", sourceArchive, destination);
                Process x = Process.Start(pro);
                x.WaitForExit();
            }
            catch (System.Exception Ex)
            {
                //handle error
            }
        }
        private static void DecompressFileLZMA(string inFile, string outFile)
        {
            SevenZip.Compression.LZMA.Decoder coder = new SevenZip.Compression.LZMA.Decoder();
            FileStream input = new FileStream(inFile, FileMode.Open);
            FileStream output = new FileStream(outFile, FileMode.Create);

            // Read the decoder properties
            byte[] properties = new byte[5];
            input.Read(properties, 0, 5);

            // Read in the decompress file size.
            byte[] fileLengthBytes = new byte[8];
            input.Read(fileLengthBytes, 0, 8);
            long fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(input, output, input.Length, fileLength, null);
            output.Flush();
            output.Close();
        }
        public void DownloadFile(string url, string savePath)
        {
            var client = new WebClient();
            client.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
            client.DownloadFile(url, savePath);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }
    }
}
