using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class WidgetTypeWidgetConfigurationBuilder : Business.Builder.Builder, IDisposable
    {
        private IWidgetTypeWidgetConfiguration widgetTypeWidgetConfiguration = null;
        private AdminFacade _adminFacade = null;
        public WidgetTypeWidgetConfigurationBuilder(AdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }

        public override void Build<T>(T resultClass)
        {
            widgetTypeWidgetConfiguration = (WidgetTypeWidgetConfiguration)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            widgetTypeWidgetConfiguration.WidgetType = _adminFacade.GetWidgetTypeByWidgetTypeId((int)(resultClass as dynamic).WidgetTypeId);
            widgetTypeWidgetConfiguration.WidgetConfiguration = _adminFacade.GetWidgetConfigurationByWidgetConfigurationId((int)(resultClass as dynamic).WidgetConfigurationId);
        }

        public void Dispose()
        {
            widgetTypeWidgetConfiguration = null;
        }

        public override object GetResult()
        {
            return widgetTypeWidgetConfiguration;
        }
    }
}
