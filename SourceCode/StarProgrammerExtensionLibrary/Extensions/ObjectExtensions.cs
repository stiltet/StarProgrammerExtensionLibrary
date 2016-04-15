using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WebGrease.Css.Extensions;

namespace StarProgrammerExtensionLibrary.Extensions
{
    public static class ObjectExtensions
    {
        public static bool AreTwoObjectsEqual<T>(
            T newObject,
            T oldObject,
            List<string> ignoreList = null,
            List<string> ignoreNullValuesOfTheseProperties = null)
        {
            var newObjectProperties = newObject.GetType().GetProperties();
            newObjectProperties = newObjectProperties.Where(x =>
                !x.Name.Equals("Id") &&
                !x.Name.Contains("Updated") &&
                !x.PropertyType.FullName.StartsWith(ProjectExtensions.GetProjectName()) &&
                !x.PropertyType.FullName.StartsWith("System.Collections.Generic.ICollection") &&
                !x.PropertyType.FullName.StartsWith("System.Collections.Generic.List") &&
                null != x.SetMethod).ToArray();

            if (null != ignoreList)
                newObjectProperties = ignoreList.Aggregate(newObjectProperties,
                    (current, str) => current.Where(x => !x.Name.Contains(str)).ToArray());

            var newObjectDictionary = newObjectProperties.ToDictionary(pName => pName.Name,
                pValue => pValue.GetValue(newObject, null));

            var oldObjectProperties = oldObject.GetType().GetProperties();
            var oldObjectDictionary = oldObjectProperties.ToDictionary(pName => pName.Name,
                pValue => pValue.GetValue(oldObject, null));

            return
                newObjectDictionary.Keys.Where(
                    item =>
                        (null != newObjectDictionary[item] && null != oldObjectDictionary[item]) 
                        || null == ignoreNullValuesOfTheseProperties 
                        || !ignoreNullValuesOfTheseProperties.Contains(item))
                    .All(item => !AreTwoObjectPropertiesDifferent(newObjectDictionary[item], oldObjectDictionary[item]));
        }

        public static bool AreTwoObjectPropertiesDifferent(
            object newItem, 
            object oldItem)
        {
            return null == newItem && null != oldItem ||
                   null != newItem && !newItem.Equals(oldItem);
        }

        public static object ConvertToHistoryObject<T>(
            T baseObject, 
            string baseObjectAsString,
            bool addHistoryValues = true)
        {
            var historyObject =
                Assembly.GetExecutingAssembly().CreateInstance(string.Format("{0}History", baseObjectAsString));

            if (null == historyObject)
                throw new InvalidDataException(
                    string.Format(
                        "Error in ObjectExtensions.ConvertToHistoryObject(): HistoryObject is null! Check that {0}History object exists and that value of ProjectName is correct!",
                        baseObjectAsString));

            var baseObjectProperties = baseObject.GetType().GetProperties();
            var baseObjectDictionary = baseObjectProperties.ToDictionary(pName => pName.Name,
                pValue => pValue.GetValue(baseObject, null));

            var baseObjectAsStringWithoutNamespace =
                baseObjectAsString.Substring(baseObjectAsString.LastIndexOf('.') + 1);

            var baseObjectIdPropertyName = string.Format("{0}Id", baseObjectAsStringWithoutNamespace);

            if (null == historyObject.GetProperty(baseObjectIdPropertyName))
                throw new InvalidDataException(
                    string.Format(
                        "Error in ObjectExtensions.ConvertToHistoryObject(): There is no property for {0}!",
                        baseObjectIdPropertyName));

            baseObjectDictionary.ForEach(dictItem =>
            {
                historyObject.SetPropertyValue(
                    dictItem.Key.ToLower() == "id"
                        ? baseObjectIdPropertyName
                        : dictItem.Key, dictItem.Value);
            });

            if (!addHistoryValues)
                return historyObject;

            var historyObjectProperties = historyObject.GetType().GetProperties();
            var historyObjectDictionary = historyObjectProperties.ToDictionary(pName => pName.Name,
                pValue => pValue.PropertyType);

            historyObjectDictionary
                .Where(dictItem => !baseObjectDictionary.ContainsKey(dictItem.Key)
                                   && dictItem.Key != baseObjectIdPropertyName)
                .ForEach(dictItem =>
                {
                    switch (dictItem.Key)
                    {
                        case "Id":
                            //if (dictItem.Value == typeof(Guid))
                            //    historyObject.SetPropertyValue(dictItem.Key, new Guid());
                            //else if (dictItem.Value == typeof(int))
                            //    historyObject.SetPropertyValue(dictItem.Key, new int());
                            break;
                        case "HistoryDate":
                            historyObject.SetPropertyValue(dictItem.Key, DateTime.Now);
                            break;
                        default:
                            throw new MissingMemberException(
                                string.Format(
                                    "Error in ObjectExtensions.ConvertToHistoryObject(): There is no default property value for {0}!",
                                    dictItem.Key));
                    }
                });

            return historyObject;
        }
    }
}