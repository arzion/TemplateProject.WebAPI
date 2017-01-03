using System;
using System.Data.Entity;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The factory that creates <see cref="DbContext"/> instance.
    /// </summary>
    /// <seealso cref="IDbContextProvider" />
    public class DbContextProvider : IDbContextProvider, IDisposable
    {
        private DbContext _dbContext;
        private static readonly object LockObject = new object();

        /// <summary>
        /// Gets the database context.
        /// </summary>
        /// <returns>Created DB context.</returns>
        public DbContext GetDbContext()
        {
            if (_dbContext != null)
            {
                return _dbContext;
            }

            lock (LockObject)
            {
                if (_dbContext == null)
                {
                    _dbContext = new TemplateProjectContext();
                    return _dbContext;
                }
                return _dbContext;
            }
        }

        #region disposing

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing,
        /// or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Finalizes an instance of the <see cref="EfWriter{TEntity}"/> class.
        /// </summary>
        ~DbContextProvider()
        {
            Dispose(false);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // Free other state (managed objects).
                }
                // Free your own state (unmanaged objects).
                // Set large fields to null.
                if (_dbContext != null)
                {
                    _dbContext.Dispose();
                    _dbContext = null;
                }
                _disposed = true;
            }
        }

        #endregion
    }
}