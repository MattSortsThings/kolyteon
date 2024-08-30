namespace Kolyteon.Common.Internals;

internal abstract class SolutionVerifier<TSolution, TProblem>
    where TProblem : ISolutionVerifier<TSolution>
{
    internal abstract Result VerifyCorrect(TSolution solution, TProblem problem);

    internal SolutionVerifier<TSolution, TProblem> Then(SolutionVerifier<TSolution, TProblem> next) =>
        new ChainNode(this, next);

    private sealed class ChainNode : SolutionVerifier<TSolution, TProblem>
    {
        public ChainNode(SolutionVerifier<TSolution, TProblem> first, SolutionVerifier<TSolution, TProblem> next)
        {
            First = first;
            Next = next;
        }

        private SolutionVerifier<TSolution, TProblem> First { get; }

        private SolutionVerifier<TSolution, TProblem> Next { get; }

        internal override Result VerifyCorrect(TSolution solution, TProblem problem) =>
            First.VerifyCorrect(solution, problem) is { IsSuccessful: false } firstResult
                ? firstResult
                : Next.VerifyCorrect(solution, problem);
    }
}
