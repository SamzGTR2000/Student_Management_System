using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Navigation_Menu_App.Models;
using Microsoft.EntityFrameworkCore;

namespace Navigation_Menu_App
{
    /// <summary>
    /// Interaction logic for UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        private Lecturer _lecturer;
        public UserWindow(Lecturer lecturer)
        {
            InitializeComponent();
            _lecturer = lecturer;
            Read(_lecturer.LecturerId);
            
        }

        public List<Student> GetStudentsByLecturer(int lecturerId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Lecturer lecturer = context.Lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);

                if (lecturer != null)
                {
                    List<Student> students = lecturer.Students?.ToList() ?? new List<Student>();
                    return students;
                }
                else
                {
                    // Handle the case when lecturer is null
                    return new List<Student>();
                }
            }
        }

        public List<Student> DatabaseStudents { get; private set; }


        public void Read(int LecId)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                DatabaseStudents = context.Students.ToList();
                List<Student> filteredStudents = (from student in DatabaseStudents
                                                  join lecturer in context.Lecturers on student.LecturerId equals lecturer.LecturerId
                                                  where lecturer.LecturerId == LecId
                                                  select student).ToList();
                StudentList.ItemsSource = filteredStudents;
            }

        }

        public List<Module> DatabaseModules { get; private set; }
        public void ReadModules()
        {
            Student? selectedStudent = StudentList.SelectedItem as Student;
            using(DatabaseContext context = new DatabaseContext())
            {
                DatabaseModules = context.Modules.ToList();
                List<Module> filteredModules = (from module in DatabaseModules
                                                join student in context.Students on module.StudentId equals student.StudentId
                                                where student.StudentId == selectedStudent.StudentId
                                                select module).ToList();
                ModuleList.ItemsSource = filteredModules;
            }
        }
        public double CalcGPA()
        {
            
            Student? selectedStudent = StudentList.SelectedItem as Student;
            using(DatabaseContext context = new DatabaseContext())
            {
                double Gpa;
                int totCredit = 0;
                double gradePoint;
                double totGradePoint = 0;
                DatabaseModules = context.Modules.ToList();
                List<Module> filteredModules = (from module in DatabaseModules
                                                join student in context.Students on module.StudentId equals student.StudentId
                                                where student.StudentId == selectedStudent.StudentId
                                                select module).ToList();
                foreach(Module module in filteredModules)
                {
                    int credit = int.Parse(module.ModuleCode[3].ToString());
                    totCredit = totCredit + credit;

                    

                    switch (module.Grade)
                    {
                        case 'A':
                            gradePoint = 4.00;
                            break;
                        case 'B':
                            gradePoint = 3.00;
                            break;
                        case 'C':
                            gradePoint = 2.00;
                            break;
                        case 'D':
                            gradePoint = 1.00;
                            break;
                        case 'E':
                            gradePoint = 0.00;
                            break;
                        default:
                            gradePoint = 0.00;
                            break;
                    }

                    totGradePoint = totGradePoint + (gradePoint * credit);

                }
                Gpa = totGradePoint / totCredit;
                selectedStudent.GPA = Gpa;
                return Gpa;
            }
        }
        public void Create(int LecId)
        {
            using(DatabaseContext context = new DatabaseContext())
            {
                Lecturer lecturer = context.Lecturers?.FirstOrDefault(l => l.LecturerId == LecId);
                //List<Student> studentList = GetStudentsByLecturer(LecId);

                DatabaseStudents = context.Students.ToList();
                List<Student> filteredStudents = (from student in DatabaseStudents
                                                  join l in context.Lecturers on student.LecturerId equals l.LecturerId
                                                  where l.LecturerId == LecId
                                                  select student).ToList();

                var firstname = FirstNameTextBox.Text;
                var lastname = LastNameTextBox.Text;
                //var gpa = GPATextBox.Text;

                if(firstname != null && lastname != null)
                {
                    Student newStudent = new Student() { StudentFirstName = firstname, StudentLastName = lastname, LecturerId = LecId, Lecturer = lecturer, GPA = 0.00};
                    filteredStudents.Add(newStudent);
                    context.Students.Add(newStudent);
                    context.SaveChanges();
                }
            }
            
        }
        public void CreateModule()
        {
            Student? selectedStudent = StudentList.SelectedItem as Student;
            using (DatabaseContext context = new DatabaseContext())
            {
                DatabaseModules = context.Modules.ToList();
                List<Module> filteredModules = (from module in DatabaseModules
                                                join student in context.Students on module.StudentId equals student.StudentId
                                                where student.StudentId == selectedStudent.StudentId
                                                select module).ToList();
                var moduleCode = ModuleCodeTextBox.Text;
                var moduleName = ModuleNameTextBox.Text;
                var grade = GradeTextBox.Text;
                var modCoordinatorId = ModuleCoordinatorIdTextBox.Text;
                int modCoordId = int.Parse(modCoordinatorId);
                Lecturer? moduleCoordinator = context.Lecturers?.FirstOrDefault(l => l.LecturerId == modCoordId);
                if (moduleCode != null && moduleName != null && grade != null && modCoordinatorId != null)
                {
                    Module newModule = new Module()
                    {
                        ModuleCode = moduleCode,
                        ModuleName = moduleName,
                        Grade = char.Parse(grade),
                        ModuleCoordinator = moduleCoordinator,
                        Student = selectedStudent,
                        StudentId = selectedStudent.StudentId
                    };
                    var freshStudent = context.Students.Find(selectedStudent.StudentId);
                    newModule.Student = freshStudent;
                    newModule.StudentId = freshStudent.StudentId;

                    filteredModules.Add(newModule);
                    context.Modules.Add(newModule);
                    context.SaveChanges();
                }
            }
        }
        public void Update(int LecId)
        {
            //List<Student> studentList = GetStudentsByLecturer(LecId);

            using (DatabaseContext context = new DatabaseContext())
            {

                Student? selectedStudent = StudentList.SelectedItem as Student;

                var firstname = FirstNameTextBox.Text;
                var lastname = LastNameTextBox.Text;
                //var gpa = GPATextBox.Text;

                if (firstname != null && lastname != null)
                {
                    Student? student = context.Students.Find(selectedStudent.StudentId);
                    student.StudentFirstName = firstname;
                    student.StudentLastName = lastname;
                    student.GPA = 0.00;

                    context.SaveChanges();                
                }

            }
        }
        public void EditModule()
        {
            Module? selectedModule = ModuleList.SelectedItem as Module;
            using(DatabaseContext context = new DatabaseContext())
            {
                var moduleCode = ModuleCodeTextBox.Text;
                var moduleName = ModuleNameTextBox.Text;
                var grade = GradeTextBox.Text;
                var modCoordinatorId = ModuleCoordinatorIdTextBox.Text;
                int modCoordId = int.Parse(modCoordinatorId);
                Lecturer? moduleCoordinator = context.Lecturers?.FirstOrDefault(l => l.LecturerId == modCoordId);
                if (selectedModule != null && moduleCode != null && moduleName != null && grade != null && modCoordinatorId != null)
                {
                    Module? module = context.Modules.Find(selectedModule.ModuleId);
                    module.ModuleCode = moduleCode;
                    module.ModuleName = moduleName;
                    module.Grade = char.Parse(grade);
                    module.ModuleCoordinator = moduleCoordinator;

                    context.SaveChanges();
                }
            }
        }

        public void Delete(int LecId)
        {
            //List<Student> studentList = GetStudentsByLecturer(LecId);
            

            using (DatabaseContext context = new DatabaseContext())
            {
                DatabaseStudents = context.Students.ToList();
                List<Student> filteredStudents = (from student in DatabaseStudents
                                                  join l in context.Lecturers on student.LecturerId equals l.LecturerId
                                                  where l.LecturerId == LecId
                                                  select student).ToList();
                Student? selectedStudent = StudentList.SelectedItem as Student;

                if(selectedStudent != null)
                {
                    
                    context.Students.Remove(context.Students.Find(selectedStudent.StudentId));
                    filteredStudents.Remove(selectedStudent);
                    
                    context.SaveChanges();
                }
            }
        }
        public void RemoveModule()
        {
            Student? selectedStudent = StudentList.SelectedItem as Student;
            Module? selectedModule = ModuleList.SelectedItem as Module;
            using(DatabaseContext context = new DatabaseContext())
            {
                if(selectedStudent != null && selectedModule != null)
                {
                    context.Modules.Remove(context.Modules.Find(selectedModule.ModuleId));
                    context.SaveChanges();
                }
            }
        }
        
        /*private void ReadButton_Click(object sender, RoutedEventArgs e)
        {
            Read(_lecturer.LecturerId);
        }*/

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create(_lecturer.LecturerId);
            Read(_lecturer.LecturerId);
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update(_lecturer.LecturerId);
            Read(_lecturer.LecturerId);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete(_lecturer.LecturerId);
            Read(_lecturer.LecturerId);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            StudentList.Items.Clear();

        }

        private void CalcGPAButton_Click(object sender, RoutedEventArgs e)
        {
            double gpa = CalcGPA();
            MessageBox.Show($"GPA is {gpa}");
        }

        private void ShowModulesButton_Click(object sender, RoutedEventArgs e)
        {
            ReadModules();
        }

        private void AddModule_Click(object sender, RoutedEventArgs e)
        {
            CreateModule();
            ReadModules();
        }

        private void EditModule_Click(object sender, RoutedEventArgs e)
        {
            EditModule();
            ReadModules();
        }

        private void RemoveModule_Click(object sender, RoutedEventArgs e)
        {
            RemoveModule();
            ReadModules();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            Close();
            
        }
    }
}
