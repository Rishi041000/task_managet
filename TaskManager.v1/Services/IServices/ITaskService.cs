using TaskManager.v1.Models.Entities;
using TaskManager.v1.Models.Request;

namespace TaskManager.v1.Services.IServices
{
    public interface ITaskService
    {
        void CreateTask(TaskRequest task);
        void updateTask(TaskRequest task);
        void DeleteTask(int id);
        Tasks getTask(int id);
        List<Tasks> getAll(int id);
        List<Tasks> getByCriteria(int id, string criteria, string value, string orderBy, string order);
    }
}
