using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;


namespace restaurant
{
    class cGenel
    {
        // Data Source = localhost; location=RestaurantOtomasyon;User ID = postgres; password=Fatma99?;
        // NpgsqlConnection conString = new NpgsqlConnection("Server = localhost; Port = 5432; Database = RestaurantOtomasyon; User Id = postgres; Password = Fatma99?;");
        // public string conString = ("Server=BEYZANUR\\SQLSERVER2017EXP;Database=RestaurantOtomasyon;Trusted_Connection=True");
        public string connString = "Server = localhost; Port = 5432; Database = RestaurantOtomasyon; User Id = postgres; Password = Fatma99?;";
        public static int _personelId; //her yerden ulaşabilmek için static yapıyoruz
        public static int _gorevId;

        public static string _ButtonValue;
        public static string _ButtonName;
    }
}
