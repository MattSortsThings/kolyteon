namespace Kolyteon.Common.Internals;

internal interface IProblemValidator<TProblem>
{
    internal CheckingResult Validate(TProblem problem);

    internal IProblemValidator<TProblem> Then(IProblemValidator<TProblem> next) => new ChainNode(this, next);

    private sealed class ChainNode : IProblemValidator<TProblem>
    {
        public ChainNode(IProblemValidator<TProblem> first, IProblemValidator<TProblem> next)
        {
            First = first;
            Next = next;
        }

        private IProblemValidator<TProblem> First { get; }

        private IProblemValidator<TProblem> Next { get; }

        public CheckingResult Validate(TProblem problem) =>
            First.Validate(problem) is { IsSuccessful: false } firstResult
                ? firstResult
                : Next.Validate(problem);
    }
}
