using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure
{
    public class GetProcedureInput : IRequest<GetProcedureOutput>
    {
        public Guid Id { get; set; }

        public GetProcedureInput(Guid id)
        {
            Id = id;
        }
    }
}
