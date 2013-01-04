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
    }
}
