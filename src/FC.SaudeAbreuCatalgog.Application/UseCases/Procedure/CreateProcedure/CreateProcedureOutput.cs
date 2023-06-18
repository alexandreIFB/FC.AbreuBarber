

using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;


namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure
{
    public class CreateProcedureOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public CreateProcedureOutput(
                Guid id,
                string name,
                double value, string description,
                bool isActive,
                DateTime createdAt
            )
        {
            Id = id;
            Name = name;
            Value = value;
            IsActive = isActive;
            Description = description;
            CreatedAt = createdAt;
        }


        public static CreateProcedureOutput FromProcedure(DomainEntity.Procedure procedure)
        {
            return new CreateProcedureOutput(procedure.Id,
                procedure.Name, procedure.Value,
                procedure.Description, procedure.IsActive,
                procedure.CreatedAt);
        }

    }
}
