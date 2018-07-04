using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class PropertyResult : IDapperResult<IProperty>
    {
        public int PropertyId { get; set; }
        public string Name { get; set; }
        public int ObjectTypeId { get; set; }
        public IObjectType ObjectType { get; set; }

        public IProperty Convert()
        {
            IProperty property = new Property()
            {
                PropertyId = PropertyId,
                Name = Name,
            };

            return property;
        }
    }
}
