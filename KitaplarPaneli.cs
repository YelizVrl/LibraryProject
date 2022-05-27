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
    public partial class UCtrlKitaplar : UserControl
    {

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter data;

        public UCtrlKitaplar()
        {
            InitializeComponent();
        }


        BaglantiSinif bgl = new BaglantiSinif();


        void VeriGetir()
        {
            baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            data = new SqlDataAdapter("SELECT *FROM Kitaplar", baglanti);
            DataTable tablo = new DataTable();
            data.Fill(tablo);
            dataGVKitap.DataSource = tablo;
            baglanti.Close();
        }


        void Temizle()
        {
            txtKitapAdi.Text = "";
            txtYazar.Text = "";
            txtSayfaSayi.Text = "";
            txtTuru.Text = "";
            txtYayinevi.Text = "";
            txtBasimYil.Text = "";
        }

        private void UCtrlKitaplar_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }

        private void dataGVKitap_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtKitapAdi.Text = dataGVKitap.CurrentRow.Cells[1].Value.ToString();
            txtYazar.Text = dataGVKitap.CurrentRow.Cells[2].Value.ToString();
            txtSayfaSayi.Text = dataGVKitap.CurrentRow.Cells[3].Value.ToString();
            txtTuru.Text = dataGVKitap.CurrentRow.Cells[4].Value.ToString();
            txtYayinevi.Text = dataGVKitap.CurrentRow.Cells[5].Value.ToString();
            txtBasimYil.Text = dataGVKitap.CurrentRow.Cells[6].Value.ToString();

        }

        private void btnKitapKaydet_Click(object sender, EventArgs e)
        {
            string sorgu = "INSERT INTO Kitaplar(KitapAd,KitapYazar,KitapSayfa,KitapTuru,KitapYayinevi,KitapBasimyili) VALUES (@KitapAd,@KitapYazar,@KitapSayfa,@KitapTuru,@KitapYayinevi,@KitapBasimyili)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@KitapAd", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@KitapYazar", txtYazar.Text);
            komut.Parameters.AddWithValue("@KitapSayfa", txtSayfaSayi.Text);
            komut.Parameters.AddWithValue("@KitapTuru", txtTuru.Text);
            komut.Parameters.AddWithValue("@KitapYayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@KitapBasimyili", txtBasimYil.Text);

            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Kitap Kaydı başarılı bir şekilde oluşturuldu.");
            Temizle();
        }

        private void btnKitapGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Kitaplar SET KitapAd = @KitapAd, KitapYazar = @KitapYazar, KitapSayfa = @KitapSayfa, KitapTuru = @KitapTuru, KitapYayinevi = @KitapYayinevi, KitapBasimyili = @KitapBasimyili WHERE Kitapid = @Kitapid";
            komut = new SqlCommand(sorgu, baglanti);
            int guncellenecekId = Convert.ToInt32(dataGVKitap.CurrentRow.Cells[0].Value);
            komut.Parameters.AddWithValue("@Kitapid", guncellenecekId);
            komut.Parameters.AddWithValue("@KitapAd", txtKitapAdi.Text);
            komut.Parameters.AddWithValue("@KitapYazar", txtYazar.Text);
            komut.Parameters.AddWithValue("@KitapSayfa", txtSayfaSayi.Text);
            komut.Parameters.AddWithValue("@KitapTuru", txtTuru.Text);
            komut.Parameters.AddWithValue("@KitapYayinevi", txtYayinevi.Text);
            komut.Parameters.AddWithValue("@KitapBasimyili", txtBasimYil.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Kitap Kaydı başarılı bir şekilde güncelleştirildi.");
            Temizle();
        }

        private void btnKitapSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Kitaplar WHERE Kitapid = @Kitapid";
            komut = new SqlCommand(sorgu, baglanti);
            int silinecekId = Convert.ToInt32(dataGVKitap.CurrentRow.Cells[0].Value);
            komut.Parameters.AddWithValue("@Kitapid", silinecekId);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Kitap Kaydı başarılı bir şekilde silindi.");
            Temizle();
        }

        private void btnKitapAra_Click(object sender, EventArgs e)
        {
            if (txtKitapAra.Text != "")
            {
                string sorgu = "SELECT *FROM Kitaplar WHERE KitapAd = @KitapAd";
                komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@KitapAd", txtKitapAra.Text);

                data = new SqlDataAdapter(komut);
                DataTable tablo = new DataTable();
                data.Fill(tablo);
               

                if (tablo.Rows.Count > 0)
                {
                    dataGVKitap.DataSource = tablo;
                    baglanti.Close();
                }

                else
                {
                    MessageBox.Show("Aradığınız kitap bulunamamaktadır. Lütfen istenilen bilgiyi eksiksiz ve doğru giriniz.");
                    txtKitapAra.Text = "";
                }
            }
        }

        private void txtKitapAra_TextChanged(object sender, EventArgs e)
        {
            if (txtKitapAra.Text == "")
                VeriGetir();
        }

        private void UCtrlKitaplar_Leave(object sender, EventArgs e)
        {
            Temizle();
            txtKitapAra.Text = "";
        }

        
    }
}
