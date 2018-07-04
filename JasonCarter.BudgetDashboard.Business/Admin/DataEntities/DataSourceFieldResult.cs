using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class DataSourceFieldResult : IDapperResult<IDataSourceField>
    {
        public IDataSourceField Convert()
        {
            throw new NotImplementedException();
        }
    }
}
