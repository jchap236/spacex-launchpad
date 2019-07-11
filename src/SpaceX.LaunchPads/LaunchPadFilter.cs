using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace SpaceX.LaunchPads
{
    public class LaunchPadFilter
    {
        public string Filter { get; set; }
        public int? Limit { get; set; }

        public LaunchPadFilter()
        {
        }

        public dynamic FilterProperties(LaunchPad launchPad)
        {
            if (string.IsNullOrEmpty(Filter)) return launchPad;
            var filteredObject = new ExpandoObject() as IDictionary<string, Object>;
            var launchPadType = launchPad.GetType();

            foreach (var propertyName in Filter.Split(','))
            {
                var property = launchPadType
                    .GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (property != null)
                {
                    filteredObject[propertyName] = property.GetValue(launchPad, null);
                }
            }

            return filteredObject;
        }
    }
}
