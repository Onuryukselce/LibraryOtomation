using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_otomasyon.EntityLayer
{
    interface IMember //Member sınıfının arayüzü, ekleme ve silme fonkisyonu interfacede tanımlandı
    {
        bool AddMember();
        bool DeleteMember();

    }
}
