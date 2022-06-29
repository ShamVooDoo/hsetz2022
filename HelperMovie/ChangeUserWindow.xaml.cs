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
    /// Логика взаимодействия для ChangeUserWindow.xaml
    /// </summary>
    public partial class ChangeUserWindow : Window
    {
        User SelectedUser { get; set; }
        User CurrentUser { get; set; }
        string UserAge { get; set; }
        string UserPosition { get; set; }
        string UserTeam { get; set; }
        int NowRule { get; set; }

        public ChangeUserWindow(User currentUser, User selectedUser)
        {
            InitializeComponent();
            SelectedUser = selectedUser;
            CurrentUser = currentUser;

            nameTextBox.Text = selectedUser.Name;
            CheckNull();
            NowRule = selectedUser.Admin;

            CheckRule(NowRule);
        }

        private void CheckRule(int rule) // Устанавливает какие права у пользователя в данный момент
        {
            if (rule == 1)
            {
                answerYes.IsChecked = true;
                answerNo.IsChecked = false;
            }
            else
            {
                answerYes.IsChecked = false;
                answerNo.IsChecked = true;
            }
        }

        private void CheckNull()
        {
            if (SelectedUser.Age == null) { ageTextBox.Text = "no specified."; UserAge = "no specified."; }
            else { ageTextBox.Text = SelectedUser.Age.ToString(); UserAge = SelectedUser.Age.ToString(); }

            if (SelectedUser.Position == null) { positionTextBox.Text = "no specified."; UserPosition = "no specified."; }
            else { positionTextBox.Text = SelectedUser.Position; UserPosition = SelectedUser.Position; }

            if (SelectedUser == null) { teamTextBox.Text = "not choosen yet."; UserTeam = "not choosen yet."; }
            else { teamTextBox.Text = SelectedUser.Team; UserTeam = SelectedUser.Team; }
        }

        private void nameTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (nameTextBox.Text == SelectedUser.Name)
            {
                nameTextBox.Clear();
                nameTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void nameTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            nameTextBox.Text = SelectedUser.Name;
            nameTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void ageTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ageTextBox.Text == UserAge)
            {
                ageTextBox.Clear();
                ageTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void ageTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            ageTextBox.Text = UserAge;
            ageTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void positionTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (positionTextBox.Text == UserPosition)
            {
                positionTextBox.Clear();
                positionTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void positionTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            positionTextBox.Text = UserPosition;
            positionTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void teamTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            if (teamTextBox.Text == UserTeam)
            {
                teamTextBox.Clear();
                teamTextBox.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void teamTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            teamTextBox.Text = UserTeam;
            teamTextBox.Foreground = new SolidColorBrush(Colors.DarkGray);
        }

        private void answerYes_Checked(object sender, RoutedEventArgs e)
        {
            if (answerYes.IsChecked == true)
            {
                answerNo.IsChecked = false; ;
                NowRule = 1;
            }
            if (answerNo.IsChecked == true)
            {
                answerYes.IsChecked = false;
                NowRule = 0;
            }
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (nameTextBox.Text != SelectedUser.Name)
            {
                int result;
                if (!int.TryParse(nameTextBox.Text, out result))
                {
                    SelectedUser.Name = nameTextBox.Text;
                }
                else { MessageBox.Show("The data in name is entered incorrectly."); ++count; }
            }
            if (ageTextBox.Text != UserAge)
            {
                int result;
                if (int.TryParse(ageTextBox.Text, out result) && result > 14)
                {
                    SelectedUser.Age = result;
                }
                else { MessageBox.Show("The data in age is entered incorrectly."); ++count; }
            }

            if (positionTextBox.Text != UserPosition)
            {
                int result;
                if (!int.TryParse(positionTextBox.Text, out result))
                {
                    SelectedUser.Position = UserPosition;
                }
                else { MessageBox.Show("The data in position is entered incorrectly."); ++count; }
            }

            if (teamTextBox.Text != UserTeam)
            {
                int result;
                if (!int.TryParse(teamTextBox.Text, out result))
                {
                    SelectedUser.Team = UserTeam;
                }
                else { MessageBox.Show("The data in team is entered incorrectly."); ++count; }
            }
            
            if (SelectedUser.Admin != NowRule)
            {
                SelectedUser.Admin = NowRule;
            }

            if (count == 0)
            {
                UserPageWindow window = new UserPageWindow(CurrentUser);
                window.Show();
                this.Close();
            }
            else {
                MessageBox.Show("If you want to exit without changes, then press the appropriate " +
                                "button or correct errors in the input data.");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            UserPageWindow window = new UserPageWindow(CurrentUser);
            window.Show();
            this.Close();
        }

    }
}
