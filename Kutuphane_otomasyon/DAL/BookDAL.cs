using System;
using Kutuphane_otomasyon.EntityLayer;
using System.Data.OleDb;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane_otomasyon.DAL
{
    class BookDAL :ICrud<Book>
    {

        public DataTable Execute(OleDbCommand command)
        {
            DataTable dt = new DataTable();
            try
            {
                OleDbConnection con = Connection.dbConnection;
                OleDbDataAdapter queryAdapter = new OleDbDataAdapter(command.CommandText, con);
                queryAdapter.Fill(dt);
            }
            catch (Exception)
            {
                MessageBox.Show("Lütfen ayarlar seçeneğinden, veritabanı yolunu doğru seçtiğinizden emin olunuz!");
            }
            return dt;
        }

        public int ExecuteNonQuery(OleDbCommand command)
        {
            int affectedColumns = 0;
            OleDbConnection con = Connection.dbConnection;

            try
            {
                con.Open();
                command.Connection = con;
                affectedColumns = command.ExecuteNonQuery();

            } catch (Exception e)
            {
                MessageBox.Show(e.ToString());
    }
            finally
            {
                con.Close();
            }
            return affectedColumns;
        }
        public bool INSERT(Book data) //ICrud interface'de tanımlanan polymorph INSERT metodu implemente edilmiştir.
        {
            bool status = false; //Sonucun başarılı olup olmadığına dair veriyi tutacak bool değişken

            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Book(ISBN, BookName, Author, Issue, Released) VALUES ( ?, ?, ?, ?, ?)";
            command.Parameters.AddWithValue("@ISBN", data.ISBN);
            command.Parameters.AddWithValue("@BookName", data.BookName);
            command.Parameters.AddWithValue("@Author", data.Author);
            command.Parameters.AddWithValue("@Issue", Convert.ToInt32(data.Issue));
            command.Parameters.AddWithValue("@Released", Convert.ToDateTime(data.ReleaseDate));



            try // Sorgu execute sırasında, sorguda ya da bağlantıda bir hata meydana gelmesi durumuna karşı hata ayıklama yapılıyor.
            {
                int resultSet = ExecuteNonQuery(command); //ExeCuteNonQuery fonksiyonu etkilenen kolon sayısını döndürdüğü için resultSet altında bunu tutuyor, aynı zamanda bu satırda sorgu çalışıyor.
                if (resultSet != 0) //Eğer resultSet 0 değilse, işlemin hatasız meydana geldiğini bildiren status = true, eğer hi sonuç etkilenmemişse bir problem olduğunu belirten status = false
                    status = true;
                else
                    status = false;

            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Yürütme sırasında bir hata oluştu! Hata : " + e.Message, "Operasyon Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return status; //Sonuç döndürülüyor.

        }

        public bool UPDATE(Book data) //ICrud interface'de tanımlanan polymorph UPDATE metodu implemente edilmiştir.
        {
            bool status = false; //Sonucun başarılı olup olmadığına dair veriyi tutacak bool değişken

            //SQL sorgusu için OleDBCommand hazırlanıyor.

            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Book SET ISBN = ?, BookName = ?, Author = ?, Issue = ?, Released = ? Where BookNo = ?";
            command.Parameters.AddWithValue("@ISBN", data.ISBN);
            command.Parameters.AddWithValue("@BookName", data.BookName);
            command.Parameters.AddWithValue("@Author", data.Author);
            command.Parameters.AddWithValue("@Issue", data.Issue);
            command.Parameters.AddWithValue("@Released", data.ReleaseDate);
            command.Parameters.AddWithValue("@BookNo", data.BookNo);
            try // Sorgu execute sırasında, sorguda ya da bağlantıda bir hata meydana gelmesi durumuna karşı hata ayıklama yapılıyor.
            {
                int resultSet = ExecuteNonQuery(command); //ExeCuteNonQuery fonksiyonu etkilenen kolon sayısını döndürdüğü için resultSet altında bunu tutuyor, aynı zamanda bu satırda sorgu çalışıyor.
                if (resultSet != 0) //Eğer resultSet 0 değilse, işlemin hatasız meydana geldiğini bildiren status = true, eğer hi sonuç etkilenmemişse bir problem olduğunu belirten status = false
                    status = true;
                else
                    status = false;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Yürütme sırasında bir hata oluştu! Hata : " + e.Message, "Operasyon Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return status; //Sonuç döndürülüyor.
        }

        public bool DELETE(Book data) //ICrud interface'de tanımlanan polymorph DELETE metodu implemente edilmiştir.
        {
            bool status = false;

            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "DELETE FROM Book Where BookNo = ? ";
            command.Parameters.AddWithValue("@BookNo",long.Parse(data.BookNo));
            try // Sorgu execute sırasında, sorguda ya da bağlantıda bir hata meydana gelmesi durumuna karşı hata ayıklama yapılıyor.
            {
                int resultSet = ExecuteNonQuery(command); //ExeCuteNonQuery fonksiyonu etkilenen kolon sayısını döndürdüğü için resultSet altında bunu tutuyor, aynı zamanda bu satırda sorgu çalışıyor.
                if (resultSet != 0) //Eğer resultSet 0 değilse, işlemin hatasız meydana geldiğini bildiren status = true, eğer hi sonuç etkilenmemişse bir problem olduğunu belirten status = false
                    status = true;
                else
                    status = false;
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Yürütme sırasında bir hata oluştu! Hata : " + e.Message, "Operasyon Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return status; //Sonuç döndürülüyor.
        }

        public DataTable SELECT() //ICrud interfacede tanımlanan SELECT methodu, dataTable cinsinden tanımlanmıştır.
        {
            DataTable queryResult = new DataTable(); //Sorgu sonuçlarını alacağımız queryResult DataTable type tanımlanmıştır.

            
            // SQL sorgusu için OleDBCommand hazırlanıyor.
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Select * From Book";
            try // Execute işlemi sırasında hatalı işlem olasılığına karşı hata yakalama yapılmıştır.
            {
                queryResult = Execute(command);
            }
            catch (InvalidOperationException e)
            {
                MessageBox.Show("Yürütme sırasında bir hata oluştu! Hata : " + e.Message, "Operasyon Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return queryResult; //Sonuçlar BLL'de kullanılmak üzere return ediliyor
        }
    }
}
