﻿
using FC.AbreuBarber.Domain.Entity;
using FC.AbreuBarber.Infra.Data.EF.Configurations;
using FC.AbreuBarber.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace FC.AbreuBarber.IntegrationTests.Infra.Data.EF.Repositories.ProcedureRepository
{
    [CollectionDefinition(nameof(ProcedureRepositoryTestFixture))]
    public class ProcedureRepositoryTestFixtureCollection : ICollectionFixture<ProcedureRepositoryTestFixture> { }

    public class ProcedureRepositoryTestFixture : BaseFixture
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

        public Procedure GetExampleProcedure()
        {
            return new(GetValidProcedureName(), GetValidProcedureDescription(), GetValidProcedureValue(), getRandomBoolean());
        }

        public List<Procedure> GetExampleProceduresList(int length = 15)
        {
            return Enumerable.Range(1, length).Select(_ => 
                GetExampleProcedure()
            ).ToList();
        }

        public List<Procedure> GetExampleProceduresListWithNames(List<string> names)
        {
            return names.Select(name => {
                var procedure = GetExampleProcedure();
                procedure.Update(name);
                return procedure;
            }).ToList();
        }


        public AbreuBarberDbContext CreateDbContext(bool preservedData = false)
        {
            var context = new AbreuBarberDbContext(
                new DbContextOptionsBuilder<AbreuBarberDbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
                );

            if(preservedData == false)
                context.Database.EnsureDeleted();

            return context;
        }

        public List<string> GetBarberProceduresNamesStatic() => new List<string>
        {
            "Corte de Cabelo",
            "Barba Tradicional",
            "Barba Moderna",
            "Penteado Clássico",
            "Penteado com Pomada",
            "Desenho de Barba",
            "Corte Degradê",
            "Corte Social",
            "Corte de Máquina",
            "Tintura de Barba",
            "Hidratação de Barba",
            "Massagem Capilar",
            "Design de Sobrancelhas",
            "Aplicação de Cera Quente",
            "Aparar Bigode",
            "Depilação Facial",
            "Tratamento para Queda de Cabelo",
            "Barboterapia (Barba + Hidratação)",
            "Relaxamento Capilar",
            "Touca de Gesso",
            "Corte Infantil",
            "Corte Feminino",
            "Sobrancelhas Masculinas",
            "Dreadlocks",
            "Trança Masculina",
            "Acabamento na Nuca",
            "Barba Feita na Navalha"
        };

    }
}
