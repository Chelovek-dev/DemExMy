using System.Runtime.InteropServices.Marshalling;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace NewExam
{
    /// <summary>
    /// Логика взаимодействия для KrossovkiCard.xaml
    /// </summary>
    public partial class KrossovkiCard : UserControl
    {
        public KrossovkiCard()
        {
            InitializeComponent();

        }
        int prodId;
        public void SetData(dynamic prod, string role)
        {
            prodId = prod.Id;
            Name.Content = $"{prod.Name} || {prod.Kategory}";
            Opisanie.Content = $"Описание {prod.Opisanie}";
            Proizvoditel.Content = "Производитель: "+prod.Proizvoditel;
            Postavshik.Content = "Поставщик: " + prod.Postavshik;
            EdIzmer.Content = "Единица измерения: " + prod.EdIzmer;
            Kolvo.Content = "Количество: " + prod.Kolvo;
            Skidka.Content = prod.Skidka + "%";
            IMG.Source = new BitmapImage(new Uri("Images/" + prod.Foto, UriKind.Relative));
            if (prod.Foto == "")
                IMG.Source = new BitmapImage(new Uri("Images/picture.png", UriKind.Relative));
            decimal d = prod.Skidka;
            decimal p = prod.Price;
            decimal final = p * (1 - d / 100);
            if (d > 0)
            {
                OldPrice.Visibility = Visibility.Visible;
                OldPrice.TextDecorations = TextDecorations.Strikethrough;
                OldPrice.Text = "Старая цена: " + p.ToString();
                Price.Content = "Цена: " + final;
            }
            else
            {
                OldPrice.Visibility = Visibility.Collapsed;
                Price.Content = "Цена: " + p;
            }
            if (d > 15)
                BACK.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF0099"));
            if(prod.Kolvo == 0)
                BACK.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#339999"));
            if(role == "Администратор")
            {
                DeleteTovarBTN.Visibility = Visibility.Visible;
                EditTovarBTN.Visibility = Visibility.Visible;
            }
        }
        private void EditTovarClick(object sender, RoutedEventArgs e)
        {

        }


        private void DeleteTovarClick(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Уверены, что удалить?","Подвердите", MessageBoxButton.YesNo, MessageBoxImage.Question)==MessageBoxResult.Yes) 
                ((MainWindow)Window.GetWindow(this)).DeleteProduct(prodId);
        }
    }
}