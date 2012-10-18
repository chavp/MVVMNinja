// -----------------------------------------------------------------------
// <copyright file="IProjectsView.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectBilling.MVP.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ProjectBilling.MVP.Models;
    using BillingWin.DataAccess;
    using System.Windows.Media;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProjectsView
    {
        int NONE_SELECTED { get; }
        int SelectedProjectId { get; }
        void UpdateProject(Project project);
        void LoadProjects(IEnumerable<Project> projects);
        void UpdateDetails(Project project);
        void EnableControls(bool isEnabled);
        void SetEstimatedColor(Color? color);
        event EventHandler<ProjectEventArgs> ProjectUpdated;
        event EventHandler<ProjectEventArgs> DetailsUpdated;
        event EventHandler SelectionChanged;
    }
}
