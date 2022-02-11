using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_otomasyon.EntityLayer
{
    interface IBook // Book sınıfının interface'i ve işlemleri burada tanımlandı
    {
        bool AddBook();
        bool DeleteBook();

        bool UpdateBook();
    }
}
