using Shortly.Domain.Entities;

namespace Shortly.Application.Interfaces;

public interface IUserService
{
    ///<summary>
    /// Register a new User in the system.
    /// The method will check if the email is already registered.
    /// The method will hash the password before save in the db.
    ///</summary> 
    Task<User> Register(User? user);
    
    ///<summary>
    /// Retrieve a List of all the Users registered in the db.
    ///</summary> 
    Task<List<User>> GetAllUsers();

    ///<summary>
    /// Retrieve a User from backend with that email and password.
    ///</summary> 
    Task<User> Login(string email, string password );

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    Task<User> GetUserByEmail(string email);
}