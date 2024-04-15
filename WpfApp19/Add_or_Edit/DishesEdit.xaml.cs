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
    /// Логика взаимодействия для DishesEdit.xaml
    /// </summary>
    public partial class DishesEdit : Page
    {
        private Dishes repairs = new Dishes();
        Entities Entities = new Entities();
        public int user;

        public DishesEdit(Dishes rep, int ID)
        {
            InitializeComponent();
            if (rep != null)
            {
                repairs = rep;
            }

            else
            {
                repairs = new Dishes();
            }
            DataContext = repairs;

            cmb1.DisplayMemberPath = "Name";

            user = ID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(repairs.Name))
                errors.AppendLine("Название блюда не введено");
            if (string.IsNullOrWhiteSpace(repairs.Description))
                errors.AppendLine("Описание блюда не введено");
            int Price;
            if (!int.TryParse(repairs.Price.ToString(), out Price) && Price < 0)
                errors.AppendLine("Некорректный формат стоимости");
            if (cmb1.SelectedItem == null)
                errors.AppendLine("Категория блюда не выбрана");



            if (errors.Length > 0)
            {
                Logger.LogsEnter(user, 9, "Добавление блюда(ошибка)");

                System.Windows.MessageBox.Show(errors.ToString());
                return;
            }

            if (repairs.ID_Dishes == 0)
            {
                Entities.GetContext().Dishes.Add(repairs);
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено", "Сохранено");
                Logger.LogsEnter(user, 9, "Добавление блюда(успешно)");
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
                cmb1.ItemsSource = Entities.GetContext().Category.ToList();

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
