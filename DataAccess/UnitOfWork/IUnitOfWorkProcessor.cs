using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The processor of the Unit of Work entities.
    /// </summary>
    public interface IUnitOfWorkProcessor
    {
        /// <summary>
        /// Process the changes of the entities.
        /// </summary>
        Task Process(IEnumerable<UnitOfWorkEntity> entities);
    }
}