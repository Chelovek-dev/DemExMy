using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;

namespace NewExam
{
    /// <summary>
    /// Логика взаимодействия для OrdersWindow.xaml
    /// </summary>
    public partial class OrdersWindow : Window
    {
        DataHelper db = new DataHelper();

        public OrdersWindow()
        {
            InitializeComponent();
            LoadOrders();
        }

        private void LoadOrders()
        {
            OrdersPanel.Children.Clear();
            DataTable dt = db.GetZakaz();

            foreach (DataRow dr in dt.Rows)
            {
                var card = new OrderCard();
                card.SetData(new
                {
                    Id = dr["Id"],
                    Status = dr["Status"],
                    PVZ = dr["PVZ"],
                    DateZakaza = dr["DateZakaza"],
                    DateDostavki = dr["DateDostavki"]
                });
                OrdersPanel.Children.Add(card);
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}