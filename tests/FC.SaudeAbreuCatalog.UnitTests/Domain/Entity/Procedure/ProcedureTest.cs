using FC.SaudeAbreuCatalgog.Domain.Exceptions;
using Xunit;

using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;

namespace FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure
{
    public class ProcedureTest
    {
        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain" , "Procedure -  Aggregates")]
        public void Instantiate()
        {
            // Arrange
            var validData = new
            {
                Name = "Procedure Valide Name",
                Description = "Procedure Description",
                Value = 109.21
            };
            // Act
            var dateTimeBefore = DateTime.Now;

            var procedure = new DomainEntity.Procedure(validData.Name,validData.Description,validData.Value);

            var dateTimeAfter = DateTime.Now;


            // Assert
            Assert.NotNull(procedure);
            Assert.Equal(procedure.Name, validData.Name);
            Assert.Equal(procedure.Description, validData.Description);
            Assert.Equal(procedure.Value, validData.Value);
            Assert.NotEqual(procedure.Id, default(Guid));
            Assert.NotEqual(procedure.CreatedAt, default(DateTime));
            // Fazendo separado para rastrear mais facilmente
            Assert.True(procedure.CreatedAt > dateTimeBefore);
            Assert.True(procedure.CreatedAt < dateTimeAfter);
            Assert.True(procedure.IsActive);
        }

        [Theory(DisplayName = nameof(InstantiateWithIsActiveStatus))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(true)]
        [InlineData(false)]
        public void InstantiateWithIsActiveStatus(bool isActive)
        {
            // Arrange
            var validData = new
            {
                Name = "Procedure Valide Name",
                Description = "Procedure Description",
                Value = 109.21,
            };

            // Act
            var dateTimeBefore = DateTime.Now;


            var procedure = new DomainEntity.Procedure(validData.Name, validData.Description, validData.Value,isActive);

            var dateTimeAfter = DateTime.Now;


            // Assert
            Assert.NotNull(procedure);
            Assert.Equal(procedure.Name, validData.Name);
            Assert.Equal(procedure.Description, validData.Description);
            Assert.Equal(procedure.Value, validData.Value);
            Assert.NotEqual(procedure.Id, default(Guid));
            Assert.NotEqual(procedure.CreatedAt, default(DateTime));
            Assert.True(procedure.CreatedAt > dateTimeBefore);
            Assert.True(procedure.CreatedAt < dateTimeAfter);
            Assert.Equal(procedure.IsActive, isActive);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("            ")]
        public void InstantiateErrorWhenNameIsEmpty(string? name)
        {
            Action action = 
                () => new DomainEntity.Procedure(name!, "Procedure Description", 120.12);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            Action action =
                () => new DomainEntity.Procedure("Name valid", null!, 120.12);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should not be null", exception.Message);
        }


        [Fact(DisplayName = nameof(InstantiateWhenDescriptionEmpty))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateWhenDescriptionEmpty()
        {
            var procudure =  new DomainEntity.Procedure("Name valid", "", 120.12);

            Assert.Equal("", procudure.Description);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenValueIsMinor))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(0)]
        [InlineData(5.3)]
        [InlineData(49.99)]
        public void InstantiateErrorWhenValueIsMinor(double value)
        {
            Action action =
                () => new DomainEntity.Procedure("Procedure Name", "Procedure Description", value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be less than 50", exception.Message);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenValueIsBigger))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(1000.00001)]
        [InlineData(1012400.02)]
        public void InstantiateErrorWhenValueIsBigger(double value)
        {
            Action action =
                () => new DomainEntity.Procedure("Procedure Name", "Procedure Description", value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be bigger than 1000", exception.Message);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData("N")]
        [InlineData("Na")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            Action action =
                () => new DomainEntity.Procedure(invalidName, "Procedure Description", 120.12);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Procedure(invalidName, "Procedure Description", 120.12);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

            Action action =
                () => new DomainEntity.Procedure("Name Procedure", invalidDescription, 120.12);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should be less or equal 10_000 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(ChangeIsActiveStatus))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void ChangeIsActiveStatus()
        {
            // Arrange
            var validData = new
            {
                Name = "Procedure Valide Name",
                Description = "Procedure Description",
                Value = 109.21
            };
            // Act

            var procedure = new DomainEntity.Procedure(validData.Name, validData.Description, validData.Value);

            procedure.Activate();
            Assert.True(procedure.IsActive);

            procedure.Deactivate();
            Assert.False(procedure.IsActive);

            procedure.Activate();
            Assert.True(procedure.IsActive);
        }


    }
}
