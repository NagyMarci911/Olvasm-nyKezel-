﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    interface IOlvasmány
    {
        int Oldalszám { get; set; }
        int Értékelés { get; set; }
        int Tárhely { get; set; }
    }
}
