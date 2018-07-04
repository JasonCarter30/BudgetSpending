using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class SupportedValue : ISupportedValue
    {
        public int SupportedValueId { get; set; }
        public string Name { get; set; }
    }
}
