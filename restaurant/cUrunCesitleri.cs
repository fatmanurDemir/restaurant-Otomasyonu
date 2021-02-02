using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Npgsql;
namespace restaurant
{
    class cUrunCesitleri
    {
        cGenel gnl = new cGenel();

        #region Fields
        private int _UrunTurNo;
        private int _KategoriAd;
        private string _Aciklama;
        #endregion
        #region Properties
        public int UrunTurNo
        {
            get
            {
                return _UrunTurNo;
            }

            set
            {
                _UrunTurNo = value;
            }
        }

        public int KategoriAd
        {
            get
            {
                return _KategoriAd;
            }

            set
            {
                _KategoriAd = value;
            }
        }

        public string Aciklama
        {
            get
            {
                return _Aciklama;
            }

            set
            {
                _Aciklama = value;
            }
        }
        #endregion

        public void getByProductTypes(ListView Cesitler, Button btn)
        {
            Cesitler.Items.Clear();
            NpgsqlConnection conn = new NpgsqlConnection(gnl.connString);
            string query = "Select \"URUNAD\",\"FIYAT\"::int4,\"urunler\".\"ID\"::int4 From \"kategoriler\" Inner Join \"urunler\" on \"kategoriler\".\"ID\"::int4=\"urunler\".\"KATEGORIID\"::int4 Where \"urunler\".\"KATEGORIID\"::int4=@KATEGORIID";
            //string query = "Select \"URUNAD\"::int4,\"FIYAT\" From \"kategoriler\", \"urunler\" where  \"kategoriler\".\"ID\"=\"urunler\".\"KATEGORIID\" and  \"urunler\".\"KATEGORIID\"=@KATEGORIID";
            NpgsqlCommand cmd = new NpgsqlCommand(query, conn);

            string aa = btn.Name;
            int uzunluk = aa.Length;

            cmd.Parameters.AddWithValue("@KATEGORIID", SqlDbType.Int).Value = aa.Substring(uzunluk - 1, 1);
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();

            }
            NpgsqlDataReader dr = cmd.ExecuteReader();
            int i = 0;
            while (dr.Read()) 
            {
                Cesitler.Items.Add(dr["URUNAD"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["FIYAT"].ToString());
                Cesitler.Items[i].SubItems.Add(dr["ID"].ToString());
                i++;

            }
            dr.Close();
            conn.Dispose();
            conn.Close();
        }
    }
}
