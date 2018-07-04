using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public class ObjectType : IObjectType
    {
        public int ObjectTypeId { get; set; }
        public string Name { get; set; }
    }
}
