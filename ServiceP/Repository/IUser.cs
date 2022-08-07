using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;
namespace ServiceP.Repository;

public interface IUser
{
    IEnumerable<User> getAll();
    Task<User> GetById(int userId);


    void DeleteUser(int id);


    Task createCustomer(Customer user);
    Task createProvider(Provider user);

    Task<User> GetByEmail(string email);
    User updateUser(User user);
    

}
