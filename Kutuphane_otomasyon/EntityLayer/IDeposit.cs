using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane_otomasyon.EntityLayer
{
    interface IDeposit
    {
        bool AddDeposit();
        bool DeleteDeposit();

        bool UpdateDeposit();
    }
}
