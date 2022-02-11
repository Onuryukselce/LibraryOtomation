using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kutuphane_otomasyon.BLL;
using System.Data;

namespace Kutuphane_otomasyon.EntityLayer
{
    public class Book : IDisposable,IBook // Book sınıfı Interface'i ile birlikte oluşturulmuştur
    {
        string _bookNo;
        string _isbn;
        string _bookName;
        string _author;
        string _issue;
        string _released;
        //Değişkenler tanımlandı ve encapsulate edildi
        public string BookNo
        {
            get { return _bookNo; }
            set { _bookNo = value;  }
        }

        public string ISBN
        {
            get { return _isbn; }
            set { _isbn = value; }
        }

        public string BookName
        {
            get { return _bookName; }
            set { _bookName = value; }
        }

        public string Author
        {
            get { return _author; }
            set { _author = value; }
        }

        public string Issue
        {
            get { return _issue; }
            set { _issue = value; }
        }

        public string ReleaseDate
        {
            get { return _released; }
            set { _released = value; }
        }

        //Constructor
        public Book()
        {

        }
         //Constructor (Paremetreli)
        public Book(string _isbn, string _bookname, string _author, string _issue, string _releasedate)
        {
            ISBN = _isbn;
            BookName = _bookname;
            Author = _author;
            Issue = _issue;
            ReleaseDate = _releasedate;
        }

        public Book(string _bookno, string _isbn, string _bookname, string _author, string _issue, string _releasedate)
        {
            BookNo = _bookno;
            ISBN = _isbn;
            BookName = _bookname;
            Author = _author;
            Issue = _issue;
            ReleaseDate = _releasedate;
        }
        public bool AddBook() //BLL sınıfındaki AddBook fonksiyonuna erişiliyor
        {
            BookBLL book = new BookBLL();
            bool status = book.AddBook(this);
            return status;
        }

        public bool DeleteBook() // BLL sınıfındaki DeleteBook fonksiyonuna erişiliyor
        {
            BookBLL book = new BookBLL();
            bool status = book.DeleteBook(this);
            return status;
        }

        public bool UpdateBook() // BLL sınıfındaki UpdateBook fonksiyonuna erişiliyor
        {
            BookBLL book = new BookBLL();
            bool status = book.EditBook(this);
            return status;
        }

        public DataTable ShowBooks() // BLL sınıfındaki getAAllBooks fonksiyonuna erişiliyor
        {
            BookBLL book = new BookBLL();
            DataTable query = book.getAllBooks();
            return query;
        }

        public DataTable getBookById(long id)
        {
            BookBLL book = new BookBLL();
            DataTable query = book.getBookById(id);
            return query;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

    }
}
