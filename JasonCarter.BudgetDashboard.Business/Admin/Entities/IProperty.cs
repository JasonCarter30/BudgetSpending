using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Entities
{
    public interface IProperty
    {
        int PropertyId { get; set; }
        string Name { get; set; }
        IObjectType ObjectType { get; set; }
    }
}
