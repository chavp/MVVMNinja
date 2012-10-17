// -----------------------------------------------------------------------
// <copyright file="IProjectsController.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectBilling.MVC.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using BillingWin.DataAccess;
    using ProjectBilling.MVC.Models;
    using ProjectBilling.MVC.Views;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProjectsController
    {
        void ShowProjectsView(Window owner);
        void Update(Project project);
    }

    public class ProjectsController : IProjectsController
    {
        private readonly IProjectsModel _model;
        public ProjectsController(IProjectsModel projectModel)
        {
            if (projectModel == null) throw new ArgumentNullException("projectModel");
            _model = projectModel;
        }
        public void ShowProjectsView(Window owner)
        {
            ProjectsView view = new ProjectsView(this, _model);
            view.Owner = owner;
            view.Show();
        }
        public void Update(Project project)
        {
            _model.UpdateProject(project);
        }
    }
}
