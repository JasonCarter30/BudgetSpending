using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business;
using System;
using System.Collections.Generic;
using System.Text;


namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class DataSourceBuilder : Business.Builder.Builder, IDisposable
    {
        internal IDataSource dataSource;

        public override void Build<T>(T resultClass)
        {
            dataSource = (DataSource)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            dataSource = null;
        }

        public override object GetResult()
        {
            return dataSource;
        }
    }
}
