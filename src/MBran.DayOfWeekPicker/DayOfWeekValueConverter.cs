using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.PropertyEditors;

namespace MBran.DayOfWeekPicker
{

    [PropertyValueType(typeof(DaysOfWeek))]
    [PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
    public class DayOfWeekValueConverter : IPropertyValueConverter
    {
        public bool IsConverter(PublishedPropertyType propertyType)
        {
            return propertyType.PropertyEditorAlias == Constants.EditorAlias;
        }

        public object ConvertDataToSource(PublishedPropertyType propertyType, object source, bool preview)
        {
            return JsonConvert.DeserializeObject<IEnumerable<string>>(Convert.ToString(source));
        }

        public object ConvertSourceToObject(PublishedPropertyType propertyType, object source, bool preview)
        {
            var model = source as IEnumerable<string>;
            bool dayIntValue;

            var days = model?.Select((selected, index) =>
                    new {IsSelected = selected, DayIndex = index})
                .Where(day => bool.TryParse(day.IsSelected, out dayIntValue) && dayIntValue)
                .Select(day => (DayOfWeek) day.DayIndex);

            DaysOfWeek sourceModel = new DaysOfWeek
            {
                DayOfWeek = days,
                Names = days.GetNames(),
                AbbreviatedNames = days.GetAbbreviatedNames()
            };

            return sourceModel;

        }

        public object ConvertSourceToXPath(PublishedPropertyType propertyType, object source, bool preview)
        {
            return null;
        }


    }
}