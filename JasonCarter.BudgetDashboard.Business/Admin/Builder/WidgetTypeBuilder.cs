using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class WidgetTypeBuilder : Business.Builder.Builder, IDisposable
    {
        private IWidgetType widgetType { get; set; }
        public override void Build<T>(T resultClass)
        {
            widgetType = (WidgetType)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            
        }

        public void Dispose()
        {
            widgetType = null;
        }

        public override object GetResult()
        {
            return widgetType;
        }
    }
}
