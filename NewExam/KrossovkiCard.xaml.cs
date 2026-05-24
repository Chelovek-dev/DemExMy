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
            prodId = prod.Id;
            Name.Content = prod.Name + prod.Kategory;
            Skidka.Content = prod.Skidka;
            Kolvo.Content = prod.Kolvo;
            Postavshik.Content = prod.Postavshik;
            IMG.Source = new BitmapImage(new Uri("Images/" + prod.Foto, UriKind.Relative));
            if (prod.Foto == "")
                IMG.Source = new BitmapImage(new Uri("Images/picture.png", UriKind.Relative));
            decimal p = prod.Price;
            decimal d = prod.Skidka;
            decimal final = p * (1 - d / 100);
            if (d > 0)
            {
                OldPrice.Text = p.ToString();
                Price.Content = final.ToString();
                OldPrice.TextDecorations = TextDecorations.Strikethrough;
            }
            if (d > 15)
                BACK.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF00FF"));
            if (prod.Kolvo == 0)
                Kolvo.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4355FF"));
            if (role == "admin")
            {
                DeleteTovarBTN.Visibility = Visibility.Visible;
                EditTovarBTN.Visibility = Visibility.Visible;
            }
        }
        private void EditTovarClick(object sender, RoutedEventArgs e)
        {

        }

        //private void DeleteTovarClick(object sender, RoutedEventArgs e)
        //{
        //    if (MessageBox.Show("Удалить товар?", "Подтверждение",
        //        MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
        //    {
        //        Window parent = Window.GetWindow(this);
        //        if (parent is MainWindow mainWindow)
        //        {
        //            mainWindow.DeleteProduct(prodId);
        //        }
        //    }
        //}
        private void DeleteTovarClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить товар?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ((MainWindow)Window.GetWindow(this)).DeleteProduct(prodId);
            }
        }
    }
}