using System.Threading;
using System.Threading.Tasks;
using ProductContext.Domain.Lookup;

namespace ProductContext.Application.Ports.Persistence
{
    public interface IBusinessUnitRepository
    {
        Task<BusinessUnit> GetById(int id,CancellationToken cancellationToken=default);
        Task Save(BusinessUnit entity,CancellationToken cancellationToken=default);
    }
}