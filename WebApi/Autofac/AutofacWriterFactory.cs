using Autofac;
using TemplateProject.DataAccess;
using TemplateProject.DomainModel;

namespace TemplateProject.WebApi.Autofac
{
    /// <summary>
    /// The factory that creates writers through the Autofac container.
    /// </summary>
    /// <seealso cref="TemplateProject.DataAccess.IWriterFactory" />
    public class AutofacWriterFactory : IWriterFactory
    {
        private readonly IComponentContext _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacWriterFactory"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public AutofacWriterFactory(IComponentContext container)
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