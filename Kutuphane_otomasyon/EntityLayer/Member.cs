using System;
using Kutuphane_otomasyon.BLL;
using System.Data;

namespace Kutuphane_otomasyon.EntityLayer
{
 
       public class Member : IDisposable, IMember
        {
            private string _memberNo;
            private string _name;
            private string _surname;
            private string _phone;
            private string _dob;
        //Değişkenler oluşturuldu ve encapsullate edildi.

            public string MemberNo
        {
            get { return _memberNo; }
            set { _memberNo = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public string DOB
        {
            get { return _dob; }
            set { _dob = value;  }
        }

        //Constructorlar
           public Member()
            {

            }

           public Member(string _name, string _surname, string _phone, string _dob)
        {
            Name = _name;
            Surname = _surname;
            Phone = _phone;
            DOB = _dob;
        }
            public bool AddMember() // BLL katmanındaki AddMember fonksiyonuna erişiliyor
            {
            MemberBLL Member = new MemberBLL();
            bool status = Member.AddMember(this);
            return status;
            }

            public bool DeleteMember() // BLL katmanındaki DeleteMember fonksiyonuna erişiliyor
        {
            MemberBLL Member = new MemberBLL();
            bool status = Member.DeleteMember(this);
            return status;
        }

        public bool EditMember() // BLL katmanındaki EditMember fonksiyonuna erişiliyor
        {
            MemberBLL Member = new MemberBLL();
            bool status = Member.EditMember(this);
            return status;
        }

        public DataTable ShowMember() // BLL katmanındaki fetPople fonksiyonuna erişiliyor
        {
            MemberBLL Member = new MemberBLL();
            DataTable result = Member.getPeople();
            return result;
        }

        public DataTable getMemberById(long id)
        {
            MemberBLL Member = new MemberBLL();
            DataTable result = Member.getMemberById(id);
            return result;
        }
            public void Dispose()
            {
                GC.SuppressFinalize(this);
            }
       }
   
}
