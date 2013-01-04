using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;

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
            if (hasNTNUIP()) MessageBox.Show("yes");
            else MessageBox.Show("no");
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
    }
}
