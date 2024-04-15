using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using WpfApp19.Registration;
using WpfApp19.Classes;
using WpfApp19.AdminsForm;
using WpfApp19.UsersForms;

namespace WpfApp19
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int count_nepravilno = 0;

        Entities Entities = new Entities();

        public MainWindow()
        {
            InitializeComponent();

            Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);

            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.LogsEnter(2, 5, "Окно было закрыто");
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationUser = new RegistrationForm();
            registrationUser.Show();
            this.Close();
            Logger.LogsEnter(2, 2, "Переход к окну регистрации");

        }

        private async void Auth_Click(object sender, RoutedEventArgs e)
        {
            string login = Login_Box.Text.Trim();
            string password = Password_Box.Password.Trim();
            string captcha = Captcha_Box.Text.Trim();

            Users user = new Users();
            user = Entities.Users.Where(p => p.Login == login).FirstOrDefault();
            int userCount = Entities.Users.Where(p => p.Login == login).Count();

            if (userCount > 0 && Passwords.VerifyHashedPassword(user.Password, password))
            {
                if (captcha == Captcha.text_)
                {
                    MessageBox.Show("Добро пожаловать, " + user.Login + "!", "Вход", MessageBoxButton.OK, MessageBoxImage.Information);
                    Logger.LogsEnter(user.ID_User, 1, "Удачно");
                    if (user.ID_Role == 1)
                    {
                        AdminWindow software = new AdminWindow(user.Login, user.ID_User);
                        software.Show();
                        count_nepravilno = 0;
                        Logger.LogsEnter(user.ID_User, 2, "Переход к окну администратора");
                        this.Close();

                    }
                    else if (user.ID_Role == 2)
                    {
                        UserWindow userWindow = new UserWindow(user.Login, user.ID_User, (int)user.Balance);
                        userWindow.Show();
                        count_nepravilno = 0;
                        Logger.LogsEnter(user.ID_User, 2, "Переход к окну пользователя");
                        this.Close();
                    }
                    else
                    {
                        UserWindow userWindow = new UserWindow(user.Login, user.ID_User, (int)user.Balance);
                        userWindow.Show();
                        count_nepravilno = 0;
                        Logger.LogsEnter(user.ID_User, 2, "Переход к окну пользователя");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Введите капчу правильно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);
                    count_nepravilno += 1;
                    Captcha_Box.Text = "";
                    Logger.LogsEnter(2, 1, "Неудача(Капча)");

                }
            }
            else
            {
                MessageBox.Show("Вы ввели неправильный логин или пароль!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);
                count_nepravilno += 1;
                Logger.LogsEnter(2, 1, "Неудача(Логин или пароль)");
            }
            if (count_nepravilno >= 3)
            {
                MessageBox.Show($"Превышено количество неудачных попыток входа {count_nepravilno}!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                await DisableWindowAsync(TimeSpan.FromSeconds(10));
                Logger.LogsEnter(2, 1, "Неудача(заблокировано на 10 секунд)");
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);
            Logger.LogsEnter(2, 4, "Обновление капчи");

        }

        private async void ButtonClick(object sender, RoutedEventArgs e)
        {
            await DisableWindowAsync(TimeSpan.FromSeconds(10));
        }

        private async Task DisableWindowAsync(TimeSpan duration)
        {
            // Создаем задачу задержки
            var delayTask = Task.Delay(duration);

            // Блокируем пользовательский интерфейс
            this.IsEnabled = false;

            try
            {
                // Отображаем MessageBox
                await this.Dispatcher.InvokeAsync(() =>
                {
                    MessageBox.Show($"Окно будет заблокировано в течение {duration.TotalSeconds} секунд.", "Блокировка окна", MessageBoxButton.OK, MessageBoxImage.Information);
                });

                // Ожидаем завершения задачи задержки
                await delayTask;
            }
            finally
            {
                // Разблокируем пользовательский интерфейс
                this.Dispatcher.Invoke(() =>
                {
                    this.IsEnabled = true;
                });
            }
        }


    }
}

