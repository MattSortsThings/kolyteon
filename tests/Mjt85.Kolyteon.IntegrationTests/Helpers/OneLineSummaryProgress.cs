using Mjt85.Kolyteon.Solving.Common;
using Mjt85.Kolyteon.Solving.Verbose;

namespace Mjt85.Kolyteon.IntegrationTests.Helpers;

public class OneLineSummaryProgress<V, D> : SolvingProgress<V, D>
    where V : struct, IComparable<V>, IEquatable<V>
    where D : struct, IComparable<D>, IEquatable<D>
{
    public string OneLineSummary { get; private set; } = string.Empty;

    protected override void StateHasChanged()
    {
        if (CurrentSearchState == SearchState.Final)
        {
            OneLineSummary = CurrentSearchLevel == SearchTreeRootLevel
                ? "finished at root level"
                : CurrentSearchLevel == SearchTreeLeafLevel
                    ? "finished at leaf level"
                    : "finished at intermediary level";
        }
        else
        {
            OneLineSummary = "not finished";
        }
    }
}
