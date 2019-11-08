using System.Threading;
using System.Threading.Tasks;
using ProductContext.Domain.Lookup;

namespace ProductContext.Application.Ports.Persistence
{
    public interface IBrandRepository
    {
        Task<Brand> GetById(int id,CancellationToken cancellationToken=default);
        Task Save(Brand entity,CancellationToken cancellationToken=default);
    }
}