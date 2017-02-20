using System;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Resolvers;

namespace TemplateProject.WebApi.Windsor
{
    /// <summary>
    /// The dependency resolver that propagate the inline dependencies by default.
    /// </summary>
    /// <seealso cref="Castle.MicroKernel.Resolvers.DefaultDependencyResolver" />
    public class InlineDependenciesPropagatingDependencyResolver : DefaultDependencyResolver
    {
        /// <summary>
        /// This method rebuild the context for the parameter type. Naive implementation.
        /// </summary>
        protected override CreationContext RebuildContextForParameter(
            CreationContext current,
            Type parameterType)
        {
            if (parameterType.ContainsGenericParameters)
            {
                return current;
            }

            return new CreationContext(parameterType, current, true);
        }
    }
}