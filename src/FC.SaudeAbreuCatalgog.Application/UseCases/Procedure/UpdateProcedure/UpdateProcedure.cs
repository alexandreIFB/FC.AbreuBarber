

using FC.AbreuBarber.Application.Interfaces;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.Repository;

namespace FC.AbreuBarber.Application.UseCases.Procedure.UpdateProcedure
{
    public class UpdateProcedure : IUpdateProcedure
    {
        private readonly IProcedureRepository _procedureRepository;
        private readonly IUnityOfWork _unityOfWork;

        public UpdateProcedure(IProcedureRepository procedureRepository, IUnityOfWork unityOfWork)
        {
            _procedureRepository = procedureRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<ProcedureModelOutput> Handle(UpdateProcedureInput input, CancellationToken cancellationToken)
        {
            var procedureForUpdate = await _procedureRepository.Get(input.Id, cancellationToken);

            procedureForUpdate.Update(input.Name, input.Description, input.Value);

            if (
                input.IsActive != null &&
                input.IsActive != procedureForUpdate.IsActive
            )
            {
                if ((bool)input.IsActive!) procedureForUpdate.Activate();
                else procedureForUpdate.Deactivate();
            }


            await _procedureRepository.Update(procedureForUpdate, cancellationToken);
            await _unityOfWork.Commit(cancellationToken);

            return ProcedureModelOutput.FromProcedure(procedureForUpdate);
        }
    }
}
