using Bogus.DataSets;
using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure;
using FC.SaudeAbreuCatalog.UnitTests.Application.Common;
using FC.SaudeAbreuCatalog.UnitTests.Application.CreateProcedure;
using FC.SaudeAbreuCatalog.UnitTests.Common;
using FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.CreateProcedure
{
    public class CreateProcedureTestFixture : ProcedureUseCaseFixture
    {
        public CreateProcedureInput GetValidInput()
        {
            return new CreateProcedureInput(GetValidProcedureName(),
                GetValidProcedureValue(),GetValidProcedureDescription(),
                getRandomBoolean() 
               );
        }
    }
}

[CollectionDefinition(nameof(CreateProcedureTestFixture))]
public class CreateProcedureTestFixtureCollection : ICollectionFixture<CreateProcedureTestFixture> { }