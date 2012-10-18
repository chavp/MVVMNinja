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
using ProjectBilling.MVP.Models;
using BillingWin.DataAccess;

namespace ProjectBilling.MVP.Views
{
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class ProjectsView : Window, IProjectsView
    {
        public int NONE_SELECTED { get { return -1; } }
        public event EventHandler<ProjectEventArgs> ProjectUpdated = delegate { };
        public int SelectedProjectId { get; private set; }
        public event EventHandler SelectionChanged = delegate { };
        public event EventHandler<ProjectEventArgs> DetailsUpdated = delegate { };

        public ProjectsView()
        {
            InitializeComponent();
            SelectedProjectId = NONE_SELECTED;
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Project project = new Project();
            project.Estimate = GetDouble(EstimatedTextBox.Text);
            project.Actual = GetDouble(ActualTextBox.Text);
            project.ID = int.Parse(ProjectsComboBox.SelectedValue.ToString());
            ProjectUpdated(this, new ProjectEventArgs(project));
        }

        private double GetDouble(string text)
        {
            return string.IsNullOrEmpty(text) ? 0 : double.Parse(text);
        }

        private void ProjectsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedProjectId= (ProjectsComboBox.SelectedValue == null)? NONE_SELECTED: int.Parse(
            ProjectsComboBox.SelectedValue.ToString());
            SelectionChanged(this,new EventArgs());
        }

        public void UpdateProject(Project project)
        {
            // Null checks excluded
            IEnumerable<Project> projects = ProjectsComboBox.ItemsSource as IEnumerable<Project>;
            Project projectToUpdate = projects.Where(p => p.ID == project.ID).First();
            projectToUpdate.Estimate = project.Estimate;
            projectToUpdate.Actual = project.Actual;
            if (project.ID == SelectedProjectId)
                UpdateDetails(project);
        }
        public void LoadProjects(IEnumerable<Project> projects)
        {
            ProjectsComboBox.ItemsSource = projects;
            ProjectsComboBox.DisplayMemberPath = "Name";
            ProjectsComboBox.SelectedValuePath = "ID";
        }
        public void EnableControls(bool isEnabled)
        {
            EstimatedTextBox.IsEnabled = isEnabled;
            ActualTextBox.IsEnabled = isEnabled;
            UpdateButton.IsEnabled = isEnabled;
        }
        public void SetEstimatedColor(Color? color)
        {
            EstimatedTextBox.Foreground = (color == null) ? ActualTextBox.Foreground: new SolidColorBrush((Color)color);
        }
        public void UpdateDetails(Project project)
        {
            EstimatedTextBox.Text = project.Estimate.ToString();
            ActualTextBox.Text = project.Actual.ToString();
            DetailsUpdated(this,new ProjectEventArgs(project));
        }
    }
}
