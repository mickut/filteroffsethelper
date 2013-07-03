namespace FilterOffsetHelper
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }
    }
}