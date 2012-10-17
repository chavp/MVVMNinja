// -----------------------------------------------------------------------
// <copyright file="ProjectsView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Billing.Monolithic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using BillingWin.DataAccess;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    sealed class ProjectsView : Window
    {
        private static readonly Thickness _margin = new Thickness(5);
        private readonly ComboBox _projectsComboBox = new ComboBox() { Margin = _margin };
        private readonly TextBox _estimateTextBox = new TextBox() { IsEnabled = false, Margin = _margin };
        private readonly TextBox _actualTextBox = new TextBox() { IsEnabled = false, Margin = _margin };
        private readonly Button _updateButton = new Button()
        {
            IsEnabled = false,
            Content = "Update",
            Margin = _margin
        };

        public ProjectsView()
        {
            Title = "Project";
            Width = 250;
            MinWidth = 250;
            Height = 180;
            MinHeight = 180;

            LoadProjects();
            AddControlsToWindow();

            _updateButton.Click += updateButton_Click;
        }

        private void projectsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox != null && comboBox.SelectedIndex > -1)
            {
                UpdateDetails();
            }
            else
            {
                DisableDetails();
            }
        }

        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            Project selectedProject = _projectsComboBox.SelectedItem as Project;
            if (selectedProject != null)
            {
                selectedProject.Estimate = double.Parse(_estimateTextBox.Text);
                if (!string.IsNullOrEmpty(_actualTextBox.Text))
                {
                    selectedProject.Actual = double.Parse(_actualTextBox.Text);
                }
                SetEstimateColor(selectedProject);
            }
        }

        private void LoadProjects()
        {
            foreach (Project project in new DataServiceStub().GetProjects())
            {
                _projectsComboBox.Items.Add(project);
            }
            _projectsComboBox.DisplayMemberPath = "Name";
            _projectsComboBox.SelectionChanged += new SelectionChangedEventHandler(projectsListBox_SelectionChanged);
        }

        private void AddControlsToWindow()
        {
            UniformGrid grid = new UniformGrid() { Columns = 2 };
            grid.Children.Add(new Label() { Content = "Project:" });
            grid.Children.Add(_projectsComboBox);
            Label label = new Label() { Content = "Estimated Cost:" };
            grid.Children.Add(label);
            grid.Children.Add(_estimateTextBox);
            label = new Label() { Content = "Actual Cost:" };
            grid.Children.Add(label);
            grid.Children.Add(_actualTextBox);
            grid.Children.Add(_updateButton);
            Content = grid;
        }

        private Grid GetGrid()
        {
            Grid grid = new Grid();
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.ColumnDefinitions.Add(new ColumnDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            return grid;
        }

        private void UpdateDetails()
        {
            Project selectedProject = _projectsComboBox.SelectedItem as Project;
            _estimateTextBox.IsEnabled = true;
            _estimateTextBox.Text= selectedProject.Estimate.ToString();
            _actualTextBox.IsEnabled = true;
            _actualTextBox.Text= (selectedProject.Actual == 0)? "": selectedProject.Actual.ToString();
            SetEstimateColor(selectedProject);
            _updateButton.IsEnabled = true;
        }

        private void DisableDetails()
        {
            _estimateTextBox.IsEnabled = false;
            _actualTextBox.IsEnabled = false;
            _updateButton.IsEnabled = false;
        }

        private void SetEstimateColor(Project selectedProject)
        {
            if (selectedProject.Actual == 0)
            {
                this._estimateTextBox.Foreground = _actualTextBox.Foreground;
            }
            else if (selectedProject.Actual <= selectedProject.Estimate)
            {
                this._estimateTextBox.Foreground = Brushes.Green;
            }
            else
            {
                this._estimateTextBox.Foreground = Brushes.Red;
            }
        }
    }
}
