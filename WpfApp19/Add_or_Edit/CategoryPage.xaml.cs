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
    /// Логика взаимодействия для CategoryPage.xaml
    /// </summary>
    public partial class CategoryPage : Page
    {

        private Category repairs = new Category();
        Entities Entities = new Entities();
        public int user;

        public CategoryPage(Category rep, int ID)
        {
            InitializeComponent();
            if (rep != null)
            {
                repairs = rep;
            }

            else
            {
                repairs = new Category();
            }
            DataContext = repairs;

      

            user = ID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(repairs.Name))
                errors.AppendLine("Название категории не введено");



            if (errors.Length > 0)
            {
                Logger.LogsEnter(user, 9, "Добавление категории(ошибка)");

                System.Windows.MessageBox.Show(errors.ToString());
                return;
            }

            if (repairs.ID_Category == 0)
            {
                Entities.GetContext().Category.Add(repairs);
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено", "Сохранено");
                Logger.LogsEnter(user, 9, "Добавление категории(успешно)");
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

            }

        }
    }
}
