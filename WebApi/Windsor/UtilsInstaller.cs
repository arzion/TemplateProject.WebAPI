using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Ploeh.Hyprlinkr;
using TemplateProject.WebApi.Infrastracture.Routes;
using TemplateProject.WebApi.Utils;

namespace TemplateProject.WebApi.Windsor
{
    /// <summary>
    /// Autofac module that register all utils dependencies.
    /// </summary>
    public class UtilsInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="container">The container.</param>
        /// <param name="store">The configuration store.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container
                .Register(Component
                    .For(typeof(IRouteDispatcher))
                    .ImplementedBy(typeof(HttpRouteCollectionDispatcher))
                    .LifestylePerWebRequest());

            container
                .Register(Component
                    .For(typeof(IUrlHelper))
                    .ImplementedBy(typeof(RouteLinkerProxy))
                    .LifestylePerWebRequest());

            container.Register(Component
                .For<RouteLinker, IResourceLinker>()
                .LifestylePerWebRequest());
        }
    }
}