using ServiceP.Models;
namespace ServiceP.Repository
{
    public interface IBooking
    {

        IEnumerable<Booking> getAll();

        Booking get(int id);

        void updateBooking(Booking booking);

        void deleteBooking(int id);

        void createBooking(Booking booking);
    }
}
