using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class WidgetConfigurationPropertySupportedValue : IWidgetConfigurationPropertySupportedValue
    {
        public int WidgetConfigurationPropertySupportedValueId { get; set; }
        public IWidgetConfigurationProperty WidgetConfigurationProperty { get; set; }
        public ISupportedValue SupportedValue { get; set; }
    }
}
