using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class WidgetConfigurationPropertyBuilder : Business.Builder.Builder, IDisposable
    {
        public WidgetConfigurationPropertyBuilder(AdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }

        public WidgetConfigurationPropertyBuilder()
        {
         
        }


        private IWidgetConfigurationProperty widgetConfigurationProperty { get; set; }
        private AdminFacade _adminFacade { get; set; }
        public override void Build<T>(T resultClass)
        {
            widgetConfigurationProperty = (WidgetConfigurationProperty)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            widgetConfigurationProperty.WidgetConfiguration = _adminFacade.GetWidgetConfigurationByWidgetConfigurationIdLite((int)(resultClass as dynamic).WidgetConfigurationId);
            widgetConfigurationProperty.Property = _adminFacade.GetPropertyByPropertyId((int)(resultClass as dynamic).PropertyId);
            widgetConfigurationProperty.WidgetConfigurationPropertySupportedValues = _adminFacade.GetWidgetConfigurationPropertySupportedValuesByWidgetConfigurationPropertyId((int)(resultClass as dynamic).WidgetConfigurationPropertyId);   
        }

        public void BuildLite<T>(T resultClass)
        {
            widgetConfigurationProperty = (WidgetConfigurationProperty)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            widgetConfigurationProperty = null;
        }

        public override object GetResult()
        {
            return widgetConfigurationProperty;
        }
    }
}
