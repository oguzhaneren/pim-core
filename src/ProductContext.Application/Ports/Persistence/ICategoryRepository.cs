using System.Threading;
using System.Threading.Tasks;
using ProductContext.Domain.Lookup;

namespace ProductContext.Application.Ports.Persistence
{
    public interface ICategoryRepository
    {
        Task<Category> GetById(int id,CancellationToken cancellationToken=default);
        Task Save(Category entity,CancellationToken cancellationToken=default);
    }
}