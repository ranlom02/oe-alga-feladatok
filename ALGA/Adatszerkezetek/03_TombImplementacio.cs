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
}
