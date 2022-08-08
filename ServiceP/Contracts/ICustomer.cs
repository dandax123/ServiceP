using ServiceP.Models;

namespace ServiceP.Repository

{
    public interface ICustomer
    {
        Task<Customer> getById(int id);
        Task<Customer> getByEmail(string email);

        Task createCustomer(Customer user);


    }
}
