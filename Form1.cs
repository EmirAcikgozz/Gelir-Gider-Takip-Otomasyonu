using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace Gelir_Gider_Takip_Otomasyonu
{
    public partial class Form1 : Form
    {
        public string kullaniciTipi = "";

        public Form1()
        {
            InitializeComponent();

            TabloOlustur();

            VerileriListele();

            dataGridView2.EnableHeadersVisualStyles = false;

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor =
                Color.RoyalBlue;

            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dataGridView2.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView2.ColumnHeadersHeight = 35;

            dataGridView2.DefaultCellStyle.SelectionBackColor =
                Color.DodgerBlue;

            dataGridView2.DefaultCellStyle.SelectionForeColor =
                Color.White;
        }
        SQLiteConnection baglanti =
        new SQLiteConnection(
        @"Data Source=|DataDirectory|\gelirgider.db");


        void TabloOlustur()
        {
            baglanti.Open();

            string sql =
            "CREATE TABLE IF NOT EXISTS Hareketler (" +
            "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
            "Tur TEXT," +
            "Kategori TEXT," +
            "Aciklama TEXT," +
            "Tutar REAL)";
            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            komut.ExecuteNonQuery();

            baglanti.Close();
        }

        void VerileriListele()
        {
            baglanti.Open();

            string sql =
            "SELECT * FROM Hareketler";

            SQLiteDataAdapter da =
            new SQLiteDataAdapter(sql, baglanti);

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView2.DataSource = dt;

            baglanti.Close();

            ToplamHesapla();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtTur.Text == "" ||
                txtKategori.Text == "" ||
                txtAciklama.Text == "" ||
                txtTutar.Text == "")
            {
                MessageBox.Show("Boş alan bırakmayınız!");

                return;
            }
            double tutar;

            if (!double.TryParse(txtTutar.Text, out tutar))
            {
                MessageBox.Show("Geçerli bir tutar giriniz!");

                return;
            }

            if (tutar <= 0)
            {
                MessageBox.Show("Tutar 0'dan büyük olmalıdır!");

                return;
            }

            baglanti.Open();

            string sql =
            "INSERT INTO Hareketler " +
            "(Tur,Kategori,Aciklama,Tutar) " +
            "VALUES (@tur,@kategori,@aciklama,@tutar)";

            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            komut.Parameters.AddWithValue(
                "@tur",
                txtTur.Text);

            komut.Parameters.AddWithValue(
                "@kategori",
                txtKategori.Text);

            komut.Parameters.AddWithValue(
                "@aciklama",
                txtAciklama.Text);

            komut.Parameters.AddWithValue(
                "@tutar",
                txtTutar.Text);


            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Kayıt eklendi");

            VerileriListele();
        }

        int secilenId = -1;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenId = Convert.ToInt32(dataGridView2.CurrentRow.Cells["Id"].Value);

            txtTur.Text =
            dataGridView2.CurrentRow.Cells["Tur"].Value.ToString();

            txtKategori.Text =
            dataGridView2.CurrentRow.Cells["Kategori"].Value.ToString();

            txtAciklama.Text =
            dataGridView2.CurrentRow.Cells["Aciklama"].Value.ToString();

            txtTutar.Text =
            dataGridView2.CurrentRow.Cells["Tutar"].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (secilenId == -1)
            {
                MessageBox.Show("Kayıt seçiniz!");

                return;
            }

            double tutar;

            if (!double.TryParse(txtTutar.Text, out tutar))
            {
                MessageBox.Show("Geçerli bir tutar giriniz!");

                return;
            }

            if (tutar <= 0)
            {
                MessageBox.Show("Tutar 0'dan büyük olmalıdır!");

                return;
            }

            baglanti.Open();

            string sql =
            "UPDATE Hareketler SET " +
            "Tur=@tur," +
            "Kategori=@kategori," +
            "Aciklama=@aciklama," +
            "Tutar=@tutar " +
            "WHERE Id=@id";

            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            komut.Parameters.AddWithValue("@tur", txtTur.Text);
            komut.Parameters.AddWithValue("@kategori", txtKategori.Text);
            komut.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
            komut.Parameters.AddWithValue("@tutar", txtTutar.Text);
            komut.Parameters.AddWithValue("@id", secilenId);

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Güncellendi");

            VerileriListele();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (secilenId == -1)
            {
                MessageBox.Show("Kayıt seçiniz!");

                return;
            }

            baglanti.Open();

            string sql =
            "DELETE FROM Hareketler WHERE Id=@id";

            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            komut.Parameters.AddWithValue("@id", secilenId);

            komut.ExecuteNonQuery();

            baglanti.Close();

            MessageBox.Show("Silindi");

            VerileriListele();
        }

        private void dataGridView2_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView2.Rows[e.RowIndex].Cells["Tur"].Value != null)
            {
                string tur =
                dataGridView2.Rows[e.RowIndex]
                .Cells["Tur"].Value.ToString();

                if (tur == "Gelir")
                {
                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.BackColor =
                    Color.Honeydew;

                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.ForeColor =
                    Color.Green;

                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.Font =
                    new Font("Segoe UI", 10, FontStyle.Bold);
                }

                else if (tur == "Gider")
                {
                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.BackColor = Color.MistyRose;


                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.ForeColor = Color.DarkRed;


                    dataGridView2.Rows[e.RowIndex]
                    .DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                VerileriListele();

                return;
            }

            baglanti.Open();

            string sql =
            "SELECT * FROM Hareketler " +
            "WHERE Kategori LIKE @arama";

            SQLiteDataAdapter da =
            new SQLiteDataAdapter(sql, baglanti);

            da.SelectCommand.Parameters.AddWithValue(
                "@arama",
                "%" + txtSearch.Text + "%");

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView2.DataSource = dt;

            baglanti.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Kategori ara...")
            {
                txtSearch.Text = "";
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
            if (txtSearch.Text == "Kategori ara...")
            {
                txtSearch.Text = "";
                txtSearch.ForeColor = Color.Black;
            }
        }

        private void txtTutar_Enter(object sender, EventArgs e)
        {
            if (txtTutar.Text == "Tutar giriniz")
            {
                txtTutar.Text = "";
                txtTutar.ForeColor = Color.Black;
            }
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            txtTur.SelectedIndex = -1;
            txtKategori.SelectedIndex = -1;

            txtAciklama.Clear();

            txtTutar.Clear();

            txtSearch.Text = "Kategori ara...";

            txtSearch.ForeColor = Color.Gray;

            secilenId = -1;

        }

        private void txtTutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) &&
       e.KeyChar != ',' &&
       e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void lblToplamGelir_Click(object sender, EventArgs e)
        {

        }

        private void lblToplamGider_Click(object sender, EventArgs e)
        {

        }


        void ToplamHesapla()
        {
            double gelir = 0;

            double gider = 0;

            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                if (row.Cells["Tur"].Value == null)
                    continue;

                string tur =
                row.Cells["Tur"].Value.ToString();

                double tutar =
                Convert.ToDouble(
                row.Cells["Tutar"].Value);

                if (tur == "Gelir")
                {
                    gelir += tutar;
                }

                else if (tur == "Gider")
                {
                    gider += tutar;
                }
            }

            lblToplamGelir.Text =
            "Toplam Gelir : " +
            gelir.ToString("N2") + " ₺";

            lblToplamGider.Text =
            "Toplam Gider : " +
            gider.ToString("N2") + " ₺";

            lblBakiye.Text =
            "Bakiye : " +
            (gelir - gider).ToString("N2") + " ₺";
        }

        private void btnRapor_Click(object sender, EventArgs e)
        {
            // Eğer admin değilse

            if (kullaniciTipi != "admin")
            {
                MessageBox.Show(
                "Bu alana sadece admin erişebilir!",
                "Yetki Hatası",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);

                return;
            }

            // Admin ise rapor formunu aç

            RaporForm frm = new RaporForm();

            frm.ShowDialog();
        }
    }
}
