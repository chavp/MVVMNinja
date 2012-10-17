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
using BillingWin.DataAccess;

namespace BillingWin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            System.Windows.Data.CollectionViewSource projectViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("projectViewSource")));
            // Load data by setting the CollectionViewSource.Source property:
            projectViewSource.Source = new DataServiceStub().GetProjects();
        }

        private void cmdUpdate_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = this.nameComboBox.SelectedItem as Project;
            if (selectedProject != null)
            {
                selectedProject.Estimate = double.Parse(this.estimateTextBox.Text);
                if (!string.IsNullOrEmpty(
                this.actualTextBox.Text))
                {
                    selectedProject.Actual = double.Parse(
                    this.actualTextBox.Text);
                }
                SetEstimateColor(selectedProject);
            }
        }

        private void SetEstimateColor(Project selectedProject)
        {
            if (selectedProject.Actual == 0)
            {
                this.estimateTextBox.Foreground = Brushes.Black;
            }
            else if (selectedProject.Actual <= selectedProject.Estimate)
            {
                this.estimateTextBox.Foreground = Brushes.Green;
            }
            else
            {
                this.estimateTextBox.Foreground = Brushes.Red;
            }
        }

        private void nameComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            // If there is a selected item
            if (comboBox != null && comboBox.SelectedIndex > -1)
            {
                Project selectedProject
                = comboBox.SelectedItem as Project;
                SetEstimateColor(selectedProject);
                this.cmdUpdate.IsEnabled = true;
            }
            else
            {
                this.estimateTextBox.IsEnabled = false;
                this.actualTextBox.IsEnabled = false;
                this.cmdUpdate.IsEnabled = false;
            }
        }
    }
}
