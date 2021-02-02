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
    class cSiparis
    {
        cGenel gnl = new cGenel();

        #region Fields
        private int _Id;
        private int _adisyonID;
        private int _urunId;
        private int _adet;
        private int _masaId;
        #endregion
        #region Properties
        public int Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = value;
            }
        }

        public int AdisyonID
        {
            get
            {
                return _adisyonID;
            }

            set
            {
                _adisyonID = value;
            }
        }

        public int UrunId
        {
            get
            {
                return _urunId;
            }

            set
            {
                _urunId = value;
            }
        }

        public int Adet
        {
            get
            {
                return _adet;
            }

            set
            {
                _adet = value;
            }
        }

        public int MasaId
        {
            get
            {
                return _masaId;
            }

            set
            {
                _masaId = value;
            }
        } 
        #endregion

        //siparişleri getir
        public void getByOrder(ListView lv, int AdisyonId)
        {
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            string query = "select \"URUNAD\",\"FIYAT\", \"satislar\".\"URUNID\",\"satislar\".\"ADET\" FROM \"satislar\" Inner Join \"urunler\" on \"Satislar\".\"URUNID\"=\"Urunler\".\"ID\" Where \"ADISYON\"=@AdisyonId";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader dr = null;
            cmd.Parameters.AddWithValue("@AdisyonId", SqlDbType.Int).Value = AdisyonId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                dr = cmd.ExecuteReader();
                int sayac = 0;
                while (dr.Read()) ;
                {
                    lv.Items.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["ADET"].ToString());
                    lv.Items[sayac].SubItems.Add(dr["URUNID"].ToString());
                    lv.Items[sayac].SubItems.Add(Convert.ToString(Convert.ToDecimal(dr["FİYAT"]) * Convert.ToDecimal(dr["ADET"])));
                    lv.Items[sayac].SubItems.Add(dr["ID"].ToString());

                    sayac++;
                }
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
        }
        public bool setSaveOrder(cSiparis Bilgiler)
        {
            bool sonuc = false;

            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            NpgsqlCommand cmd = new NpgsqlCommand("Insert Into \"Satislar\"(\"ADISYONID\",\"URUNID\",\"ADET\",\"MASAID\") values (@AdisyonNo, @UrunId, @Adet, @masaId", con);
            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                cmd.Parameters.AddWithValue("@AdisyonNo", SqlDbType.Int).Value = Bilgiler._adisyonID;
                cmd.Parameters.AddWithValue("@UrunId", SqlDbType.Int).Value = Bilgiler._urunId;
                cmd.Parameters.AddWithValue("@Adet", SqlDbType.Int).Value = Bilgiler._adet;
                cmd.Parameters.AddWithValue("@masaId", SqlDbType.Int).Value = Bilgiler._masaId;
                sonuc = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {

                con.Dispose();
                con.Close();
            }
            return sonuc;
            
        }
        public void setDeleteOrder(int satisId)
        {
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            NpgsqlCommand cmd = new NpgsqlCommand("Delete From \"satislar\" Where ID=@SatisID", con);

            cmd.Parameters.AddWithValue("@SatisID", SqlDbType.Int).Value = satisId;

            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }
            cmd.ExecuteNonQuery();
            con.Dispose();
            con.Close();
        }

    }
}
