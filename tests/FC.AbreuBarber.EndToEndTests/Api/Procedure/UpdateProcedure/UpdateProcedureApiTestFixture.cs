
using Bogus;
using Fc.AbreuBarber.Api.ApiModels.Procedure;
using FC.AbreuBarber.EndToEndTests.Api.Procedure.Common;
using Xunit;

namespace FC.AbreuBarber.EndToEndTests.Api.Procedure.UpdateProcedure
{

    [CollectionDefinition(nameof(UpdateProcedureApiTestFixture))]
    public class UpdateProcedureApiTestFixtureCollection : ICollectionFixture<UpdateProcedureApiTestFixture> { }

    public class UpdateProcedureApiTestFixture : ProcedureBaseFixture
    {

        public UpdateProcedureApiInput GetUpdateProcedureInput()
        {

            var input = new UpdateProcedureApiInput(
                GetValidProcedureName(),
                GetValidProcedureValue(),
                GetValidProcedureDescription(),
                GetRandomBoolean()
            );

            return input;
        }

        public UpdateProcedureApiInput GetInvalidInputShortName()
        {
            var invalidInputShortName = GetUpdateProcedureInput();
            invalidInputShortName.Name =
                invalidInputShortName.Name.Substring(0, 2);
            return invalidInputShortName;
        }
        public UpdateProcedureApiInput GetInvalidInputTooLongName()
        {
            var invalidInputTooLongName = GetUpdateProcedureInput();
            var tooLongNameForCategory = Faker.Commerce.ProductName();
            while (tooLongNameForCategory.Length <= 255)
                tooLongNameForCategory = $"{tooLongNameForCategory} {Faker.Commerce.ProductName()}";
            invalidInputTooLongName.Name = tooLongNameForCategory;
            return invalidInputTooLongName;
        }

        public UpdateProcedureApiInput GetInvalidInputTooLongDescription()
        {
            var invalidInputTooLongDescription = GetUpdateProcedureInput();
            var tooLongDescriptionForCategory = Faker.Lorem.Paragraph();
            while (tooLongDescriptionForCategory.Length <= 10_000)
                tooLongDescriptionForCategory = $"{tooLongDescriptionForCategory} {Faker.Lorem.Paragraph()}";
            invalidInputTooLongDescription.Description = tooLongDescriptionForCategory;
            return invalidInputTooLongDescription;
        }

        public UpdateProcedureApiInput GetInvalidInputDescriptionNull()
        {
            var invalidInputDescriptionNull = GetUpdateProcedureInput();
            invalidInputDescriptionNull.Description = null!;
            return invalidInputDescriptionNull;
        }

        public UpdateProcedureApiInput GetInvalidInputValueIsBigger()
        {
            var invalidInputValueIsBigger = GetUpdateProcedureInput();
            invalidInputValueIsBigger.Value = 1000.01;
            return invalidInputValueIsBigger;
        }

        public UpdateProcedureApiInput GetInvalidInputValueIsMinor()
        {
            var invalidInputValueIsMinor = GetUpdateProcedureInput();
            invalidInputValueIsMinor.Value = 29.99;
            return invalidInputValueIsMinor;
        }
    }
}
