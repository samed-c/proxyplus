using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace proxyplus
{
    static class Program
    {



  
        public static form mainform;
        public static About aboutform;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            mainform = new form();
            aboutform = new About();

            Application.Run(mainform);



        }
    }
}
