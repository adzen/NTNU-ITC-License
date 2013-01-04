using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.IO;
using System.ServiceProcess;
using System.Diagnostics;

namespace license
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            if ( !hasNTNUIP() )
            {
                MessageBox.Show(@"您似乎還沒連上網路，或是電腦並沒在師大的校園網路內！", @"錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string workingDir = searchForWorkingDir();
            if (workingDir == null)
            {
                MessageBox.Show(@"您似乎還沒安裝 Office 2010！", @"錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Windows XP
            if (Environment.OSVersion.ToString().Contains(@"Windows NT 5") && !osppsvcIsRunning())
            {
                if (!runScript(@"/osppsvcrestart", workingDir, @"Successfully restarted",
                                @"Office Software Protection Platform 服務重新啟動失敗！")) return;
            }

            if ( runScript(@"/dstatus", workingDir, @"LICENSE STATUS:  ---LICENSED---", null) )
            {
                if (MessageBox.Show(@"您早已啟用成功...還需要重新啟用嗎？", @"問題",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == System.Windows.Forms.DialogResult.No) return;
            }

            if (!runScript(@"/sethst:w7-kms.itc.ntnu.edu.tw", workingDir, @"Successfully applied setting.", @"KMS 伺服器設定失敗！"))
            {
                return;
            }
            


        }

        private bool hasNTNUIP()
        {
            try
            {
                IPAddress[] ipList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
                foreach (IPAddress ip in ipList)
                {
                    if (ip.ToString().StartsWith(@"140.122.")) return true;
                }
            }
            catch(Exception goodgame)
            {
                MessageBox.Show(goodgame.Message);
            }
            return false;
        }

        private string searchForWorkingDir() 
        {
            for (char disk = 'C'; disk <= 'Z'; disk++)
            {
                if (File.Exists(disk + @":\Program Files\Microsoft Office\Office14\ospp.vbs"))
                {
                    return (disk + @":\Program Files\Microsoft Office\Office14");
                }
                if (File.Exists(disk + @":\Program Files (x86)\Microsoft Office\Office14\ospp.vbs"))
                {
                    return (disk + @":\Program Files (x86)\Microsoft Office\Office14");
                }
            }
            return null;
        }

        private bool osppsvcIsRunning()
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();

                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == "osppsvc" && service.Status == ServiceControllerStatus.Running)
                        return true;
                }
            }
            catch (Exception gg)
            {
                MessageBox.Show(gg.Message);                
            }
            return false;
        }

        private bool runScript(string arguments, string workingDir, string successText, string errMsg) 
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(@"cscript", @"ospp.vbs " + arguments);
                psi.RedirectStandardOutput = true;
                psi.UseShellExecute = false;
                psi.WorkingDirectory = workingDir;

                Process pc = new Process();
                pc.StartInfo = psi;
                pc.Start();
                outputTextBox.Text = pc.StandardOutput.ReadToEnd();

                if (!outputTextBox.Text.Contains(successText))
                {
                    if(errMsg != null) MessageBox.Show(errMsg, @"錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            catch (Exception gg)
            {
                MessageBox.Show(gg.Message);
            }
            return true;
        }
    }
}
