using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class WidgetTypeResult : IDapperResult<IWidgetType>
    {
        public int WidgetTypeId { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string Type { get; set; }
        public IWidgetType Convert()
        {
            IWidgetType widgetType = new WidgetType()
            {
                WidgetTypeId = WidgetTypeId,
                Name = Name,
                Title = Title,
                Icon = Icon,
                Type = Type
            };

            return widgetType;
        }
    }
}
