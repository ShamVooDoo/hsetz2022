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
    /// Логика взаимодействия для InvitationFormWindow.xaml
    /// </summary>
    public partial class InvitationFormWindow : Window
    {
        User CurrentUser { get; set; }
        User SelectedUser { get; set; }

        public InvitationFormWindow(User currentUser, User selectedUser)
        {
            InitializeComponent();

            CurrentUser = currentUser;
            SelectedUser = selectedUser;
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            UserPageWindow window = new UserPageWindow(CurrentUser);
            window.Show();
            this.Close();
        }

        private void continueButton_Click(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            int check;
            if (teamName.Text != null && proposedPosition.Text != null && !int.TryParse(proposedPosition.Text, out check))
            {
                Invite newInvite = new Invite(CurrentUser.Login, SelectedUser.Login, CurrentUser.Position, 
                                              proposedPosition.Text, teamName.Text);

                foreach(var invite in ApplicationContext.invites)
                {
                    if (newInvite.AddresseLogin == invite.AddresseLogin && newInvite.TeamName == invite.TeamName 
                        && newInvite.SenderLogin == invite.SenderLogin)
                    {
                        flag = false;
                    }
                }

                if (flag)
                {
                    ApplicationContext.invites.Add(newInvite);
                    MessageBox.Show("Your, invite successfully created.");
                    UserPageWindow window = new UserPageWindow(CurrentUser);
                    window.Show();
                    this.Close();
                }
                else { MessageBox.Show("Sorry, such an invitation already exists."); }
            }
            else { MessageBox.Show("Incorrect or empty!"); }
        }
    }
}
