

using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure;
using FluentAssertions;
using Moq;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.GetProcedure
{

    [Collection(nameof(GetProcedureTestFixture))]
    public class GetProcedureInputValidatorTest
    {
        private readonly GetProcedureTestFixture _fixture;

        public GetProcedureInputValidatorTest(GetProcedureTestFixture fixture) =>
            _fixture = fixture;

        [Fact(DisplayName = nameof(ValidationSucess))]
        [Trait("Application", "GetCategoryInputProcedure - Use Cases")]
        public void ValidationSucess()
        {

            var validInput = new GetProcedureInput(Guid.NewGuid());


            var validator = new GetProcedureInputValidator();

            var validationResult = validator.Validate(validInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeTrue();
            validationResult.Errors.Should().HaveCount(0);
        }

        [Fact(DisplayName = nameof(ValidationFailed))]
        [Trait("Application", "GetCategoryInputProcedure - Use Cases")]
        public void ValidationFailed()
        {

            var invalidInput = new GetProcedureInput(Guid.Empty);


            var validator = new GetProcedureInputValidator();

            var validationResult = validator.Validate(invalidInput);

            validationResult.Should().NotBeNull();
            validationResult.IsValid.Should().BeFalse();
            validationResult.Errors[0].ErrorMessage
                .Should().Be("'Id' must not be empty.");
        }
    }
}
