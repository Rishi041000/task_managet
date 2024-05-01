using TaskManager.v1.Models.Entities;

namespace TaskManager.v1.Services.IServices
{
    public interface ISessionService
    {
        string CreateToken(User user);
        User ValidateToken(string sessionId);
        void EndToken(string sessionId);
    }
}
