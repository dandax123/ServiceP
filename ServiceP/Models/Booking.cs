using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceP.Models
{
    public class Booking
    {
        public int bookingId { get; set; }

        public Service service { get; set; }

        public Customer customer { get; set; }
        public int quantity { get; set; }


    }
}
