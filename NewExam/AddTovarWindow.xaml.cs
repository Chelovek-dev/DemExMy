using System.Windows;

namespace NewExam
{
    /// <summary>
    /// Логика взаимодействия для AddTovarWindow.xaml
    /// </summary>
    public partial class AddTovarWindow : Window
    {
        public AddTovarWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
