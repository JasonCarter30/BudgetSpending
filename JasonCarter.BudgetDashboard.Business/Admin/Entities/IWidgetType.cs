using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IWidgetType
    {
        int WidgetTypeId { get; set; }
        string Name { get; set; }
        string Title { get; set; }
        string Icon { get; set; }
        string Type { get; set; }
        IEnumerable<IWidgetConfiguration> WidgetConfigurations { get; set; }
    }
}
