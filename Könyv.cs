using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class Könyv : IOlvasmány
    {
        public int Oldalszám { get; set; }
        public int Értékelés { get; set; }
        public int Tárhely { get; set; }

        public string Szerző { get; set; }
        public string Cím { get; set; }
        public LancoltLista Szereplők { get; set; }
    }
}
