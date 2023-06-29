

using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Domain.Repository;
using MediatR;

namespace FC.AbreuBarber.Application.UseCases.Procedure.DeleteProcedure
{
    public class DeleteProcedure : IDeleteProcedure
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IUnityOfWork _unityOfWork;


        public DeleteProcedure(IProcedureRepository procedureRepository, IUnityOfWork unityOfWork)
        {
            _procedureRepository = procedureRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Handle(DeleteProcedureInput input, CancellationToken cancellationToken)
        {
            var procedureForDelete = await _procedureRepository.Get(input.Id, cancellationToken);
            await _procedureRepository.Delete(procedureForDelete, cancellationToken);
            await _unityOfWork.Commit(cancellationToken);
        }
    }
}
