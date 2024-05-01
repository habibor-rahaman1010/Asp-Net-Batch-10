using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomJsonFormatter
{
    public class JsonFormatter
    {
        public static string Convert(object obj)
        {
            var jsonBuilder = new StringBuilder();
            ConvertObjectToJson(obj, jsonBuilder);
            return jsonBuilder.ToString();
        }

        private static void ConvertObjectToJson(object obj, StringBuilder jsonBuilder)
        {
            if (obj == null)
            {
                jsonBuilder.Append("null");
                return;
            }

            Type objType = obj.GetType();

            if (IsPrimitive(objType) || objType == typeof(string) || objType == typeof(DateTime))
            {
                AppendToPrimitiveValueInjsonBuilder(obj, jsonBuilder);
            }
            else if (objType.IsArray)
            {
                ConvertToArray((Array)obj, jsonBuilder);
            }
            else if (obj is IEnumerable enumerable)
            {
                ConvertToCollection(enumerable, jsonBuilder);
            }
            else if(objType.IsClass)
            {
                ConvertToObject(obj, jsonBuilder);
            }
        }

        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive || type.IsValueType;
        }

        private static void AppendToPrimitiveValueInjsonBuilder(object value, StringBuilder jsonBuilder)
        {
            if (value is string)
            {
                jsonBuilder.Append('"').Append(value).Append('"');
            }
            else if (value is DateTime dateTime)
            {
                jsonBuilder.Append('"').Append(dateTime.ToString("yyyy-MM-ddTHH:mm:ss")).Append('"');
            }
            else
            {
                jsonBuilder.Append(value);
            }
        }

        private static void ConvertToArray(Array array, StringBuilder jsonBuilder)
        {
            jsonBuilder.Append("[");
            bool flag = true;

            foreach (var obj in array)
            {
                if (!flag)
                {
                    jsonBuilder.Append(",");
                }
                ConvertObjectToJson(obj, jsonBuilder);
                flag = false;
            }

            jsonBuilder.Append("]");
        }

        private static void ConvertToCollection(IEnumerable collection, StringBuilder jsonBuilder)
        {
            jsonBuilder.Append("[");
            bool flag = true;

            foreach (var obj in collection)
            {
                if (!flag)
                {
                    jsonBuilder.Append(",");
                }
                ConvertObjectToJson(obj, jsonBuilder);
                flag = false;
            }

            jsonBuilder.Append("]");
        }

        private static void ConvertToObject(object obj, StringBuilder jsonBuilder)
        {
            Type objType = obj.GetType();
            PropertyInfo[] properties = objType.GetProperties();

            jsonBuilder.Append("{");
            bool flag = true;

            foreach (PropertyInfo property in properties)
            {
                if (!flag)
                {
                    jsonBuilder.Append(",");
                }

                jsonBuilder.Append('"').Append(property.Name).Append("\":");

                object? propertyValue = property.GetValue(obj, null);

                if (propertyValue != null)
                {
                    if (property.PropertyType == typeof(string) || property.PropertyType.IsEnum)
                    {
                        jsonBuilder.Append('"').Append(propertyValue).Append('"');
                    }
                    else
                    {
                        ConvertObjectToJson(propertyValue, jsonBuilder);
                    }
                }
                else
                {
                    jsonBuilder.Append("null");
                }

                flag = false;
            }

            jsonBuilder.Append("}");
        }
    }
}