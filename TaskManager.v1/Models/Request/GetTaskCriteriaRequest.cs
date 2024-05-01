namespace TaskManager.v1.Models.Request
{
    public class GetTaskCriteriaRequest
    {
        public string criteria { get; set; }
        public string value { get; set; }
        public string token { get; set; }
        public string sortField { get; set; }
        public string sort { get; set; }
    }
}
