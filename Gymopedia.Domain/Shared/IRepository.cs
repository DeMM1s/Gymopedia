namespace Gymopedia.Domain.Shared
{
    public interface IRepository
    {
        Task Commit(CancellationToken cancellationToken);
    }
}
