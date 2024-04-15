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
using WpfApp19.Add_or_Edit;
using WpfApp19.Classes;

namespace WpfApp19.AdminsForm
{
    /// <summary>
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {

        Entities Entities = new Entities();

        private Category soft = new Category();

        public int Id_User1;

        public CategoryPage(int Id)
        {
            InitializeComponent();

            DataContext = soft;
            Count.Content = "Количество записей: " + Repair_List.Items.Count;

            Id_User1 = Id;
        }

        private void o(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)

                Repair_List.ItemsSource = Entities.GetContext().Category.ToList();
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
            var soft_remove = Repair_List.SelectedItems.Cast<Category>().ToList();
            if (soft_remove != null)
            {
                if (MessageBox.Show($"Вы точно хотите удалить следующие {soft_remove.Count()} элементов ? ",
                                "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (soft_remove.Count() > 0)
                    {


                        try
                        {
                            Entities.Category.RemoveRange(soft_remove);
                            Entities.SaveChanges();
                            MessageBox.Show("Данные успешно удалены", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                            Repair_List.ItemsSource = Entities.Category.ToList();
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

        private void EditClick(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new Add_or_Edit.CategoryPage((sender as Button).DataContext as Category, Id_User1));
            Logger.LogsEnter(Id_User1, 2, "Переход к окну редактирования");

        }

        public void UpdateService()
        {
            try
            {
                var SoftwareList = Entities.GetContext().Category.ToList();



                SoftwareList = SoftwareList.Where(p =>
                    p.Name.ToLower().Contains(poisk2.Text.ToLower())).ToList();


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
            Manager.MainFrame.Navigate(new Add_or_Edit.CategoryPage(null, Id_User1));
            Logger.LogsEnter(Id_User1, 2, "Переход к окну редактирования");
        }
    }
}
