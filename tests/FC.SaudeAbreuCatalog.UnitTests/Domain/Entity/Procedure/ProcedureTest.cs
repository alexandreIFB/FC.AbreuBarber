using FC.SaudeAbreuCatalgog.Domain.Exceptions;
using FC.SaudeAbreuCatalgog.Domain.SeedWork;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Xunit;

using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;

namespace FC.SaudeAbreuCatalog.UnitTests.Domain.Entity.Procedure
{
    [Collection(nameof(ProcedureTestFixture))]
    public class ProcedureTest
    {
        private readonly ProcedureTestFixture _fixture;

        public ProcedureTest() => _fixture = new ProcedureTestFixture();

        [Fact(DisplayName = nameof(Instantiate))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void Instantiate()
        {
            // Arrange
            var validProcedure = _fixture.GetValidProcedure();
            // Act
            var dateTimeBefore = DateTime.Now;

            var procedure = new DomainEntity.Procedure(validProcedure.Name, validProcedure.Description, validProcedure.Value);

            var dateTimeAfter = DateTime.Now;


            // Assert
            Assert.NotNull(procedure);
            Assert.Equal(procedure.Name, validProcedure.Name);
            Assert.Equal(procedure.Description, validProcedure.Description);
            Assert.Equal(procedure.Value, validProcedure.Value);
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
            var validProcedure = _fixture.GetValidProcedure();

            // Act
            var dateTimeBefore = DateTime.Now;


            var procedure = new DomainEntity.Procedure(validProcedure.Name, validProcedure.Description, validProcedure.Value, isActive);

            var dateTimeAfter = DateTime.Now;


            // Assert
            Assert.NotNull(procedure);
            Assert.Equal(procedure.Name, validProcedure.Name);
            Assert.Equal(procedure.Description, validProcedure.Description);
            Assert.Equal(procedure.Value, validProcedure.Value);
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
            var validProcedure = _fixture.GetValidProcedure();


            Action action =
                () => new DomainEntity.Procedure(name!, validProcedure.Description, validProcedure.Value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsNull))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenDescriptionIsNull()
        {
            var validProcedure = _fixture.GetValidProcedure();


            Action action =
                () => new DomainEntity.Procedure(validProcedure.Name, null!, validProcedure.Value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should not be null", exception.Message);
        }


        [Fact(DisplayName = nameof(InstantiateWhenDescriptionEmpty))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateWhenDescriptionEmpty()
        {
            var validProcedure = _fixture.GetValidProcedure();


            var procudure = new DomainEntity.Procedure(validProcedure.Name, "", validProcedure.Value);

            Assert.Equal("", procudure.Description);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenValueIsMinor))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(0)]
        [InlineData(5.3)]
        [InlineData(29.99)]
        public void InstantiateErrorWhenValueIsMinor(double value)
        {
            var validProcedure = _fixture.GetValidProcedure();

            Action action =
                () => new DomainEntity.Procedure(validProcedure.Name, validProcedure.Description, value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be less than 50", exception.Message);
        }

        [Theory(DisplayName = nameof(InstantiateErrorWhenValueIsBigger))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(1000.00001)]
        [InlineData(1012400.02)]
        public void InstantiateErrorWhenValueIsBigger(double value)
        {
            var validProcedure = _fixture.GetValidProcedure();

            Action action =
                () => new DomainEntity.Procedure(validProcedure.Name, validProcedure.Description, value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be bigger than 1000", exception.Message);
        }


        [Theory(DisplayName = nameof(InstantiateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData("N")]
        [InlineData("Na")]
        public void InstantiateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            var validProcedure = _fixture.GetValidProcedure();

            Action action =
                () => new DomainEntity.Procedure(invalidName, validProcedure.Description, validProcedure.Value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(InstantiateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            var validProcedure = _fixture.GetValidProcedure();

            Action action =
                () => new DomainEntity.Procedure(invalidName, validProcedure.Description, validProcedure.Value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void InstantiateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

            var validProcedure = _fixture.GetValidProcedure();


            Action action =
                () => new DomainEntity.Procedure(validProcedure.Name, invalidDescription, validProcedure.Value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should be less or equal 10_000 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(ChangeIsActiveStatus))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void ChangeIsActiveStatus()
        {
            // Arrange
            var validProcedure = _fixture.GetValidProcedure();
            // Act


            validProcedure.Activate();
            Assert.True(validProcedure.IsActive);

            validProcedure.Deactivate();
            Assert.False(validProcedure.IsActive);

            validProcedure.Activate();
            Assert.True(validProcedure.IsActive);
        }

        [Fact(DisplayName = nameof(Update))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void Update()
        {
            var validProcedure = _fixture.GetValidProcedure();
            var newValues = new
            {
                Name = "New Procedure Valide Name",
                Description = "New Procedure Description",
                Value = 100.12
            };

            validProcedure.Update(newValues.Name, newValues.Description, newValues.Value);


            Assert.Equal(newValues.Name, validProcedure.Name);
            Assert.Equal(newValues.Description, validProcedure.Description);
            Assert.Equal(newValues.Value, validProcedure.Value);
        }

        [Fact(DisplayName = nameof(UpdateOnlyName))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateOnlyName()
        {
            var validProcedure = _fixture.GetValidProcedure();

            var newName = "New Procedure Valide Name";

            var initDescription = validProcedure.Description;


            validProcedure.Update(newName);


            Assert.Equal(newName, validProcedure.Name);
            Assert.Equal(validProcedure.Description, initDescription);
        }

        [Fact(DisplayName = nameof(UpdateOnlyDescription))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateOnlyDescription()
        {
            var validProcedure = _fixture.GetValidProcedure();
            var procedure = _fixture.GetValidProcedure();
            var newDescription = "New Procedure Valide Description";


            procedure.Update(null,newDescription);


            Assert.Equal(newDescription, procedure.Description);
            Assert.Equal(validProcedure.Name, procedure.Name);
            Assert.Equal(validProcedure.Value, procedure.Value);
        }

        [Fact(DisplayName = nameof(UpdateOnlyValue))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateOnlyValue()
        {
            var validProcedure = _fixture.GetValidProcedure();
            var procedure = _fixture.GetValidProcedure();
            var newValue = 111.9;

            procedure.Update(null, null, newValue);


            Assert.Equal(newValue,procedure.Value);
            Assert.Equal(validProcedure.Name, procedure.Name);
            Assert.Equal(validProcedure.Description, procedure.Description);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsEmpty))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData("")]
        [InlineData("            ")]
        public void UpdateErrorWhenNameIsEmpty(string? name)
        {
            var procedure = _fixture.GetValidProcedure();
            Action action =
                () => procedure.Update(name);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should not be empty or null", exception.Message);
        }

        [Fact(DisplayName = nameof(UpdateWhenDescriptionIsNull))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateWhenDescriptionIsNull()
        {

            var procedure = _fixture.GetValidProcedure();
            var initDescription = procedure.Description;


            procedure.Update(null, null);

            Assert.Equal(initDescription, procedure.Description);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenValueIsMinor))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(0)]
        [InlineData(5.3)]
        [InlineData(29.99)]
        public void UpdateErrorWhenValueIsMinor(double value)
        {
            var procedure = _fixture.GetValidProcedure();

            Action action =
                () => procedure.Update(null, null, value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be less than 50", exception.Message);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenValueIsBigger))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData(1000.00001)]
        [InlineData(1012400.02)]
        public void UpdateErrorWhenValueIsBigger(double value)
        {
            var procedure = _fixture.GetValidProcedure();

            Action action =
                () => procedure.Update(null, null, value);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Value should not be bigger than 1000", exception.Message);
        }

        [Theory(DisplayName = nameof(UpdateErrorWhenNameIsLessThan3Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        [InlineData("N")]
        [InlineData("Na")]
        public void UpdateErrorWhenNameIsLessThan3Characters(string invalidName)
        {
            var procedure = _fixture.GetValidProcedure();

            Action action =
                () => procedure.Update(invalidName);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be at leats 3 characters long", exception.Message);
        }


        [Fact(DisplayName = nameof(UpdateErrorWhenNameIsGreaterThan255Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateErrorWhenNameIsGreaterThan255Characters()
        {
            var invalidName = String.Join(null, Enumerable.Range(1, 256).Select(_ => "a").ToArray());

            var procedure = _fixture.GetValidProcedure();

            Action action =
                () => procedure.Update(invalidName);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Name should be less or equal 255 characters long", exception.Message);
        }

        [Fact(DisplayName = nameof(UpdateErrorWhenDescriptionIsGreaterThan10_000Characters))]
        [Trait("Domain", "Procedure -  Aggregates")]
        public void UpdateErrorWhenDescriptionIsGreaterThan10_000Characters()
        {
            var invalidDescription = String.Join(null, Enumerable.Range(1, 10001).Select(_ => "a").ToArray());

            var procedure = _fixture.GetValidProcedure();

            Action action =
                () => procedure.Update(null,invalidDescription);

            var exception = Assert.Throws<EntityValidationException>(() => action());
            Assert.Equal("Description should be less or equal 10_000 characters long", exception.Message);
        }

    }
}
