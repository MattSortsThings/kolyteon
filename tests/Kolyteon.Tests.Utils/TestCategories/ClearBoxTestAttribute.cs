using Kolyteon.Tests.Utils.Common;
using Xunit.Sdk;

namespace Kolyteon.Tests.Utils.TestCategories;

[TraitDiscoverer(ClearBoxTestDiscoverer.DiscovererTypeName, Constants.TestUtilsAssemblyName)]
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class ClearBoxTestAttribute : Attribute, ITraitAttribute;
