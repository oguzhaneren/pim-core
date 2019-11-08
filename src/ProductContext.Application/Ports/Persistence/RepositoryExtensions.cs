using System;
using System.Threading;
using System.Threading.Tasks;
using ProductContext.Application.Exceptions;
using ProductContext.Domain.Lookup;
using Attribute = ProductContext.Domain.Lookup.Attribute;

namespace ProductContext.Application.Ports.Persistence
{
    public static class RepositoryExtensions
    {
        public static Task<Category> Load(this ICategoryRepository repository, int id,CancellationToken cancellationToken=default)
        {
            return Load(x=>repository.GetById(x,cancellationToken), id, cancellationToken);
        }
        
        public static Task<BusinessUnit> Load(this IBusinessUnitRepository repository, int id,CancellationToken cancellationToken=default)
        {
            return Load(x=>repository.GetById(x,cancellationToken), id, cancellationToken);
        }
        
        public static Task<Attribute> Load(this IAttributeRepository repository, int id,CancellationToken cancellationToken=default)
        {
            return Load(x=>repository.GetById(x,cancellationToken), id, cancellationToken);
        }
        public static Task<Brand> Load(this IBrandRepository repository, int id,CancellationToken cancellationToken=default)
        {
            return Load(x=>repository.GetById(x,cancellationToken), id, cancellationToken);
        }
        
        private static async Task<TEntity> Load<TId, TEntity>(Func<TId, Task<TEntity>> getById, TId id,CancellationToken cancellationToken=default)
        {
            var entity = await getById(id);
            if (entity == null)
            {
                throw new EntityNotFoundException<TEntity>(id);
            }

            return entity;
        }
    }
}