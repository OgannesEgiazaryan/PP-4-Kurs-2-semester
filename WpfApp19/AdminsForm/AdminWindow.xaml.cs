using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
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
using WpfApp19.Classes;

namespace WpfApp19.AdminsForm
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {

        Entities Entities = new Entities();
        public string Login1;
        public int Id_User1;

        public AdminWindow(string Login, int ID)
        {
            InitializeComponent();

            Login1 = Login;
            Id_User1 = ID;
            this.Closing += MainWindow_Closing;
            Logged_User.Content = "Вы вошли как: " + Login;
            Manager.MainFrame = MainFrame;
            MainFrame.Navigate(new OrdersPage(Id_User1));
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.LogsEnter(Id_User1, 5, "Окно было закрыто");
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
            Logger.LogsEnter(Id_User1, 2, "Переход к окну авторизации");
        }


        private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new OrdersPage(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с заявками");
        }

        private void Types_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new DishesPage(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с блюдами");

        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UsersPage(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с клиентами");
        }

        private void Log_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new LogsPage(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с логами");
        }

        private void Category_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CategoryPage(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с логами");
        }
    }
}
