using System;
using TemplateProject.DataAccess.UnitOfWork;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess
{
    /// <summary>
    /// The configuration of the Data Access layer.
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Gets the unit of work processor.
        /// </summary>
        /// <value>
        /// The unit of work processor.
        /// </value>
        public static Func<IUnitOfWorkProcessor> UnitOfWorkProcessor { get; set; } = () => new DefaultUnitOfWorkProcessor();

        /// <summary>
        /// Provides the writer factory.
        /// </summary>
        /// <returns>The writer of the entity.</returns>
        public static Func<Entity, IWriter> WriterFactory { get; set; }
    }
}