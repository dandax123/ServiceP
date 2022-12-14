using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ServiceP.Models;

public class User
{

    public int userId { get; set; }
    public string first_name { get; set; } = string.Empty;

    public string last_name { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    public string role { get; set; } = "Customer";

    [JsonIgnore]
    public byte[] password_hash { get; set; }

    [JsonIgnore]
    public byte[] password_salt { get; set; }


    public List<Booking> bookings { get; set; }


    public string brand_name { get; set; } = string.Empty;

    public List<Service> services { get; set; }




}
