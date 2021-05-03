using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRCGB4_EBookTarolo
{
    delegate void BinaryTreeHandler(IOlvasmány olvasmány);
    delegate void ErrorHandler(string message);
    class BinaryTree
    {
        private TreeElement root;      

        class TreeElement
        {
            public IOlvasmány olvasmány;
            public int key;
            public int értékelés;
            public TreeElement bal;
            public TreeElement jobb;
            public TreeElement(IOlvasmány olvasmány)
            {
                this.olvasmány = olvasmány;
                értékelés = olvasmány.Értékelés;
            }
        }

        public void upload(IOlvasmány inputOlvasmány,BinaryTreeHandler method,ErrorHandler onError,BinaryTreeHandler onDelete)
        {
            TreeElement element = new TreeElement(inputOlvasmány);
            inOrderBejaras((item) =>
            {
                if (GetOlvasmányKulcs(item) == GetOlvasmányKulcs(inputOlvasmány))
                {
                    throw new IlyenMárVanException();
                }
            });
            element.key = GetOlvasmányKulcs(inputOlvasmány);
            try
            {
                _upload(ref root, element, element.key,method);
            }
            catch (NincsMárHelyException ex)
            {
                onError?.Invoke(ex.Message);
                _deleteEnough(ex.SzükségesHely,onDelete);
                _upload(ref root, element, element.key,method);
            }

        }
        private void _upload(ref TreeElement p, TreeElement element ,int kulcs,BinaryTreeHandler method)
        {
            if (!CheckForSpace(element.olvasmány.Tárhely))
            {
                int szükséges = element.olvasmány.Tárhely - Tárhely.SzabadHely;
                throw new NincsMárHelyException("Betelt a tárhely",szükséges);
            }
            if (p == null)
            {
                p = element;
                Tárhely.SzabadHely -= element.olvasmány.Tárhely;
                method?.Invoke(p.olvasmány);
            }
            else if(p.key > kulcs)
            {
                _upload(ref p.bal, element, kulcs,method);
            }else if(p.értékelés < kulcs)
            {
                _upload(ref p.jobb, element, kulcs,method);
            }
            else
            {
                int current = element.key; ;
                int other = p.key;
                if (other == current)
                {
                    throw new IlyenMárVanException();
                }else if(other > current)
                {
                    _upload(ref p.bal, element, kulcs,method);
                }
                else
                {
                    _upload(ref p.jobb, element, kulcs,method);
                }

            }

        }

        private int GetOlvasmányKulcs(IOlvasmány olvasmány)
        {
            int key = olvasmány.Értékelés * 100000000 - olvasmány.Oldalszám - olvasmány.Tárhely;
            if(olvasmány is Könyv)
            {
                key++;
            }else if( olvasmány is Dia)
            {
                key+=2;
            }
            else
            {
                key += 3;
            }
            return key;
        }

        private bool CheckForSpace(int szükségesTárhely)
        {
            if(szükségesTárhely > Tárhely.SzabadHely)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void _deleteEnough(int szükségesHely,BinaryTreeHandler onDelete)
        {
            int hely = szükségesHely;
            inOrderDelete(ref hely, ref root,onDelete);
            
        }

        private void inOrderDelete(ref int szükségesHely,ref TreeElement p, BinaryTreeHandler onDelete)
        {
            if (p != null)
            {
                inOrderDelete(ref szükségesHely, ref p.bal, onDelete);
                if (szükségesHely > 0)
                {
                    szükségesHely -= p.olvasmány.Tárhely;
                    Tárhely.SzabadHely += p.olvasmány.Tárhely;
                    onDelete?.Invoke(p.olvasmány);
                    if(p.jobb != null)
                    {
                        p = p.jobb;
                    }
                    else
                    {
                        p = null;
                    }
                }
                else
                {
                    return;
                }
                if (p != null)
                {
                    inOrderDelete(ref szükségesHely, ref p.jobb,onDelete);
                }

            }
        }

        
        public void inOrderBejaras(BinaryTreeHandler method)
        {
            _inOrderBejárás(root, method);
        }

        private void _inOrderBejárás(TreeElement p,BinaryTreeHandler method)
        {
            if (p!=null)
            {
                _inOrderBejárás(p.bal,method);
                method?.Invoke(p.olvasmány);
                _inOrderBejárás(p.jobb, method);
            }
            

        }

        public void Read(IOlvasmány olvasmány, BinaryTreeHandler method, BinaryTreeHandler uploadmethod, ErrorHandler error, BinaryTreeHandler onDelete)
        {
            IOlvasmány current = _Read(GetOlvasmányKulcs(olvasmány), ref root);
            method?.Invoke(current);
            current.Értékelés = int.Parse(Console.ReadLine());
            upload(current,uploadmethod,error,onDelete);

        }

        private IOlvasmány _Read(int kulcs, ref TreeElement p)
        {
            if(p != null)
            {
                if(p.key== kulcs)
                {
                    IOlvasmány outp = p.olvasmány;
                    Tárhely.SzabadHely += p.olvasmány.Tárhely;
                    Delete(ref p);
                    return outp;

                }else if(p.key > kulcs)
                {
                   return  _Read(kulcs, ref p.bal);
                }else
                {
                   return _Read(kulcs, ref p.jobb);
                }
            }
            else
            {
                throw new ArgumentException("Ilyen nem létezik");
            }
        }

        private void Delete( ref TreeElement element)
        {
            if(element.jobb == null && element.bal == null)
            {
                element = null;
            }else if(element.jobb==null && element.bal != null)
            {
                element = element.bal;
            }else if(element.jobb != null && element.bal == null)
            {
                element = element.jobb;
            }
            else
            {
                DeleteWithTwoChild(element, ref element.bal);
            }
        }
        private void DeleteWithTwoChild(TreeElement toDelete, ref TreeElement leftOne)
        {
            if(leftOne.jobb != null)
            {
                DeleteWithTwoChild(toDelete, ref leftOne.jobb);
            }
            else
            {
                toDelete.olvasmány = leftOne.olvasmány;
                toDelete.key = leftOne.key;
                toDelete.értékelés = leftOne.értékelés;
                leftOne = leftOne.bal;
            }
        }
    }
}
