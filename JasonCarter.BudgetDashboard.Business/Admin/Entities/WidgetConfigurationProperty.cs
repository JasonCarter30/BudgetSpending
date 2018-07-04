using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class WidgetConfigurationProperty : IWidgetConfigurationProperty
    {
        public int WidgetConfigurationPropertyId { get; set; }
        public int WidgetConfigurationId { get; set; }
        public int PropertyId { get; set; }
        public dynamic DefaultValue { get; set; }
        public bool Required { get; set; }
        public IWidgetConfiguration WidgetConfiguration { get; set; }
        public IProperty Property { get; set; }
        public IEnumerable<IWidgetConfigurationPropertySupportedValue> WidgetConfigurationPropertySupportedValues { get; set; }
    }
}
