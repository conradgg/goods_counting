using System.Windows;

namespace goods_counting
{
    public partial class Roles : Window
    {
        DbUsers dbUsers = new DbUsers();
        public Roles()
        {
            InitializeComponent();
            getData();
        }

        public void getData()
        {
            rolesDG.ItemsSource = dbUsers.getRoles();
        }

        private void saveChanges_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRow = rolesDG.SelectedItem as Role;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали роль!");
            else
            {
                dbUsers.updateRole(selectedRow);
                snackbar.MessageQueue.Enqueue("Роль обновлена!");
            }
        }

        private void remove_Click(object sender, RoutedEventArgs e)
        {
            Role selectedRow = rolesDG.SelectedItem as Role;
            if (selectedRow == null)
                snackbar.MessageQueue.Enqueue("Вы не выбрали роль!");
            else
            {
                dbUsers.removeRole(selectedRow.role);
                snackbar.MessageQueue.Enqueue("Роль удалена!");
            }
            getData();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (newRole.Text == "")
                snackbar.MessageQueue.Enqueue("Вы не ввели название для роли!");
            else
            {
                var role = new Role
                {
                    role = newRole.Text,
                    adminDasboardAccess = adminAccess.IsChecked.Value,
                    sellerModeAccess = sellerAccess.IsChecked.Value,
                    viewModeAccess = viewAccess.IsChecked.Value,
                    recordModeAcess = addAccess.IsChecked.Value
                };
                dbUsers.addRole(role);
                snackbar.MessageQueue.Enqueue("Роль создана!");
            }
            getData();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.Show();
            Close();
        }
    }
}
