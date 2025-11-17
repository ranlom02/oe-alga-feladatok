using System;

namespace OE.ALGA.Adatszerkezetek
{
    public class Kupac<T>
    {
        protected T[] E;
        public int n;
        public Func<T, T, bool> nagyobbPrioritás;

        public Kupac(T[] e, int n, Func<T, T, bool> nagyobbPrioritás)
        {
            E = e;
            this.n = n;
            this.nagyobbPrioritás = nagyobbPrioritás;
            KupacotEpit();
        }

        public static int Bal(int i)
        {
            return 2 * i + 1;
        }
        public static int Jobb(int i)
        {
            return 2 * i + 2;
        }
        public static int Szulo(int i)
        {
            return (i - 1) / 2;
        }
        public void Kupacol(int i)
        {
            int b = Bal(i);
            int j = Jobb(i);
            int max = i;
            if (b < n && nagyobbPrioritás(E[b], E[max]))
            {
                max = b;
            }
            if (j < n && nagyobbPrioritás(E[j], E[max]))
            {
                max = j;
            }
            if (max != i)
            {
                T seged = E[i];
                E[i] = E[max];
                E[max] = seged;
                Kupacol(max);
            }
        }
        public void KupacotEpit()
        {
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Kupacol(i);
            }
        }
    }
    public class KupacRendezes<T> : Kupac<T> where T : IComparable
    {
        public KupacRendezes(T[] A) : base(A, A.Length, (a, b) => a.CompareTo(b) > 0)
        {
        }
        public void Rendez()
        {

            KupacotEpit();
            for (int i = n - 1; i > 0; i--)
            {
                T seged = E[0];
                E[0] = E[i];
                E[i] = seged;
                n--;
                if (n > 0)
                {
                    Kupacol(0);
                }
            }
        }
    }
    public class KupacPrioritasosSor<T> : Kupac<T>, PrioritasosSor<T>
    {
        public KupacPrioritasosSor(int meret, Func<T, T, bool> nagyobbPrioritás) : base(new T[meret], 0, nagyobbPrioritás)
        {

        }
        private void KulcsotFelvisz(int i)
        {
            int sz = Szulo(i);
            if (sz >= 0 && nagyobbPrioritás(E[i], E[sz]))
            {
                T seged = E[sz];
                E[sz] = E[i];
                E[i] = seged;
                KulcsotFelvisz(sz);
            }
        }

        public bool Ures => n == 0;

        public T Elso()
        {
            if (!Ures)
            {
                return E[0];

            }
            else
            {
                throw new NincsElemKivetel();
            }
        }

        public void Frissit(T elem)
        {
            int i = 0;
            while (i < n && !E[i].Equals(elem))
            {
                i++;
            }
            if (i < n)
            {
                KulcsotFelvisz(i);
                Kupacol(i);
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

                E[n] = ertek;
                n++;
                KulcsotFelvisz(n - 1);
            }
            else
            {
                throw new NincsHelyKivetel();
            }
        }

        public T Sorbol()
        {
            T max;
            if (!Ures)
            {
                max = E[0];
                E[0] = E[n - 1];
                n--;
                Kupacol(0);
                return max;
            }
            else
            {
                throw new NincsElemKivetel();
            }
        }
    }
}