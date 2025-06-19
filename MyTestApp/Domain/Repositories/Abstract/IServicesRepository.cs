using MyTestApp.Domain.Entities;

namespace MyTestApp.Domain.Repositories.Abstract
{
    public interface IServicesRepository
    {

        Task<IEnumerable<Service>> GetServicesAsync();
        Task<Service?> GetServiceByIdAsync(int id);
        Task SaveServiceAsync(Service entity);
        Task DeleteServiceAsync(int id);
    }
}
