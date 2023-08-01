

using FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure;
using FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Application.UseCases.Procedure.CreateProcedure
{
    [CollectionDefinition(nameof(CreateProcedureTestFixture))]
    public class CreateProcedureTestFixtureCollection : ICollectionFixture<CreateProcedureTestFixture> { }

    public class CreateProcedureTestFixture : ProcedureUseCaseFixture
    {
        public CreateProcedureInput GetValidInput()
        {
            return new CreateProcedureInput(GetValidProcedureName(),
                GetValidProcedureValue(), GetValidProcedureDescription(),
                getRandomBoolean()
               );
        }

        public CreateProcedureInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetValidInput();
            invalidInputShortName.Name =
                invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }
        public CreateProcedureInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetValidInput();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public CreateProcedureInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetValidInput();
            var tooLongDescriptionForCategory = Faker.Lorem.Paragraph();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Lorem.Paragraph()}";
            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

        public CreateProcedureInput GetInvalidInputDescriptionNull()
        {
            var invalidInputDescriptionNull = GetValidInput();
            invalidInputDescriptionNull.Description = null!;
            return invalidInputDescriptionNull;
        }

        public CreateProcedureInput GetInvalidInputValueIsBigger()
        {
            var invalidInputValueIsBigger = GetValidInput();
            invalidInputValueIsBigger.Value = 1000.01;
            return invalidInputValueIsBigger;
        }

        public CreateProcedureInput GetInvalidInputValueIsMinor()
        {
            var invalidInputValueIsMinor = GetValidInput();
            invalidInputValueIsMinor.Value = 29.99;
            return invalidInputValueIsMinor;
        }
    }
}
