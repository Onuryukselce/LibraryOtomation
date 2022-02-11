using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Kutuphane_otomasyon.Util;
namespace Kutuphane_otomasyon.DAL
{
    public static class Connection //Connection classı DAL'da bağlantı için kullanılıyor
    {
        private static string connectionPathReadFromINI = new INI("Settings.ini").Read("DatabasePath");
        public static string connectionPath = connectionPathReadFromINI; // DB Yolu (Bu ayar ini dosyasından değiştirilebilmektedir)
        public static OleDbConnection dbConnection = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = " + "'" +connectionPath+"'");
    }
}