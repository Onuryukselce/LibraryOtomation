using System.Data;
namespace Kutuphane_otomasyon.DAL
{
    interface ICrud<T> // CRUD işlemlerinin tanımlandığı polymorph INTERFACE
    {
        bool INSERT(T arg); //CREATE
        DataTable SELECT(); //READ
        bool UPDATE(T arg); // UPDATE
        bool DELETE(T arg); // DELETE

    }
}
