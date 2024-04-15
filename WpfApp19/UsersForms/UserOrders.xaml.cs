using System;
using System.Collections.Generic;
using System.Data.Odbc;
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
    /// Логика взаимодействия для UserOrders.xaml
    /// </summary>
    public partial class UserOrders : Page
    {

        Entities Entities = new Entities();

        private Orders soft = new Orders();

        public int Id_User1;

        public int mon;

        public UserOrders(int Id, int money)
        {
            InitializeComponent();

            DataContext = soft;
            Count.Content = "Количество записей: " + Repair_List.Items.Count;

            Id_User1 = Id;

            mon= money;

            foreach (var items in Entities.Orders)
            {
                if (items.ID_User == Id)
                {
                    Repair_List.Items.Add(items);

                }
            }
        }

        private void o(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                Count.Content = "Количество записей: " + Repair_List.Items.Count;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logger.LogsEnter(Id_User1, 5, "Окно было закрыто");
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void DelClick(object sender, RoutedEventArgs e)
        {
            var soft_remove = Repair_List.SelectedItems.Cast<Orders>().ToList();
            if (soft_remove != null)
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {soft_remove.Count()} элементов ? ",
                                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (soft_remove.Count() > 0)
                    {


                        try
                        {
                            Entities.Orders.RemoveRange(soft_remove);
                            Entities.SaveChanges();
                            MessageBox.Show("Данные успешно удалены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Repair_List.ItemsSource = Entities.Orders.ToList();
                            Logger.LogsEnter(Id_User1, 8, "Успешно");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка при удалении!", "Провал!", MessageBoxButton.OK, MessageBoxImage.Error);
                            Logger.LogsEnter(Id_User1, 8, "Не успешно");

                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка при удалении!", "Провал!", MessageBoxButton.OK, MessageBoxImage.Error);
                        Logger.LogsEnter(Id_User1, 8, "Не успешно");

                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сначала запись", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.LogsEnter(Id_User1, 8, "Не успешно");
            }

        }

        public void UpdateService()
        {
            try
            {
                var SoftwareList = Entities.GetContext().Orders.ToList();
                if (poisk.SelectedIndex == 1)
                {
                    SoftwareList = SoftwareList.Where(p => p.Dishes.Price != null || p.Dishes.Price < 500).ToList();
                }
                if (poisk.SelectedIndex == 2)
                {
                    SoftwareList = SoftwareList.Where(p => p.Dishes.Price != null || p.Dishes.Price >= 500).ToList();
                }
                SoftwareList = SoftwareList.Where(p =>
                    p.Status.ToLower().Contains(poisk2.Text.ToLower()) || p.Date_.ToString().ToLower().Contains(poisk2.Text.ToLower()) ||
                    p.Users.Name.ToLower().Contains(poisk2.Text.ToLower()) || p.Users.Login.ToLower().Contains(poisk2.Text.ToLower()) ||
                    p.Dishes.Description.ToLower().Contains(poisk2.Text.ToLower()) || p.Dishes.Name.ToLower().Contains(poisk2.Text.ToLower()) ||
                    p.Dishes.Price.ToString().ToLower().Contains(poisk2.Text.ToLower()) || p.Dishes.Category.Name.ToLower().Contains(poisk2.Text.ToLower())).ToList();


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

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new UserDishes(Id_User1));
            Repair_List.Items.Refresh();
            Logger.LogsEnter(Id_User1, 2, "Переход к окну редактирования");
        }

        private void EditClick(object sender, RoutedEventArgs e)
        {
            var SoftwareList = Repair_List.SelectedItem as Orders;

            if(SoftwareList== null)
            {
                MessageBox.Show("Выберите сначала запись", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                Logger.LogsEnter(Id_User1, 10, "Не успешно");

            }
            else
            {
                if (mon > SoftwareList.Dishes.Price)
                {
                    Users user = Entities.Users.FirstOrDefault(u => u.ID_User == Id_User1);

                    if (user != null)
                    {
                        
                        int a = (int)(user.Balance - SoftwareList.Dishes.Price);
                        user.Balance = a;
                        MessageBox.Show("Успешно оплачено!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        SoftwareList.Status = "Оплачен";
                        Entities.SaveChanges();
                        Repair_List.Items.Refresh();    
                        Logger.LogsEnter(Id_User1, 10, "Успешно");

                    }


                }
                else
                {
                    MessageBox.Show("Недостаточно средства", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    Logger.LogsEnter(Id_User1, 10, "Не успешно");

                }
            }
            

        }

        //private void Refresh_Click(object sender, RoutedEventArgs e)
        //{
        //    //Repair_List.Items.Clear();

        //    foreach (var items in Entities.Repairs)
        //    {
        //        if (items.ID_Client == Id_User1)
        //        {

        //            Repair_List.Items.Add(items);

        //        }
        //    }
        //    Repair_List.Items.Refresh();
        //}
    }
}
