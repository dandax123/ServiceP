
using ServiceP.Models;

namespace ServiceP.Data;

public class DataContext: DbContext
{
      public DataContext(DbContextOptions<DataContext> options) : base(options){}

      public DbSet<User> Users { get; set; }

      public DbSet<Booking> Bookings { get; set; }

      public DbSet<Service> Services { get; set; }

       public DbSet<Provider> Providers { get; set; }
    public DbSet<Customer> Customers { get; set; }

}
