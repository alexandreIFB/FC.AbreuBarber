using Bogus.DataSets;
using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using FC.SaudeAbreuCatalog.UnitTests.Application.Common;
using FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalog.UnitTests.Common;
using FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.Procedure.CreateProcedure
{
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

[CollectionDefinition(nameof(CreateProcedureTestFixture))]
public class CreateProcedureTestFixtureCollection : ICollectionFixture<CreateProcedureTestFixture> { }