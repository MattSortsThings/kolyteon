using FluentAssertions.Execution;
using Mjt85.Kolyteon.MapColouring;
using Mjt85.Kolyteon.Solving;
using Mjt85.Kolyteon.UnitTests.Helpers;

namespace Mjt85.Kolyteon.UnitTests.Solving;

/// <summary>
///     Unit tests for the abstract <see cref="SolvingProgress{V,D}" /> class using a vanilla concrete derivative class
///     parametrized over the Map Colouring problem types.
/// </summary>
public sealed class SolvingProgressTests
{
    private static readonly Region R0 = Region.FromId("R0");
    private static readonly Region R1 = Region.FromId("R1");

    private sealed class TestSolvingProgress : SolvingProgress<Region, Colour>
    {
        protected override void StateHasChanged()
        {
            // Does nothing
        }
    }

    [UnitTest]
    public sealed class Report_Method
    {
        [Fact]
        public void SetupStep_CurrentStateIsSafe_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Initial);
                sut.CurrentSearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.LatestStepType.Should().BeNull();
                sut.SetupSteps.Should().Be(0);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(0);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Setup);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(1);
            }
        }

        [Fact]
        public void SetupStep_CurrentStateIsFinal_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Initial);
                sut.CurrentSearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.LatestStepType.Should().BeNull();
                sut.SetupSteps.Should().Be(0);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(0);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Final,
                CurrentSearchLevel = -1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Final);
                sut.CurrentSearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Setup);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(1);
            }
        }

        [Fact]
        public void VisitingStep_CurrentStateIsSafe_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Setup);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(1);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black)
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().ContainSingle();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(1);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(2);
            }
        }

        [Fact]
        public void VisitingStep_CurrentStateIsUnsafe_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Setup);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(1);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(1);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(2);
            }
        }

        [Fact]
        public void VisitingStep_CurrentStateIsFinal_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black)
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().ContainSingle();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(1);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(2);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Final,
                CurrentSearchLevel = 2,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R1).AndDomainValue(Colour.White)
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().HaveCount(2);
                sut.CurrentSearchState.Should().Be(SearchState.Final);
                sut.CurrentSearchLevel.Should().Be(2);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(3);
            }
        }

        [Fact]
        public void BacktrackingStep_CurrentStateIsSafe_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black)
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().ContainSingle();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(3);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Backtracking,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Safe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Backtracking);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(1);
                sut.TotalSteps.Should().Be(4);
            }
        }

        [Fact]
        public void BacktrackingStep_CurrentStateIsUnsafe_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black)
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().ContainSingle();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(3);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Backtracking,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Backtracking);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(1);
                sut.TotalSteps.Should().Be(4);
            }
        }

        [Fact]
        public void BacktrackingStep_CurrentStateIsFinal_Updates()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(0);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(1);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(2);
            }

            // Act
            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Backtracking,
                CurrentSearchState = SearchState.Final,
                CurrentSearchLevel = -1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });
        }
    }

    [UnitTest]
    public sealed class Reset_Method
    {
        [Fact]
        public void ResetsAllProperties()
        {
            // Arrange
            SolvingProgress<Region, Colour> sut = new TestSolvingProgress();

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Setup,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 0,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Safe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = GetAssignment.WithVariable(R0).AndDomainValue(Colour.Black)
            });

            sut.Report(new StepNotification<Region, Colour>
            {
                StepType = StepType.Visiting,
                CurrentSearchState = SearchState.Unsafe,
                CurrentSearchLevel = 1,
                SearchTreeLeafLevel = 2,
                LatestAssignment = null
            });

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().ContainSingle();
                sut.CurrentSearchState.Should().Be(SearchState.Unsafe);
                sut.CurrentSearchLevel.Should().Be(1);
                sut.SearchTreeLeafLevel.Should().Be(2);
                sut.LatestStepType.Should().Be(StepType.Visiting);
                sut.SetupSteps.Should().Be(1);
                sut.VisitingSteps.Should().Be(2);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(3);
            }

            // Act
            sut.Reset();

            // Assert
            using (new AssertionScope())
            {
                sut.CurrentAssignments.Should().BeEmpty();
                sut.CurrentSearchState.Should().Be(SearchState.Initial);
                sut.CurrentSearchLevel.Should().Be(-1);
                sut.SearchTreeLeafLevel.Should().Be(0);
                sut.LatestStepType.Should().BeNull();
                sut.SetupSteps.Should().Be(0);
                sut.VisitingSteps.Should().Be(0);
                sut.BacktrackingSteps.Should().Be(0);
                sut.TotalSteps.Should().Be(0);
            }
        }
    }
}
