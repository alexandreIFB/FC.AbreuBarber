using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.Repository;
using DomainEntity = FC.AbreuBarber.Domain.Entity;


namespace FC.AbreuBarber.Application.UseCases.Procedure.CreateProcedure
{
    public class CreateProcedure : ICreateProcedure
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IProcedureRepository _procedureRepository;

        public CreateProcedure(IUnityOfWork unityOfWork, IProcedureRepository procedureRepository)
        {
            _unityOfWork = unityOfWork;
            _procedureRepository = procedureRepository;
        }

        public async Task<ProcedureModelOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken)
        {
            var procedure = new DomainEntity.Procedure(input.Name, input.Description, input.Value, input.IsActive);

            await _procedureRepository.Insert(procedure, cancellationToken);
            await _unityOfWork.Commit(cancellationToken);

            return ProcedureModelOutput.FromProcedure(procedure);
        }
    }
}
