using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class WidgetTypeWidgetConfiguration : IWidgetTypeWidgetConfiguration
    {
        public int WidgetTypeWidgetConfigurationId { get; set; }
        public IWidgetType WidgetType { get; set; }
        public IWidgetConfiguration WidgetConfiguration { get; set; }
        public bool Required { get; set; }
        public bool IsEditable { get; set; }
    }
}
