using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Kutuphane_Otomasyonu
{
    public partial class FrmGiris : Form
    {

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter data;
        

        public FrmGiris()
        {
            InitializeComponent();
        }

        BaglantiSinif bgl = new BaglantiSinif();


        void Temizle()
        {
            txtKullaniciAdi.Text = "";
            txtSifre.Text = "";
        }

        private void txtKullaniciAdi_Click(object sender, EventArgs e) //Kullanıcı mesaj kutusuna tıklayınca rengin değişmesini sağlar
        {
            txtKullaniciAdi.BackColor = Color.Snow;
            panel1G.BackColor = Color.Snow;
            panel2G.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5EFF5");
            txtSifre.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5EFF5");
        }

        private void txtSifre_Click(object sender, EventArgs e) //şifre mesaj kutusuna tıklayınca rengin değişmesini sağlar
        {
            txtSifre.BackColor = Color.Snow;
            panel2G.BackColor = Color.Snow;
            panel1G.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5EFF5");
            txtKullaniciAdi.BackColor = System.Drawing.ColorTranslator.FromHtml("#F5EFF5");

        }

        private void btnKapat_Click(object sender, EventArgs e) //tıklayınca uygulamayı sonlandırır
        {
            Application.Exit();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e) //tıklayınca şifrenin gözükmesini sağlar
        {
            txtSifre.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            txtSifre.UseSystemPasswordChar = true;
        }

        private void FrmGiris_Load(object sender, EventArgs e) // panel arkaplan rengini transparan yapar.
        {
            panel4G.BackColor = Color.FromArgb(100, 0, 0, 0);

        }

        
        private void btnGiris_Click(object sender, EventArgs e)
        {

            baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            string sorgu = "SELECT *FROM Personeller WHERE PersonelKullaniciAd = @PersonelKullaniciAd COLLATE SQL_Latin1_General_CP1_CS_AS AND PersonelSifre = @PersonelSifre COLLATE SQL_Latin1_General_CP1_CS_AS ";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@PersonelKullaniciAd", txtKullaniciAdi.Text.Trim());
            komut.Parameters.AddWithValue("@PersonelSifre", txtSifre.Text.Trim());
            DataTable tablo = new DataTable();
            data = new SqlDataAdapter(komut);
            data.Fill(tablo);

            if (tablo.Rows.Count > 0)
            {
                this.Hide();
                string user = txtKullaniciAdi.Text;
                FrmAnasayfa obj = new FrmAnasayfa(char.ToUpper(user[0]) + user.Substring(1));  //kullanıcı adını anasayfanın üstüne yazdırır.
                obj.Show();
            }

            else
            {
                baglanti = new SqlConnection(bgl.Adres);
                MessageBox.Show("Hatalı giriş yaptınız. Lütfen tekrar deneyin.");
            }
           


            // string girilen_ad = txtKullaniciAdi.Text;
            //string girilen_sifre = txtSifre.Text;
            

            //if(girilen_ad == "yeliz" && girilen_sifre == "123")
            //{
              //  this.Hide();
                //string user = txtKullaniciAdi.Text;
                //FrmAnasayfa obj = new FrmAnasayfa(char.ToUpper(user[0]) + user.Substring(1));  //kullanıcı adını anasayfanın üstüne yazdırır.
               // obj.Show();
           // }
                       

        }

        

       
    }
}
