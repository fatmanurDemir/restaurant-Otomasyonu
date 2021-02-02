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
    class cPersonelHareketleri
    {

        cGenel gnl = new cGenel();
        #region Fields
        private int _ID;
        private int _personelId;
        private string _Islem;
        private DateTime _Tarih;
        private bool _Durum;
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

        public int PersonelId
        {
            get
            {
                return _personelId;
            }

            set
            {
                _personelId = value;
            }
        }

        public string Islem
        {
            get
            {
                return _Islem;
            }

            set
            {
                _Islem = value;
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

        public bool Durum
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
        #endregion

        public bool PersonelActionSave(cPersonelHareketleri ph)
        {
            bool result = false;
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            string query = "Insert into \"personelHareketleri\" (\"PERSONELID\",\"ISLEM\",\"TARIH\") Values (@personelId,@islem,@tarih)";     // "Insert Into personelHareketleri(PERSONELID,ISLEM,TARIH)Values(@personelId,@islem,@tarih)"
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            try
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
                
                cmd.Parameters.AddWithValue("@personelId", SqlDbType.Int).Value = ph._personelId;
                cmd.Parameters.AddWithValue("@islem", SqlDbType.VarChar).Value = ph._Islem;
                cmd.Parameters.AddWithValue("@tarih", SqlDbType.DateTime).Value = ph._Tarih;

                result =Convert.ToBoolean( cmd.ExecuteNonQuery());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }

            return result;
        }


    }
}
