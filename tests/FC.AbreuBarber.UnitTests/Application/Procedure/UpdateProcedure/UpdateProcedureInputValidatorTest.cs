using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FluentAssertions;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{
    [Collection(nameof(UpdateProcedureTestFixture))]
    public class UpdateProcedureInputValidatorTest
    {
        public readonly UpdateProcedureTestFixture  _fixture;

        public UpdateProcedureInputValidatorTest(UpdateProcedureTestFixture updateProcedureTestFixture) => _fixture = updateProcedureTestFixture;

        [Fact(DisplayName = nameof(ErrorWhenIdUpdateIsEmpty))]
        [Trait("Application", "UpdateProcedureInputValidator - Use Cases")]
        public void ErrorWhenIdUpdateIsEmpty()
        {
            var invalidInput = _fixture.GetValidProcedureInput(Guid.Empty);

            var validator = new UpdateProcedureInputValidator();

            var validateResult = validator.Validate(invalidInput);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeFalse();
            validateResult.Errors.Should().HaveCount(1);
            validateResult.Errors[0].ErrorMessage.Should().Be("'Id' must not be empty.");
        }

        [Fact(DisplayName = nameof(ValidateSucess))]
        [Trait("Application", "UpdateProcedureInputValidator - Use Cases")]
        public void ValidateSucess()
        {
            var invalidInput = _fixture.GetValidProcedureInput();

            var validator = new UpdateProcedureInputValidator();

            var validateResult = validator.Validate(invalidInput);

            validateResult.Should().NotBeNull();
            validateResult.IsValid.Should().BeTrue();
            validateResult.Errors.Should().HaveCount(0);
        }
    }
}
