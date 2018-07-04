using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class SupportedValueResult : IDapperResult<ISupportedValue>
    {
        public int SupportedValueId { get; set; }
        public string Name { get; set; }

        public ISupportedValue Convert()
        {
            ISupportedValue supportedValue = new SupportedValue()
            {
                SupportedValueId = SupportedValueId,
                Name = Name
            };

            return supportedValue;
        }
    }
}
