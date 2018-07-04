using JasonCarter.BudgetDashboard.Business.Admin.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Admin.Builder
{
    public class ObjectTypeBuilder : Business.Builder.Builder, IDisposable
    {
        private IObjectType objectType { get; set; }
        public override void Build<T>(T resultClass)
        {
            objectType = (ObjectType)resultClass.GetType().GetMethod("Convert").Invoke(resultClass, null);
        }

        public void Dispose()
        {
            objectType = null;
        }

        public override object GetResult()
        {
            return objectType;
        }
    }
}
