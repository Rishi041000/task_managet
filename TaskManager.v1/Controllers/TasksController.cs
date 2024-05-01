using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.v1.Models.Entities;
using TaskManager.v1.Models.Request;
using TaskManager.v1.Services.IServices;

namespace TaskManager.v1.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        public ISessionService _sessionService;
        public ITaskService _taskService;
        public TasksController(ISessionService sessionService, ITaskService taskService)
        {
            _sessionService = sessionService;
            _taskService = taskService;
        }
        [HttpPost]
        [Route("Create")]
        public dynamic Create(TaskRequest task)
        {
            try
            {
                if (string.IsNullOrEmpty(task.token))
                {
                    return new
                    {
                        code = 500,
                        message = "invalid request, token is missing"
                    };
                }
                var user = _sessionService.ValidateToken(task.token);
                task.CreatedBy = user.Id;
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                _taskService.CreateTask(task);
                return new
                {
                    code = 200,
                    message = "Task created"
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
        [Route("Update")]
        public dynamic Update(TaskRequest task)
        {
            try
            {
                if (string.IsNullOrEmpty(task.token))
                {
                    return new
                    {
                        code = 500,
                        message = "invalid request, token is missing"
                    };
                }
                var user = _sessionService.ValidateToken(task.token);
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                _taskService.updateTask(task);
                return new
                {
                    code = 200,
                    message = "Task updated"
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
        [Route("Delete")]
        public dynamic Delete(DeleteTaskRequest request)
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
                var user = _sessionService.ValidateToken(request.token);
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                _taskService.DeleteTask(request.id);
                return new
                {
                    code = 200,
                    message = "Task Deleted"
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
        [Route("Id")]
        public dynamic GetById(GetTaskRequest request)
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
                var user = _sessionService.ValidateToken(request.token);
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                var task = _taskService.getTask(request.id);
                return task;
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
        public dynamic GetAll(Request request)
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
                var user = _sessionService.ValidateToken(request.token);
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                var task = _taskService.getAll(user.Id);
                return task;
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
        [Route("search")]
        public dynamic getByCriteria(GetTaskCriteriaRequest request)
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
                var user = _sessionService.ValidateToken(request.token);
                if (user == null)
                {
                    return new
                    {
                        code = "501-1",
                        message = "invalid token or session expired"
                    };
                }
                var task = _taskService.getByCriteria(user.Id, request.criteria, request.value, request.sortField, request.sort);
                return task;
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

        [HttpGet]
        [Route("demoapi")]
        public dynamic demoAPi()
        {
            return new
            {
                code = 200
            };
        }
    }
}
