using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class IlyenMárVanException: Exception
    {
        public IlyenMárVanException():base("HIBA: Ilyen Már létezik!")
        {

        }
    }
}
