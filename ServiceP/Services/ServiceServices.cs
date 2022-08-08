using ServiceP.Repository;
using ServiceP.Models;
using ServiceP.DTO;

namespace ServiceP.Services
{
    public class ServiceServices : IService
    {
        DataContext _dataContext;
        IProvider _myProvider;
        public ServiceServices(DataContext dataContext, IProvider a)
        {
            _dataContext = dataContext;
            _myProvider = a;
        }
        public async Task createService(int creatorId, ServiceDto service)
        {
            if (service.type != "Hour" || service.type != "Quantity")
            {
                //handle later
            }

            Provider creator = await _myProvider.getById(creatorId);
            Service newservice = new Service
            {

                service_name = service.name,
                service_type = service.type,
                bookings = new List<Booking>(),
                description = service.description,
                creator = creator

            };
            await _dataContext.Services.AddAsync(newservice);
            await _dataContext.SaveChangesAsync();
        }

        public async Task deleteService(int service_id)
        {

        }

        public async Task<IEnumerable<Service>> getAll()
        {
            return await _dataContext.Services.ToListAsync();
        }

        public async Task<Provider> getProvider(int serviceId)
        {
            Service y = await GetService(serviceId);

            return y.creator;

        }

        public async Task<Service> GetService(int service_id)
        {
            Service? foundService = await _dataContext.Services.FindAsync(service_id);
            if (foundService == null)
            {
                throw new NotImplementedException();

                //handle
            }
            return foundService;
        }

        public async Task<IEnumerable<Service>> getServicesByUser(int userId)
        {
            var services = await _dataContext.Services.Where(y => y.creator.userId == userId).ToListAsync();
            return services;
        }

        public Task updateServiceDetails(ServiceDto service)
        {
            throw new NotImplementedException();
        }



    }
}
