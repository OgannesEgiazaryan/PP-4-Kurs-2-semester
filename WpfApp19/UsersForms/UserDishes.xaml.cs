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

namespace WpfApp19.UsersForms
{
    /// <summary>
    /// Логика взаимодействия для UserDishes.xaml
    /// </summary>
    public partial class UserDishes : Page
    {
        Entities Entities = new Entities();

        private Dishes soft = new Dishes();

        public int Id_User1;

        public UserDishes(int Id)
        {
            InitializeComponent();
            DataContext = soft;
            Count.Content = "Количество записей: " + Repair_List.Items.Count;

            Id_User1 = Id;
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var Selected = Repair_List.SelectedItem as Dishes;
            if(Selected != null)
            {
                Orders orders = new Orders();
                Entities.Orders.Add(orders);

                orders.ID_Dishes = Selected.ID_Dishes;
                orders.ID_User = Id_User1;
                orders.Date_ = DateTime.Now;
                orders.Status = "На одобрении";

                Entities.SaveChanges();
                MessageBox.Show("Заказ успешно оформлен!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                Logger.LogsEnter(Id_User1, 11, $"Успешно{Selected.Name}");

            }
            else
            {
                MessageBox.Show("Сначала выберите блюдо!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);

                Logger.LogsEnter(Id_User1, 11, $"Не успешно");
            }
        }

        private void o(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                Repair_List.ItemsSource = Entities.GetContext().Dishes.ToList();
            Count.Content = "Количество записей: " + Repair_List.Items.Count;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.LogsEnter(Id_User1, 5, "Окно было закрыто");
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        public void UpdateService()
        {
            try
            {
                var SoftwareList = Entities.GetContext().Dishes.ToList();



                SoftwareList = SoftwareList.Where(p =>
                    p.Name.ToLower().Contains(poisk2.Text.ToLower()) || p.Price.ToString().ToLower().Contains(poisk2.Text.ToLower()) ||
                    p.Description.ToLower().Contains(poisk2.Text.ToLower()) || p.Category.Name.ToLower().Contains(poisk2.Text.ToLower())).ToList();


                Repair_List.ItemsSource = SoftwareList.ToList();

            }
            catch
            {

            }
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
