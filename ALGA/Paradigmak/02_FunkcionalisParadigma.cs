using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Paradigmak
{
    public class FeltetelesFeladatTarolo<T> : FeladatTarolo<T>, IEnumerable<T> where T : IVegrehajthato
    {
        public Func<T, bool> BejaroFeltetel { get; set; }
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
        public IEnumerator<T> GetEnumerator()
        {
            FeltetelesFeladatTaroloBejaro<T> bejaro = new FeltetelesFeladatTaroloBejaro<T>(tarolo, n, BejaroFeltetel);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
            this.feltetel = feltetel ?? (_ => true);
        }

        public T Current => tarolo[aktualisIndex];

        object IEnumerator.Current => Current;

        public void Dispose()
        {

        }

        public bool MoveNext()
        {

            while (++aktualisIndex < n)
            {
                if (feltetel(tarolo[aktualisIndex]))
                {
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
