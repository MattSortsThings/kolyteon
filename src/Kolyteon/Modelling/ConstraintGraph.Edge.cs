using System.Collections;
using Kolyteon.Modelling.Testing;

namespace Kolyteon.Modelling;

public abstract partial class ConstraintGraph<TVariable, TDomainValue, TProblem>
    where TVariable : struct, IComparable<TVariable>, IEquatable<TVariable>
    where TDomainValue : struct, IComparable<TDomainValue>, IEquatable<TDomainValue>
    where TProblem : class
{
    private sealed record Edge
    {
        private BitArray ConsistencyMatrix { get; init; } = null!;

        private Node FirstNode { get; init; } = null!;

        private int FirstDomainSize { get; init; }

        private Node SecondNode { get; init; } = null!;

        private int SecondDomainSize { get; init; }

        private int InconsistentAssignmentPairs { get; init; }

        private int CartesianProductSize => ConsistencyMatrix.Length;

        public double Tightness => (double)InconsistentAssignmentPairs / CartesianProductSize;

        public (ForwardEdge forwardEdge, ReversedEdge reversedEdge) ToDirectedEdgePair() =>
            (new ForwardEdge(this), new ReversedEdge(this));

        public ConstraintGraphEdge<TVariable, TDomainValue> ToConstraintGraphEdge() => new()
        {
            FirstVariable = FirstNode.Variable,
            SecondVariable = SecondNode.Variable,
            AssignmentPairs = GetAllAssignmentPairs(),
            Tightness = Tightness
        };

        public bool Consistent(int firstDomainValueIndex, int secondDomainValueIndex)
        {
            int initialOffset = firstDomainValueIndex >= 0 && firstDomainValueIndex < FirstDomainSize
                ? SecondDomainSize * firstDomainValueIndex
                : throw new DomainValueIndexOutOfRangeException();

            int finalOffset = secondDomainValueIndex >= 0 && secondDomainValueIndex < SecondDomainSize
                ? initialOffset + secondDomainValueIndex
                : throw new DomainValueIndexOutOfRangeException();

            return ConsistencyMatrix[finalOffset];
        }

        public static Edge? CreateIfProvenAdjacent(Node firstNode,
            Node secondNode,
            Func<TDomainValue, TDomainValue, bool> predicate)
        {
            (TDomainValue[] firstDomain, TDomainValue[] secondDomain) = (firstNode.Domain, secondNode.Domain);
            (int firstDomainSize, int secondDomainSize) = (firstDomain.Length, secondDomain.Length);
            (int cartesianProduct, int inconsistent) = (firstDomainSize * secondDomainSize, 0);
            int index = 0;

            for (; inconsistent == 0 && index < cartesianProduct; index++)
            {
                TDomainValue firstDomainValue = firstDomain[index / secondDomainSize];
                TDomainValue secondDomainValue = secondDomain[index % secondDomainSize];
                if (!predicate(firstDomainValue, secondDomainValue))
                {
                    inconsistent++;
                }
            }

            if (inconsistent == 0)
            {
                return null;
            }

            BitArray arr = new(cartesianProduct, true) { [index - 1] = false };

            for (; index < cartesianProduct; index++)
            {
                TDomainValue firstDomainValue = firstDomain[index / secondDomainSize];
                TDomainValue secondDomainValue = secondDomain[index % secondDomainSize];
                if (predicate(firstDomainValue, secondDomainValue))
                {
                    continue;
                }

                inconsistent++;
                arr[index] = false;
            }

            return CreateEdgeBetweenNodes(firstNode, secondNode, arr, inconsistent);
        }

        private AssignmentPair<TDomainValue>[] GetAllAssignmentPairs()
        {
            TDomainValue[] firstDomain = FirstNode.Domain;
            TDomainValue[] secondDomain = SecondNode.Domain;

            IEnumerable<AssignmentPair<TDomainValue>> assignmentPairQuery =
                from i in Enumerable.Range(0, FirstDomainSize)
                from j in Enumerable.Range(0, SecondDomainSize)
                let consistent = Consistent(i, j)
                select new AssignmentPair<TDomainValue>(firstDomain[i], secondDomain[j], consistent);

            return assignmentPairQuery.ToArray();
        }

        private static Edge CreateEdgeBetweenNodes(Node firstNode, Node secondNode, BitArray consistencyMatrix, int inconsistent)
        {
            Edge edge = new()
            {
                ConsistencyMatrix = consistencyMatrix,
                InconsistentAssignmentPairs = inconsistent,
                FirstNode = firstNode,
                FirstDomainSize = firstNode.DomainSize,
                SecondNode = secondNode,
                SecondDomainSize = secondNode.DomainSize
            };

            firstNode.Degree++;
            secondNode.Degree++;

            double tightness = edge.Tightness;
            firstNode.SumTightness += tightness;
            secondNode.SumTightness += tightness;

            return edge;
        }
    }
}
