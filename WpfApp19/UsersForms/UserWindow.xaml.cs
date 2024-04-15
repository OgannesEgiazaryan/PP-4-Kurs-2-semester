using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
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
using WpfApp19.AdminsForm;
using WpfApp19.Classes;

namespace WpfApp19.UsersForms
{
    /// <summary>
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        Entities Entities = new Entities();
        public string Login1;
        public int Id_User1;
        public int Mon;


        public UserWindow(string Login, int ID, int money)
        {
            InitializeComponent();
            Login1 = Login;
            Id_User1 = ID;
            Mon = money;
            this.Closing += MainWindow_Closing;
            Logged_User.Content = "Вы вошли как: " + Login;
            Manager.MainFrame = MainFrame;
            MainFrame.Navigate(new UserOrders(Id_User1, money));
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
            MainFrame.Navigate(new UserOrders(Id_User1, Mon));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с заявками");
        }

        private void MainFrame_Navigated_1(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {

        }

        private void Types_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new UserDishes(Id_User1));
            Manager.MainFrame = MainFrame;
            Logger.LogsEnter(Id_User1, 2, "Переход к странице с блюдами");
        }
    }
}
