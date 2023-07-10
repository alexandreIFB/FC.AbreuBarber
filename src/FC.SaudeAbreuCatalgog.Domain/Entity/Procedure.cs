using FC.AbreuBarber.Domain.SeedWork;
using FC.AbreuBarber.Domain.Exceptions;

namespace FC.AbreuBarber.Domain.Entity
{
    public class Procedure : AggregateRoot
    {

        public string Name { get; private set; }
        public string Description { get; private set; }

        public double Value { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }


        public Procedure(string name, string description, double value, bool isActive = true)
            : base()
        {
            Name = name;
            Description = description;
            Value = value;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
            }

            if (Name.Length < 3)
            {
                throw new EntityValidationException($"{nameof(Name)} should be at leats 3 characters long");
            }

            if (Name.Length > 255)
            {
                throw new EntityValidationException($"{nameof(Name)} should be less or equal 255 characters long");
            }

            if (Description == null)
            {
                throw new EntityValidationException($"{nameof(Description)} should not be null");
            }

            if (Description.Length > 10_000)
            {
                throw new EntityValidationException($"{nameof(Description)} should be less or equal 10_000 characters long");
            }

            if (Value < 30.0)
            {
                throw new EntityValidationException($"{nameof(Value)} should not be less than 30");
            }

            if (Value > 1000.0)
            {
                throw new EntityValidationException($"{nameof(Value)} should not be bigger than 1000");
            }
        }


        public void Activate()
        {
            IsActive = true;
            Validate();
        }

        public void Deactivate()
        {
            IsActive = false;
            Validate();
        }

        public void Update(string name, string? description = null, double? value = null)
        {
            Name = name;
            Description = description ?? Description;
            Value = value ?? Value;

            Validate();
        }
    }
}
