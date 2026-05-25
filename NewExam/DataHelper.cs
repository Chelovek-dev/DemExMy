using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.IsisMtt;
using System.Data;
using System.Net.Http.Headers;

namespace NewExam
{
    public class DataHelper
    {
        string conn = "Server=localhost;DataBase=Krossovki;Uid=root;Pwd=;";

        public DataTable GetKrossovki()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT T.Id, T.Name, T.Price, T.Skidka, T.Kolvo, T.Foto, K.Kategory, PO.Postavshik " +
                "FROM Tovar T " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                "ORDER BY T.Id";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
                a.Fill(dt);
            return dt;
        }
        public DataTable OrdersWindow()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT * FROM Zakaz";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
                a.Fill(dt);
            return dt;

        }
        public DataTable SearchKrossovki(string searchTXT, string PostavshikCMB)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT T.Id, T.Name, T.Price, T.Skidka, T.Kolvo, T.Foto, K.Kategory, PO.Postavshik " +
                "FROM Tovar T " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                $" WHERE (T.Name LIKE '%{searchTXT}%' OR K.Kategory LIKE '%{searchTXT}%' OR PO.Postavshik LIKE '%{searchTXT}%') ";
                
            if(PostavshikCMB != "Все поставщики")
                sql += $" AND PO.Postavshik = '{PostavshikCMB}' ";
            sql += " ORDER BY T.Id ";

            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
            {
                a.Fill(dt);
            }
            return dt;
        }
        public DataTable GetPostavshik()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Postavshik FROM Postavshik";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
                a.Fill(dt);
            return dt;
        }
        public int DeleteProduct(int prodId)
        {
            string sql = $"SELECT COUNT(*) FROM Sostav WHERE Tovar_Id = {prodId}";
            using (MySqlConnection c = new MySqlConnection(conn))
            {
                c.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, c))
                {
                    long count = (long)cmd.ExecuteScalar();
                    if(count > 0)
                        return -1;
                }
                string deletesql = $"DELETE FROM Tovar WHERE Id = {prodId}";
                using (MySqlCommand cmd = new MySqlCommand(deletesql, c))
                    return cmd.ExecuteNonQuery();
            }
        }
    }
}
