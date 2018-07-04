using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IDashboard
    {
        int DashboardId { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        DateTime CreateDate { get; set; }
    }
}
