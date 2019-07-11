using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaceX.LaunchPads.SpaceXDataService
{
    public static class FilterExtension
    {
        public static string ApplyToUri(this LaunchPadFilter filter, string requestUri)
        {
            var query = HttpUtility.ParseQueryString(string.Empty);
            var filterString = string.Empty;

            if (filter.Limit.HasValue)
                query["limit"] = filter.Limit.Value.ToString();

            if (!string.IsNullOrEmpty(filter.Filter))
            {
                query["filter"] = "filterString";
                filterString = string.Join(",",
                    filter.Filter.Split(',').Select(x=> Convert(x)));
            }

            var newUri = string.Join("?", requestUri, query.ToString());
            newUri = newUri.Replace("filterString", filterString);
            return newUri;

        }

        private static string Convert(string filterName)
        {
            if (ConversionMap.ContainsKey(filterName))
            {
                return ConversionMap[filterName];
            }
            return filterName;
        }

        private static Dictionary<string, string> ConversionMap = new Dictionary<string, string>()
        {
            {"name", "full_name" }
        };
    }
}
