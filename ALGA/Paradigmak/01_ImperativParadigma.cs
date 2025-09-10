using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        public void Vegrehajtas();
    }
    public class FeladatTarolo<T>:IEnumerable<T> where T : IVegrehajthato
    {
        protected T[] tarolo;
        public FeladatTarolo(int meret)
        {
            tarolo = new T[meret];
        }
        protected int n = 0;
        public void Felvesz(T a)
        {
            if (n < tarolo.Length)
            {
                tarolo[n] = a;
                n++;
            }
            else
            {
                throw new TaroloMegteltKivetel();
            }
        }
        public virtual void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                tarolo[i].Vegrehajtas();
            }
            //foreach (var item in tarolo) nem jó mert nullákat is végrehajt, nullexception 
        }

        public IEnumerator<T> GetEnumerator()
        {
            FeladatTaroloBejaro<T> bejaro = new FeladatTaroloBejaro<T>(tarolo, n);
            return bejaro;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    public interface IFuggo
    {
        public bool FuggosegTeljesul { get; }
    }
    public class FuggoFeladatTarolo<T> : FeladatTarolo<T> where T : IVegrehajthato, IFuggo
    {
        public FuggoFeladatTarolo(int meret) : base(meret)
        {
        }
        override public void MindentVegrehajt()
        {
            for (int i = 0; i < n; i++)
            {
                
                
                if (tarolo[i].FuggosegTeljesul)
                {
                        tarolo[i].Vegrehajtas();
                }
            }
        }
    }

    public class TaroloMegteltKivetel : Exception
    {

    }
    class FeladatTaroloBejaro<T> : IEnumerator<T>
    {
        T[] tarolo;
        int n;
        int aktualis = -1;
        public FeladatTaroloBejaro(T[] tarolo, int n)
        {
            this.tarolo = tarolo;
            this.n = n;
        }

        public T Current => tarolo[aktualis];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            if(aktualis< n - 1)
            {
                aktualis++;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            aktualis=-1;
        }
    }

}




