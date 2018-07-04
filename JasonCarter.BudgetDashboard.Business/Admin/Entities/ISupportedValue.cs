using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface ISupportedValue
    {
        int SupportedValueId { get; set; }
        string Name { get; set; }
    }
}
