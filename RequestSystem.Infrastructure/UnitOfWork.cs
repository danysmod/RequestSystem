using Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext context;
        private bool disposed;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<int> Save()
        {
            var affectedRows = await this.context.SaveChangesAsync();
            return affectedRows;
        }

        private void Dispose(bool dis)
        {
            if (!this.disposed)
            {
                if (dis)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
