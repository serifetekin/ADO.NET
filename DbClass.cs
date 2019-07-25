using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ADO.NET
{
    class DbClass
    {
        SqlConnection cnn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;initial catalog=ISKUR;Integrated security=true");
        public DataTable TabloGetir(string sql, params SqlParameter[] prms)
        {
            // komut oluşturup connection'a bağlanıyoruz.
            SqlCommand cmd = new SqlCommand(sql, cnn); // komut bir connection'a bağlı olmak zorunda.
                                                       // veri taşıma işlemlerini yapan bir adaptor oluşturuyoruz.

            if (prms != null) cmd.Parameters.AddRange(prms);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable(); // dataadaptor komutu kullnaır, fill de dataadaptoru kullanır.
            da.Fill(dt); // sql cümlesini çalıştırıp, dt tablosuna doldurur.
            return dt;

        }
        public void SqlCalistir(string sql, params SqlParameter[] prms)
        {
            SqlCommand cmd = new SqlCommand(sql, cnn);

            if (prms != null) cmd.Parameters.AddRange(prms);

            if (cnn.State == ConnectionState.Closed)
            {
                cnn.Open();
            }
            cmd.ExecuteNonQuery();
            cnn.Close();
        }
    }
}
