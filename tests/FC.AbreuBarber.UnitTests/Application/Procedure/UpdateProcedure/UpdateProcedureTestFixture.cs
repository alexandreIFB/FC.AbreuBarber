using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure;
using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure;
using Xunit;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.UpdateProcedure
{
    public class UpdateProcedureTestFixture : ProcedureUseCaseFixture
    {

        public UpdateProcedureInput GetValidInput(Guid? id = null)
        {
            var fixture = new UpdateProcedureTestFixture();

            var input = new UpdateProcedureInput(
                    id ?? Guid.NewGuid(),
                    fixture.GetValidProcedureName(),
                    fixture.GetValidProcedureValue(),
                    fixture.GetValidProcedureDescription(),
                    fixture.GetRandomBoolean()
                   );

            return input;
        }

        public UpdateProcedureInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetValidInput();
            invalidInputShortName.Name =
                invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }
        public UpdateProcedureInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetValidInput();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public UpdateProcedureInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetValidInput();
            var tooLongDescriptionForCategory = Faker.Lorem.Paragraph();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Lorem.Paragraph()}";
            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

        public UpdateProcedureInput GetInvalidInputDescriptionNull()
        {
            var invalidInputDescriptionNull = GetValidInput();
            invalidInputDescriptionNull.Description = null!;
            return invalidInputDescriptionNull;
        }

        public UpdateProcedureInput GetInvalidInputValueIsBigger()
        {
            var invalidInputValueIsBigger = GetValidInput();
            invalidInputValueIsBigger.Value = 1000.01;
            return invalidInputValueIsBigger;
        }

        public UpdateProcedureInput GetInvalidInputValueIsMinor()
        {
            var invalidInputValueIsMinor = GetValidInput();
            invalidInputValueIsMinor.Value = 29.99;
            return invalidInputValueIsMinor;
        }
    }
}

[CollectionDefinition(nameof(UpdateProcedureTestFixture))]
public class UpdateProcedureTestFixtureCollection : ICollectionFixture<UpdateProcedureTestFixture> { }