using System.ComponentModel;

namespace Task_Management.ViewModels
{
    public class TaskViewModel
    {
        public Guid TaskId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }

        [DisplayName("Student Name")]
        public string StudentName { get; set; }
        public List<StudentList> StudentList { get; set; }

    }

    public class StudentList
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set;}
    }

    //public class TasksDetails
    //{
    //    public string Name { get; set; }
    //    public string Description { get; set; }
    //    public string Status { get; set; }
    //    public string StudentName { get; set; }
    //}

    //public class TasksList
    //{
    //    public List<TasksDetails> Tasks { get; set; }
    //}
}
