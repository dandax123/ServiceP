using ServiceP.DTO;
using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IBooking
    {

        Task<IEnumerable<Booking>> getAll();


        Task<IEnumerable<Booking>> getBookingsByCustomer(int customerId);
        Task<Booking> getBookingDetails(int customerId, int id);



        Task updateBooking(int customerId, int id, int quantity);

        Task deleteBooking(int customerId, int id);


        Task addBooking(int serviceId, int customerId, int quantity);

    }
}
