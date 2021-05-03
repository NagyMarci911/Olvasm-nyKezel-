using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LRCGB4_EBookTarolo
{
    static class Tárhely
    {
        static public int MaxTárhely { get; private set; }
        static public int SzabadHely { get;  set; }

        static public void init()
        {
            bool alreadyExists = TárhelyEllenőrzés();
            if (!alreadyExists)
            {
                MaxTárhely = GetMaxTárhely();
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\maxTárhely", MaxTárhely.ToString());

            }
            SzabadHely = MaxTárhely;
        }
        static private int GetMaxTárhely()
        {
            int tarhely = 0;
            Console.WriteLine("Adja meg a maximum tárhelyet!");
            string input = Console.ReadLine();
            try
            {
                tarhely = int.Parse(input);
                if (tarhely <= 0)
                {
                    GetMaxTárhely();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Nem megfelelő szám. Próbálja újra!");
                GetMaxTárhely();
            }

            return tarhely;
        }
        static private bool TárhelyEllenőrzés()
        {
            string path = Directory.GetCurrentDirectory() + "\\maxTárhely";
            if (File.Exists(path))
            {
                try
                {
                    MaxTárhely = int.Parse(File.ReadAllText(path));
                    return true;
                }
                catch (Exception ex)
                {
                    FileStream connection = File.Create(path);
                    connection.Close();
                    return false;
                }
               
            }
            else
            {
                FileStream connection = File.Create(path);
                connection.Close();
                return false;
            }
            
        }

        static public void ChangeMaxTárhely(int hely)
        {
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\maxTárhely", hely.ToString());
            MaxTárhely = hely;
        }

    }
}
