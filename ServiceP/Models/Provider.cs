namespace ServiceP.Models
{
    public class Provider : User
    {
        public string brand_name { get; set; } = string.Empty;

        public List<Service> services { get; set; }
    }
}
