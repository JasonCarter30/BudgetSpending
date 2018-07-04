using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IWidgetTypeWidgetConfiguration
    {
        int WidgetTypeWidgetConfigurationId { get; set; }
        IWidgetType WidgetType { get; set; }
        IWidgetConfiguration WidgetConfiguration { get; set; }
        bool Required { get; set; }
        bool IsEditable { get; set; }
    }
}
