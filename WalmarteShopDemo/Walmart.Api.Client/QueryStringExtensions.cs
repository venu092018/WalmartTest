using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Walmart.Api.Client
{
    public static class QueryStringExtensions
    {
        public static string AppendQueryStringComponent(this string queryString, string component)
        {
            if (string.IsNullOrWhiteSpace(queryString))
                return component ?? string.Empty;

            if (string.IsNullOrWhiteSpace(component))
                return queryString;
            else
                return $"{queryString}&{component}";
        }

        public static string GetQueryComponent<T>(this string key, T value)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;
            var sanitizedValue = value == null ? string.Empty : value.ToString();
            var encodedValue = WebUtility.UrlEncode(sanitizedValue);
            return $"{key}={encodedValue}";
        }

        public static string AppendValue<T>(this string urlQuery, string name, T value)
        {
            string categoryIdComponent = name
                .GetQueryComponent<T>(value);
            return urlQuery.AppendQueryStringComponent(categoryIdComponent);
        }

        public static string GetCommaSeparatedItems(this List<int> list)
        {
            if (list == null)
                return string.Empty;
            return string.Join(",",list.ToArray());
        }
    }
}
