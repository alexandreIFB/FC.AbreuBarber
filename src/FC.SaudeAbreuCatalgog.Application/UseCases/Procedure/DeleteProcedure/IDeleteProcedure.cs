using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using MediatR;


namespace FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure
{
    public interface IDeleteProcedure : IRequestHandler<DeleteProcedureInput> { }
    
}
