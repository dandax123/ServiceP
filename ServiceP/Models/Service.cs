using System.ComponentModel.DataAnnotations;

namespace ServiceP.Models
{
    public class Service
    {
        [Key]
        [Required]
        public int serviceId { get; set; }

        [Required]
        [MaxLength(50)]
        public string service_name { get; set; }

        [Required]

        [MaxLength(300)]
        public string description { get; set; }


        [Required]
        [MaxLength(15)]
        public string service_type { get; set; }




        public User creator { get; set; }

        public List<Booking> bookings { get; set; }
    }
}
