using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Context;
using Castle.MicroKernel.Resolvers;

namespace TemplateProject.WebAPI.Utils
{
    public class InlineDependenciesPropagatingDependencyResolver :
    DefaultDependencyResolver
    {
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