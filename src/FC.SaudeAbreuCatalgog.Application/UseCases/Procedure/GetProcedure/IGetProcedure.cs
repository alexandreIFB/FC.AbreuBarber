using DomainEntity = FC.SaudeAbreuCatalgog.Domain.Entity;


namespace FC.SaudeAbreuCatalgog.Application.UseCases.Procedure.GetProcedure
{
    public interface IGetProcedure
    {
        public Task<DomainEntity.Procedure> Handle(GetProcedureInput input,CancellationToken cancellationToken);
    }
}
