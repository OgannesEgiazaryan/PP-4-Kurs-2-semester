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
    /// Логика взаимодействия для OrderEdit.xaml
    /// </summary>
    public partial class OrderEdit : Page
    {

        private Orders repairs = new Orders();
        Entities Entities = new Entities();
        public int user;

        public OrderEdit(Orders rep, int ID)
        {
            InitializeComponent();

            if (rep != null)
            {
                repairs = rep;
                cmb1.Text = rep.Status;
            }
                
            else
            {
                repairs = new Orders();
            }
            DataContext = repairs;

            cmb2.Items.Add("На одобрении");
            cmb2.Items.Add("Принят");
            cmb2.Items.Add("В процессе готовки");
            cmb2.Items.Add("Приготовлен");
            cmb2.Items.Add("Оплачен");

            

            cmb1.DisplayMemberPath = "Name";
            cmb3.DisplayMemberPath = "Name";

            user = ID;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            repairs.Date_ = t1.SelectedDate.Value;
            if (string.IsNullOrWhiteSpace(repairs.Date_.ToString()))
                errors.AppendLine("Дата не введена");
           
            if (cmb1.SelectedItem == null)
                errors.AppendLine("Блюдо не выбрано");

            if (cmb2.SelectedItem == null)
                errors.AppendLine("Клиент не выбран");
            

            if (cmb3.SelectedItem == null)
                errors.AppendLine("Статус не выбран");

            if (errors.Length > 0)
            {
                Logger.LogsEnter(user, 9, "Добавление заявки(ошибка)");

                System.Windows.MessageBox.Show(errors.ToString());
                return;
            }

            if (repairs.ID_Order == 0)
            {
                Entities.GetContext().Orders.Add(repairs);
            }

            try
            {
                Entities.GetContext().SaveChanges();
                MessageBox.Show("Сохранено", "Сохранено");
                Logger.LogsEnter(user, 9, "Добавление заявки(успешно)");
                Manager.MainFrame.GoBack();

            }
            catch (Exception ex) { }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
            Logger.LogsEnter(user, 2, "Возвращение обратно");

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs
e)
        {
            if (Visibility == Visibility.Visible)
            {

                cmb1.ItemsSource = Entities.GetContext().Dishes.ToList();
                cmb3.ItemsSource = Entities.GetContext().Users.ToList();
            }

        }
    }
}
