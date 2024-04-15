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

namespace WpfApp19.AdminsForm
{
    /// <summary>
    /// Логика взаимодействия для LogsPage.xaml
    /// </summary>
    public partial class LogsPage : Page
    {

        Entities Entities = new Entities();

        private Logs soft = new Logs();

        public int Id_User1;

        public LogsPage(int Id)
        {
            InitializeComponent();
            Id_User1 = Id;

            DataContext = soft;
            Count.Content = "Количество записей: " + Repair_List.Items.Count;

            foreach (var items in Entities.Logs)
            {
                Repair_List.Items.Add(items);
            }
        }

        private void o(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                //Repair_List.ItemsSource = Entities.GetContext().Logs.ToList();
                Count.Content = "Количество записей: " + Repair_List.Items.Count;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.LogsEnter(Id_User1, 5, "Окно было закрыто");
        }

        public void UpdateService()
        {
            try
            {
                var SoftwareList = Entities.GetContext().Logs.ToList();
                SoftwareList = SoftwareList.Where(p =>
               p.Users.Login.ToLower().Contains(poisk2.Text.ToLower()) || p.Description.ToLower().Contains(poisk2.Text.ToLower()) ||
               p.Date_.ToString().ToLower().Contains(poisk2.Text.ToLower()) || p.Action.Name.ToLower().Contains(poisk2.Text.ToLower())).ToList();
                Repair_List.ItemsSource = SoftwareList.ToList();
            }
            catch { }

        }

        private void poisk_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
            Count.Content = "Количество записей: " + Repair_List.Items.Count;
            Logger.LogsEnter(Id_User1, 7, "Поиск данных в листе");
        }

        private void poisk2_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
            Count.Content = "Количество записей: " + Repair_List.Items.Count;
            Logger.LogsEnter(Id_User1, 7, "Поиск данных в листе");
        }

        private void comboService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
            Count.Content = "Количество записей: " + Repair_List.Items.Count;
            Logger.LogsEnter(Id_User1, 6, "Фильтрация данных в листе");

        }
    }
}
