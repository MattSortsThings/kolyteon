using System.Reflection;
using Mjt85.Kolyteon.MapColouring;
using TechTalk.SpecFlow.Assist;

namespace Mjt85.Kolyteon.FeatureTests.ValueRetrievers;

internal class PresetMapValueRetriever : IValueRetriever
{
    public bool CanRetrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType) =>
        propertyType == typeof(PresetMap) || keyValuePair.Key.Equals("PresetMap");

    public object Retrieve(KeyValuePair<string, string> keyValuePair, Type targetType, Type propertyType)
    {
        MethodInfo m = typeof(PresetMaps).GetMethod(keyValuePair.Value, BindingFlags.Public | BindingFlags.Static)!;

        return (PresetMap)m.Invoke(null, null)!;
    }
}
