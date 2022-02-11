using System;
using System.Linq;
using System.Data;
using Kutuphane_otomasyon.EntityLayer;
using Kutuphane_otomasyon.DAL;

namespace Kutuphane_otomasyon.BLL
{
    //Business Logic Layer sınıfı, DAL' katman sınıfını kullanarak verilerin işlenmesini sağlar, DAL'den gelen ham verileri programın ihtiyacı olan şekilde filtreler ve / veya aynı şekilde gönderir.
    class MemberBLL
    {

        public DataTable getPeople() // DAL sınıfından select sorgusuyla bütün Person datasını çeken fonksiyon
        {
            MemberDAL person = new MemberDAL(); // Yeni bir DAL nesnesi oluşturuyorum select methodunu kullanmak için.
            return person.SELECT(); //DAL katmanından Select işlemini return eder.
        }

        public DataTable getPersonByID(Member data) // Parametre olarak verilen Member idsine göre getirme işlemi yapar.
        {
            DataTable result = new DataTable(); //Return etmek için DataTable cinsinden result değişkeni oluşturulmuştur.
            result = getPeople(); // BLL sınıfının içindeki getPople fonksiyonu kullanılarak result değişkenine tablo verisi alınmıştır.

            try // Data null gelebilir, bağlantı probleminde dolayı eksik gelebilir bu yüzden hata yakalama yapıyoruz.
            {
                DataRow[] query = result.Select("MemberNo = " + data.MemberNo); //tüm sorgu sonuçları içinden id eşleşmesine göre select sorgusu yapıyoruz.
                result.Clear(); //sorgudan sonra result tablosunun içini sadece eklemek istediğimiz parametreyi eklemek için sıfırlıyoruz.
                result.LoadDataRow(query, true); //Datayı result değişkenine yüklüyoruz.
            } 
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
            return result; //result değişkeni kullanılmak üzere döndürülüyor.
        }

        public DataTable getPersonByName(Member data) // Parametre olarak verilen Member name'e göre getirme işlemi yapar.
        {
            DataTable result = new DataTable(); // Return etmek için DataTable cinsinden result değişkeni oluşturulmuştur.
             result = getPeople(); // BLL sınıfının içindeki getPople fonksiyonu kullanılarak result değişkenine tablo verisi alınmıştır.

            try // Data null gelebilir, bağlantı probleminde dolayı eksik gelebilir bu yüzden hata yakalama yapıyoruz.
            {
                DataRow[] query = result.Select("Ad = " + data.Name); // Kisi adina göre filtreleme yapılıp sadece ad uyuşan kişileri filtreliyoruz. 
                result.Clear();  //sorgudan sonra result tablosunun içini sadece eklemek istediğimiz parametreyi eklemek için sıfırlıyoruz.
                result.LoadDataRow(query, true); //Datayı result değişkenine yüklüyoruz.
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return result; //result değişkeni kullanılmak üzere döndürülüyor.
        }

        public bool AddMember(Member data) // Parametre olarak verilen Member DAL aracılığıyla DB'ye INSERT edilecektir.
        {
            MemberDAL person = new MemberDAL(); //Insert fonksiyonunu kullanmak için yeni bir MemberDAL nesnesi oluşturululuyor.
            bool status = person.INSERT(data); // data insert methoduna gönderilip, status ile işlemin başarılıp olup olmadığına dair bilgi alınıyor.
            return status; //status döndürülüyor.
        }

        public bool DeleteMember(Member data) // Parametre olarak verilen Member DAL aracılığıyla DB'ye DELETE edilecektir.
        {
            MemberDAL person = new MemberDAL();//DELETE fonksiyonunu kullanmak için yeni bir MemberDAL nesnesi oluşturululuyor.
            bool status = person.DELETE(data); // data delete methoduna gönderilip, status ile işlemin başarılıp olup olmadığına dair bilgi alınıyor.
            return status;  //status döndürülüyor.
        }

        public bool EditMember(Member data) // Parametre olarak verilen Member DAL aracılığıyla DB'ye UPDATE edilecektir.
        {
            MemberDAL person = new MemberDAL(); //UPDATE fonksiyonunu kullanmak için yeni bir MemberDAL nesnesi oluşturululuyor.
            bool status = person.UPDATE(data); // data UPDATE methoduna gönderilip, status ile işlemin başarılıp olup olmadığına dair bilgi alınıyor.
            return status; //status döndürülüyor.

        }

        private DataTable getMemberByQuery(string expression)
        {

            DataTable table = getPeople();
            DataTable MemberTable = new DataTable();
            DataRow[] selectedRows = table.Select(expression);
            MemberTable.Columns.Clear();
            MemberTable.Columns.Add("MemberNumber");
            MemberTable.Columns.Add("Name");
            MemberTable.Columns.Add("Surname");
            MemberTable.Columns.Add("Phone");
            MemberTable.Columns.Add("DOB");


            foreach (DataRow dr in selectedRows)
            {

                DataRow row = MemberTable.Rows.Add();
                row["MemberNumber"] = dr["MemberNumber"];
                row["Name"] = dr["Name"];
                row["Surname"] = dr["Surname"];
                row["Phone"] = dr["Phone"];
                row["DOB"] = dr["DOB"];
            }

            return MemberTable;
        }

        public DataTable getMemberById(long id)
        {
            string expression = "MemberNumber=" + id.ToString();
            DataTable memberTable = getMemberByQuery(expression);
            return memberTable;
        }


    }
}
