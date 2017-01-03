using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemplateProject.DataAccess.UnitOfWork
{
    /// <summary>
    /// The Unit of Work.
    /// </summary>
    internal interface IUnitOfWork
    {
        /// <summary>
        /// Gets the list of unit of work entities.
        /// </summary>
        IList<UnitOfWorkEntity> Entities { get; }

        /// <summary>
        /// Saves the changes of the entities.
        /// </summary>
        Task SaveChanges();
    }
}