using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IBooking
    {

        Task<IEnumerable<Booking>> getAll();


        Task<IEnumerable<Booking>> getBookingsByCustomer(int customerId);
        Task<Booking> get(int id);



        void updateBooking(Booking booking);

        void deleteBooking(int id);


        Task addBooking(int serviceId, int customerId, int quantity);

    }
}
