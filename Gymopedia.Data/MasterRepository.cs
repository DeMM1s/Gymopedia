using Microsoft.EntityFrameworkCore;
using Gymopedia.Domain.Shared;

namespace Gymopedia.Data
{
    public class MasterRepository : IRepository, IDisposable//, IAsyncDisposable
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Context != null)
                {
                    Context.Dispose();
                    Context = null;
                }
            }
        }
        //public void dispose()
        //{
        //    throw new notimplementedexception();
        //}

        //public valuetask disposeasync()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        #endregion
    }
}