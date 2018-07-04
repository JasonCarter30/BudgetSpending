using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class WidgetTypeWidgetConfigurationResult : IDapperResult<IWidgetTypeWidgetConfiguration>
    {
        public int WidgetTypeWidgetConfigurationId { get; set; }
        public int WidgetTypeId { get; set; }
        public int WidgetConfigurationId { get; set; }
        public bool Required { get; set; }
        public bool IsEditable { get; set; }

        public IWidgetTypeWidgetConfiguration Convert()
        {
            IWidgetTypeWidgetConfiguration widgetTypeWidgetConfiguration = new WidgetTypeWidgetConfiguration()
            {
                WidgetTypeWidgetConfigurationId = WidgetTypeWidgetConfigurationId,
                Required = Required,
                IsEditable = IsEditable
            };

            return widgetTypeWidgetConfiguration;
        }
    }
}
