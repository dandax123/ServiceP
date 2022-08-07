using System.Text.Json.Serialization;

namespace ServiceP.Models;

public class User
{
    [JsonIgnore]
    public int userId { get; set; }
    public string first_name { get; set; } = string.Empty;

    public string last_name { get; set; } = string.Empty;

    public string email { get; set; } = string.Empty;

    [JsonIgnore]
    public byte[] password_hash { get; set; }

    [JsonIgnore]
    public byte[] password_salt { get; set; }





}
