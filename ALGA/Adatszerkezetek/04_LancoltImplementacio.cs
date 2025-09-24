using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    internal class LancElem<T>
    {
        public T tart;
        public LancElem<T>? kov;
        public LancElem(T tart, LancElem<T>? kov)
        {
            this.tart = tart;
            this.kov = kov;
        }

    }
    public class LancoltVerem<T> : Verem<T>
    {
        LancElem<T>? fej;
        public LancoltVerem()
        {
            fej = null;
        }
        public bool Ures
        {
            get
            {
                return fej == null;
            }
        }

        public T Felso()
        {
            if (!Ures)
            {
                return fej.tart;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Verembe(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, fej);
            fej = uj;
        }

        public T Verembol()
        {
            if (!Ures)
            {
                T ertek = fej.tart;
                fej = fej.kov;
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class LancoltSor<T> : Sor<T>
    {
        LancElem<T>? fej;
        LancElem<T>? vege;
        public bool Ures
        {
            get
            {
                return vege == null;
            }
        }
        public LancoltSor()
        {
            fej = null;
            vege = null;
        }

        public T Elso()
        {
            if (fej != null)
            {
                return fej.tart;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if (!Ures)
            {
                vege.kov = uj;
            }
            else
            {
                fej = uj;
            }
            vege = uj;
        }

        public T Sorbol()
        {
            if (fej != null)
            {
                T ertek = fej.tart;
                LancElem<T> q = fej;
                fej = fej.kov;
                if (fej == null)
                {
                    vege = null;
                }
                return ertek;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class LancoltLista<T> : Lista<T>, IEnumerable<T>
    {
        LancElem<T>? fej;
        int n = 0;
        public LancoltLista()
        {
            fej = null;
        }
        public int Elemszam { get { return n; } }

        public void Bejar(Action<T> muvelet)
        {
            LancElem<T>? p = fej;
            while (p != null)
            {
                muvelet(p.tart);
                p = p.kov;
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (fej == null || index == 0)
            {
                LancElem<T> uj = new LancElem<T>(ertek, fej);
                fej = uj;
                n++;
            }
            else
            {
                LancElem<T> p = fej;
                int i = 1;
                while (p.kov != null && i < index)
                {
                    p = p.kov;
                    i++;
                }
                if (i <= index)
                {
                    LancElem<T> uj = new LancElem<T>(ertek, p.kov);
                    p.kov = uj;
                    n++;
                }
                else
                {
                    throw new HibasIndexKivetel();
                }
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            LancoltListaBejaro<T> bejaro = new LancoltListaBejaro<T>(fej);
            return bejaro;
        }

        public void Hozzafuz(T ertek)
        {
            LancElem<T> uj = new LancElem<T>(ertek, null);
            if (fej == null)
            {
                fej = uj;
            }
            else
            {
                LancElem<T> p = fej;
                while (p.kov != null)
                {
                    p = p.kov;
                }
                p.kov = uj;
            }
            n++;
        }

        public T Kiolvas(int index)
        {
            LancElem<T>? p = fej;
            int i = 0;
            while (p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if (p != null)
            {
                return p.tart;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            LancElem<T>? p = fej;
            int i = 0;
            while (p != null && i < index)
            {
                p = p.kov;
                i++;
            }
            if (p != null)
            {
                p.tart = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            LancElem<T>? p = fej;
            LancElem<T>? e = null;
            while (p != null)
            {
                while (p != null && !p.tart!.Equals(ertek))
                {
                    e = p;
                    p = p.kov;
                }
                if (p != null)
                {
                    LancElem<T>? q = p.kov;
                    if (e == null)
                    {
                        fej = q;

                    }
                    else
                    {
                        e.kov = q;

                    }
                    p = q;
                    n--;
                }

            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class LancoltListaBejaro<T> : IEnumerator<T>
        {
            LancElem<T>? fej;
            LancElem<T>? aktualisElem;
            public LancoltListaBejaro(LancElem<T>? fej)
            {
                this.fej = fej;
                aktualisElem = null;
            }
            public T Current
            {
                get
                {
                    return aktualisElem.tart;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                
            }

            public bool MoveNext()
            {
                if (aktualisElem == null)
                {
                    aktualisElem = fej;
                }
                else
                {
                    aktualisElem = aktualisElem.kov;
                }
                return aktualisElem != null;
            }

            public void Reset()
            {
                aktualisElem = null;
            }
        }
    }
    
}