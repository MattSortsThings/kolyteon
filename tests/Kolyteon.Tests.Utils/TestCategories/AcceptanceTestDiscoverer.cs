using Kolyteon.Tests.Utils.Common;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Kolyteon.Tests.Utils.TestCategories;

public class AcceptanceTestDiscoverer : ITraitDiscoverer
{
    internal const string DiscovererTypeName = Constants.TestCategoriesNamespace + "." + nameof(AcceptanceTestDiscoverer);

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "AcceptanceTest");
    }
}
