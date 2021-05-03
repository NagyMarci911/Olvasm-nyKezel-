using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class NincsMárHelyException : Exception
    {
        public int SzükségesHely { get; set; }
        public NincsMárHelyException(string message, int szükségesHely):base(message)
        {
            SzükségesHely = szükségesHely;
        }
    }
}
