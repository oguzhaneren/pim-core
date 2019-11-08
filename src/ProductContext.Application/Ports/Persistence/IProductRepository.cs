using System.Threading;
using System.Threading.Tasks;
using ProductContext.Domain;

namespace ProductContext.Application.Ports.Persistence
{
    public interface IProductRepository
    {
        Task<Product> GetById(long productId,CancellationToken cancellationToken=default);
        Task Save(Product product,CancellationToken cancellationToken=default);
    }
}