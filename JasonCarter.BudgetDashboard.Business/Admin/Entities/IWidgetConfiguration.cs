using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IWidgetConfiguration
    {
        int WidgetConfigurationId { get; set; }
        string Name { get; set; }
        bool IsEditable { get; set; }
        bool IsDataConfig { get; set; }
        IObjectType ObjectType { get; set; }
        IEnumerable<IWidgetConfigurationProperty> WidgetConfigurationProperties { get; set; }
    }
}
