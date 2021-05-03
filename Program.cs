using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    class Program
    {
        static void Main(string[] args)
        {
            Tárhely.init();
            BinaryTree tree = new BinaryTree();
            FlowHandler.tree = tree;
            FlowHandler.startProgram();
            ;
        }

        
    }
}
