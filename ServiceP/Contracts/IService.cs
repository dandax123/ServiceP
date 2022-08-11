using ServiceP.DTO;
using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IService
    {
        Task<IEnumerable<ServiceResponseDto>> getAll();


        Task<IEnumerable<ServiceDto>> getServicesByUser(int userId);
        Task<ServiceResponseDto> GetServiceDetails(int service_id);
        Task<Service> GetService(int service_id);

        Task<Service> GetService(int creatorId, int service_id);
        Task deleteService(int creatorId, int service_id);

        Task createService(int creatorId, ServiceBaseDto service);

        Task updateServiceDetails(int creatorId, int service_id, ServiceBaseDto service_request);

        Task<User> getProvider(int serviceId);

    }
}
