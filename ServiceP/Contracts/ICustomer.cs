using Microsoft.AspNetCore.Mvc;
using ServiceP.DTO;
using ServiceP.Models;

namespace ServiceP.Repository

{
    public interface ICustomer
    {
        Task<Customer> getById(int id);
        Task<Customer> getByEmail(string email);


        Task<string> RegisterCustomer(BaseRegistrationRequest request);


        Task updateCustomer(int user_id, UserDto a);

        Task deleteCustomer(int user_id);

    }
}
