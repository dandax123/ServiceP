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
        IUser _myUserService;

        public BookingService(DataContext context, IService service, IUser myUserService)
        {
            _dbcontext = context;
            _myService = service;
            _myUserService = myUserService;
        }
        public async Task addBooking(int serviceId, int customerId, int quantity)
        {

            if(quantity <= 0)
            {
                throw new AppException("You have to book at least 1 unit");
            }
            Service service = await _myService.GetService(serviceId);
            User customer = await _myUserService.GetById(customerId);
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

            Booking booking = await getBooking(customerId, id);
            _dbcontext.Bookings.Remove(booking);
            await _dbcontext.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookingDisplayDto>> getBookingsByService(int creatorId, int serviceId)
        {
            var bookings = await _dbcontext.Bookings.Include(y => y.customer).Where(y => y.service.serviceId == serviceId && y.service.creator.userId == creatorId).ToListAsync();
            return bookings.ConvertAll(new Converter<Booking, BookingDisplayDto>(BookingDto.Booking2BookingDisplayDto));
            
        }

        public async Task<BookingDisplayDto> getBookingDetails(int customerId, int id)
        {
            Booking a = await getBooking(customerId, id);
            return BookingDto.Booking2BookingDisplayDto(a);
        }

        public async Task<Booking> getBooking(int customerId, int id)
        {
            Booking? booking = await _dbcontext.Bookings.Include(y => y.customer).Where(y => y.bookingId == id && y.customer.userId == customerId).FirstAsync();
            if (booking == null)
            {
                throw new MissingException("Booking Not found");
            }
            return booking;
        }

        public async Task<IEnumerable<BookingDisplayDto>> getBookingsByCustomer(int customerId)
        {
            var bookings = await _dbcontext.Bookings.Where(y => y.customer.userId == customerId).ToListAsync();
            return bookings.ConvertAll(new Converter<Booking, BookingDisplayDto>(BookingDto.Booking2BookingDisplayDto));
        }

        public async Task updateBooking(int customerId, int id, int quantity)
        {
            Booking booking = await getBooking(customerId, id);
            booking.quantity = quantity;
            await _dbcontext.SaveChangesAsync();
        }

        public async  Task<IEnumerable<BookingDisplayDto>> getAll()
        {
            var bookings = await _dbcontext.Bookings.ToListAsync();
            return bookings.ConvertAll(new Converter<Booking, BookingDisplayDto>(BookingDto.Booking2BookingDisplayDto));
        }
    }
}
