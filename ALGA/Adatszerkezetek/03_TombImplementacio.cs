using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    public class TombVerem<T> : Verem<T>
    {
        T[] E;
        int n = 0;

        public TombVerem(int meret)
        {
            E = new T[meret];
        }

        public bool Ures { get { return n == 0; } }


        public T Felso()
        {
            if (!Ures)
            {
                return E[n - 1];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Verembe(T ertek)
        {
            if (n < E.Length)
            {
                E[n] = ertek;
                n++;
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Verembol()
        {
            if (!Ures)
            {
                return E[--n];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class TombSor<T> : Sor<T>
    {
        T[] E;
        int e = 0;
        int u = 0;
        int n = 0;

        public bool Ures { get { return n == 0; } }
        public TombSor(int meret)
        {
            E = new T[meret];
        }
        public T Elso()
        {
            if (!Ures)
            {
                return E[e];
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Sorba(T ertek)
        {
            if (n < E.Length)
            {
                E[u] = ertek;
                n++;
                u++;
                if (u >= E.Length)
                {
                    u = 0;
                }

            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            if (!Ures)
            {
                T ideiglenes = E[e];
                n--;
                e++;
                if (e >= E.Length)
                {
                    e = 0;
                }
                return ideiglenes;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
    public class TombLista<T> : Lista<T>, IEnumerable<T>
    {
        T[] E;
        int n;

        public int Elemszam { get { return n; } }
        public TombLista(int meret = 4)
        {
            n = 0;
            E = new T[meret];
        }
        public void MeretNovel()
        {
            T[] Ideiglenes = E;
            E = new T[E.Length * 2];
            for (int i = 0; i < Ideiglenes.Length; i++)
            {
                E[i] = Ideiglenes[i];
            }
        }
        public void Bejar(Action<T> muvelet)
        {
            for (int i = 0; i < n; i++)
            {
                muvelet(E[i]);
            }
        }

        public void Beszur(int index, T ertek)
        {
            if (index <= n)
            {
                if (n == E.Length)
                {
                    MeretNovel();
                }
                for (int i = n; i > index; i--)
                {
                    E[i] = E[i - 1];
                }
                E[index] = ertek;
                n++;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Hozzafuz(T ertek)
        {
            Beszur(n, ertek);
        }

        public T Kiolvas(int index)
        {
            if (index <= n)
            {
                return E[index];
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Modosit(int index, T ertek)
        {
            if (index <= n)
            {
                E[index] = ertek;
            }
            else
            {
                throw new HibasIndexKivetel();
            }
        }

        public void Torol(T ertek)
        {
            int db = 0;
            for (int i = 0; i < n; i++)
            {
                if (E[i].Equals(ertek))
                {
                    db++;
                }
                else
                {
                    E[i - db] = E[i];
                }
            }
            n = n - db;
        }

        public IEnumerator<T> GetEnumerator()
        {
            TombListaBejaro<T> bejaro = new TombListaBejaro<T>(E, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public class TombListaBejaro<T> : IEnumerator<T>
    {
        private T[] E;
        private int n;
        private int aktualisIndex = -1;
        public TombListaBejaro(T[] E, int n)
        {
            this.n = n;
            this.E = E;
        }
        public T Current => E[aktualisIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (aktualisIndex < n - 1)
            {
                aktualisIndex++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualisIndex = -1;
        }
    }
}
