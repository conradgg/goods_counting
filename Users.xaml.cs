using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace goods_counting
{
    public partial class Users : Window
    {
        DbUsers dbUsers = new DbUsers();
        List<User> users;

        public Users()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            users = dbUsers.getUsers();
            usersDG.ItemsSource = users;
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            Close();
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<User> filteredList = users
                .Where(user => user.role.ToLower().Contains(role.Text.ToLower()))
                .Where(user => user.user.ToLower().Contains(needUser.Text.ToLower()))
                .ToList();
            usersDG.ItemsSource = filteredList;
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            User selectedRow = usersDG.SelectedItem as User;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали пользователя!");
            else
            {
                dbUsers.updateUser(selectedRow);
                snackbar.MessageQueue.Enqueue("Пользователь обновлен!");
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            User selectedRow = usersDG.SelectedItem as User;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали пользователя!");
            else
            {
                dbUsers.removeUser(selectedRow.id);
                snackbar.MessageQueue.Enqueue("Пользователь удален!");
                getData();
            }
        }
    }
}
