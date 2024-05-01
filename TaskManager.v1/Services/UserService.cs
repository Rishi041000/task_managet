using Dapper;
using System.Data.SqlClient;
using TaskManager.v1.Models.Entities;
using TaskManager.v1.Models.Request;
using TaskManager.v1.Services.IServices;

namespace TaskManager.v1.Services
{
    public class UserService : IUserService
    {
        public IConfiguration _configuration;
        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public User ValidateUser(LogInRequest logIn)
        {
            string sql = $@"select id, firstName, LastName,  Mail from users ";
            sql = sql + $"where mail = '{logIn.Mail}' and password = '{logIn.Password}'";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                return dbConn.Query<User>(sql).FirstOrDefault();
            }
        }

        public bool CreateUser(UserRequest user)
        {
            string result;
            string sql = $@"if exists(select * from Users where Mail = '{user.mail}')
	                            begin
		                            select 'False'
	                            end
                            else
	                            begin
		                            insert into Users values ('{user.LastName}','{user.FirstName}','{user.password}','{user.mail}')
		                            select 'True'
	                            end";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                result = dbConn.Query<string>(sql).FirstOrDefault();
            }
            return result == "True" ? true : false;
        }
    }
}
