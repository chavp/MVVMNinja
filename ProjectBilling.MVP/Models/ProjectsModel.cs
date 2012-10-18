// -----------------------------------------------------------------------
// <copyright file="ProjectsModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectBilling.MVP.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BillingWin.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProjectsModel : IProjectsModel
    {
        private IEnumerable<Project> projects = null;

        public event EventHandler<ProjectEventArgs> ProjectUpdated = delegate { };

        public ProjectsModel()
        {
            projects = new DataServiceStub().GetProjects();
        }

        public void UpdateProject(BillingWin.DataAccess.Project project)
        {
            ProjectUpdated(this, new ProjectEventArgs(project));
        }

        public IEnumerable<Project> GetProjects()
        {
            return projects;
        }

        public Project GetProject(int Id)
        {
            return projects.Where(p => p.ID == Id).First() as Project;
        }
    }
}
