using HelperMovie.Core;
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

namespace HelperMovie
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void Button_Back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Hide();
        }

        private void Button_Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = log.Text.Trim();
            string password = pass.Password.Trim();

            if (login.Length < 5 || string.IsNullOrEmpty(login))
            {
                log.ToolTip = "Incorrect or empty!";
                log.Background = Brushes.IndianRed;
                return;
            }
            else if (string.IsNullOrEmpty(password) || password.Length < 3)
            {
                pass.ToolTip = "Incorrect or empty!";
                pass.Background = Brushes.IndianRed;
                return;
            }
            else
            {
                log.ToolTip = "Ok!";
                log.Background = Brushes.Green;
                pass.ToolTip = "Ok!";
                pass.Background = Brushes.Green;

                User authUser = null;
                using(ApplicationContext db = new ApplicationContext())
                {
                    authUser = db.Users.Where(user => user.Login == login && user.Pass == password).FirstOrDefault();
                }

                if (authUser != null)
                {
                    MessageBox.Show("You have entered your account!");
                    UserPageWindow userPageWindow = new UserPageWindow(authUser);
                    userPageWindow.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Something wrong!");
                }
                
            }
        }
    }
}
