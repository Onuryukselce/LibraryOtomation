using Kutuphane_otomasyon.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_otomasyon.EntityLayer
{
    public class Deposit : IDisposable, IDeposit
    {
        public string ID { get; set; }
        public string MemberNo { get; set; }
        public string BookNo { get; set; }
        public DateTime DepositDate { get; set; }
        public DateTime WithdrawDate { get; set; }
        public Decimal Penalty { get; set; }

        public string Status {get; set;}
        public Deposit()
        {

        }

        public Deposit(string _id, string _memberno, string _bookno, DateTime _depositdate, DateTime _withdrawdate, Decimal _penalty, String _status)
        {
            ID = _id;
            MemberNo = _memberno;
            BookNo = _bookno;
            DepositDate = _depositdate;
            WithdrawDate = _withdrawdate;
            Penalty = _penalty;
            Status = _status;
        }

        public Deposit(string _memberno, string _bookno, DateTime _depositdate, DateTime _withdrawdate, Decimal _penalty, String _status)
        {
            MemberNo = _memberno;
            BookNo = _bookno;
            DepositDate = _depositdate;
            WithdrawDate = _withdrawdate;
            Penalty = _penalty;
            Status = _status; 
        }

        public bool AddDeposit()
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.AddDeposit(this);
        }

        public bool DeleteDeposit()
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.DeleteDeposit(this);
        }

        public bool UpdateDeposit()
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.Update(this);
        }

        public DataTable getDeposits()
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.getDepositTable();
        }

        public DataTable getDepositByStudentId(long id)
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.getDepositByStudentId(id);
        }

        public DataTable getDepositByBookId(long id)
        {
            DepositBLL deposit = new DepositBLL();
            return deposit.getDepositByBookId(id);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
