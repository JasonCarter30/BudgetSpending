using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class DashboardResult : IDapperResult<IDashboard>
    {
        public int DashboardId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }

        public IDashboard Convert()
        {
            IDashboard dashboard = new Dashboard()
            {
                DashboardId = DashboardId,
                Name = Name,
                Title = Title,
                CreateDate = CreateDate
            };

            return dashboard;
        }
    }
}
