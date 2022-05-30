using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Data
{
    public class Repository<TEntity> : IRepository, IDisposable, IAsyncDisposable where TEntity : DbContext
    {
        protected TEntity Context;

        protected Repository(IDbContextFactory<TEntity> factory)
        {
            Context = factory.CreateDbContext();
        }

        public async Task Commit(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }


        #region Dispose

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}