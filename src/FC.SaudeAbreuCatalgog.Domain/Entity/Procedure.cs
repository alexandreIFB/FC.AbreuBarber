using FC.SaudeAbreuCatalgog.Domain.Exceptions;

namespace FC.SaudeAbreuCatalgog.Domain.Entity
{
    public class Procedure
    {

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public double Value { get; private set; }

        public bool IsActive { get; private set; }

        public DateTime CreatedAt { get; private set; }


        public Procedure(string name, string description, double value, bool isActive = true)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Value = value;
            IsActive = isActive;
            CreatedAt = DateTime.Now;

            Validate();
        }

        public void Validate()
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                throw new EntityValidationException($"{nameof(Name)} should not be empty or null");
            }

            if(Description == null)
            {
                throw new EntityValidationException($"{nameof(Description)} should not be null");
            }

            if(Value < 50.0)
            {
                throw new EntityValidationException($"{nameof(Value)} should not be less than 50");
            }

            if (Value > 1000.0)
            {
                throw new EntityValidationException($"{nameof(Value)} should not be bigger than 1000");
            }
        }
    }
}
