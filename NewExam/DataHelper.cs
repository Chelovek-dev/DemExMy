using MySql.Data.MySqlClient;
using System.Data;

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
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "ORDER BY T.Id";
            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn)) 
                a.Fill(dt);
            return dt;
        }
        public DataTable SearchKrossovki(string searchTXT, string PostavshikCMB)
        {
            DataTable dt = new DataTable();
            string sql = "SELECT T.Id, T.Name, T.Price, T.Skidka, T.Kolvo, T.Foto, K.Kategory, PO.Postavshik " +
                "FROM Tovar T " +
                "LEFT JOIN Kategory K ON T.Kategory = K.Id " +
                "LEFT JOIN Postavshik PO ON T.Postavshik = PO.Id " +
                "WHERE (T.Name LIKE @search OR K.Kategory LIKE @search OR PO.Postavshik LIKE @search) ";
            if (PostavshikCMB != "Все поставщики")
                sql += " AND PO.Postavshik = @postavshik ";

            using (MySqlDataAdapter a = new MySqlDataAdapter(sql, conn))
            {
                a.SelectCommand.Parameters.AddWithValue("@search", "%" + searchTXT + "%");
                if (PostavshikCMB != "Все поставщики")
                    a.SelectCommand.Parameters.AddWithValue("@postavshik", PostavshikCMB);
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
        //public int DeleteKrossovki(int productId)
        //{
        //    // Проверяем, есть ли товар в заказах
        //    string sql = "SELECT COUNT(*) FROM Sostav WHERE Tovar_Id = @id";
        //    using (MySqlConnection c = new MySqlConnection(conn))
        //    {
        //        using (MySqlCommand cmd = new MySqlCommand(sql, c))
        //        {
        //            cmd.Parameters.AddWithValue("@id", productId);
        //            int count = (int)cmd.ExecuteScalar();

        //            if (count > 0)
        //            {
        //                return -1;  // товар в заказах, нельзя удалить
        //            }
        //        }
        //        c.Open();



        //        string deleteSql = "DELETE FROM Tovar WHERE Id = @id";
        //        using (MySqlCommand cmd = new MySqlCommand(deleteSql, c))
        //        {
        //            cmd.Parameters.AddWithValue("@id", productId);
        //            return cmd.ExecuteNonQuery();
        //        }
        //    }
        //}
        public int DeleteKrossovki(int productId)
        {
            string sql = "SELECT COUNT(*) FROM Sostav WHERE Tovar_Id = @id";
            using(MySqlConnection c = new MySqlConnection(conn))
            {
                c.Open();

                using (MySqlCommand cmd = new MySqlCommand(sql, c))
                {
                    cmd.Parameters.AddWithValue("@id", productId);

                    long count = (long)cmd.ExecuteScalar();
                    if (count > 0)
                        return -1;
                }

                string deletesql = "DELETE FROM Tovar WHERE Id = @id";
                using (MySqlCommand cmd = new MySqlCommand(deletesql, c))
                {
                    cmd.Parameters.AddWithValue("@id", productId);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
    }
}