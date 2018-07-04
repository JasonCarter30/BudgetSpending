using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IDataSource
    {
        int DataSourceId { get; set; }
        string Name { get; set; }
        string CommandText { get; set; }
        DateTime CreateDate { get; set; }
    }
}
