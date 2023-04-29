using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace goods_counting
{
    public partial class RegisterWindow : Window
    {
        DbUsers dbUsers = new DbUsers();
        Encryption encryption = new Encryption();

        public RegisterWindow()
        {
            InitializeComponent();
            user.Focus();
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new User
            {
                user = user.Text,
                password = encryption.Encrypt(passwd.Password),
                role = "user"
            };

            if (user.Text == "" || passwd.Password == "")
                snackbar.MessageQueue.Enqueue("Необходимо заполнить все поля");
            else if (user.Text.Length < 4)
                snackbar.MessageQueue.Enqueue("Имя пользователя должно быть не менее 4 символов");
            else if (dbUsers.userExist(user.Text))
                snackbar.MessageQueue.Enqueue("Такое имя пользователя уже существует");
            else if (passwd.Password != passwd_2.Password)
                snackbar.MessageQueue.Enqueue("Пароли не совпадают");
            else if (passwd.Password.Length < 8)
                snackbar.MessageQueue.Enqueue("Длинна пароля должна быть не менее 8 символов");
            else
            {
                dbUsers.userRegister(newUser);
                snackbar.MessageQueue.Enqueue("Регистация прошла успешно!\nПеренаправление на авторизацию");
                DelayedExecution();
            }
        }

        async void DelayedExecution()
        {
            await Task.Delay(2000);
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }

        private void authozire_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            Close();
        }
    }
}
