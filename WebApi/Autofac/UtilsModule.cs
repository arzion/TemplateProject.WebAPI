using System.Net.Http;
using Autofac;
using Ploeh.Hyprlinkr;
using TemplateProject.WebApi.Infrastracture.Routes;
using TemplateProject.WebApi.Utils;

namespace TemplateProject.WebApi.Autofac
{
    /// <summary>
    /// Autofac module that register all utils dependencies.
    /// </summary>
    public class UtilsModule : Module
    {
        /// <summary>
        /// Override to add registrations to the container.
        /// </summary>
        /// <param name="builder">The builder through which components can be
        /// registered.</param>
        /// <remarks>
        /// Note that the ContainerBuilder parameter is unique to this module.
        /// </remarks>
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<HttpRouteCollectionDispatcher>()
                .As<IRouteDispatcher>();

            builder
                .Register(c => new RouteLinker(
                    c.Resolve<HttpRequestMessage>(),
                    c.Resolve<IRouteDispatcher>()))
                .InstancePerRequest();

            builder
                .RegisterType<RouteLinkerProxy>()
                .As<IUrlHelper>()
                .InstancePerRequest();
        }
    }
}