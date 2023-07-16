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

namespace Navigation_Menu_App
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public Admin _admin;
        public AdminWindow(Admin admin)
        {
            
            InitializeComponent();
            _admin = admin;
            Read(_admin.AdminId);
             
        }

        public List<Lecturer> DatabaseLecturers { get; private set; }
        public List<Lecturer> GetLecturersByAdmin(int adminId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                Admin admin = context.Admins.FirstOrDefault(a => a.AdminId == adminId);
                //Lecturer lecturer = context.Lecturers.FirstOrDefault(l => l.LecturerId == lecturerId);

                if (admin != null)
                {
                    List<Lecturer> lecturers = admin.Lecturers?.ToList() ?? new List<Lecturer>();
                    //List<Student> students = lecturer.Students?.ToList() ?? new List<Student>();
                    return lecturers;
                    //return students;
                }
                else
                {
                    // Handle the case when lecturer is null
                    return new List<Lecturer>();
                }
            }
        }
        public void Create(int adminId)
        {

            using (DatabaseContext context = new DatabaseContext())
            {
                Admin admin = context.Admins?.FirstOrDefault(a => a.AdminId == adminId);

                var firstname = FirstNameTextBox.Text;
                var lastname = LastNameTextBox.Text;
                var username = UsernameTextBox.Text;
                var password = PasswordTextBox.Text;

                if (firstname != null && lastname != null && username != null && password != null)
                {
                    context.Lecturers.Add(new Lecturer() { LecturerFirstName = firstname, LecturerLastName = lastname, LecturerUsername = username, LecturerPassword = password, AdminId = adminId, Admin = admin });
                    context.SaveChanges();
                }

            }
        }
        

        public void Read(int adminId)
        {
            using (DatabaseContext context = new DatabaseContext())
            {
                              
                DatabaseLecturers = context.Lecturers.ToList();
                List<Lecturer> filteredLecturers = (from lecturer in DatabaseLecturers
                                                    join admin in context.Admins on lecturer.AdminId equals admin.AdminId
                                                    where admin.AdminId == adminId
                                                    select lecturer).ToList();
                LecturerList.ItemsSource = filteredLecturers;
                
            }

        }

        public void Update(int adminId)
        {


            using (DatabaseContext context = new DatabaseContext())
            {

                Lecturer? selectedLecturer = LecturerList.SelectedItem as Lecturer;

                var firstname = FirstNameTextBox.Text;
                var lastname = LastNameTextBox.Text;
                var username = UsernameTextBox.Text;
                var password = PasswordTextBox.Text;

                if (firstname != null && lastname != null && username != null && password != null)
                {

                    Lecturer? lecturer = context.Lecturers.Find(selectedLecturer.LecturerId);
                    lecturer.LecturerFirstName = firstname;
                    lecturer.LecturerLastName = lastname;
                    lecturer.LecturerUsername = username;
                    lecturer.LecturerPassword = password;
                   
                    context.SaveChanges();
                }

            }



        }

        public void Delete(int adminId)
        {


            using (DatabaseContext context = new DatabaseContext())
            {

                Lecturer? selectedLecturer = LecturerList.SelectedItem as Lecturer;

                if (selectedLecturer != null)
                {
                    Lecturer lecturer = context.Lecturers.Single(x => x.LecturerId == selectedLecturer.LecturerId);

                    context.Remove(lecturer);
                    context.SaveChanges();

                }



            }



        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            Create(_admin.AdminId);
            Read(_admin.AdminId);
        }

        
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            Update(_admin.AdminId);
            Read(_admin.AdminId);
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Delete(_admin.AdminId);
            Read(_admin.AdminId);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            LecturerList.Items.Clear();

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
