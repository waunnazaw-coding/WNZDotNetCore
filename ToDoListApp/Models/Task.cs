namespace ToDoListApp.Models
{
    public class Task
    {

        public int TaskId { get; set; }

        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public int ? CategoryId { get; set; }

        public string PriorityLevel { get; set; }

        public string Status { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime CompletedDate { get; set; }
    }
}
