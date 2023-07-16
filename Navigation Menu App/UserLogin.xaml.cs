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
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    public partial class UserLogin : Window
    {
        public UserLogin()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var username = User_Username.Text;
            var password = User_Password.Password;

            using (DatabaseContext context = new DatabaseContext())
            {
                bool userLog = context.Lecturers.Any(Lecturer => Lecturer.LecturerUsername == username && Lecturer.LecturerPassword == password);

                if (userLog)
                {
                    Lecturer? lecturer = context.Lecturers.FirstOrDefault(Lecturer => Lecturer.LecturerUsername == username);
                    UserWindow userWindow = new UserWindow(lecturer);
                    userWindow.Show();
                    Close();

                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
        }

        private void UserGrantAccess()
        {
            
        }

        private void smallCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow window = new MainWindow();
            window.Show();
        }
    }
}
