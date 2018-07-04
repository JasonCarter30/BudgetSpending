using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JasonCarter.BudgetDashboard.Business.Helpers
{
    public class LinqHelper
    {

        public static object Sum(IGrouping<dynamic, dynamic> y, string field)
        {
            decimal returnValue = 0;

            returnValue = y.Sum(x => (decimal)((IDictionary<String, Object>)x)[field]);

            return returnValue;
        }

        public static object Count(IGrouping<dynamic, dynamic> y, string field)
        {
            int returnValue = 0;

            returnValue = y.Count();

            return returnValue;
        }

        public string GetMonthYearPeriodFromDate(DateTime dateTime)
        {
            string returnValue = "";

            return returnValue;
        }

        public void groupBy(object input, string transformType)
        {

        }

        public dynamic groupByDateTime(DateTime input, string transformType)
        {
            dynamic returnValue = null;


            switch (transformType)
            {
                case "":
                    {
                        returnValue = input;
                        break;
                    }
                case "MonthYear":
                    {
                        string month = input.Month.ToString();
                        string year = input.Year.ToString();

                        //returnValue = input.Month;
                        returnValue = month + " " + year;
                        break;
                    }
            }


            return returnValue;
        }

        public dynamic GroupBy(object input, string transformType)
        {
            dynamic returnValue = null;

            Type type = input.GetType();

            PropertyInfo prop;

            

            var conv = TypeDescriptor.GetConverter(type);

            //var input2 = Convert.ToDateTime(input);
            //var input2 = conv.ConvertFrom(input);

            switch (Type.GetTypeCode(type)) {
                case TypeCode.DateTime:
                    {
                        returnValue = groupByDateTime(((DateTime)input), transformType);
                        break;
                    }
                case TypeCode.String:
                    {
                        returnValue = input;
                        break;
                    }

            }


            groupBy(input, transformType);

         

            return returnValue;
        }

        public dynamic GroupBy(string input, string transformType)
        {
            dynamic returnValue = null;


            switch (transformType)
            {
                case "":
                    {
                        returnValue = input;
                        break;
                    }
                case "MonthYear":
                    {
                        break;
                    }
            }


            return returnValue;
        }
    }
}
