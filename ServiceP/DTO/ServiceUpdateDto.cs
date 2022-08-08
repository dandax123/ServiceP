using System.ComponentModel.DataAnnotations;

namespace ServiceP.DTO
{
    public class ServiceUpdateDto
    {
        [Required]
        [MaxLength(300)]
        public string description { get; set; }

        [Required]
        public string name { get; set; }

        [Required]
        public string type { get; set; }

    }
}
