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
    class cMasalar
    {
        #region Fields
        private int _ID;
        private int _KAPASITE;
        private int _SERVISTURU;
        private int _DURUM;
        private int _ONAY;
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

        public int KAPASITE
        {
            get
            {
                return _KAPASITE;
            }

            set
            {
                _KAPASITE = value;
            }
        }

        public int SERVISTURU
        {
            get
            {
                return _SERVISTURU;
            }

            set
            {
                _SERVISTURU = value;
            }
        }

        public int DURUM
        {
            get
            {
                return _DURUM;
            }

            set
            {
                _DURUM = value;
            }
        }

        public int ONAY
        {
            get
            {
                return _ONAY;
            }

            set
            {
                _ONAY = value;
            }
        }
        #endregion

        cGenel gnl = new cGenel();
        public string SessionSum(int state)
        {
            string dt = "";
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            
            string query = "Select \"TARIH\",\"MASAID\" FROM  \"adisyon\" Right Join \"masalar\" on \"adisyon\".\"MASAID\"=\"masalar\".\"ID\" Where \"masalar\".\"DURUM\"=@durum and \"adisyon\".\"DURUM\"=0";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            NpgsqlDataReader dr = null;
            cmd.Parameters.AddWithValue("@durum", SqlDbType.Int).Value = state;
            
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    dt = Convert.ToDateTime(dr["Tarih"]).ToString();
                }
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            finally
            {
                dr.Close();
                con.Dispose();
                con.Close();
            }
            return dt;
        }

        public int TableGetByNumber(string TableValue)
        {
            string aa = TableValue;
            int length = aa.Length;

            return Convert.ToInt32(aa.Substring(length - 1, 1));
        }

        public bool TableGetByState(int ButtonName, int state)
        {
            bool result = false;
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            NpgsqlCommand cmd = new NpgsqlCommand("select \"DURUM\" From \"masalar\" where \"ID\"=@TableId and \"DURUM\"=@state", con);

            cmd.Parameters.AddWithValue("@TableId", SqlDbType.Int).Value = ButtonName;
            cmd.Parameters.AddWithValue("@state", SqlDbType.Int).Value = state;
            try
            {
                if(con.State==ConnectionState.Closed)
                {
                    con.Open();

                }
                result = Convert.ToBoolean(cmd.ExecuteScalar());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
            }
            finally
            {
                con.Dispose();
                con.Close();
            }
            return result;
        }

        public void setChangeTableState(string ButonName, int state)
        {
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            NpgsqlCommand cmd = new NpgsqlCommand("Update \"masalar\" Set \"DURUM\"=@Durum where \"ID\"=@MasaNo", con);


            if (con.State == ConnectionState.Closed)
            {
                con.Open();

            }

            string aa = ButonName;
            int uzunluk = aa.Length;
            cmd.Parameters.AddWithValue("@Durum", SqlDbType.Int).Value = state;
            cmd.Parameters.AddWithValue("@MasaNo", SqlDbType.Int).Value = aa.Substring(uzunluk - 1, 1);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }


    }
}
