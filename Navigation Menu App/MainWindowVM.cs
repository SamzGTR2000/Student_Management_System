using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows;

namespace Navigation_Menu_App
{
    public partial class MainWindowVM : ObservableObject
    {
        [RelayCommand]
        public void openAdminLogin()
        {
            AdminLogin window = new AdminLogin();
            window.Show();
            Application.Current.MainWindow.Close();
        }

        [RelayCommand]
        public void openUserLogin()
        {
            UserLogin window = new UserLogin();
            window.Show();
            Application.Current.MainWindow.Close();
        }
    }
}
