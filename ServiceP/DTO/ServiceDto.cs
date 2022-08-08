using System.ComponentModel.DataAnnotations;
using ServiceP.Models;
namespace ServiceP.DTO
{
    public class ServiceBaseDto
    {
        [Required]
        [MaxLength(300)]
        public string description { get; set; }



        [Required]
        public string name { get; set; }

        [Required]
        public string service_type { get; set; }
        public static ServiceResponseDto Service2ServiceResponseDto(Service a)
        {
            return new ServiceResponseDto { service_id = a.serviceId, creator = a.creator.first_name + " " + a.creator.last_name, service_type = a.service_type, name = a.service_name, description = a.description };
        }

        public static ServiceDto Service2ServiceDto(Service a)
        {
            return new ServiceDto { service_id = a.serviceId, service_type = a.service_type, name = a.service_name, description = a.description };
        }

        public static ServiceBaseDto Service2ServiceBaseDto(Service a)
        {
            return new ServiceBaseDto {  service_type = a.service_type, name = a.service_name, description = a.description };
        }

    }
    public class ServiceResponseDto: ServiceBaseDto
    {
        public string creator { get; set; }
        public int service_id { get; set; }
    }

    public class ServiceDto: ServiceBaseDto
    {
        public int service_id { get; set; }
    }
}
