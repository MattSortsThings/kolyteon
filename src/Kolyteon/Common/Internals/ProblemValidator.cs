namespace Kolyteon.Common.Internals;

internal abstract class ProblemValidator<TProblem>
{
    internal abstract CheckingResult Validate(TProblem problem);

    internal ProblemValidator<TProblem> Then(ProblemValidator<TProblem> next) => new ChainNode(this, next);

    private sealed class ChainNode : ProblemValidator<TProblem>
    {
        internal ChainNode(ProblemValidator<TProblem> first, ProblemValidator<TProblem> next)
        {
            First = first;
            Next = next;
        }

        private ProblemValidator<TProblem> First { get; }

        private ProblemValidator<TProblem> Next { get; }

        internal override CheckingResult Validate(TProblem problem) =>
            First.Validate(problem) is { IsSuccessful: false } firstResult
                ? firstResult
                : Next.Validate(problem);
    }
}
