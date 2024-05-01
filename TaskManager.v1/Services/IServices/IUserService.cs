using TaskManager.v1.Models.Entities;
using TaskManager.v1.Models.Request;

namespace TaskManager.v1.Services.IServices
{
    public interface IUserService
    {
        User ValidateUser(LogInRequest logIn);
        bool CreateUser(UserRequest user);
    }
}
