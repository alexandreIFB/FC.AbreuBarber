using FC.SaudeAbreuCatalgog.Application.Interfaces;
using FC.SaudeAbreuCatalgog.Domain.Entity;
using FC.SaudeAbreuCatalgog.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure
{
    public class GetProcedure : IGetProcedure
    {
        private readonly IUnityOfWork _unityOfWork;
        private readonly IProcedureRepository _procedureRepository;

        public GetProcedure(IUnityOfWork unityOfWork, IProcedureRepository procedureRepository)
        {
            _unityOfWork = unityOfWork;
            _procedureRepository = procedureRepository;
        }

        public async Task<Domain.Entity.Procedure> Handle(GetProcedureInput input, CancellationToken cancellationToken)
        {
            var procedure = await _procedureRepository.Get(input.Id, cancellationToken);
            await _unityOfWork.Commit(cancellationToken);

            return procedure;
        }
    }
}
