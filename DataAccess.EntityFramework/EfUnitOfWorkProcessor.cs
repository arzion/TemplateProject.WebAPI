using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using TemplateProject.DataAccess.UnitOfWork;

namespace TemplateProject.DataAccess.EntityFramework
{
    /// <summary>
    /// The unit of work processor.
    /// </summary>
    /// <seealso cref="TemplateProject.DataAccess.UnitOfWork.IUnitOfWorkProcessor" />
    public class EfUnitOfWorkProcessor : IUnitOfWorkProcessor
    {
        private readonly DbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfUnitOfWorkProcessor"/> class.
        /// </summary>
        /// <param name="provider">The factory of <see cref="DbContext"/>.</param>
        public EfUnitOfWorkProcessor(IDbContextProvider provider)
        {
            _context = provider.GetDbContext();
        }

        /// <summary>
        /// Process the changes of the entities.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns>The result of processing changes operation.</returns>
        public async Task Process(IEnumerable<UnitOfWorkEntity> entities)
        {
            await _context.SaveChangesAsync();
        }
    }
}