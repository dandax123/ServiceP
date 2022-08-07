using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IService
    {
        IEnumerable<Service> getAll();

        Service GetService(int service_id);

        void deleteService(int service_id);

        void createService(Service service);

        void updateServer(Service service);
    }
}
