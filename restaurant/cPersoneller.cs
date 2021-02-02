using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace restaurant
{

    class cPersoneller
    {
        cGenel gnl = new cGenel(); //nesne oluşturduk
        #region Fields
        private int _Id;
        private int _PersonelId;
        private int _PersonelGorevId;
        private string _PersonelAd;
        private string _PersonelSoyad;
        private string _PersonelParola;
        private string _PersonelKullaniciAdi;
        private bool _PersonelDurum;

        #endregion
        #region Properties
        public int PersonelId
        {
            get
            {
                return _PersonelId;
            }

            set
            {
                _PersonelId = value;
            }
        }
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
        public int PersonelGorevId
        {
            get
            {
                return _PersonelGorevId;
            }

            set
            {
                _PersonelGorevId = value;
            }
        }

        public string PersonelAd
        {
            get
            {
                return _PersonelAd;
            }

            set
            {
                _PersonelAd = value;
            }
        }

        public string PersonelSoyad
        {
            get
            {
                return _PersonelSoyad;
            }

            set
            {
                _PersonelSoyad = value;
            }
        }

        public string PersonelParola
        {
            get
            {
                return _PersonelParola;
            }

            set
            {
                _PersonelParola = value;
            }
        }

        public string PersonelKullaniciAdi
        {
            get
            {
                return _PersonelKullaniciAdi;
            }

            set
            {
                _PersonelKullaniciAdi = value;
            }
        }

        public bool PersonelDurum
        {
            get
            {
                return _PersonelDurum;
            }

            set
            {
                _PersonelDurum = value;
            }
        }
        #endregion
        public bool personelEntryControl(string password,int UserId)
        {
           // NpgsqlConnection conString = new NpgsqlConnection("Server = localhost; Port = 5432; Database = RestaurantOtomasyon; User Id = postgres; Password = Fatma99?;");
            bool result ;
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            string query = "SELECT FROM \"personeller\" where \"ID\"=@Id AND \"PAROLA\"=@password";  // "Select * from Personeller where ID=@Id and PAROLA=@password"  AND \"PAROLA\"
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", SqlDbType.VarChar).Value = UserId;
            cmd.Parameters.AddWithValue("@password", SqlDbType.VarChar).Value = password;
            
            try
            {
               if(con.State==ConnectionState.Closed)
                {
                    con.Open();

                }
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            catch(SqlException ex)
            {
                string hata = ex.Message;
                throw;
            }
            return result;

        }
        public void personelGetbyInformation(ComboBox cbKullanici)
        {
            cbKullanici.Items.Clear();
            NpgsqlConnection con = new NpgsqlConnection(gnl.connString);
            string query = "select * from  \"personeller\"";
            NpgsqlCommand cmd = new NpgsqlCommand(query, con);
           
            
                if (con.State == ConnectionState.Closed)
                {
                    con.Open();

                }
            NpgsqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                cPersoneller p = new cPersoneller();
                p._PersonelId = Convert.ToInt32(dr["ID"]);
                p._PersonelGorevId = Convert.ToInt32(dr["GOREVID"]);
                p._PersonelAd = Convert.ToString(dr["AD"]);
                p._PersonelSoyad = Convert.ToString(dr["SOYAD"]);
                p._PersonelParola = Convert.ToString(dr["PAROLA"]);
                p._PersonelKullaniciAdi = Convert.ToString(dr["KULLANICIADI"]);
               // p._PersonelDurum = Convert.ToBoolean(dr["DURUM"]);
                cbKullanici.Items.Add(p);

            }
            dr.Close();
            con.Close();
                            
            
        }
        public override string ToString()
        {
            return PersonelAd + " " +PersonelSoyad;
        }

    }  

}
