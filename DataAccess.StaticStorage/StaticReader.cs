using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TemplateProject.DomainModel;

namespace TemplateProject.DataAccess.StaticStorage
{
    /// <summary>
    /// The reader of the Domain Entity.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="TemplateProject.DataAccess.IReader{TEntity}" />
    public class StaticReader<TEntity> : IReader<TEntity> where TEntity : Entity
    {
        public Task<TEntity> FindAsync(int id)
        {
            var found = Storage<TEntity>.Entities.FirstOrDefault(it => it.Id == id);
            return Task.FromResult(found);
        }

        public Task<List<TEntity>> FindAllAsync(PagingArgs pagingArgs = null)
        {
            var entities = Storage<TEntity>.Entities.OrderBy(it => it.Id);
            return Task.FromResult(entities.ToList());
        }

        public Task<List<TEntity>> FindByCriteriaAsync(Expression<Func<TEntity, bool>> criteria, PagingArgs pagingArgs = null)
        {
            var queried = Storage<TEntity>.Entities.Where(it => criteria.Compile()(it)).ToList();
            return Task.FromResult(queried);
        }
    }
}