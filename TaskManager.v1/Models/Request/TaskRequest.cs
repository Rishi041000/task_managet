﻿namespace TaskManager.v1.Models.Request
{
    public class TaskRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public int CreatedBy { get; set; }
        public DateTime DueDate { get; set; }
        public string token { get; set; }
    }
}
