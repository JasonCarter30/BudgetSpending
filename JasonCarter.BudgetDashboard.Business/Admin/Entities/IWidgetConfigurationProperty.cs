using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IWidgetConfigurationProperty
    {
        int WidgetConfigurationPropertyId { get; set; }
        IWidgetConfiguration WidgetConfiguration { get; set; }
        IProperty Property { get; set; }
        dynamic DefaultValue { get; set; }
        bool Required { get; set; }
        IEnumerable<IWidgetConfigurationPropertySupportedValue> WidgetConfigurationPropertySupportedValues { get; set; }
    }
}
