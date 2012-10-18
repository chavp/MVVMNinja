// -----------------------------------------------------------------------
// <copyright file="ProjectsPresenter.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectBilling.MVP.Presentors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using ProjectBilling.MVP.Views;
    using ProjectBilling.MVP.Models;
    using BillingWin.DataAccess;
    using System.Windows.Media;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProjectsPresenter
    {
        private readonly IProjectsView _view = null;
        private readonly IProjectsModel _model = null;

        public ProjectsPresenter(IProjectsView projectsView, IProjectsModel projectsModel)
        {
            _view = projectsView;
            _view.ProjectUpdated += view_ProjectUpdated;
            _view.SelectionChanged += view_SelectionChanged;
            _view.DetailsUpdated += view_DetailsUpdated;
            _model = projectsModel;
            _model.ProjectUpdated += model_ProjectUpdated;
            _view.LoadProjects(_model.GetProjects());
        }

        private void view_DetailsUpdated(object sender, ProjectEventArgs e)
        {
            SetEstimatedColor(e.Project);
        }

        private void view_SelectionChanged(object sender, EventArgs e)
        {
            int selectedId = _view.SelectedProjectId;
            if (selectedId > _view.NONE_SELECTED)
            {
                Project project = _model.GetProject(selectedId);
                _view.EnableControls(true);
                _view.UpdateDetails(project);
                SetEstimatedColor(project);
            }
            else
            {
                _view.EnableControls(false);
            }
        }

        private void model_ProjectUpdated(object sender, ProjectEventArgs e)
        {
            _view.UpdateProject(e.Project);
        }

        private void view_ProjectUpdated(object sender, ProjectEventArgs e)
        {
            _model.UpdateProject(e.Project);
            SetEstimatedColor(e.Project);
        }

        private void SetEstimatedColor(Project project)
        {
            if (project.ID == _view.SelectedProjectId)
            {
                if (project.Actual <= 0)
                {
                    _view.SetEstimatedColor(null);
                }
                else if (project.Actual
                > project.Estimate)
                {
                    _view.SetEstimatedColor(Colors.Red);
                }
                else
                {
                    _view.SetEstimatedColor(Colors.Green);
                }
            }
        }
    }
}
