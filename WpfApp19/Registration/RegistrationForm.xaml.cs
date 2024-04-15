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
using System.Windows.Shapes;
using WpfApp19.Classes;

namespace WpfApp19.Registration
{
    /// <summary>
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        Entities Entities = new Entities();

        public RegistrationForm()
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
            string Login = Login_Box.Text.Trim();
            string Email = Email_Box.Text.Trim();
            string Password1 = Password_Box.Password.Trim();
            string Password = Passwords.HashPassword(Password_Box.Password.Trim());
            string Confirmed_Password1 = Confirm_Password_Box.Password.Trim();
            string captcha = Captcha_Box.Text.Trim();


            if (Login != "" || Password1 != "" || Confirmed_Password1 != "")
            {
                if (captcha == Captcha.text_)
                {


                    Users user = new Users();
                    Entities.Users.Add(user);
                    foreach (var i in Entities.Users)
                    {
                        if (i.Login == user.Login && i.Email == user.Email)
                        {
                            MessageBox.Show("Пользователь с таким логином или почтой уже существует!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            Logger.LogsEnter(2, 3, "Неудача(уже существует)");
                        }
                        if (Password1 != Confirmed_Password1)
                        {
                            MessageBox.Show("Пароли не совпадают!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            Logger.LogsEnter(2, 3, "Неудача(Пароли не совпадают)");
                        }
                    }

                    user.Login = Login;
                    user.Email = Email;
                    user.Password = Password;
                    user.ID_Role = 2;
                    Entities.SaveChanges();

                    MessageBox.Show("Пользователь успешно зарегистрирован!", "Успешно!", MessageBoxButton.OK, MessageBoxImage.Information);
                    Logger.LogsEnter(2, 3, "Успешная регистрация");
                    Logger.LogsEnter(2, 2, "Переход к окну авторизации");
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Введите капчу правильно!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);
                    Logger.LogsEnter(2, 1, "Неудача(Капча)");
                }
            }
            else
            {
                MessageBox.Show("Имеются пустые поля!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.LogsEnter(2, 3, "Неудача(пустые поля)");
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            Logger.LogsEnter(2, 2, "Переход к окну авторизации");

        }

      

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            Captcha_Image.Source = Captcha.CreateImage((int)Captcha_Image.Width, (int)Captcha_Image.Height);
            Logger.LogsEnter(2, 4, "Обновление капчи");
        }

    }
}
