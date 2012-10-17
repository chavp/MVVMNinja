using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Billing.Monolithic
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            ProjectsView mainWindow = new ProjectsView();
            new Application().Run(mainWindow);
        }
    }
}
