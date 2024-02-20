namespace SDPJob.Models
{
    public class TaskModel
    {
        public int TaskID { get; set; }
        public DateTime DueDate { get; set; }
        public int ProjectID { get; set; }
        public int Assignee { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
