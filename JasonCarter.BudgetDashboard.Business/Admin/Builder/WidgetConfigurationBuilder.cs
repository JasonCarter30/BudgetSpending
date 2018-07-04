using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class WidgetConfigurationBuilder : Business.Builder.Builder, IDisposable
    {
        public WidgetConfigurationBuilder(AdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }


        private IWidgetConfiguration widgetConfiguration { get; set; }
        private AdminFacade _adminFacade { get; set; }
        public override void Build<T>(T resultClass)
        {
            widgetConfiguration = (WidgetConfiguration)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            widgetConfiguration.ObjectType = _adminFacade.GetObjectTypeIdByObjectTypeId((int)(resultClass as dynamic).ObjectTypeId);
            widgetConfiguration.WidgetConfigurationProperties = _adminFacade.GetWidgetConfigurationPropertiesByWidgetConfigurationId((int)(resultClass as dynamic).WidgetConfigurationId);
        }

        public void BuildLite<T>(T resultClass)
        {
            widgetConfiguration = (WidgetConfiguration)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            widgetConfiguration = null;
        }

        public override object GetResult()
        {
            return widgetConfiguration;
        }
    }
}
