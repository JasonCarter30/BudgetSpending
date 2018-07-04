using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    class WidgetConfigurationPropertySupportedValueResult : IDapperResult<IWidgetConfigurationPropertySupportedValue>
    {
        public int WidgetConfigurationPropertySupportedValueId { get; set; }
        public int WidgetConfigurationPropertyId { get; set; }
        public int SupportedValueId { get; set; }
        public IWidgetConfigurationPropertySupportedValue Convert()
        {
            IWidgetConfigurationPropertySupportedValue widgetConfigurationPropertySupportedValue = new WidgetConfigurationPropertySupportedValue()
            {
                WidgetConfigurationPropertySupportedValueId = WidgetConfigurationPropertySupportedValueId
            };

            return widgetConfigurationPropertySupportedValue;
        }
    }
}
