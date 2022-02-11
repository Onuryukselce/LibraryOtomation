using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using Kutuphane_otomasyon.EntityLayer;

namespace Kutuphane_otomasyon.DAL
{
    class MemberDAL : ICrud<Member>
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

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            con.Close();
            return affectedColumns;
        }

        public bool INSERT(Member data) //ICrud interface'de tanımlanan polymorph INSERT metodu implemente edilmiştir.
        {
            bool status = false; //Sonucun başarılı olup olmadığına dair veriyi tutacak bool değişken

          
            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "INSERT INTO Member(Name, Surname, Phone, DOB)values(?,?,?,?)";
            command.Parameters.AddWithValue("@Name", data.Name.Trim());
            command.Parameters.AddWithValue("@Surname", data.Surname.Trim());
            command.Parameters.AddWithValue("@Phone", data.Phone.Trim());
            command.Parameters.AddWithValue("@DOB", Convert.ToDateTime(data.DOB.Trim()));

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

        public bool UPDATE(Member data) //ICrud interface'de tanımlanan polymorph UPDATE metodu implemente edilmiştir.
        {
           bool status = false; //Sonucun başarılı olup olmadığına dair veriyi tutacak bool değişken
            //SQL sorgusu için OleDBCommand hazırlanıyor.

            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "UPDATE Member SET Name = ?, Surname = ?, Phone = ? ,DOB = ? Where MemberNumber = ?";
            command.Parameters.AddWithValue("@Name", data.Name.Trim());
            command.Parameters.AddWithValue("@Surname", data.Surname.Trim());
            command.Parameters.AddWithValue("@Phone", data.Phone.Trim());
            command.Parameters.AddWithValue("@DOB", Convert.ToDateTime(data.DOB.Trim()));
            command.Parameters.AddWithValue("@MemberNumber", long.Parse(data.MemberNo));

              try // Sorgu execute sırasında, sorguda ya da bağlantıda bir hata meydana gelmesi durumuna karşı hata ayıklama yapılıyor.
            {
                int resultSet = ExecuteNonQuery(command); //ExeCuteNonQuery fonksiyonu etkilenen kolon sayısını döndürdüğü için resultSet altında bunu tutuyor, aynı zamanda bu satırda sorgu çalışıyor.
                if (resultSet != 0) //Eğer resultSet 0 değilse, işlemin hatasız meydana geldiğini bildiren status = true, eğer hi sonuç etkilenmemişse bir problem olduğunu belirten status = false
                    status = true;
                else
                    status = false;
            }
            catch(InvalidOperationException e)
            {
                MessageBox.Show("Yürütme sırasında bir hata oluştu! Hata : " + e.Message, "Operasyon Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return status; //Sonuç döndürülüyor.
            
        } 

      public bool DELETE(Member data) //ICrud interface'de tanımlanan polymorph DELETE metodu implemente edilmiştir.
        { 
            bool status = false; //Sonucun başarılı olup olmadığına dair veriyi tutacak bool değişken
           

            OleDbCommand command = new OleDbCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = "Delete From Member Where MemberNumber = ?";
            command.Parameters.AddWithValue("@MemberNumber",long.Parse(data.MemberNo));
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
            command.CommandText = "Select * From Member";
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
