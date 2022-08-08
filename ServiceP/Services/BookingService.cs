using ServiceP.DTO;
using ServiceP.Helper;
using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services
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
        public async Task addBooking(int serviceId, int customerId, int quantity)
        {
            Service service = await _myService.GetService(serviceId);
            Customer customer = await _myCustomer.getById(customerId);
            Booking newBooking = new Booking
            {
                service = service,
                customer = customer,
                quantity = quantity

            };
            await _dbcontext.Bookings.AddAsync(newBooking);
            await _dbcontext.SaveChangesAsync();
        }



        public async Task deleteBooking(int customerId, int id)
        {

            Booking booking = await getBookingDetails(customerId, id);
            _dbcontext.Bookings.Remove(booking);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Booking>> getBookingsByService(int creatorId, int serviceId)
        {
            var bookings = await _dbcontext.Bookings.Where(y => y.service.serviceId == serviceId && y.service.creator.userId == creatorId).ToListAsync();
            return bookings;
        }

        public async Task<Booking> getBookingDetails(int customerId, int id)
        {
            Booking? booking = await _dbcontext.Bookings.Where(y => y.bookingId == id && y.customer.userId == customerId).FirstAsync();
            if(booking == null)
            {
                throw new MissingException("Booking Not found");
            }
            return booking;
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

        public async Task updateBooking(int customerId, int id, int quantity)
        {
            Booking booking = await getBookingDetails(customerId, id);
            booking.quantity = quantity;
            await _dbcontext.SaveChangesAsync();
        }
    }
}
