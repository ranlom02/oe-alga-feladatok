using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato
    {
        public FeltetelesFeladatTarolo(int meret) : base(meret)
        {
        }
        public void FeltetelesVegrehajtas(Func<T, bool> feltetel)
        {
            for (int i = 0; i < n; i++)
            {
                if (feltetel(tarolo[i]))
                {
                    tarolo[i].Vegrehajtas();
                }
            }
        }
        public class FeltetelesFeladatTaroloBejaro<T> : IEnumerator<T>
        {
            T[] tarolo;
            int n;
            int aktualisIndex = -1;
            Func<T, bool> feltetel;

            public FeltetelesFeladatTaroloBejaro(T[] tarolo, int n, Func<T, bool> feltetel)
            {
                this.tarolo = tarolo;
                this.n = n;
                this.feltetel = feltetel;
            }

            public T Current => Current;

            object IEnumerator.Current => Current;

            public void Dispose()
            {

            }

            public bool MoveNext()
            {

                for (int i = aktualisIndex; i < n - 1; i++)
                {
                    if (feltetel(tarolo[i]))
                    {
                        aktualisIndex = i + 1;
                        return true;
                    }
                }
                return false;
            }

            public void Reset()
            {
                aktualisIndex = -1;
            }
        }
    }
}
