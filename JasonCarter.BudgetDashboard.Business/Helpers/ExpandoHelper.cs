using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Helpers
{
    internal class ExpandoHelper
    {
        internal static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
