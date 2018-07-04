using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class DataSource : IDataSource
    {
        public int DataSourceId { get; set; }
        public string Name { get; set; }
        public string CommandText { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
