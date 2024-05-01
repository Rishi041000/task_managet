using Dapper;
using System.Data.SqlClient;
using TaskManager.v1.Models.Entities;
using TaskManager.v1.Services.IServices;

namespace TaskManager.v1.Services
{
    public class SessionService : ISessionService
    {
        public IConfiguration _configuration;
        public SessionService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateToken(User user)
        {
            var sessionID = System.Guid.NewGuid().ToString() + "-" + System.Guid.NewGuid().ToString() + "-" + System.Guid.NewGuid().ToString() + "-" + System.Guid.NewGuid().ToString();
            sessionID = sessionID.Replace("-", "");
            string sql = $@"insert into Sessions values({user.Id},'{sessionID}',DATEADD(MINUTE, 5, getdate()))";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                dbConn.Query(sql);
            }
            return sessionID;
        }

        public User ValidateToken(string sessionId)
        {
            string sql = $"select id, firstname, lastname, mail from users where " +
                $" id = (select top 1 userid from sessions where sessionid = '{sessionId}' " +
                $" and getdate() < timeout ); ";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                return dbConn.Query<User>(sql).FirstOrDefault();
            }
        }

        public void EndToken(string sessionId)
        {
            string sql = $@"update sessions set timeout = getdate() where sessionID = '{sessionId}'";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                dbConn.Query(sql);
            }
        }
    }
}
