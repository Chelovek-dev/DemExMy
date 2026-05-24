using Org.BouncyCastle.Asn1.Mozilla;
using Org.BouncyCastle.Crypto.Operators;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

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
        public MainWindow(string FIO, string role)
        {
            InitializeComponent();
            UserName.Content = FIO;
            currentRole = role; 
            currentData = db.GetKrossovki();
            DisplayData(currentData);
            DataTable dt = db.GetPostavshik();
            PostavshikCombo.Items.Clear();
            PostavshikCombo.Items.Add("Все поставщики");
            foreach (DataRow dr in dt.Rows)
            {
                PostavshikCombo.Items.Add(dr["Postavshik"].ToString());
            }
            PostavshikCombo.SelectedIndex = 0;
            if (currentRole == "admin")
            {
                FilterPanel.Visibility = Visibility.Visible;
                AddTovarBTN.Visibility = Visibility.Visible;
                ZakazBTN.Visibility = Visibility.Visible;
            }
            else if (currentRole == "manager")
            {
                FilterPanel.Visibility = Visibility.Visible;
                AddTovarBTN.Visibility = Visibility.Collapsed;
                ZakazBTN.Visibility = Visibility.Visible;
            }
            else 
            {
                FilterPanel.Visibility = Visibility.Collapsed;
                AddTovarBTN.Visibility = Visibility.Collapsed;
                ZakazBTN.Visibility = Visibility.Collapsed;
            }

        }
        //public void DeleteProduct(int productId)
        //{
        //    int result = db.DeleteKrossovki(productId);
        //    if (result == -1)
        //    {
        //        MessageBox.Show("Нельзя удалить товар, который есть в заказах!",
        //            "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else if (result > 0)
        //    {
        //         ✅ Сначала обновляем данные из БД
        //        currentData = db.GetKrossovki();

        //         ✅ Потом применяем сортировку
        //        if (currentSort != "")
        //        {
        //            DataView dv = currentData.DefaultView;
        //            dv.Sort = currentSort;
        //            DisplayData(dv.ToTable());
        //        }
        //        else
        //            DisplayData(currentData);
        //        MessageBox.Show("Товар удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Ошибка при удалении!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}
        public void DeleteProduct(int productID)
        {
            int result = db.DeleteKrossovki(productID);
            if (result == -1)
            {
                MessageBox.Show("Нельзя удалить, Если есть в заказе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (result == 0)
            {
                if (currentSort != "")
                {
                    DataView dv = currentData.DefaultView;
                    dv.Sort = currentSort;
                    DisplayData(dv.ToTable());
                }
                else
                    DisplayData(currentData);
                MessageBox.Show("Товар удалён!", "Успел", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else 
            {
                MessageBox.Show("Ошибка", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    Skidka = dr["SKidka"],
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
    }
}