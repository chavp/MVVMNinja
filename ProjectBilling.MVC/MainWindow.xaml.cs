using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProjectBilling.MVC.Controllers;
using ProjectBilling.MVC.Models;

namespace ProjectBilling.MVC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IProjectsController _controller;

        public MainWindow()
        {
            InitializeComponent();
            _controller = new ProjectsController(new ProjectsModel());
        }

        private void ShowProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            _controller.ShowProjectsView(this);
        }

        private void GCCollectButton_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
