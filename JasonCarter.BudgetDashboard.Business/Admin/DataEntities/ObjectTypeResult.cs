using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using JasonCarter.BudgetDashboard.Data.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.DataEntities
{
    public class ObjectTypeResult : IDapperResult<IObjectType>
    {
        public int ObjectTypeId { get; set; }
        public string Name { get; set; }
        public IObjectType Convert()
        {
            IObjectType objectType = new ObjectType()
            {
                ObjectTypeId = ObjectTypeId,
                Name = Name
            };

            return objectType;
        }
    }
}
