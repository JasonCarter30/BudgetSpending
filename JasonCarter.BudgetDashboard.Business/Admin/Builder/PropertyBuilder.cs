using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Business.Admin.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class PropertyBuilder : Business.Builder.Builder, IDisposable
    {
        private IProperty property { get; set; }
        private AdminFacade _adminFacade { get; set; }


        public PropertyBuilder(AdminFacade adminFacade)
        {
            _adminFacade = adminFacade;
        }

        public override void Build<T>(T resultClass)
        {
            property = (Property)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
            property.ObjectType = _adminFacade.GetObjectTypeIdByObjectTypeId((int)(resultClass as dynamic).ObjectTypeId);
        }

        public void Dispose()
        {
            property = null;
            _adminFacade = null;
        }

        public override object GetResult()
        {
            return property;
        }
    }
}
