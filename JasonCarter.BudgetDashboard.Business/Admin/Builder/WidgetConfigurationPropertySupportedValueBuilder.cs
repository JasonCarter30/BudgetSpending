using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class WidgetConfigurationPropertySupportedValueBuilder : Business.Builder.Builder, IDisposable
    {
        private IWidgetConfigurationPropertySupportedValue widgetConfigurationPropertySupportedValue { get; set; }
        private AdminFacade _adminFacade { get; set; }


        public WidgetConfigurationPropertySupportedValueBuilder(AdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }


        public override void Build<T>(T resultClass)
        {
            widgetConfigurationPropertySupportedValue = (WidgetConfigurationPropertySupportedValue)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            widgetConfigurationPropertySupportedValue.WidgetConfigurationProperty = _adminFacade.GetWidgetConfigurationPropertyLite((int)(resultClass as dynamic).WidgetConfigurationPropertyId);
            widgetConfigurationPropertySupportedValue.SupportedValue = _adminFacade.GetSupportedValueBySupportedValueId((int)(resultClass as dynamic).SupportedValueId);
        }

        public void BuildLite<T>(T resultClass)
        {
            widgetConfigurationPropertySupportedValue = (WidgetConfigurationPropertySupportedValue)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            widgetConfigurationPropertySupportedValue = null;
        }

        public override object GetResult()
        {
            return widgetConfigurationPropertySupportedValue;
        }
    }
}
