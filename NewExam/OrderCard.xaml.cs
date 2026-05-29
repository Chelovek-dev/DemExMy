using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace NewExam
{
    /// <summary>
    /// Логика взаимодействия для OrderCard.xaml
    /// </summary>
    public partial class OrderCard : UserControl
    {
        public OrderCard()
        {
            InitializeComponent();
        }
        int OrdId;
        public void SetData(dynamic prod)
        {
            OrdId = prod.Id;
            Articul.Content = "Артикул: " + prod.Id;
            DateDostavki.Content = "Дата доставки: " + prod.DateDostavki;
            DateZakaza.Content = "Дата заказа: " + prod.DateZakaza;
            Status.Content = "Статус: " + prod.Status;
            Adres.Content = $"{prod.City}, {prod.Street}, {prod.Home}";

        }

        private void DeleteCLICK(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Уверены, что хотите удалить заказ?","Нужно подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes) 
                ((OrdersWindow)Window.GetWindow(this)).DeleteOrder(OrdId);
        }

        private void EditCLICK(object sender, RoutedEventArgs e)
        {
            
        }
    }
}