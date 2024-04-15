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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp19.Classes;

namespace WpfApp19.Add_or_Edit
{
    /// <summary>
    /// Логика взаимодействия для UsersEdit.xaml
    /// </summary>
    public partial class UsersEdit : Page
    {
        private Users repairs = new Users();
        Entities Entities = new Entities();
        public int user;

        public UsersEdit(Users rep, int ID)
        {
            InitializeComponent();
            if (rep != null)
            {
                repairs = rep;
            }

            else
            {
                repairs = new Users();
            }
            DataContext = repairs;

            cmb1.DisplayMemberPath = "Name";

            user = ID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(repairs.Login))
                errors.AppendLine("Логин пользователя не введен");
            if (string.IsNullOrWhiteSpace(repairs.Name))
                errors.AppendLine("ФИО пользователя не введено");
            if (string.IsNullOrWhiteSpace(repairs.Phone) || !t4.Text.StartsWith("+7") || !t4.Text.Substring(2).All(char.IsDigit))
                errors.AppendLine("Номер телефона не введен");
            if (string.IsNullOrWhiteSpace(repairs.Email))
                errors.AppendLine("Эл. Почта не введена");

            int Price;
            if (!int.TryParse(repairs.Balance.ToString(), out Price) && Price < 0)
                errors.AppendLine("Некорректный формат баланса пользователя");
            if (cmb1.SelectedItem == null)
                errors.AppendLine("Роль блюда не выбрана");



            if (errors.Length > 0)
            {
                Logger.LogsEnter(user, 9, "Добавление пользователя(ошибка)");

                System.Windows.MessageBox.Show(errors.ToString());
                return;
            }

            if (repairs.ID_User == 0)
            {
                Entities.GetContext().Users.Add(repairs);
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено", "Сохранено");
                Logger.LogsEnter(user, 9, "Добавление пользователя(успешно)");
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex) { }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs
e)
        {
            if (Visibility == Visibility.Visible)
            {
                cmb1.ItemsSource = Entities.GetContext().Roles.ToList();

            }

        }

        private void btnFotoPath_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true && !string.IsNullOrWhiteSpace(dlg.FileName))
                textFotoPath.Text = dlg.FileName.ToString();
            // textFotoPath.Focus();
            repairs.Photo = dlg.FileName;
        }
    }
}
