using ChilindoTechTest.Core.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;

namespace ChilindoTechTest.Core
{
    public static class Extensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static HttpStatusCode GetHttpStatusCode(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            HttpStatusCodeAttribute[] attributes = (HttpStatusCodeAttribute[])fi.GetCustomAttributes(typeof(HttpStatusCodeAttribute), false);
            return (attributes.Length > 0) ? attributes[0].StatusCode : HttpStatusCode.OK;
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value.Length <= maxLength ? value : (value.Substring(0, maxLength) + "...");
            }
            else
            {
                return value;
            }
        }
    }
}