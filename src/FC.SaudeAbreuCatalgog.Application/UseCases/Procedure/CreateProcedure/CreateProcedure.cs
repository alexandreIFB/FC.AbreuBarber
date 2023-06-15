using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.CreateProcedure
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

        public async Task<CreateProcedureOutput> Handle(CreateProcedureInput input, CancellationToken cancellationToken)
        {
            var procedure = new DomainEntity.Procedure(input.Name,input.Description,input.Value,input.IsActive);

            await _procedureRepository.Insert(procedure,cancellationToken);
            await _unityOfWork.Commit(cancellationToken);

            return new CreateProcedureOutput(procedure.Id,
                procedure.Name,procedure.Value,
                procedure.Description,procedure.IsActive,
                procedure.CreatedAt);
        }
    }
}
