using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gelir_Gider_Takip_Otomasyonu
{
    public partial class LoginForm : Form
    {

        public string kullaniciTipi = "";
        public LoginForm()
        {
            InitializeComponent();

            txtSifre.UseSystemPasswordChar = false;

            txtSifre.Text = "Şifre";

            txtSifre.ForeColor = Color.Gray;

            this.StartPosition =
                FormStartPosition.CenterScreen;

            this.FormBorderStyle =
                FormBorderStyle.FixedSingle;

            this.MaximizeBox = false;

            txtSifre.UseSystemPasswordChar = true;

            btnGiris.FlatStyle = FlatStyle.Flat;

            btnGiris.FlatAppearance.BorderSize = 0;


            txtKullaniciAdi.Text = "Kullanıcı Adı";
            txtKullaniciAdi.ForeColor = Color.Gray;


        }

        private void btnGiris_Click_1(object sender, EventArgs e)
        {
            // ADMIN GİRİŞİ

            if (txtKullaniciAdi.Text == "admin" &&
                txtSifre.Text == "123")
            {
                Form1 frm = new Form1();

                frm.kullaniciTipi = "admin";

                frm.Show();

                this.Hide();
            }

            // NORMAL KULLANICI GİRİŞİ

            else if (txtKullaniciAdi.Text == "kullanici" &&
                     txtSifre.Text == "123")
            {
                Form1 frm = new Form1();

                frm.kullaniciTipi = "kullanici";

                frm.Show();

                this.Hide();
            }

            // HATALI GİRİŞ
            else
            {
                MessageBox.Show(
                "Kullanıcı adı veya şifre yanlış!");
            }
        }

        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre")
            {
                txtSifre.Text = "";

                txtSifre.ForeColor = Color.Black;

                txtSifre.UseSystemPasswordChar = true;
            }
        }

        private void groupBox1_Leave(object sender, EventArgs e)
        {
            if (txtSifre.Text == "")
            {
                txtSifre.UseSystemPasswordChar = false;

                txtSifre.Text = "Şifre";

                txtSifre.ForeColor = Color.Gray;
            }
        }

        private void txtSifre_Enter(object sender, EventArgs e)
        {
            if (txtSifre.Text == "Şifre")
            {
                txtSifre.Text = "";

                txtSifre.ForeColor = Color.Black;

                txtSifre.PasswordChar = '*';
            }
        }

        private void txtSifre_Leave(object sender, EventArgs e)
        {
            if (txtSifre.Text.Trim() == "")
            {
                txtSifre.PasswordChar = '\0';

                txtSifre.Text = "Şifre";

                txtSifre.ForeColor = Color.Gray;
            }
        }
    }
}
