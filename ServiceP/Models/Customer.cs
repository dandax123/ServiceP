namespace ServiceP.Models;

public class Customer : User
{
    public List<Booking> bookings { get; set; }
}
