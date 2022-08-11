using ServiceP.Models;

namespace ServiceP.DTO
{
    public class BookingDto
    {
        public int serviceId { get; set; }
        public int unit { get; set; }

        public static BookingDisplayDto Booking2BookingDisplayDto(Booking a)
        {
            return new BookingDisplayDto { serviceId = a.service.serviceId, unit = a.quantity, customer = a.customer.first_name + " " + a.customer.last_name , bookingId = a.bookingId };
        }

    }
    public class BookingUpdateDto
    {
        public int unit { get; set; }
    }

    public class BookingDisplayDto: BookingDto
    {
        public string customer { get; set; }

        public int bookingId { get; set; }
    }
}
