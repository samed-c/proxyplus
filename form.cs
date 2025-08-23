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

using System.Collections;
using System.Security.Principal;




namespace proxyplus
{

    public partial class form : Form
    {

        bool herDurumdaKapat = false;
        public Icon mainico;
 



        public form()
        {
            InitializeComponent();
        }



        public void hidemainform()
        {
            Program.mainform.Hide();
        }
        public void showmainform()
        {
            Program.mainform.Show();
        }







        private void activate()
        {
            hidemainform();
            proxyplus activate = new proxyplus();
            activate.activate();
        }

        public void passive(bool silence)
        {
            proxyplus passive = new proxyplus();
            passive.passive(silence);
          }

        private void main_Load(object sender, EventArgs e)
        {
            Program.mainform.mainico = Program.mainform.notifyIcon1.Icon;  // PRIMARY ICON **
            passive(true);

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e2)
        {
            if (herDurumdaKapat == false)
            {
                e2.Cancel = true;
                hidemainform();
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            activate();
        }

        private void passivateProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            passive(false);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
        
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            herDurumdaKapat = true;
            passive(true);
            Application.Exit();
        }

        private void activateProxyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activate();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            showmainform();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.aboutform.ShowDialog();
        }


 
  




    }
}
