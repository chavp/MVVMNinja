// -----------------------------------------------------------------------
// <copyright file="ProjectEventArgs.cs" company="">
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
    public class ProjectEventArgs : EventArgs
    {
        public Project Project { get; set; }
        public ProjectEventArgs(Project project)
        {
            Project = project;
        }
    }
}
