using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class WidgetType : IWidgetType
    {
        public int WidgetTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public IEnumerable<IWidgetConfiguration> WidgetConfigurations { get; set; }
        public string Type { get; set; }
    }
}
