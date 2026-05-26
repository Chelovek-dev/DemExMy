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
        public void SetData(dynamic prod)
        {
            Articul.Content = $"Артикул: {prod.Id}";
            Status.Content = $"Статус: {prod.Status}";
            Adres.Content = $"Адрес: {prod.PVZ}";
            DateZakaza.Content = $"Дата заказа: {prod.DateZakaza}";
            DateDostavki.Content = prod.DateDostavki;
        }
    }
}
