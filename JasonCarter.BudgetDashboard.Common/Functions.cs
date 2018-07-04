using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace JasonCarter.BudgetDashboard.Common
{
    public class Functions
    {

        /// <summary>
        /// Recursive function
        /// </summary>
        /// <param name="jArray"></param>
        /// <returns></returns>
        private static object[] parseJSONArray(JArray jArray)
        {
            int inc = 0;
            object[] items = new object[jArray.Count];
            foreach (var arrayItem in jArray)
            {
                JToken jToken = arrayItem;

                if (jToken.Type == JTokenType.Array)
                {
                    items[inc] = parseJSONArray(arrayItem as JArray);
                }
                else if (jToken.Type == JTokenType.Float)
                {
                    items[inc] = arrayItem.Value<double>();
                }
                else if (jToken.Type == JTokenType.Integer)
                {
                    items[inc] = arrayItem.Value<int>();
                }
                else if (jToken.Type == JTokenType.Object)
                {
                    items[inc] = parseJSONObject(arrayItem);
                }
                else
                {
                    items[inc] = arrayItem.Value<string>();
                }

                inc++;
            }

            return items;
        }


        /// <summary>
        /// Recursive function
        /// </summary>
        /// <param name="jObject"></param>
        private static Dictionary<string, object> parseJSONObject(JObject jObject)
        {
            Dictionary<string, object> dictionaryItems = new Dictionary<string, object>();

            foreach (var obj in jObject)
            {
                string key = obj.Key;
                object value = null;

                var tokenType = obj.Value.Type;

                switch (tokenType)
                {
                    case JTokenType.Boolean:
                        {
                            value = (bool)obj.Value;
                            break;
                        }
                    case JTokenType.String:
                        {
                            value = (string)obj.Value;
                            break;
                        }
                    case JTokenType.Integer:
                        {
                            value = (int)obj.Value;
                            break;
                        }
                    case JTokenType.Object:
                        {
                            JObject jObjectValue = obj.Value as JObject;
                            value = parseJSONObject(jObjectValue);
                            break;
                        }
                    case JTokenType.Array:
                        {
                            JArray jArrayValue = obj.Value as JArray;
                            value = parseJSONArray(jArrayValue);
                            break;
                        }
                    default:
                        {
                            value = obj.Value;
                            break;
                        }
                }

                dictionaryItems.Add(key, value);
            }

            return dictionaryItems;
        }


        private static Dictionary<string, object> parseJSONObject(JToken jToken)
        {
            List<Dictionary<string, object>> returnValue = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictionaryItems = new Dictionary<string, object>();
            
            var enumer = jToken.AsJEnumerable().GetEnumerator();

            while (enumer.MoveNext())
            {
                var item = enumer.Current;

                string propertyName = ((JProperty)item).Name;
                var values = item.Values<JToken>();
                var enem = values.GetEnumerator();
                dynamic value = null;

                while (enem.MoveNext())
                {
                    var currentItem = enem.Current;

                    switch (currentItem.Type)
                    {
                        case JTokenType.Integer:
                            {
                                value = (int)currentItem;
                                break;
                            }
                        case JTokenType.String:
                            {
                                value = (string)currentItem;
                                break;
                            }
                        case JTokenType.Date:
                        {
                            value = currentItem;
                            break;
                        }
                        case JTokenType.Array:
                            {
                                value = parseJSONArray((JArray)currentItem);
                                break;
                            }
                        case JTokenType.Object:
                            {
                                value = parseJSONObject(currentItem);
                                break;
                            }
                        case JTokenType.Property:
                            {
                                value = parseJSONObject(currentItem);
                                break;
                            }
                    }

                    dictionaryItems.Add(propertyName, value);
                }
            }


            return dictionaryItems;
        }

        private static void parseJSONProperty(JToken item)
        {

            var enumer = item.AsQueryable().GetEnumerator();
            
            while (enumer.MoveNext())
            {
                var currentItem = enumer.Current;
                
            }

          
        }

        public static Dictionary<string, object> ParseJSON(string json)
        {
            dynamic items = JsonConvert.DeserializeObject(json);
            List<Dictionary<string, object>> returnValue = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictionaryItems = new Dictionary<string, object>();
            
            JObject jObject = JObject.Parse(json);

            var enumer = jObject.GetEnumerator();
            while (enumer.MoveNext())
            {
                var item = enumer.Current;
                string name = item.Key;
                dynamic value = null;

                switch (item.Value.Type)
                {
                    case JTokenType.Object:
                        {
                            value = parseJSONObject(item.Value);
                            break;
                        }
                    case JTokenType.Array:
                        {
                            value = parseJSONArray((JArray)item.Value);
                            break;
                        }
                    default:
                        {
                            value = (string)item.Value;
                            break;
                        }
                }

                dictionaryItems.Add(name, value);
            }
            

            return dictionaryItems;
        }

     

        /// <summary>
        /// Recursive function used to parsing JSON.
        /// </summary>
        /// <param name="jObject"></param>
        /// <returns></returns>
        public static Dictionary<string, object> ParseJSONAttributes(JObject jObject)
        {
            Dictionary<string, object> returnValue = new Dictionary<string, object>();

            foreach (var item in jObject)
            {
                string name = item.Key;
                object value;
                var tokenType = item.Value.Type;


                switch (tokenType)
                {
                    case JTokenType.Boolean:
                        value = (bool)item.Value;
                        break;
                    case JTokenType.String:
                        value = (string)item.Value;
                        break;
                    case JTokenType.Integer:
                        value = (int)item.Value;
                        break;
                    case JTokenType.Array:
                        //Dictionary<string, object> arrayValues1 = GetColumnDefsFromJSON(columnDefItem.Value.ToString());
                        dynamic arrayItems = JsonConvert.DeserializeObject(item.Value.ToString());
                        Dictionary<string, object> arrayValues = new Dictionary<string, object>();
                        foreach (JObject arrayItem in arrayItems)
                        {
                            arrayValues = ParseJSONAttributes(arrayItem);
                        }
                        value = arrayValues.ToList();
                        break;
                    default:
                        value = item.Value;
                        break;
                }

                returnValue.Add(name, value);

            }


            return returnValue;
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static string GetQuarterByDate(DateTime dateTime)
        {
            string returnValue = "";

            if (dateTime.Month > 0 && dateTime.Month <= 3)
            {
                returnValue = "Q1 " + dateTime.Year.ToString();
            }
            if (dateTime.Month > 3 && dateTime.Month <= 6)
            {
                returnValue = "Q2 " + dateTime.Year.ToString();
            }
            if (dateTime.Month > 6 && dateTime.Month <= 9)
            {
                returnValue = "Q3 " + dateTime.Year.ToString();
            }
            if (dateTime.Month > 9 && dateTime.Month <= 12)
            {
                returnValue = "Q4 " + dateTime.Year.ToString();
            }

            return returnValue;
        }

        public static int GetQuarterValueByDate(DateTime dateTime)
        {
            int returnValue = 0;

            if (dateTime.Month > 0 && dateTime.Month <= 3)
            {
                returnValue = 1;
            }
            if (dateTime.Month > 3 && dateTime.Month <= 6)
            {
                returnValue = 2;
            }
            if (dateTime.Month > 6 && dateTime.Month <= 9)
            {
                returnValue = 3;
            }
            if (dateTime.Month > 9 && dateTime.Month <= 12)
            {
                returnValue = 4;
            }

            return returnValue;
        }

     

        
        

        public static string GetMonthName(int month)
        {
            string returnValue = "";

            switch (month)
            {
                case 1:
                    {
                        returnValue = "January";
                        break;
                    }
                case 2:
                    {
                        returnValue = "February";
                        break;
                    }
                case 3:
                    {
                        returnValue = "March";
                        break;
                    }
                case 4:
                    {
                        returnValue = "April";
                        break;
                    }
                case 5:
                    {
                        returnValue = "May";
                        break;
                    }
                case 6:
                    {
                        returnValue = "June";
                        break;
                    }
                case 7:
                    {
                        returnValue = "July";
                        break;
                    }
                case 8:
                    {
                        returnValue = "August";
                        break;
                    }
                case 9:
                    {
                        returnValue = "September";
                        break;
                    }
                case 10:
                    {
                        returnValue = "October";
                        break;
                    }
                case 11:
                    {
                        returnValue = "November";
                        break;
                    }
                case 12:
                    {
                        returnValue = "December";
                        break;
                    }
            }

            return returnValue;
        }

        public static string GetMonthYear(DateTime dateTime)
        {
            string returnValue = "";
            string month = GetMonthName(dateTime.Month);
            string year = dateTime.Year.ToString();

            returnValue = string.Format("{0} - {1}", month, year);

            return returnValue;
        }

        public static string GetYear(DateTime dateTime)
        {
            string returnValue = "";
            string year = dateTime.Year.ToString();

            returnValue = year;

            return returnValue;
        }
    }
}
