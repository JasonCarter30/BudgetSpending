using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JasonCarter.BudgetDashboard.Business.Builder
{
    public abstract class Builder
    {
        public abstract void Build<T>(T resultClass) where T : class;
        public abstract object GetResult();
    }
}
