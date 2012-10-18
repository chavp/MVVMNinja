// -----------------------------------------------------------------------
// <copyright file="IProjectsModel.cs" company="">
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
    public interface IProjectsModel
    {
        void UpdateProject(Project project);
        IEnumerable<Project> GetProjects();
        Project GetProject(int Id);
        event EventHandler<ProjectEventArgs> ProjectUpdated;
    }
}
