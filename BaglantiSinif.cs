using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace Kutuphane_Otomasyonu
{
    class BaglantiSinif
    {
        public string Adres = File.ReadAllText(@"C:\Test.txt");
    }
}
