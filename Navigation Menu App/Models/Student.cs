using System.Collections.Generic;


namespace Navigation_Menu_App.Models
{
    public class Student
    {
        

        public int StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public List<Module> Modules { get; set; }
        public double GPA { get; set; }

        public int LecturerId { get; set; } // Foreign Key Property
        public Lecturer Lecturer { get; set; } // Navigation Property
    }
}