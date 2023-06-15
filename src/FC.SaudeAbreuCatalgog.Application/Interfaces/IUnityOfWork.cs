
namespace FC.SaudeAbreuCatalgog.Application.Interfaces
{
    public interface IUnityOfWork
    {
        public Task Commit(CancellationToken cancellationToken);
    }
}
