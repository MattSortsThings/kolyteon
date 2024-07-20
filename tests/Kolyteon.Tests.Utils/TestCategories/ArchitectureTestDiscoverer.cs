using Kolyteon.Tests.Utils.Common;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Kolyteon.Tests.Utils.TestCategories;

public class ArchitectureTestDiscoverer : ITraitDiscoverer
{
    internal const string DiscovererTypeName = Constants.TestCategoriesNamespace + "." + nameof(ArchitectureTestDiscoverer);

    public IEnumerable<KeyValuePair<string, string>> GetTraits(IAttributeInfo traitAttribute)
    {
        yield return new KeyValuePair<string, string>("Category", "ArchitectureTest");
    }
}
