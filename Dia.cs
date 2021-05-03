using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class Dia : IOlvasmány
    {
        public int Oldalszám { get; set; }
        public int Értékelés { get; set; }
        public int Tárhely { get; set; }
        public string Előadó { get; set; }
        public string Cím { get; set; }
        public DateTime Dátum { get; set; }

        public Dia(int oldalszám, int értékelés, int tárhely, string előadó, string cím, DateTime dátum)
        {
            Oldalszám = oldalszám;
            Értékelés = értékelés;
            Tárhely = tárhely;
            Előadó = előadó;
            Cím = cím;
            Dátum = dátum;
        }
    }
}
