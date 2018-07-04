using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class Dashboard : IDashboard
    {
        public int DashboardId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public  DateTime CreateDate { get; set; }
    }
}
