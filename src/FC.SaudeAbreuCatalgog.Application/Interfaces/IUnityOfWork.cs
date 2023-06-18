namespace FC.AbreuBarber.Application.Interfaces
{
    public interface IUnityOfWork
    {
        public Task Commit(CancellationToken cancellationToken);
    }
}
