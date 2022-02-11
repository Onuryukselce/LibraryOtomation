using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kutuphane_otomasyon.EntityLayer;
using Kutuphane_otomasyon.DAL;
using System.Data;
using System.Windows.Forms;

namespace Kutuphane_otomasyon.BLL
{

    //Business Logic Layer sınıfı, DAL' katman sınıfını kullanarak verilerin işlenmesini sağlar, DAL'den gelen ham verileri programın ihtiyacı olan şekilde filtreler ve / veya aynı şekilde gönderir.

    class DepositBLL
    {
        public DataTable getDeposits() // DAL sınıfından select sorgusuyla bütün Person datasını çeken fonksiyon
        {
            DepositDAL deposit = new DepositDAL(); // Yeni bir DAL nesnesi oluşturuyorum select methodunu kullanmak için.
            DataTable deposits = deposit.SELECT(); //DAL katmanından Select işlemini return eder.
            deposits = calculateStatus(deposits);
            deposits = calculate(deposits);
            return deposits;
        }

        public DataTable calculateStatus(DataTable deposit)
        {
            foreach(DataRow dr in deposit.Rows)
            {
                DateTime withdrawDate = Convert.ToDateTime(dr["WithdrawDate"]);
                DateTime today = DateTime.Today;
                int isWithdrawaDateLate = withdrawDate.CompareTo(today);
                if(isWithdrawaDateLate < 0)
                {
                    if(!dr["Status"].ToString().ToLower().Contains("teslim"))
                    {
                        dr["Status"] = "GEÇ";
                    }
                }
                else
                {
                    if(!dr["Status"].ToString().ToLower().Contains("teslim"))
                    {
                        dr["Status"] = "EMANETTE";
                    }
                }
            }

            return deposit;
        }
        public DataTable calculate(DataTable deposit)
        {
            foreach(DataRow dr in deposit.Rows)
            {
                DateTime withdrawDate = Convert.ToDateTime(dr["WithdrawDate"]);
                DateTime today = DateTime.Today;
                int isWithdrawaDateLate = withdrawDate.CompareTo(today);
                if(isWithdrawaDateLate < 0)
                {
                    double dayDifference = (today - withdrawDate).TotalDays;
                    if(!dr["Status"].ToString().ToLower().Contains("teslim"))
                    {
                        dr["PenaltyMoney"] = Convert.ToDecimal(dayDifference);
                    }
                }
                else
                {
                    dr["PenaltyMoney"] = 0;
                }
            }

            return deposit;
        }

        public DataTable getMembers()
        {
            MemberBLL member = new MemberBLL();
            return member.getPeople();
        }

        public DataTable getBooks()
        {
            BookBLL book = new BookBLL();
            return book.getAllBooks();
        }

        public DataTable BuildTable()
        {
            DataTable deposits = getDeposits();
            deposits.TableName = "deposits";
            DataTable books = getBooks();
            books.TableName = "books";
            DataTable member = getMembers();
            member.TableName = "members";



            var query = from d in deposits.AsEnumerable()
                        join m in member.AsEnumerable() on d.Field<int>("MemberNumber") equals m.Field<int>("MemberNumber")
                        join b in books.AsEnumerable() on d.Field<int>("BookNo") equals b.Field<int>("BookNo")
                        select new
                        {
                            ID = d.Field<int>("ID"),
                            BookNo = d.Field<int>("BookNo"),
                            MemberNo = d.Field<int>("MemberNumber"),
                            MemberName = m.Field<string>("Name"),
                            MemberSurname = m.Field<string>("Surname"),
                            BookName = b.Field<string>("BookName"),
                            DepositDate = d.Field<DateTime>("DepositDate"),
                            WithdrawDate = d.Field<DateTime>("WithdrawDate"),
                            Money = d.Field<Decimal>("PenaltyMoney"),
                            Status = d.Field<string>("Status")
                        };

            DataTable DepositTable = new  DataTable();
            DepositTable.Columns.Add("ID");
            DepositTable.Columns.Add("BookNo");
            DepositTable.Columns.Add("MemberNo");
            DepositTable.Columns.Add("Adı");
            DepositTable.Columns.Add("Soyadı");
            DepositTable.Columns.Add("Kitap");
            DepositTable.Columns.Add("Teslim Alma Tarihi");
            DepositTable.Columns.Add("Teslim Etme Tarihi");
            DepositTable.Columns.Add("Gecikme Ücreti");
            DepositTable.Columns.Add("Status");


            foreach (var x in query)
            {
                DataRow newRow = DepositTable.Rows.Add();
                newRow.SetField<int>("ID", x.ID);
                newRow.SetField<int>("BookNo", x.BookNo);
                newRow.SetField<int>("MemberNo", x.MemberNo);
               newRow.SetField<string>("Adı", x.MemberName);
                newRow.SetField<string>("Soyadı", x.MemberSurname);
                newRow.SetField<string>("Kitap", x.BookName);
                newRow.SetField<DateTime>("Teslim Alma Tarihi", x.DepositDate);
                newRow.SetField<DateTime>("Teslim Etme Tarihi", x.WithdrawDate);
                newRow.SetField<Decimal>("Gecikme Ücreti", x.Money);
                newRow.SetField<string>("Status", x.Status);
            }



          
            return DepositTable;


        }
       

        public DataTable getDepositTable()
        {
            return BuildTable();
        }

        private DataTable getDepositByQuery(string expression)
        {

            DataTable table = getDepositTable();
            DataTable DepositTable = new DataTable();
            DataRow[] selectedRows = table.Select(expression);
            DepositTable.Columns.Clear();
            DepositTable.Columns.Add("ID");
            DepositTable.Columns.Add("BookNo");
            DepositTable.Columns.Add("MemberNo");
            DepositTable.Columns.Add("Adı");
            DepositTable.Columns.Add("Soyadı");
            DepositTable.Columns.Add("Kitap");
            DepositTable.Columns.Add("Teslim Alma Tarihi");
            DepositTable.Columns.Add("Teslim Etme Tarihi");
            DepositTable.Columns.Add("Gecikme Ücreti");
            DepositTable.Columns.Add("Status");

            foreach (DataRow dr in selectedRows)
            {

                DataRow row = DepositTable.Rows.Add();
                row["ID"] = dr["ID"];
                row["BookNo"] = dr["BookNo"];
                row["MemberNo"] = dr["MemberNo"];
                row["Adı"] = dr["Adı"];
                row["Soyadı"] = dr["Soyadı"];
                row["Kitap"] = dr["Kitap"];
                row["Teslim Alma Tarihi"] = dr["Teslim Alma Tarihi"];
                row["Teslim Etme Tarihi"] = dr["Teslim Etme Tarihi"];
                row["Gecikme Ücreti"] = dr["Gecikme Ücreti"];
                row["Status"] = dr["Status"];
            }

            return DepositTable;
        }
        public DataTable getDepositByStudentId(long studentId)
        {
            string expression = "MemberNo=" + studentId.ToString();
            DataTable DepositTable = getDepositByQuery(expression);
            return DepositTable;
        }

        public DataTable getDepositByBookId(long bookId)
        {
            string expression = "BookNo=" + bookId.ToString();
            DataTable DepositTable = getDepositByQuery(expression);
            return DepositTable;
        }

        public bool Update(Deposit d)
        {
            DepositDAL deposit = new DepositDAL();
            return deposit.UPDATE(d);
        }

        public bool AddDeposit(Deposit d)
        {
            DepositDAL deposit = new DepositDAL();
            return deposit.INSERT(d);
        }

        public bool DeleteDeposit(Deposit d)
        {
            DepositDAL deposit = new DepositDAL();
            return deposit.DELETE(d);
        }
    }
}
