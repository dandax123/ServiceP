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
        public async Task createService(int creatorId, ServiceBaseDto service)
        {
            string HOUR_TYPE = "hour";
            string QTY_TYPE = "qty";
            //if (!service.service_type.Equals(HOUR_TYPE) || !service.service_type.Equals(QTY_TYPE))
            //{
              //  throw new Exception("Invalid type of service");
            //}

            Provider creator = await _myProvider.getById(creatorId);
            Service newservice = new Service
            {

                service_name = service.name,
                service_type = service.service_type,
                bookings = new List<Booking>(),
                description = service.description,
                creator = creator

            };
            await _dataContext.Services.AddAsync(newservice);
            await _dataContext.SaveChangesAsync();
        }

        public async Task deleteService(int creatorId, int service_id)
        {
            Service service = await GetService(creatorId, service_id);
            _dataContext.Services.Remove(service);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<ServiceResponseDto>> getAll()
        {
            var services =  await _dataContext.Services.Include(y => y.creator).ToListAsync();
            return services.ConvertAll(new Converter<Service, ServiceResponseDto>(ServiceDto.Service2ServiceResponseDto));
        }

        public async Task<Provider> getProvider(int serviceId)
        {
            Service y = await GetService(serviceId);

            return y.creator;

        }

        public async Task<Service> GetService(int service_id)
        {
            Service? foundService = await _dataContext.Services.Include(y => y.creator).FirstOrDefaultAsync( y => y.serviceId == service_id);
            if (foundService == null)
            {
                throw new NotImplementedException();

                //handle
            }
            return foundService;
        }
        public async Task<ServiceResponseDto> GetServiceDetails(int service_id)
        {
            Service foundService = await GetService(service_id);
            
            return ServiceDto.Service2ServiceResponseDto(foundService);
        }

        public async Task<Service> GetService(int creatorId, int service_id)
        {
            Service foundService = await GetService(service_id);
            Console.WriteLine(foundService.creator.userId + " " + creatorId);
            if (!foundService.creator.userId.Equals(creatorId))
            {
                throw new Exception("Service Doesn't exist");
            }
            return foundService;
        }

        public async Task<IEnumerable<ServiceDto>> getServicesByUser(int userId)
        {
            var services = await _dataContext.Services.Where(y => y.creator.userId == userId).ToListAsync();
            return services.ConvertAll(new Converter<Service, ServiceDto>(ServiceDto.Service2ServiceDto));

        }

        public async Task updateServiceDetails(int creatorId, int service_id, ServiceBaseDto service_request)
        {
            Service service = await GetService(creatorId, service_id);
            service.description = service_request.description;
            service.service_name = service_request.name;
            service.service_type = service_request.service_type;
            await _dataContext.SaveChangesAsync();
        }
    }
}
