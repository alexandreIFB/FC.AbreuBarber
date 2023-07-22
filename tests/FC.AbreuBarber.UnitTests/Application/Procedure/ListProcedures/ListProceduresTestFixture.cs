using FC.AbreuBarber.Application.UseCases.Procedure.ListProcedures;
using FC.AbreuBarber.Domain.SeedWork.SearchableRepository;
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

        public ListProceduresInput GetExampleInput()
        {

            var random = new Random();

            return new ListProceduresInput(
                page: random.Next(1,20),
                perPage: random.Next(15,40),
                search: Faker.Commerce.ProductName(),
                sort: "name",
                dir: random.Next(0,10) > 5 ? SearchOrder.Asc : SearchOrder.Desc
            );
        }
    }
}


[CollectionDefinition(nameof(ListProceduresTestFixture))]
public class ListProceduresTestFixtureCollection : ICollectionFixture<ListProceduresTestFixture> { }
