using ServiceP.DTO;
using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IBooking
    {

        Task<IEnumerable<BookingDisplayDto>> getAll();


        Task<IEnumerable<BookingDisplayDto>> getBookingsByCustomer(int customerId);
        Task<BookingDisplayDto> getBookingDetails(int customerId, int id);

        Task<IEnumerable<BookingDisplayDto>> getBookingsByService(int creatorId, int serviceId);

        Task updateBooking(int customerId, int id, int quantity);

        Task deleteBooking(int customerId, int id);


        Task addBooking(int serviceId, int customerId, int quantity);

        Task<IEnumerable<BookingDisplayDto>> getBookingsByProvider(int providerID);

    }
}
