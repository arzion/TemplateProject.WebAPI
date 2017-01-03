using System.Collections.Generic;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.StaticStorage
{
    public static class Storage<TEntity> where TEntity : Entity
    {
        public static IList<TEntity> Entities { get; set; }

        static Storage()
        {
            Entities = new List<TEntity>();
        }
    }
}