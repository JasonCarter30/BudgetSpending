using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class SupportedValueBuilder : Business.Builder.Builder, IDisposable
    {
        private ISupportedValue supportedValue { get; set; }
        public override void Build<T>(T resultClass)
        {
            supportedValue = (SupportedValue)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            supportedValue = null;
        }

        public override object GetResult()
        {
            return supportedValue;
        }
    }
}
