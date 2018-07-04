using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IWidgetConfigurationPropertySupportedValue
    {
        int WidgetConfigurationPropertySupportedValueId { get; set; }
        IWidgetConfigurationProperty WidgetConfigurationProperty { get; set; }
        ISupportedValue SupportedValue { get; set; }
    }
}
