using System.Windows;

namespace goods_counting
{
    public partial class AuthWindow : Window
    {

        DbUsers dbUsers = new DbUsers();
        Encryption encryption = new Encryption();
        
        public AuthWindow()
        {
            InitializeComponent();
            user.Focus();

            if (Properties.Settings.Default.rememberAuth)
                getRememberLogin();
        }

        private void authorize_Click(object sender, RoutedEventArgs e)
        {

            var needUser = new User
            {
                user = user.Text,
                password = encryption.Encrypt(passwd.Password)
            };

            if (user.Text == "" || passwd.Password == "")
                snackbar.MessageQueue.Enqueue("Необходимо заполнить все поля");
            else
            {
                if (dbUsers.userAuth(needUser))
                {
                    Properties.Settings.Default.rememberUser = needUser.user;
                    Properties.Settings.Default.rememberPassword = needUser.password;

                    if (rememberPasswd.IsChecked == true)
                        Properties.Settings.Default.rememberAuth = true;

                    Properties.Settings.Default.Save();

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    Close();
                }
                else
                    snackbar.MessageQueue.Enqueue("Введен неверный логин или пароль");
            }
        }

        private void getRememberLogin()
        {
            var needUser = new User
            {
                user = Properties.Settings.Default.rememberUser,
                password = Properties.Settings.Default.rememberPassword
            };

            if (dbUsers.userAuth(needUser))
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            else
            {
                Properties.Settings.Default.rememberAuth = false;
                Properties.Settings.Default.rememberUser = null;
                Properties.Settings.Default.rememberPassword = null;
                Properties.Settings.Default.Save();

                snackbar.MessageQueue.Enqueue("Данные авторизации устарели");
            }
        }

        private void register_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            Close();
        }
    }
}
