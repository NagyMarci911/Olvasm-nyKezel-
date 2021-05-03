using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class Dokumentum : IOlvasmány
    {
        public int Oldalszám { get; set; }
        public int Értékelés { get; set; }
        public int Tárhely { get; set; }

        public Dokumentum(int oldalszám, int tárhely)
        {
            Oldalszám = oldalszám;
            Tárhely = tárhely;
            Értékelés = 5;
        }
    }
}
