using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Data
{
    public class MasterRepository : IRepository, IDisposable, IAsyncDisposable
    {
        protected LocalDbContext Context;

        protected MasterRepository (LocalDbContext context)
        {
            Context = context;
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