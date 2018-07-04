using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class WidgetConfigurationPropertyResult : IDapperResult<IWidgetConfigurationProperty>
    {
        public int WidgetConfigurationPropertyId { get; set; }
        public int WidgetConfigurationId { get; set; }
        public int PropertyId { get; set; }
        public dynamic DefaultValue { get; set; }
        public bool Required { get; set; }

        public IWidgetConfigurationProperty Convert()
        {
            IWidgetConfigurationProperty widgetConfigurationProperty = new WidgetConfigurationProperty()
            {
                WidgetConfigurationPropertyId = WidgetConfigurationPropertyId,
                WidgetConfigurationId = WidgetConfigurationId,
                DefaultValue = DefaultValue,
                Required = Required
            };

            return widgetConfigurationProperty;
        }
    }
}
