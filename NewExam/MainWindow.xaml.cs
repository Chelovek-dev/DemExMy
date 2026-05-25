using System.Data;
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
            currentData = db.GetKrossovki();
            currentRole = Role;
            DisplayData(currentData);
            UserName.Content = currentRole;
            DataTable dt = db.GetPostavshik();
            PostavshikCombo.Items.Clear();
            PostavshikCombo.Items.Add("Все поставщики");
            foreach (DataRow dr in dt.Rows)
            {
                PostavshikCombo.Items.Add(dr["Postavshik"]);
            }
            PostavshikCombo.SelectedIndex = 0;
            if(currentRole == "admin")
            {
                AddTovarBTN.Visibility = Visibility.Visible;
                FilterPanel.Visibility = Visibility.Visible;
                ZakazBTN.Visibility = Visibility.Visible;
            }
            else if (currentRole == "manager")
            {
                AddTovarBTN.Visibility = Visibility.Collapsed;
                FilterPanel.Visibility = Visibility.Visible;
                ZakazBTN.Visibility = Visibility.Visible;
            }
            else 
            {
                AddTovarBTN.Visibility = Visibility.Collapsed;
                FilterPanel.Visibility = Visibility.Collapsed;
                ZakazBTN.Visibility = Visibility.Collapsed;
            }

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
                    Name = dr["Name"],
                    Price = dr["Price"],
                    Skidka = dr["Skidka"],
                    Kolvo = dr["Kolvo"],
                    Foto = dr["Foto"],
                    Kategory = dr["Kategory"],
                    Postavshik = dr["Postavshik"]
                }, currentRole);
                KrossovkiPanel.Children.Add(card);
            }
        }

        private void AddTovarClick(object sender, RoutedEventArgs e)
        {

        }

        private void ZakazClick(object sender, RoutedEventArgs e)
        {
            new OrdersWindow().ShowDialog();
        }

        private void SearchTB_TextChanged(object sender, TextChangedEventArgs e)
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
                DisplayData(filtred);
        }

        private void PostavshikCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable filtred = db.SearchKrossovki(SearchTB.Text, PostavshikCombo.SelectedItem.ToString());
            currentData= filtred;
            if (currentSort != "")
            {
                DataView dv = currentData.DefaultView;
                dv.Sort = currentSort;
                DisplayData(dv.ToTable());
            }
            else
                DisplayData(filtred);
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

        public void DeleteProduct(int prodId)
        {
            int result = db.DeleteProduct(prodId);
            if (result == -1)
                MessageBox.Show("Нельзя");
            else if (result > 0)
            {
                currentData = db.GetKrossovki();
                if (currentSort != "")
                {
                    DataView dv = currentData.DefaultView;
                    dv.Sort = currentSort;
                    DisplayData(dv.ToTable());
                }
                else
                    DisplayData(currentData);
                MessageBox.Show("Удачно");
            }
            else
                MessageBox.Show("Ошибка");

        }
    }
}