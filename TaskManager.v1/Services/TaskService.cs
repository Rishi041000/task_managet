using Dapper;
using System.Data.SqlClient;
using TaskManager.v1.Models.Entities;
using TaskManager.v1.Models.Request;
using TaskManager.v1.Services.IServices;

namespace TaskManager.v1.Services
{
    public class TaskService : ITaskService
    {
        public IConfiguration _configuration;
        public TaskService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateTask(TaskRequest task)
        {
            string sql = $"insert into Task values ('{task.Title}', '{task.Description}','{task.Status}'," +
                $" '{task.Priority}',getdate(),{task.CreatedBy},'{task.DueDate}',getdate())";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                dbConn.Query(sql);
            }
        }

        public void updateTask(TaskRequest task)
        {
            string sql = $"update Task set Title = '{task.Title}', Description= '{task.Description}',Status= '{task.Status}',Priority= '{task.Priority}'" +
                $",ModifiedDate = GETDATE(), DueDate = '{task.DueDate}' where ID  = {task.Id}";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                dbConn.Query(sql);
            }
        }

        public void DeleteTask(int id)
        {
            string sql = $"delete from Task where ID  = {id}";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                dbConn.Query(sql).FirstOrDefault();
            }
        }

        public Tasks getTask(int id)
        {
            string sql = $"select * from Task where ID  = {id}";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                return dbConn.Query<Tasks>(sql).FirstOrDefault();
            }
        }

        public List<Tasks> getAll(int id)
        {
            string sql = $"select id, CASE    WHEN len(Title) < 10  THEN Title   ELSE SUBSTRING(Title,0,20) + '...'  END Title,  Description ,Status ,Priority ,ModifiedDate ,CreatedBy ,DueDate ,createdDate  from Task where createdBy = {id}";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                return dbConn.Query<Tasks>(sql).ToList();
            }
        }

        public List<Tasks> getByCriteria(int id, string criteria, string value, string orderBy, string order)
        {
            string sql = $"select id, CASE    WHEN len(Title) < 10  THEN Title   ELSE SUBSTRING(Title,0,20) + '...'  END Title,  Description ,Status ,Priority ,ModifiedDate ,CreatedBy ,DueDate ,createdDate  from Task where createdBy = {id} and ";
            if(criteria == "Number")
            {
                sql += $" ID = {value}  ";
            }
            if (criteria == "Title")
            {
                sql += $" Title like '%{value}%'";
            }
            if (criteria == "Priority")
            {
                sql += $"Priority = '{value}'";
            }
            if (criteria == "Status")
            {
                sql += $"Status = '{value}'";
            }
            if (criteria == "createdDate")
            {
                var dates = value.Split("___").ToList();
                sql += $"createdDate >= '{dates[0]}' and createdDate <= '{dates[1]}'";
            }

            sql = sql + $" order by {orderBy} {order} ";
            using (var dbConn = new SqlConnection(_configuration.GetConnectionString("Default")))
            {
                return dbConn.Query<Tasks>(sql).ToList();
            }
        }
    }
}
