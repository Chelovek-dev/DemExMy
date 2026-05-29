using System.Data;
using System.Windows;

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
            try
            {
                DataTable dt = db.GetZakaz();
                LoadData(dt);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadData(DataTable dt)
        {
            OrdersPanel.Children.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                var card = new OrderCard();
                card.SetData(new
                {

                    Id = dr["Id"],
                    DateDostavki = dr["DateDostavki"],
                    DateZakaza = dr["DateZakaza"],
                    Street = dr["Street"],
                    Status = dr["Status"],
                    Home = dr["Home"],
                    City = dr["City"]
                });
                OrdersPanel.Children.Add(card);
            }
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        internal void DeleteOrder(int ordId)
        {
            try
            {
                int result = db.DeleteOrder(ordId);
                if (result > 0)
                {
                    MessageBox.Show("Удачное удаление", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    DataTable dt = db.GetZakaz();
                    LoadData(dt);
                }
                else
                    MessageBox.Show("Ошибка УДАЛЕНИЯ", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}