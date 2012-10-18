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
using ProjectBilling.MVP.Models;
using ProjectBilling.MVP.Views;
using ProjectBilling.MVP.Presentors;

namespace ProjectBilling.MVP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IProjectsModel _model = null;

        public MainWindow()
        {
            InitializeComponent();
            _model = new ProjectsModel();
        }

        private void ShowProjectsButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectsView view = new ProjectsView();
            ProjectsPresenter presenter = new ProjectsPresenter(view, _model);
            view.Owner = this;
            view.Show();
        }
    }
}
