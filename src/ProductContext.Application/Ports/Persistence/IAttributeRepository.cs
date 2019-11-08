using System.Threading;
using System.Threading.Tasks;
using ProductContext.Domain.Lookup;

namespace ProductContext.Application.Ports.Persistence
{
    public interface IAttributeRepository
    {
        Task<Attribute> GetById(int id,CancellationToken cancellationToken=default);
        Task Save(Attribute entity,CancellationToken cancellationToken=default);
    }
}