using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure
{
    public class CreateProcedureInput : IRequest<ProcedureModelOutput>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public bool IsActive { get; set; }

        public CreateProcedureInput(string name, double value, string? description = null, bool isActive = true)
        {
            Name = name;
            Description = description ?? "";
            Value = value;
            IsActive = isActive;
        }


    }
}
