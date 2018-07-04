using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Data.DataAccess
{
    public interface IDapperResult<out T>
    {
        T Convert();
    }
}
