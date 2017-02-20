using Castle.MicroKernel;
using TemplateProject.DataAccess;
using TemplateProject.DomainModel;

namespace TemplateProject.WebApi.Windsor
{
    /// <summary>
    /// The factory that creates writers through the Windsor container.
    /// </summary>
    /// <seealso cref="TemplateProject.DataAccess.IWriterFactory" />
    public class WindsorWriterFactory : IWriterFactory
    {
        private readonly IKernel _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorWriterFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorWriterFactory(IKernel container)
        {
            _container = container;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>
        /// The writer of particular entity.
        /// </returns>
        public IWriter Create(Entity entity)
        {
            return _container.Resolve(typeof(IWriter<>).MakeGenericType(entity.GetType())) as IWriter;
        }
    }
}