using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace restaurant
{
    class cAdisyon
    {
        cGenel gnl = new cGenel();


        #region Fields
        private int _ID;
        private int _ServisTurNo;
        private decimal _Tutar;
        private DateTime _Tarih;
        private int _Durum;
        private int _MasaId;
        private int PersonelId;
        #endregion
        #region Properties
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }

        public int ServisTurNo
        {
            get
            {
                return _ServisTurNo;
            }

            set
            {
                _ServisTurNo = value;
            }
        }

        public decimal Tutar
        {
            get
            {
                return _Tutar;
            }

            set
            {
                _Tutar = value;
            }
        }

        public DateTime Tarih
        {
            get
            {
                return _Tarih;
            }

            set
            {
                _Tarih = value;
            }
        }

        public int Durum
        {
            get
            {
                return _Durum;
            }

            set
            {
                _Durum = value;
            }
        }

        public int MasaId
        {
            get
            {
                return _MasaId;
            }

            set
            {
                _MasaId = value;
            }
        }

        public int PersonelId1
        {
            get
            {
                return PersonelId;
            }

            set
            {
                PersonelId = value;
            }
        }


        #endregion

        public int getByAddition(int MasaId)
        {
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            string query = "Select \"ID\" From \"adisyon\" Where \"MASAID\"=@masaId Order by \"ID\" desc limit 1";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);

            cmd.Parameters.AddWithValue("@MasaId", SqlDbType.Int).Value = MasaId;
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                MasaId = Convert.ToInt32(cmd.ExecuteScalar());
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
            return MasaId;

        }
        public bool setByAdditionel(cAdisyon Bilgiler)
        {
            bool sonuc = false;

            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            NpgsqlCommand cmd = new NpgsqlCommand("Insert Into \"adisyon\" (\"SERVISTURNO\",\"PERSONELID\",\"MASAID\",\"DURUM\") values (@ServisTurNo,@PersonelID, @MasaId, @Durum", con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }

                cmd.Parameters.AddWithValue("@ServisTurNo", SqlDbType.Int).Value = Bilgiler._ServisTurNo;

                //cmd.Parameters.AddWithValue("@Tarih", SqlDbType.DateTime).Value = Bilgiler.Tarih;
                cmd.Parameters.AddWithValue("@PersonelID", SqlDbType.Int).Value = Bilgiler.PersonelId1;
                cmd.Parameters.AddWithValue("@MasaId", SqlDbType.Int).Value = Bilgiler.MasaId;
                cmd.Parameters.AddWithValue("@Durum", SqlDbType.Bit).Value = 0;
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




    }
}
