using System;
using System.Data;
using Kutuphane_otomasyon.DAL;
using Kutuphane_otomasyon.EntityLayer;

namespace Kutuphane_otomasyon.BLL
{
    class BookBLL //Business Logic Layer sınıfı, DAL' katman sınıfını kullanarak verilerin işlenmesini sağlar, DAL'den gelen ham verileri programın ihtiyacı olan şekilde filtreler ve / veya aynı şekilde gönderir.
    {
        public DataTable getAllBooks() //DAL sınıfının yaptığı sorgudan bütün datayı çeken fonksiyon.
        {
            BookDAL book = new BookDAL(); //Yeni bir bookDAL nesnesi oluşturularak SELECT fonksiyonuyla verilerin aktarılması sağlanmıştır.
            DataTable books = book.SELECT();
            return books; //Datayı return ediyoruz.
        }

        public DataTable getBookByID(Book data) //ID ile kitap datasını filtrelemek için kullandığımız fonksiyon.
        {
            DataTable book = getAllBooks(); //books adlı değişkene veriyi aktarıyoruz
            try // Data null gelebilir, bağlantı probleminde dolayı eksik gelebilir bu yüzden hata yakalama yapıyoruz.
            {
                DataRow[] query = book.Select("MemberNo = " + data.BookNo); //tüm sorgu sonuçları içinden id eşleşmesine göre select sorgusu yapıyoruz.
                book.Clear(); //sorgudan sonra book tablosunun içini sadece eklemek istediğimiz parametreyi eklemek için sıfırlıyoruz.
                book.LoadDataRow(query, true); //Datayı book değişkenine yüklüyoruz.
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return book; //book değişkeni kullanılmak üzere döndürülüyor.
        }

        public DataTable getBookByName(Book data) // Parametre olarak verilen book name'e göre getirme işlemi yapar.
        {
            DataTable book = new DataTable(); // Return etmek için DataTable cinsinden book değişkeni oluşturulmuştur.
            book = getAllBooks(); // BLL sınıfının içindeki getAllBooks fonksiyonu kullanılarak book değişkenine tablo verisi alınmıştır.

            try // Data null gelebilir, bağlantı probleminde dolayı eksik gelebilir bu yüzden hata yakalama yapıyoruz.
            {
                DataRow[] query = book.Select("BookName LIKE %" + data.BookName + "%"); // Kisi adina göre filtreleme yapılıp sadece ad uyuşan kişileri filtreliyoruz. 
                book.Clear();  //sorgudan sonra result tablosunun içini sadece eklemek istediğimiz parametreyi eklemek için sıfırlıyoruz.
                book.LoadDataRow(query, true); //Datayı book değişkenine yüklüyoruz.
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return book; //book değişkeni kullanılmak üzere döndürülüyor.
        }

        public bool AddBook(Book data) //INSERT sorgusunu yapmak için add fonksiyonu
        {
            BookDAL book = new BookDAL(); //BookDAL nesnesi oluşturuyorum
            bool status = book.INSERT(data); // DAL içindeki INSERT methodunu kullanarak data ekliyorum.
            return status; // Sonucu dönüyorum
        }

        public bool DeleteBook(Book data) //DELETE sorgusunu yapmak için delete fonksiyonu
        {
            BookDAL book = new BookDAL(); //BookDAL nesnesi oluşturuyorum
            bool status = book.DELETE(data); //DELETE fonksiyonu ile sorgumu gönderiuorum
            return status; // Sonucu dönüyorum
        }

        public bool EditBook(Book data) //UPDATE sorgusu için fonksiyon
        {
            BookDAL book = new BookDAL(); //BookDAL nesnesi oluşturuyorum
            bool status = book.UPDATE(data); // UPDATE fonksiyonu ile sorgumu gönderiyorum
            return status; // Sonucu dönüyorum
        }

        private DataTable getBookByQuery(string expression)
        {

            DataTable table = getAllBooks();
            DataTable BookTable = new DataTable();
            DataRow[] selectedRows = table.Select(expression);
            BookTable.Columns.Clear();
            BookTable.Columns.Add("BookNo");
            BookTable.Columns.Add("ISBN");
            BookTable.Columns.Add("BookName");
            BookTable.Columns.Add("Author");
            BookTable.Columns.Add("Issue");
            BookTable.Columns.Add("Released");


            foreach (DataRow dr in selectedRows)
            {

                DataRow row = BookTable.Rows.Add();
                row["BookNo"] = dr["BookNo"];
                row["ISBN"] = dr["ISBN"];
                row["BookName"] = dr["BookName"];
                row["Author"] = dr["Author"];
                row["Issue"] = dr["Issue"];
                row["Released"] = dr["Released"];

            }

            return BookTable;

        }

        public DataTable getBookById(long id)
        {
            string expression = "BookNo =" + id.ToString();
            DataTable bookTable = getBookByQuery(expression);
            return bookTable;
        }

    }
}
