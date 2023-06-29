

using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure
{
    public class UpdateProcedureInput : IRequest<ProcedureModelOutput>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool? IsActive { get; set; }
        public double? Value { get; set; }

        public UpdateProcedureInput(
            Guid id,
            string? name = null,
            double? value = null,
            string? description = null,
            bool? isActive = null
        )
        {
            Id = id;
            Name = name;
            Value = value;
            Description = description ;
            IsActive = isActive;
        }
    }
}
