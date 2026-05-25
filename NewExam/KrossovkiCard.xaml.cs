using System.Runtime.InteropServices.Marshalling;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
            if (role == "admin")
            {
                DeleteTovarBTN.Visibility = Visibility.Visible;
                EditTovarBTN.Visibility = Visibility.Visible;
            }
            prodId = prod.Id;
            Name.Content = prod.Name + prod.Kategory;
            Skidka.Content = prod.Skidka;
            Kolvo.Content = prod.Kolvo;
            Postavshik.Content = prod.Postavshik;
            IMG.Source = new BitmapImage(new Uri("Images/" + prod.Foto, UriKind.Relative));
            if(prod.Foto == "")
                IMG.Source = new BitmapImage(new Uri("Images/picture.png", UriKind.Relative));

            decimal p = prod.Price;
            decimal d = prod.Skidka;
            decimal final = p * (1 - d / 100);
            if (d > 0)
            {
                Price.Content = final;
                OldPrice.Text = p.ToString();
                OldPrice.TextDecorations = TextDecorations.Strikethrough;
            }
            if (d > 15)
            {
                BACK.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#9900FF"));
            }
            if (prod.Kolvo == 0)
            {
                Kolvo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#3377FF"));
            }
            

        }
        private void EditTovarClick(object sender, RoutedEventArgs e)
        {

        }


        private void DeleteTovarClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Точно удалить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                ((MainWindow)Window.GetWindow(this)).DeleteProduct(prodId);
        }
    }
}