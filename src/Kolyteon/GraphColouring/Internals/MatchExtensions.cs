using System.Text.RegularExpressions;

namespace Kolyteon.GraphColouring.Internals;

internal static class MatchExtensions
{
    internal static Edge ToEdge(this Match match) =>
        Edge.Between(Node.FromName(match.Groups["node1"].Value),
            Node.FromName(match.Groups["node2"].Value));
}
