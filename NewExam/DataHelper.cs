using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using Org.BouncyCastle.Asn1.IsisMtt;
using System.Data;
using System.Net.Http.Headers;
using System.Windows;

namespace NewExam
{
    public class DataHelper
    {
        string conn = "Server=localhost;DataBase=Krossovki;Uid=root;Pwd=;";

        public DataTable GetKrossovki()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT T.Id, K.Kategory, T.Name, T.Opisanie, PR.Proizvoditel, PO.Postavshik, T.Price, T.EdIzmer, T.Kolvo, T.Skidka, T.Foto " +
                "FROM Tovar T " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "LEFT JOIN Proizvoditel PR ON T.Proizvoditel = PR.Id " +
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                "ORDER BY T.Id";
            using(MySqlDataAdapter a = new MySqlDataAdapter(sql,conn))
                a.Fill(dt);
            return dt;
        }
        public DataTable SearchKrossovki(string searchTXT, string PostavshikCMB)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT T.Id, K.Kategory, T.Name, T.Opisanie, PR.Proizvoditel, PO.Postavshik, T.Price, T.EdIzmer, T.Kolvo, T.Skidka, T.Foto " +
                "FROM Tovar T " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "LEFT JOIN Proizvoditel PR ON T.Proizvoditel = PR.Id " +
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                $"WHERE (T.Name LIKE '%{searchTXT}%' OR T.Opisanie LIKE '%{searchTXT}%' OR K.Kategory LIKE '%{searchTXT}%' OR PO.Postavshik LIKE '%{searchTXT}%' OR PR.Proizvoditel LIKE '%{searchTXT}%') ";
            if(PostavshikCMB != "Все поставщики")
                sql += $" AND PO.Postavshik = '{PostavshikCMB}' ";
            sql += " ORDER BY T.Id ";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
                a.Fill(dt);
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
        public DataTable GetZakaz()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT Z.Id, Z.DateDostavki, Z.DateZakaza, S.Status, P.Street, P.Home, P.City " +
                "FROM Zakaz Z " +
                "LEFT JOIN PVZ P ON Z.PVZ = P.Id " +
                "LEFT JOIN Status S ON Z.Status = S.Id";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
                a.Fill(dt);
            return dt;
        }
        public int DeleteProduct(int prodId)
        {
            string sql = "SELECT COUNT(*) FROM Sostav WHERE Tovar_Id = " + prodId;

            using (MySqlConnection c = new MySqlConnection(conn))
            {
                c.Open();
                using(MySqlCommand cmd = new MySqlCommand(sql,c))
                {
                    long count = (long)cmd.ExecuteScalar();
                    if (count > 0)
                        return -1;
                }
                string delsql = $"DELETE FROM Tovar WHERE Id = {prodId}";
                using (MySqlCommand cmd = new MySqlCommand(delsql, c))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public int DeleteOrder(int OrdId)
        {
            using (MySqlConnection c = new MySqlConnection(conn))
            {
                c.Open();
                string deleteSostavSql = $"DELETE FROM Sostav WHERE Id = {OrdId}";
                using (MySqlCommand cmd = new MySqlCommand(deleteSostavSql, c))
                {
                    cmd.ExecuteNonQuery();
                }
                string delsql = $"DELETE FROM Zakaz WHERE Id = {OrdId}";
                using (MySqlCommand cmd = new MySqlCommand(delsql, c))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}