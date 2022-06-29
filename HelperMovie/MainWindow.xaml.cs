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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HelperMovie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ApplicationContext db;

        public MainWindow()
        {
            InitializeComponent();
            db = new ApplicationContext();
        }

        private void Button_Reg_Click(object sender, RoutedEventArgs e)
        {
            string login = Login.Text.Trim();
            string name = Name.Text.Trim();
            string position = Position.Text.Trim();
            string password = Password.Password.Trim();
            string password_repeat = Password_Repeat.Password.Trim();
            int age;


            if (login.Length < 5 || string.IsNullOrEmpty(login) || CheckLogin())
            {
                Login.ToolTip = "Incorrect or empty!";
                Login.Background = Brushes.IndianRed;
                return;
            }
            else if (string.IsNullOrEmpty(name))
            {
                Password.ToolTip = "Incorrect or empty!";
                Password.Background = Brushes.IndianRed;
                return;
            }
            else if (string.IsNullOrEmpty(position) || position.Length < 2)
            {
                Password.ToolTip = "Incorrect or empty!";
                Password.Background = Brushes.IndianRed;
                return;
            }
            else if (string.IsNullOrEmpty(password) || password.Length < 3)
            {
                Password.ToolTip = "Incorrect or empty!";
                Password.Background = Brushes.IndianRed;
                return;
            }
            else if (string.IsNullOrEmpty(password_repeat))
            {
                Password_Repeat.ToolTip = "Incorrect or empty!";
                Password_Repeat.Background = Brushes.IndianRed;
                return;
            }
            else if (password != password_repeat)
            {
                MessageBox.Show("Please, check your passwords! " +
                    "They don't match!");
                return;

            }
            else if (!int.TryParse(Age.Text, out age) && age < 14)
            {
                MessageBox.Show("Please, check your age field.");
            }
            else
            {
                Login.ToolTip = "Ok!";
                Login.Background = Brushes.Green;
                Password.ToolTip = "Ok!";
                Password.Background = Brushes.Green;
                Password_Repeat.ToolTip = "Ok!";
                Password_Repeat.Background = Brushes.Green;

                MessageBox.Show("New User has been created!");
                User user = new User(login, password, name, age, position);

                // thread 1
                db.Users.Add(user);
                db.SaveChanges();
                // thread 2
                UserPageWindow userPageWindow = new UserPageWindow(user);
                userPageWindow.Show();
                Hide();
                return;
            };
        }

        private void Button_Auth_Window_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Hide();
        }

        private bool CheckLogin()
        {
            foreach (var user in db.Users)
            {
                if (Login.Text == user.Login)
                {
                    return true;
                }
            }
            return false;
        }
    }
}