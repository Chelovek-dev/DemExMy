using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;

namespace NewExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataHelper db = new DataHelper();
        DataTable currentData;
        string currentRole;
        string currentSort = "";
        public MainWindow(string FIO, string Role)
        {
            InitializeComponent();
            currentRole = Role;
            UserName.Content = FIO;
            currentData = db.GetKrossovki();
            DisplayData(currentData);
            if(currentRole == "Администратор")
            {
                AddTovarBTN.Visibility = Visibility.Visible;
                ZakazBTN.Visibility = Visibility.Visible;
                FilterPanel.Visibility = Visibility.Visible;
            }
            else if (currentRole == "Менеджер")
            {
                AddTovarBTN.Visibility = Visibility.Collapsed;
                ZakazBTN.Visibility = Visibility.Visible;
                FilterPanel.Visibility = Visibility.Visible;
            }
            else
            {
                AddTovarBTN.Visibility = Visibility.Collapsed;
                ZakazBTN.Visibility = Visibility.Collapsed;
                FilterPanel.Visibility = Visibility.Collapsed;
            }
            PostavshikCombo.Items.Clear();
            PostavshikCombo.Items.Add("Все поставщики");
            PostavshikCombo.SelectedIndex = 0;
            DataTable dt = db.GetPostavshik();
            foreach (DataRow dr in dt.Rows)
                PostavshikCombo.Items.Add(dr["Postavshik"]);
        }

        private void DisplayData(DataTable dt)
        {
            KrossovkiPanel.Children.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                var card = new KrossovkiCard();
                card.SetData(new
                {
                    Id = dr["Id"],
                    Kategory = dr["Kategory"],
                    Name = dr["Name"],
                    Opisanie = dr["Opisanie"],
                    Proizvoditel = dr["Proizvoditel"],
                    Postavshik = dr["Postavshik"],
                    Price = dr["Price"],
                    EdIzmer = dr["EdIzmer"],
                    Kolvo = dr["Kolvo"],
                    Skidka = dr["Skidka"],
                    Foto = dr["Foto"]
                }, currentRole);
                KrossovkiPanel.Children.Add(card);
            }
        }

        private void AddTovarClick(object sender, RoutedEventArgs e)
        {
            new AddTovarWindow().ShowDialog();
        }

        private void ZakazClick(object sender, RoutedEventArgs e)
        {
            new OrdersWindow().ShowDialog();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                DataTable filtred = db.SearchKrossovki(SearchTB.Text, PostavshikCombo.SelectedItem.ToString());
                currentData = filtred;
                if (currentSort != "")
                {
                    DataView dv = currentData.DefaultView;
                    dv.Sort = currentSort;
                    DisplayData(dv.ToTable());
                }
                else
                    DisplayData(currentData);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PostavshikCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataTable filtred = db.SearchKrossovki(SearchTB.Text, PostavshikCombo.SelectedItem.ToString());
                currentData = filtred;
                if (currentSort != "")
                {
                    DataView dv = currentData.DefaultView;
                    dv.Sort = currentSort;
                    DisplayData(dv.ToTable());
                }
                else
                    DisplayData(currentData);
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ASC(object sender, RoutedEventArgs e)
        {
            DataView dv = currentData.DefaultView;
            dv.Sort = "Kolvo ASC";
            currentSort = "Kolvo ASC";
            DisplayData(dv.ToTable());
        }

        private void DESC(object sender, RoutedEventArgs e)
        {
            DataView dv = currentData.DefaultView;
            dv.Sort = "Kolvo DESC";
            currentSort = "Kolvo DESC";
            DisplayData(dv.ToTable());
        }

        private void ExitBTN(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        internal void DeleteProduct(int prodId)
        {
            try
            {
                var result = db.DeleteProduct(prodId);
                if (result == -1)
                {
                    MessageBox.Show("Нельзя удалить, потому что есть в заказах", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (result > 0)
                {
                    MessageBox.Show("Удачное удаление", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    currentData = db.GetKrossovki();
                    if (currentSort != "")
                    {
                        DataView dv = currentData.DefaultView;
                        dv.Sort = currentSort;
                        DisplayData(dv.ToTable());
                    }
                    else
                        DisplayData(currentData);
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}