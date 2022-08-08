using Microsoft.AspNetCore.Mvc;
using ServiceP.Models;
namespace ServiceP.Repository;

public interface IUser
{
    IEnumerable<User> getAll();
    Task<User> GetById(int userId);


    void DeleteUser(int id);

    User updateUser(User user);
    

}
