using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class DashboardBuilder : Business.Builder.Builder, IDisposable
    {
        private IDashboard dashboard;
        public override void Build<T>(T resultClass)
        {
            dashboard = (Dashboard)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            dashboard = null;
        }

        public override object GetResult()
        {
            return dashboard;
        }
    }
}
