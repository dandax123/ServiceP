using ServiceP.DTO;
using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IService
    {
        Task<IEnumerable<Service>> getAll();


        Task<IEnumerable<Service>> getServicesByUser(int userId);
        Task<Service> GetService(int service_id);

        Task deleteService(int service_id);

        Task createService(int creatorId,  ServiceUpdateDto service);

        Task updateServiceDetails(ServiceUpdateDto service);
    }
}
