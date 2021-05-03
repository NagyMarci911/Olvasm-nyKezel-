using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class LancoltLista
    {
        class ListItem
        {
            public string szereplő;
            public ListItem next;
        }
        private ListItem head;

        public void Add(string szereplő)
        {
            ListItem uj = new ListItem();
            uj.szereplő = szereplő;
            uj.next = head;
            head = uj;
        }
    }
}
