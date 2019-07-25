using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // sql servera direkt ulaşan dlli yani kütüphaney barındırır.

namespace ADO.NET // 04.07.2019
{
    public partial class Form1 : Form
    {
        DbClass db = new DbClass();
        SqlConnection cnn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;initial catalog=ISKUR;user id=sa;password=123");
        int satir = -1;
        public Form1()
        {            
            InitializeComponent();
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e) // program çalıştığında ilk yüklenendir.
        {
            GridDoldur();
           
        }

        private void Button2_Click(object sender, EventArgs e)
        { // kaydet
            if (txtKATEGORI_ADI.Text.Trim()=="")
            {
                errorProvider1.SetError(txtKATEGORI_ADI, "Kategori Adı Giriniz");
                return;
            }else
            {
                errorProvider1.SetError(txtKATEGORI_ADI, "");
            }
            SqlParameter prm1 = new SqlParameter("@p1", txtKATEGORI_ADI.Text);
            SqlParameter prm2 = new SqlParameter("@p2", txtKATEGORI_REFNO.Text);

            string sql = "";
            if (txtKATEGORI_REFNO.Text == "")
            {
                sql = "INSERT INTO KATEGORI(KATEGORI_ADI) VALUES(@p1)";

            }else
            {
                sql = "UPDATE KATEGORI SET KATEGORI_ADI =@p1 WHERE KATEGORI_REFNO=@p2";
            }
            db.SqlCalistir(sql, prm1, prm2);               
            GridDoldur();

        }
        void GridDoldur()
        {
            DataTable dt = db.TabloGetir("SELECT * FROM KATEGORI");

            dataGridView1.DataSource = dt; // veri ızgarasının kaynağı dt set eder. ekranda otomatik olarak gösterir.
        }
        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void TxtKATEGORI_ADI_TextChanged(object sender, EventArgs e)
        {

        }

        private void DataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            satir = e.RowIndex;// hangi satıra girerse girsin o satırın ne olduğunu bulur.
            if(satir> -1)
            {
                txtKATEGORI_REFNO.Text = dataGridView1.Rows[satir].Cells["KATEGORI_REFNO"].Value.ToString();
                txtKATEGORI_ADI.Text = dataGridView1.Rows[satir].Cells["KATEGORI_ADI"].Value.ToString();

            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {// yeni
            txtKATEGORI_REFNO.Text = "";
            txtKATEGORI_ADI.Text = "";

        }

        private void Button3_Click(object sender, EventArgs e)
        {// vazgeç. son yaptığı değişikliği kaydetmesi ve ona ulaşabilmesi lazım.
            if (satir > -1)
            {
                txtKATEGORI_REFNO.Text = dataGridView1.Rows[satir].Cells["KATEGORI_REFNO"].Value.ToString();
                txtKATEGORI_ADI.Text = dataGridView1.Rows[satir].Cells["KATEGORI_ADI"].Value.ToString();

            }
        }

        private void Button4_Click(object sender, EventArgs e)
        { // sil

            string sql = "";
            if (txtKATEGORI_REFNO.Text != "")
            {
                sql = "DELETE FROM KATEGORI WHERE KATEGORI_REFNO=@p2";
                SqlParameter prm2 = new SqlParameter("@p2", txtKATEGORI_REFNO.Text);
                db.SqlCalistir(sql, prm2);
                GridDoldur();
            }
        }

        
    }
}
