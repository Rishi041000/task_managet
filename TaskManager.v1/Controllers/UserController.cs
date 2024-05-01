using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.v1.Models.Request;
using TaskManager.v1.Services.IServices;

namespace TaskManager.v1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserService _userService;
        public ISessionService _sessionService;
        public UserController(IUserService userService, ISessionService sessionService)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("Login")]
        public dynamic Login(LogInRequest login)
        {
            try
            {
                var user = _userService.ValidateUser(login);
                if (user == null)
                {
                    return new
                    {
                        code = "500",
                        message = "invalid username/Password"
                    };
                }
                var token = _sessionService.CreateToken(user);
                return new
                {
                    code = 200,
                    message = "Login successfull",
                    token = token
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    code = 500,
                    message = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Create")]
        public dynamic Create(UserRequest user)
        {
            try
            {
                var status = _userService.CreateUser(user);
                if (status == true)
                {
                    return new
                    {
                        code = 200,
                        message = "User Created",
                    };
                }
                else
                {
                    return new
                    {
                        code = 500,
                        message = "provided mail already in use",
                    };
                }
            }
            catch (Exception ex)
            {
                return new
                {
                    code = 500,
                    message = ex.Message,
                };
            }
        }

        [HttpPost]
        [Route("Logout")]
        public dynamic Logout(Request request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.token))
                {
                    return new
                    {
                        code = 500,
                        message = "invalid request, token is missing"
                    };
                }
                _sessionService.EndToken(request.token);
                return new
                {
                    code = 200,
                    message = "log out successful"
                };
            }
            catch (Exception ex)
            {
                return new
                {
                    code = 500,
                    message = ex.Message,
                };
            }
        }
    }
}
