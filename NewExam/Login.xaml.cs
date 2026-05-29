using MySql.Data.MySqlClient;
using System.Windows;
using UIAutomationClientsideProviders;

namespace NewExam
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }



        private void GuestBTN(object sender, RoutedEventArgs e)
        {
            new MainWindow("Гость", "guest").Show();
            this.Close();
        }

        private void LoginBTN(object sender, RoutedEventArgs e)
        {
            try
            {
                string conn = "Server=localhost;DataBase=Krossovki;Uid=root;Pwd=;";
                string sql = $"SELECT U.FIO, R.Role " +
                            $"FROM Users U " +
                            $"LEFT JOIN Role R ON U.Role = R.Id " +
                            $"WHERE Login = '{LoginTXT.Text}' AND Password = '{PasswordTXT.Text}'";
                using (MySqlConnection c = new MySqlConnection(conn))
                {
                    c.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, c))
                    {
                        var reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string FIO = reader["FIO"].ToString();
                            string Role = reader["Role"].ToString();
                            new MainWindow(FIO, Role).Show();
                            this.Close();
                        }
                        else
                        {
                            lblError.Content = "Неправильно логин или пароль";
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Ошибка подключения к БД!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}