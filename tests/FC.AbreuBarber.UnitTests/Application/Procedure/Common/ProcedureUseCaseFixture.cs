using Bogus;
using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Domain.Repository;
using FC.AbreuBarber.UnitTests.Common;
using Moq;
using DomainEntity = FC.AbreuBarber.Domain.Entity;

namespace FC.AbreuBarber.UnitTests.Application.Procedure.Common
{
    public class ProcedureUseCaseFixture : BaseFixture
    {
        public string GetValidProcedureName()
        {
            var procedureName = "";
            while (procedureName.Length < 3)
                procedureName = GenerateProcedureName();
            if (procedureName.Length > 255)
                procedureName = procedureName[..255];
            return procedureName;
        }

        public string GetValidProcedureDescription()
        {
            var procedureDescription = GenerateProcedureDescription();
            if (procedureDescription.Length > 10_000)
                procedureDescription = procedureDescription[..10_000];
            return procedureDescription;
        }

        public double GetValidProcedureValue()
        {
            var procedureValue = Faker.Random.Double(30, 1000);

            return procedureValue;
        }

        public bool getRandomBoolean()
        => new Random().NextDouble() < 0.5;

        private string GenerateProcedureName()
        {
            var procedureName = Faker.Lorem.Sentence(2);
            return procedureName;
        }

        private string GenerateProcedureDescription()
        {
            var procedureDescription = Faker.Lorem.Paragraph();
            return procedureDescription;
        }

        public DomainEntity.Procedure GetValidProcedure()
        {
            return new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue(), getRandomBoolean());
        }

        public Mock<IProcedureRepository> GetRepositoryMock() => new();
        public Mock<IUnityOfWork> GetUnitOfWorkMock() => new();
    }
}
