using Kolyteon.Common;
using Kolyteon.Tests.Utils.TestAssertions;

namespace Kolyteon.Tests.Unit.Common;

public static class ResultTests
{
    [UnitTest]
    public sealed class SuccessStaticFactoryMethod
    {
        [Fact]
        public void Success_ReturnsSuccessfulResult()
        {
            // Act
            Result result = Result.Success();

            // Assert
            result.Should().BeSuccessful()
                .And.HaveNullFirstError();
        }
    }

    [UnitTest]
    public sealed class FailureStaticFactoryMethod
    {
        [Fact]
        public void Failure_ReturnsUnsuccessfulResultWithSpecifiedFirstError()
        {
            // Arrange
            const string errorMessage = "Error";

            // Act
            Result result = Result.Failure(errorMessage);

            // Assert
            result.Should().BeUnsuccessful()
                .And.HaveFirstError(errorMessage);
        }

        [Fact]
        public void Failure_FirstErrorArgIsNull_Throws()
        {
            // Act
            Action act = () => Result.Failure(null!);

            // Assert
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'firstError')");
        }
    }
}
