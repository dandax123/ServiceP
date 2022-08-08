using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services.main
{
    public class BookingService : IBooking
    {
        DataContext _dbcontext;
        IService _myService;
        ICustomer _myCustomer;

        public BookingService(DataContext context, IService service, ICustomer customer)
        {
            _dbcontext = context;
            _myService = service;
            _myCustomer = customer;
        }
        public async Task addBooking(int serviceId, int customerId,   int quantity)
        {
            Service service = await _myService.GetService(serviceId);
            Customer customer = await _myCustomer.getById(customerId);
            Booking newBooking = new Booking {
                service = service,
                customer  = customer,
                quantity = quantity

            };
            await _dbcontext.Bookings.AddAsync(newBooking);
            await _dbcontext.SaveChangesAsync();
        }

    
        public void createBooking(Booking booking)
        {
            throw new NotImplementedException();
        }

        public void deleteBooking(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Booking> get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Booking>> getAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Booking>> getBookingsByCustomer(int customerId)
        {
            var bookings = await _dbcontext.Bookings.Where(y => y.customer.userId == customerId).ToListAsync();
            return bookings;
        }

        public void updateBooking(Booking booking)
        {
            throw new NotImplementedException();
        }
    }
}
