using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane_Otomasyonu
{
    public partial class FrmAnasayfa : Form
    {
        public FrmAnasayfa(string name) //kullanıcı adını anasayfa üstüne yazdırır.
        {
            
            InitializeComponent();
            lblKullaniciAdi.Text = name;



        }


        private void btnCikis_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            FrmGiris obj = new FrmGiris();
            obj.Show();
        }


        private void btnKitap_Click(object sender, EventArgs e)
        {
            
        }


        private void btnUye_Click(object sender, EventArgs e)
        {
         
        }


        private void btnEmanet_Click(object sender, EventArgs e)
        {
           
        }


        private void btnTeslim_Click(object sender, EventArgs e)
        {
           
        }
    }
}
