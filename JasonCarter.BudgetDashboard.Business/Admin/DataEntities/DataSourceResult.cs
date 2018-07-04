using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class DataSourceResult : IDapperResult<IDataSource>
    {
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public string CommandText { get; set; }
        public DateTime CreateDate { get; set; }

        public IDataSource Convert()
        {
            IDataSource dataSource = new DataSource()
            {
                DataSourceId = DataSourceId,
                Name = Name,
                CommandText = CommandText,
                CreateDate = CreateDate
            };

            return dataSource;
        }
    }
}
