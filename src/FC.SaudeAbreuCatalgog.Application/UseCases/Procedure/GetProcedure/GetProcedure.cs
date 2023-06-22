using FC.AbreuBarber.Application.Exceptions;
using FC.AbreuBarber.Application.UseCases.Procedure.Common;
using FC.AbreuBarber.Domain.Repository;

namespace FC.AbreuBarber.Application.UseCases.Procedure.GetProcedure
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

            if(procedure == null)
            {
                throw new NotFoundException($"Procedure '{input.Id}' Not Found");
            }

            return ProcedureModelOutput.FromProcedure(procedure);
        }
    }
}
