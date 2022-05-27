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
    public partial class UCtrlUyeler : UserControl
    {

        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter data;


        public UCtrlUyeler()
        {
            InitializeComponent();
        }


        BaglantiSinif bgl = new BaglantiSinif(); 


        void VeriGetir()
        {
            baglanti = new SqlConnection(bgl.Adres);
            baglanti.Open();
            data = new SqlDataAdapter("SELECT *FROM Uyeler", baglanti);
            DataTable tablo = new DataTable();
            data.Fill(tablo);
            dataGVUye.DataSource = tablo;
            baglanti.Close();
        }


        void Temizle()
        {
            txtUyeAdi.Text = "";
            txtUyeSoyadi.Text = "";
            txtUyeTc.Text = "";
            txtUyeTel.Text = "";
        }


        private void btnUyeKaydet_Click(object sender, EventArgs e)
        {
           
            string sorgu = "INSERT INTO Uyeler(UyeAd,UyeSoyad,UyeTC,UyeTelefon) VALUES (@UyeAd,@UyeSoyad,@UyeTC,@UyeTelefon)";
            komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@UyeAd", txtUyeAdi.Text);
            komut.Parameters.AddWithValue("@UyeSoyad", txtUyeSoyadi.Text);
            komut.Parameters.AddWithValue("@UyeTC", txtUyeTc.Text);
            komut.Parameters.AddWithValue("@UyeTelefon", txtUyeTel.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Üye Kaydı başarılı bir şekilde oluşturuldu.");
            Temizle();
        }


        private void btnUyeSil_Click(object sender, EventArgs e)
        {
            string sorgu = "DELETE FROM Uyeler WHERE Uyeid = @Uyeid";
            komut = new SqlCommand(sorgu, baglanti);
            int silinecekId = Convert.ToInt32(dataGVUye.CurrentRow.Cells[0].Value);
            komut.Parameters.AddWithValue("@Uyeid", silinecekId);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Üye Kaydı başarılı bir şekilde silindi.");
            Temizle();
        }


        private void btnUyeGuncelle_Click(object sender, EventArgs e)
        {
            string sorgu = "UPDATE Uyeler SET UyeAd = @UyeAd, UyeSoyad = @UyeSoyad, UyeTC = @UyeTC, UyeTelefon = @UyeTelefon WHERE Uyeid = @Uyeid";
            komut = new SqlCommand(sorgu, baglanti);
            int guncellenecekId = Convert.ToInt32(dataGVUye.CurrentRow.Cells[0].Value);
            komut.Parameters.AddWithValue("@Uyeid", guncellenecekId);
            komut.Parameters.AddWithValue("@UyeAd", txtUyeAdi.Text);
            komut.Parameters.AddWithValue("@UyeSoyad", txtUyeSoyadi.Text);
            komut.Parameters.AddWithValue("@UyeTC", txtUyeTc.Text);
            komut.Parameters.AddWithValue("@UyeTelefon", txtUyeTel.Text);
            baglanti.Open();
            komut.ExecuteNonQuery();
            baglanti.Close();
            VeriGetir();
            MessageBox.Show("Üye Kaydı başarılı bir şekilde güncelleştirildi.");
            Temizle();
        }


        private void UCtrlUyeler_Load(object sender, EventArgs e)
        {
            VeriGetir();
        }


        private void dataGVUye_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            txtUyeAdi.Text = dataGVUye.CurrentRow.Cells[1].Value.ToString();
            txtUyeSoyadi.Text = dataGVUye.CurrentRow.Cells[2].Value.ToString();
            txtUyeTc.Text = dataGVUye.CurrentRow.Cells[3].Value.ToString();
            txtUyeTel.Text = dataGVUye.CurrentRow.Cells[4].Value.ToString();
        }


        private void btnUyeAra_Click(object sender, EventArgs e)
        {
            if(txtUyeAra.Text != "")
            {
                string sorgu = "SELECT *FROM Uyeler WHERE UyeTC = @UyeTC";
                komut = new SqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@UyeTC", txtUyeAra.Text);

                data = new SqlDataAdapter(komut);
                DataTable tablo = new DataTable();
                data.Fill(tablo);
                

                if (tablo.Rows.Count > 0)
                {
                    dataGVUye.DataSource = tablo;
                    baglanti.Close();
                }

                else
                {
                    MessageBox.Show("Aradığınız üye bulunamamaktadır. Lütfen istenilen bilgiyi eksiksiz ve doğru giriniz.");
                    txtUyeAra.Text = "";
                }
            }

            

        }


        private void txtUyeAra_TextChanged(object sender, EventArgs e)
        {
            if (txtUyeAra.Text == "")
                VeriGetir();
        }

        private void UCtrlUyeler_Leave(object sender, EventArgs e)
        {
            Temizle();
            txtUyeAra.Text = "";
        }
    }
}
