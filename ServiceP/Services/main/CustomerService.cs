using ServiceP.Models;
using ServiceP.Repository;

namespace ServiceP.Services.main
{
    public class CustomerService : ICustomer
    {
        DataContext _dataContext;
        public CustomerService(DataContext  data)
        {
            _dataContext = data;

        }
        public async Task createCustomer(Customer user)
        {
            await _dataContext.Customers.AddAsync(user);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Customer> getByEmail(string email)
        {
            Customer? a = await _dataContext.Customers.FirstAsync(y => y.email == email);
            if(a == null)
            {
                throw new Exception("Can't find");
            }
            return a;
        }

        public async Task<Customer> getById(int id)
        {
            Customer? a = await _dataContext.Customers.FindAsync(id);
            if (a == null)
            {
                throw new Exception("Can't find");
            }
            return a;
        }
    }
}
