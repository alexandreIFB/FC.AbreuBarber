using FC.AbreuBarber.UnitTests.Application.Procedure.Common;
using FC.AbreuBarber.UnitTests.Application.Procedure.ListProcedures;
using Xunit;
using DomainEntity = FC.AbreuBarber.Domain.Entity;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.ListProcedures
{
    public class ListProceduresTestFixture : ProcedureUseCaseFixture
    {


        public List<DomainEntity.Procedure> GetExampleProceduresList(int lenght = 10)
        {

            var list = new List<DomainEntity.Procedure>();

            for(int i = 0; i < lenght; i++)
            {
                list.Add(GetValidProcedure());
            }

            return list;
        }
    }
}


[CollectionDefinition(nameof(ListProceduresTestFixture))]
public class ListProceduresTestFixtureCollection : ICollectionFixture<ListProceduresTestFixture> { }
