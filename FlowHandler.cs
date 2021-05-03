using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    static class FlowHandler
    {
        static public BinaryTree tree;
        static public void startProgram()
        {
            Console.WriteLine("Az eBook olvasó használata:");
            Console.WriteLine("Új olvasmány feltöltésére megkell adni a típusát a fájl-nak, amely lehet könyv, dokumentum és dia.");
            Console.WriteLine("A feltöltéshez alkalmazható parancs: 'upload típus' majd enter");
            Console.WriteLine("A parancs után megkell adni a típusra jellemző adatokat, majd megint enter.");
            Console.WriteLine("Olvasni ugyanígy lehet, csak a 'read típus' paranccsal.");
            Console.WriteLine("Kilistázni az összes olvasmányt az 'ls' paranccsal lehet.");
            try
            {
                ChoosePath(Console.ReadLine(),tree);
            }
            catch(IlyenMárVanException ex)
            {
                Console.WriteLine(ex.Message );
                ChoosePath(Console.ReadLine(), tree);
            }
            catch (Exception e)
            {

                Console.WriteLine("Nem megfelelő input");
                ChoosePath(Console.ReadLine(),tree);
            }

        }
        static private void ChoosePath(string input, BinaryTree tree)
        {
            
                if (input == "ls")
                {
                    int i = 0;
                    tree.inOrderBejaras((item) =>
                    {
                        i++; Console.Write($"{i}. olvasmány adatai: ");
                        olvasmányKiírás(item);

                    });
                }
                else if (input.Split(' ')[0] == "upload")
                {
                    string típus = input.Split(' ')[1];
                    IOlvasmány olvasmány = createOlvasmanyObjectsFromInput(típus);
                    tree.upload(olvasmány, uploadmethod, ErrorHandler, onDeleteHandler);

                }
                else if (input.Split(' ')[0] == "read")
                {
                    string típus = input.Split(' ')[1];
                    IOlvasmány olvasmány = createOlvasmanyObjectsFromInput(típus);
                    tree.Read(olvasmány, readMethod, uploadmethod, ErrorHandler, onDeleteHandler);

                }
                else
                {
                    Console.WriteLine("Nem megfelelő input!");
                }
            try
            {
                ChoosePath(Console.ReadLine(), tree);
            }
            catch (IlyenMárVanException ex)
            {
                Console.WriteLine(ex.Message);
                ChoosePath(Console.ReadLine(), tree);
            }
            catch (Exception e)
            {

                Console.WriteLine("Nem megfelelő input");
                ChoosePath(Console.ReadLine(), tree);
            }
        }

        static private void olvasmányKiírás(IOlvasmány item)
        {
            string type = item.GetType().ToString().Split('.')[item.GetType().ToString().Split('.').Length - 1];
            Console.WriteLine($"típus: {type}\t értékelés: {item.Értékelés}\t oldalszám: {item.Oldalszám}\t tárhely: {item.Tárhely}");
        }

        static void uploadmethod(IOlvasmány olvasmány)
        {
            string type = olvasmány.GetType().ToString().Split('.')[olvasmány.GetType().ToString().Split('.').Length - 1];
            Console.WriteLine($"Sikeres feltöltés: típus: {type}\tértékelés: {olvasmány.Értékelés}, oldalszám: {olvasmány.Oldalszám}, tárhely: {olvasmány.Tárhely}.");
            CurrentFreeSpace();

        }
        static void readMethod(IOlvasmány olvasmány)
        {
            string type = olvasmány.GetType().ToString().Split('.')[olvasmány.GetType().ToString().Split('.').Length - 1];
            Console.WriteLine($"Az olvasott {type} adatai: értékelés: {olvasmány.Értékelés}, oldalszám: {olvasmány.Oldalszám}, tárhely: {olvasmány.Tárhely}");
            Console.WriteLine("Hogyan értékelte az olvasmányt?");
        }
        static void CurrentFreeSpace()
        {
            double percentage = 100 - (double)(Tárhely.SzabadHely) / Tárhely.MaxTárhely * 100;
            int numberofFull = (int)Math.Floor(percentage / 10);
            string outp = "";
            int numb = 0;
            for (int i = 0; i < 10; i++)
            {
                if (numb < numberofFull)
                {
                    numb++;
                    outp += "█";
                }
                else
                {
                    outp += "░";
                }
            }

            Console.WriteLine(outp + percentage + " % tele");

        }

        static IOlvasmány createOlvasmanyObjectsFromInput(string típus)
        {
            if (típus == "könyv")
            {
                Console.WriteLine("Megfelelő input: értékelés oldalszám tárhely szerző cím szereplők(space-el tagolva)");
                string[] konyvstring = Console.ReadLine().Split();
                Könyv konyv = new Könyv();
                konyv.Értékelés = int.Parse(konyvstring[0]);
                konyv.Oldalszám = int.Parse(konyvstring[1]);
                konyv.Tárhely = int.Parse(konyvstring[2]);
                konyv.Szerző = konyvstring[3];
                konyv.Cím = konyvstring[4];
                konyv.Szereplők = new LancoltLista();
                for (int i = 5; i < konyvstring.Length; i++)
                {
                    konyv.Szereplők.Add(konyvstring[i]);
                }
                return konyv;

            }
            else if (típus == "dokumentum")
            {
                Console.WriteLine("Megfelelő input:oldalszám tárhely");
                string[] dokustring = Console.ReadLine().Split(' ');
                Dokumentum doku = new Dokumentum(int.Parse(dokustring[0]), int.Parse(dokustring[1]));
                return doku;

            }
            else if (típus == "dia")
            {
                Console.WriteLine("Megfelelő input:értékelés oldalszám tárhely előadó cím dátum(év) dátum(hónap) dátum(nap)");
                string[] diastring = Console.ReadLine().Split(' ');
                DateTime date = new DateTime(int.Parse(diastring[5]), int.Parse(diastring[6]), int.Parse(diastring[7]));
                Dia dia = new Dia(int.Parse(diastring[1]), int.Parse(diastring[0]), int.Parse(diastring[2]), diastring[3], diastring[4], date);
                return dia;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        static void ErrorHandler(string message)
        {
            Console.WriteLine(message);
        }
        static void onDeleteHandler(IOlvasmány olvasmány)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Törlődött a következő olvasmány: ");
            olvasmányKiírás(olvasmány);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
