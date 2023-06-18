using FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.Common;
using FC.SaudeAbreuCatalgog.Domain.Repository;

namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure
{
    public class GetProcedure : IGetProcedure
    {
        private readonly IProcedureRepository _procedureRepository;

        public GetProcedure(IProcedureRepository procedureRepository)
        {
            _procedureRepository = procedureRepository;
        }

        public async Task<ProcedureModelOutput> Handle(GetProcedureInput input, CancellationToken cancellationToken)
        {
            var procedure = await _procedureRepository.Get(input.Id, cancellationToken);

            return ProcedureModelOutput.FromProcedure(procedure);
        }
    }
}
