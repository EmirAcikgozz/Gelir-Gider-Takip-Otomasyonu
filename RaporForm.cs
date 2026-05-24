using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Gelir_Gider_Takip_Otomasyonu
{
    public partial class RaporForm : Form
    {

        SQLiteConnection baglanti =
       new SQLiteConnection(
       @"Data Source=|DataDirectory|\gelirgider.db");
        public RaporForm()
        {
            InitializeComponent();

            VerileriListele();

            ToplamHesapla();

            GridAyar();

            SatirRenklendir();

            GrafikOlustur();

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

            dataGridView1.DataSource = dt;

            SatirRenklendir();

            baglanti.Close();
        }

        void ToplamHesapla()
        {
            double gelir = 0;

            double gider = 0;

            baglanti.Open();

            string sql =
            "SELECT * FROM Hareketler";

            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            SQLiteDataReader dr =
            komut.ExecuteReader();

            while (dr.Read())
            {
                string tur = dr["Tur"].ToString();

                double tutar =
                Convert.ToDouble(dr["Tutar"]);

                if (tur == "Gelir")
                {
                    gelir += tutar;
                }

                else if (tur == "Gider")
                {
                    gider += tutar;
                }
            }

            baglanti.Close();

            lblGelir.Text =
            "Toplam Gelir : " +
            gelir.ToString("N2") + " ₺";

            lblGider.Text =
            "Toplam Gider : " +
            gider.ToString("N2") + " ₺";

            lblBakiye.Text =
            "Bakiye : " +
            (gelir - gider).ToString("N2") + " ₺";
        }

        void GridAyar()
        {
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dataGridView1.BackgroundColor =
                Color.White;

            dataGridView1.RowHeadersVisible = false;

            dataGridView1.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dataGridView1.EnableHeadersVisualStyles = false;

            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor =
                Color.RoyalBlue;

            dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.White;

            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 10, FontStyle.Bold);

            dataGridView1.ColumnHeadersHeight = 35;

        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SatirRenklendir()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[1].Value == null)
                    continue;

                string tur =
                row.Cells[1].Value.ToString();

                if (tur == "Gelir")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.Honeydew;

                    row.DefaultCellStyle.ForeColor =
                        Color.Green;
                }

                else if (tur == "Gider")
                {
                    row.DefaultCellStyle.BackColor =
                        Color.MistyRose;

                    row.DefaultCellStyle.ForeColor =
                        Color.DarkRed;
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            string tur =
            dataGridView1.Rows[e.RowIndex]
            .Cells[1].Value?.ToString();

            if (tur == "Gelir")
            {
                dataGridView1.Rows[e.RowIndex]
                .DefaultCellStyle.BackColor =
                Color.Honeydew;

                dataGridView1.Rows[e.RowIndex]
                .DefaultCellStyle.ForeColor =
                Color.Green;
            }

            else if (tur == "Gider")
            {
                dataGridView1.Rows[e.RowIndex]
                .DefaultCellStyle.BackColor =
                Color.MistyRose;

                dataGridView1.Rows[e.RowIndex]
                .DefaultCellStyle.ForeColor =
                Color.DarkRed;
            }
        }

        void GrafikOlustur()
        {
            double gelir = 0;

            double gider = 0;

            baglanti.Open();

            string sql =
            "SELECT * FROM Hareketler";

            SQLiteCommand komut =
            new SQLiteCommand(sql, baglanti);

            SQLiteDataReader dr =
            komut.ExecuteReader();

            while (dr.Read())
            {
                string tur = dr["Tur"].ToString();

                double tutar =
                Convert.ToDouble(dr["Tutar"]);

                if (tur == "Gelir")
                {
                    gelir += tutar;
                }

                else if (tur == "Gider")
                {
                    gider += tutar;
                }
            }

            baglanti.Close();

            chart1.Series.Clear();

            chart1.Titles.Clear();

            chart1.Titles.Add("Gelir / Gider Grafiği");

            Series s = new Series();

            s.ChartType = SeriesChartType.Pie;

            s.Points.AddXY("Gelir", gelir);

            s.Points.AddXY("Gider", gider);

            chart1.Series.Add(s);
        }
    }
}
