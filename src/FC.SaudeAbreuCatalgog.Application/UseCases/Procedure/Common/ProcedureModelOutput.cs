using DomainEntity = FC.AbreuBarber.Domain.Entity;


namespace FC.AbreuBarber.Application.UseCases.Procedure.Common
{
    public class ProcedureModelOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public ProcedureModelOutput(
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


        public static ProcedureModelOutput FromProcedure(Domain.Entity.Procedure procedure)
        {
            return new ProcedureModelOutput(procedure.Id,
                procedure.Name, procedure.Value,
                procedure.Description, procedure.IsActive,
                procedure.CreatedAt);
        }

    }
}
