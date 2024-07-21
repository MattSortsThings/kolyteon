namespace Kolyteon.Common.Internals;

internal interface ISolutionVerifier<TSolution, TProblem>
    where TProblem : ISolutionVerifier<TSolution>
{
    internal CheckingResult VerifyCorrect(TSolution solution, TProblem problem);

    internal ISolutionVerifier<TSolution, TProblem> Then(ISolutionVerifier<TSolution, TProblem> next) =>
        new ChainNode(this, next);

    private sealed class ChainNode : ISolutionVerifier<TSolution, TProblem>
    {
        public ChainNode(ISolutionVerifier<TSolution, TProblem> first, ISolutionVerifier<TSolution, TProblem> next)
        {
            First = first;
            Next = next;
        }

        private ISolutionVerifier<TSolution, TProblem> First { get; }

        private ISolutionVerifier<TSolution, TProblem> Next { get; }

        public CheckingResult VerifyCorrect(TSolution solution, TProblem problem) =>
            First.VerifyCorrect(solution, problem) is { IsSuccessful: false } firstResult
                ? firstResult
                : Next.VerifyCorrect(solution, problem);
    }
}
