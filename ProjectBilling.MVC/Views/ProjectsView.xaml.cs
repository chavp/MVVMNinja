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
using System.Windows.Shapes;
using ProjectBilling.MVC.Models;
using ProjectBilling.MVC.Controllers;
using BillingWin.DataAccess;

namespace ProjectBilling.MVC.Views
{
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : Window
    {
        private readonly IProjectsModel _model;
        private readonly IProjectsController _controller = null;
        private const int NONE_SELECTED = -1;

        public ProjectsView(IProjectsController projectsController, IProjectsModel projectsModel)
        {
            InitializeComponent();

            _controller = projectsController;
            _model = projectsModel;
            _model.ProjectUpdated += model_ProjectUpdated;
            ProjectsComboBox.ItemsSource = _model.Projects;
            ProjectsComboBox.DisplayMemberPath = "Name";
            ProjectsComboBox.SelectedValuePath = "ID";
        }

        void model_ProjectUpdated(object sender, ProjectEventArgs e)
        {
            int selectedProjectId = GetSelectedProjectId();
            if (selectedProjectId > NONE_SELECTED)
            {
                if (selectedProjectId == e.Project.ID)
                {
                    UpdateDetails(e.Project);
                }
            }
        }

        private void ProjectsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Project project = GetSelectedProject();
            if (project != null)
            {
                EstimatedTextBox.Text
                = project.Estimate.ToString();
                EstimatedTextBox.IsEnabled = true;
                ActualTextBox.Text
                = project.Actual.ToString();
                ActualTextBox.IsEnabled = true;
                UpdateButton.IsEnabled = true;
                UpdateEstimatedColor();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project()
            {
                ID = (int)ProjectsComboBox.SelectedValue,
                Name = ProjectsComboBox.Text,
                Estimate = GetDouble(EstimatedTextBox.Text),
                Actual = GetDouble(ActualTextBox.Text)
            };
            _controller.Update(project);
        }

        private void UpdateEstimatedColor()
        {
            double actual = GetDouble(ActualTextBox.Text);
            double estimated = GetDouble(EstimatedTextBox.Text);
            if (actual == 0)
            {
                EstimatedTextBox.Foreground
                = ActualTextBox.Foreground;
            }
            else if (actual > estimated)
            {
                EstimatedTextBox.Foreground
                = Brushes.Red;
            }
            else
            {
                EstimatedTextBox.Foreground
                = Brushes.Green;
            }
        }
        private void UpdateDetails(Project project)
        {
            EstimatedTextBox.Text = project.Estimate.ToString();
            ActualTextBox.Text = project.Actual.ToString();
            UpdateEstimatedColor();
        }
        private double GetDouble(string text)
        {
            return string.IsNullOrEmpty(text) ? 0 : double.Parse(text);
        }
        private Project GetSelectedProject()
        {
            return ProjectsComboBox.SelectedItem as Project;
        }
        private int GetSelectedProjectId()
        {
            Project project = GetSelectedProject();
            return (project == null)? NONE_SELECTED : project.ID;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            _model.ProjectUpdated -= model_ProjectUpdated;
        }

        ~ProjectsView()
        {
            MessageBox.Show("ProjectsView collected");
        }
    }
}
