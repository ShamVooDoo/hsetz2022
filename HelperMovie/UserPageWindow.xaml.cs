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
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>
    public partial class UserPageWindow : Window
    {
        User CurrentUser { get; set; }
        List<User> Users { get; set; }
        List<Invite> Invites { get; set; }

        public UserPageWindow(User user)
        {
            CurrentUser = user;
            InitializeComponent();

            if (CurrentUser.Admin == 1)
            {
                techBaseGrid.Visibility = Visibility.Visible;
                projectsGrid.Visibility = Visibility.Visible;
                usersGrid.Visibility = Visibility.Visible;
            }

            ApplicationContext db = new ApplicationContext();
            Users = db.Users.ToList();
            Invites = ApplicationContext.invites.ToList();
        }

        private List<Invite> SortInvites() // Выбирает приглашения, которые относятся к данному пользователю
        {
            List<Invite> userInvites = new List<Invite>();

            foreach (var invite in Invites)
            {
                if (CurrentUser.Login == invite.AddresseLogin)
                {
                    userInvites.Add(invite);
                }
            }
            if (userInvites.Count > 0)
            {
                return userInvites;
            }
            else { return null; }
        }

        private List<User> SortUsers() // Выбирает пользователей у которых нету команды
        {
            List<User> noTeamUsers = new List<User>();

            foreach (var user in Users)
            {
                if (user.Team == null)
                {
                    noTeamUsers.Add(user);
                }
            }
            if (noTeamUsers.Count > 0)
            {
                return noTeamUsers;
            }
            else { return null; }
        }

        //                       НАЧАЛО: Изменение задач                          //
        private void Tasks_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;
            sendInviteGrid.Visibility = Visibility.Hidden;
            lookInviteGrid.Visibility = Visibility.Hidden;
            openTechBaseGrid.Visibility = Visibility.Hidden;
            openProjectsGrid.Visibility = Visibility.Hidden;
            chooseActionGrid.Visibility = Visibility.Hidden;
            openUsersGrid.Visibility = Visibility.Hidden;
            deleteUserButton.Visibility = Visibility.Hidden;
            tasksGrid.Visibility = Visibility.Visible;


        }
        //                       КОНЕЦ: Изменение задач                         //


        //      НАЧАЛО: Выбор приглашения в команду и отправка приглашения в команду        //
        ///                 Начало: выбор приглашения в команду                             ///
        private void Team_Click(object sender, RoutedEventArgs e)
        {
            lookInviteGrid.Visibility = Visibility.Hidden;
            sendInviteGrid.Visibility = Visibility.Hidden;
            openTechBaseGrid.Visibility = Visibility.Hidden;
            chooseActionGrid.Visibility = Visibility.Hidden;
            openProjectsGrid.Visibility = Visibility.Hidden;
            openUsersGrid.Visibility = Visibility.Hidden;
            deleteUserButton.Visibility = Visibility.Hidden;
            tasksGrid.Visibility = Visibility.Hidden;
            inviteGrid.Visibility = Visibility.Visible;
        }

        private void lookInviteButton_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;

            if (CurrentUser.Team == null)
            {
                List<Invite> usersInvites = SortInvites();
                if (usersInvites != null && usersInvites.Count > 0)
                {
                    invitesListBox.ItemsSource = usersInvites;
                    lookInviteGrid.Visibility = Visibility.Visible;
                }
                else { MessageBox.Show("Sorry, but you haven't any invitations."); }
            }
            else
            {
                MessageBox.Show("Sorry, but you have a team already.");
            }
        }

        private void teamName_Initialized(object sender, EventArgs e)
        {
            TextBlock teamName = sender as TextBlock;
            Invite invite = teamName.DataContext as Invite;
            teamName.Text = $"Team name: {invite.TeamName}";
        }

        private void senderLogin_Initialized(object sender, EventArgs e)
        {
            TextBlock userLogin = sender as TextBlock;
            Invite invite = userLogin.DataContext as Invite;
            userLogin.Text = $"Sender's login: {invite.SenderLogin}";
        }

        private void senderPosition_Initialized(object sender, EventArgs e)
        {
            TextBlock position = sender as TextBlock;
            Invite invite = position.DataContext as Invite;
            position.Text = $"Sender's position: {invite.SenderPosition}";
        }

        private void addressePosition_Initialized(object sender, EventArgs e)
        {
            TextBlock position = sender as TextBlock;
            Invite invite = position.DataContext as Invite;
            position.Text = $"Your position: {invite.AddressePosition}";
        }

        private void invitesListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Do you want to accept this invitation?", "Acception", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Invite selectedInvite = (Invite)invitesListBox.SelectedItem;
                invitesListBox.ItemsSource = null;

                CurrentUser.Team = selectedInvite.TeamName;
                CurrentUser.Position = selectedInvite.AddressePosition;
                MessageBox.Show("Success!");
                lookInviteGrid.Visibility = Visibility.Hidden;
            }
        }
        ///             Конец: выбор приглашения в команду              ///

        ///             Начало: отправка приглашения в команду         ///
        private void sendInviteButton_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;
            List<User> sortedUsers = SortUsers();
            
            if (sortedUsers != null && sortedUsers.Count > 0)
            {
                usersListBox.ItemsSource = sortedUsers;
                sendInviteGrid.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Sorry, but there aren't avaliable people for your team.");
            }
        }

        private void userLogin_Initialized(object sender, EventArgs e)
        {
            TextBlock userLogin = sender as TextBlock;
            User user = userLogin.DataContext as User;
            userLogin.Text = $"Login: {user.Login}";
        }

        private void userName_Initialized(object sender, EventArgs e)
        {
            TextBlock userName = sender as TextBlock;
            User user = userName.DataContext as User;
            userName.Text = $"Name: {user.Name}";
        }

        private void userAge_Initialized(object sender, EventArgs e)
        {
            TextBlock userAge = sender as TextBlock;
            User user = userAge.DataContext as User;
            if (user.Age == null)
            {
                userAge.Text = $"Age: not specified";
            }
            else { userAge.Text = $"Age: {user.Age}"; }
        }

        private void usersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to send invitation to this person?", "Acception", 
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                User selectedUser = (User)usersListBox.SelectedItem;
                InvitationFormWindow invitationFormWindow = new InvitationFormWindow(CurrentUser, selectedUser);
                invitationFormWindow.Show();
                this.Close();
            }
        }
        ///                         Конец: отправка приглашения в команду                                ///
        //              КОНЕЦ: Выбор приглашения в команду и отправка приглашения в команду             //


        //                       НАЧАЛО: Изменение техники                          //
        private void TechBase_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;
            sendInviteGrid.Visibility = Visibility.Hidden;
            lookInviteGrid.Visibility = Visibility.Hidden;
            openProjectsGrid.Visibility = Visibility.Hidden;
            chooseActionGrid.Visibility = Visibility.Hidden;
            openUsersGrid.Visibility = Visibility.Hidden;
            deleteUserButton.Visibility = Visibility.Hidden;
            tasksGrid.Visibility = Visibility.Hidden;

            if (ApplicationContext.techniques.Count > 0 && ApplicationContext.techniques != null)
            {
                techBaseListBox.ItemsSource = ApplicationContext.techniques;
                openTechBaseGrid.Visibility = Visibility.Visible;
            }
            else { MessageBox.Show("Sorry, you don't have any techniques."); }

        }
        private void techBaseListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to choose this thecnic?", "Acception",
               MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Tech tech = (Tech)techBaseListBox.SelectedItem;
            }
        }
        //                       КОНЕЦ: Изменение техники                         //


        //                       НАЧАЛО: Изменение проектов                          //
        private void Projects_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;
            sendInviteGrid.Visibility = Visibility.Hidden;
            lookInviteGrid.Visibility = Visibility.Hidden;
            openTechBaseGrid.Visibility = Visibility.Hidden;
            chooseActionGrid.Visibility = Visibility.Hidden;
            openUsersGrid.Visibility = Visibility.Hidden;
            deleteUserButton.Visibility = Visibility.Hidden;
            tasksGrid.Visibility = Visibility.Hidden;

            if (ApplicationContext.projects.Count > 0 && ApplicationContext.projects != null)
            {
                projectsListBox.ItemsSource = ApplicationContext.projects;
                openProjectsGrid.Visibility = Visibility.Visible;
            }
            else { MessageBox.Show("Sorry, you don't have any projects."); }

        }

        private void projectsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to choose this project?", "Acception",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Project project = (Project)projectsListBox.SelectedItem;
            }
        }
        //                       КОНЕЦ: Изменение проектов                          //


        //                  НАЧАЛО: Изменение и удаление пользователя                       //
        ///                     Начало: изменение пользователя                             ///

        private void users_Click(object sender, RoutedEventArgs e)
        {
            inviteGrid.Visibility = Visibility.Hidden;
            sendInviteGrid.Visibility = Visibility.Hidden;
            lookInviteGrid.Visibility = Visibility.Hidden;
            openTechBaseGrid.Visibility = Visibility.Hidden;
            openProjectsGrid.Visibility = Visibility.Hidden;
            openUsersGrid.Visibility = Visibility.Hidden;
            deleteUserButton.Visibility = Visibility.Hidden;
            tasksGrid.Visibility = Visibility.Hidden;
            chooseActionGrid.Visibility = Visibility.Visible;
        }

        private void changeUserButton_Click(object sender, RoutedEventArgs e)
        {
            chooseActionGrid.Visibility = Visibility.Hidden;

            if (Users != null && Users.Count > 1)
            {
                allUsersListBox.ItemsSource = Users;
                openUsersGrid.Visibility = Visibility.Visible;
            }
            else { MessageBox.Show("Sorry, there isn't any others users, except you."); }
        }

        private void userPosition_Initialized(object sender, EventArgs e)
        {
            TextBlock userPosition = sender as TextBlock;
            User user = userPosition.DataContext as User;
            if (user.Position == null)
            {
                userPosition.Text = $"Position: not specified";
            }
            else { userPosition.Text = $"Position: {user.Position}"; }
        }

        private void userTeam_Initialized(object sender, EventArgs e)
        {
            TextBlock userTeam = sender as TextBlock;
            User user = userTeam.DataContext as User;
            if (user.Team == null)
            {
                userTeam.Text = $"Team: not choosen yet";
            }
            else { userTeam.Text = $"Team: {user.Team}"; }
        }

        private void admin_Initialized(object sender, EventArgs e)
        {
            TextBlock userAdmin = sender as TextBlock;
            User user = userAdmin.DataContext as User;
            if (user.Admin == 1)
            {
                userAdmin.Text = $"Admin: Yes";
            }
            else { userAdmin.Text = $"Admin: No"; }
        }

        private void allUsersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to choose this user?", "Acception",
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                User user = (User)allUsersListBox.SelectedItem;
                ChangeUserWindow window = new ChangeUserWindow(CurrentUser, user);
                window.Show();
                this.Close();
            }
        }
        ///                     Конец: изменение пользователя                   ///

        ///                     Начало: удаление пользователя                   ///
        private void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            chooseActionGrid.Visibility = Visibility.Hidden;

            if (Users != null && Users.Count > 1)
            {
                deleteUsersListBox.ItemsSource = Users;
                deleteUsersGrid.Visibility = Visibility.Visible;
            }
            else { MessageBox.Show("Sorry, there isn't any others users, except you."); }
        }

        private void deleteUsersListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this user?", "Acception",
                                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                User user = (User)allUsersListBox.SelectedItem;
                deleteUsersListBox.ItemsSource = null;

                Users.Remove(user);
                deleteUsersListBox.ItemsSource = Users;
                MessageBox.Show("This account successfully deleted.");
            }
        }
        ///                         Конец: удаление пользователя                                ///
        //                      КОНЕЦ: Изменение и удаление пользователя                        //
    }
}
