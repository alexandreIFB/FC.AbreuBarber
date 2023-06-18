using Bogus;
using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using FC.SaudeAbreuCatalog.UnitTests.Common;
using Moq;
using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;

namespace FC.SaudeAbreuCatalog.UnitTests.Application.Common
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

        public Double GetValidProcedureValue()
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
