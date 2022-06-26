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
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        public UserPageWindow()
        {
            InitializeComponent();

            ApplicationContext db = new ApplicationContext();
            List<User> users = db.Users.ToList();
            

        }

        private void TechBase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Projects_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Team_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Tasks_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
