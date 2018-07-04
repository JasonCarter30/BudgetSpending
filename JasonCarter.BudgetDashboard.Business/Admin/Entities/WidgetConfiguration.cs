using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class WidgetConfiguration : IWidgetConfiguration
    {
        public int WidgetConfigurationId { get; set; }
        public string Name { get; set; }
        public IObjectType ObjectType { get; set; }
        public IEnumerable<IWidgetConfigurationProperty> WidgetConfigurationProperties { get; set; }
        public bool IsEditable { get; set; }
        public bool IsDataConfig { get; set; }
    }
}
