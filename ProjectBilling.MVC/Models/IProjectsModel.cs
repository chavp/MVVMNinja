// -----------------------------------------------------------------------
// <copyright file="IProjectsModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ProjectBilling.MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BillingWin.DataAccess;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface IProjectsModel
    {
        IEnumerable<Project> Projects { get; set; }
        event EventHandler<ProjectEventArgs> ProjectUpdated;
        void UpdateProject(Project project);
        Project GetProject(int Id);
    }

    public class ProjectsModel : IProjectsModel
    {
        public IEnumerable<Project> Projects { get; set; }
        public event EventHandler<ProjectEventArgs> ProjectUpdated = delegate { };

        public ProjectsModel()
        {
            Projects = new DataServiceStub().GetProjects();
        }

        private void RaiseProjectUpdated(Project project)
        {
            ProjectUpdated(this, new ProjectEventArgs(project));
        }

        public void UpdateProject(Project project)
        {
            Project selectedProject
            = Projects.Where(p => p.ID == project.ID)
            .FirstOrDefault() as Project;
            selectedProject.Name = project.Name;
            selectedProject.Estimate = project.Estimate;
            selectedProject.Actual = project.Actual;
            RaiseProjectUpdated(selectedProject);
        }


        public Project GetProject(int Id)
        {
            return Projects.Where(p => p.ID == Id).First() as Project; ;
        }
    }

    public class ProjectEventArgs : EventArgs
    {
        public Project Project { get; set; }
        public ProjectEventArgs(Project project)
        {
            Project = project;
        }
    }
}
