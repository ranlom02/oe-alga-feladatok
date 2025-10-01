using System;

namespace OE.ALGA.Adatszerkezetek
{
    internal class SzotarElem<K, T>
    {
        public K kulcs;
        public T tart;

        public SzotarElem(K kulcs, T tart)
        {
            this.kulcs = kulcs;
            this.tart = tart;
        }
    }
    public class HasitoSzotarTulcsordulasiTerulettel<K, T> : Szotar<K, T>
    {
        private SzotarElem<K, T>[]? E;
        private Func<K, int> h;
        Lista<SzotarElem<K, T>> U = new LancoltLista<SzotarElem<K, T>>();
        public HasitoSzotarTulcsordulasiTerulettel(int meret, Func<K, int> hasitofuggveny)
        {
            E = new SzotarElem<K, T>[meret];
            h = (x => Math.Abs(hasitofuggveny(x)) % E.Length);
        }
        public HasitoSzotarTulcsordulasiTerulettel(int meret) : this(meret, x => x.GetHashCode())
        {

        }
        private SzotarElem<K, T> KulcsKeres(K kulcs)
        {
            if (E[h(kulcs)] != null && E[h(kulcs)].kulcs.Equals(kulcs))
            {
                return E[h(kulcs)];
            }
            else
            {
                SzotarElem<K, T> e = null;
                U.Bejar(x =>
                {
                    if (x.kulcs.Equals(kulcs))
                    {
                        e = x;
                    }
                });
                return e;
            }
        }
        public void Beir(K kulcs, T ertek)
        {
            SzotarElem<K, T> meglevo = KulcsKeres(kulcs);
            if (meglevo != null)
            {
                meglevo.tart = ertek;
            }
            else
            {
                SzotarElem<K, T> uj = new SzotarElem<K, T>(kulcs, ertek);
                if (E[h(kulcs)] == null)
                {
                    E[h(kulcs)] = uj;
                }
                else
                {
                    U.Hozzafuz(uj);
                }
            }
        }

        public T Kiolvas(K kulcs)
        {
            SzotarElem<K, T> meglevo = KulcsKeres(kulcs);
            if (meglevo != null)
            {
                return meglevo.tart;
            }
            else
            {
                throw new HibasKulcsKivetel();
            }
        }

        public void Torol(K kulcs)
        {
            if (E[h(kulcs)] != null && E[h(kulcs)].kulcs.Equals(kulcs))
            {
                E[h(kulcs)] = null;
            }
            else
            {
                SzotarElem<K, T> e = null;
                U.Bejar(x =>
                {
                    if (x.kulcs.Equals(kulcs))
                    {
                        e = x;
                    }
                });
                if (e != null)
                {
                    U.Torol(e);
                }
                else
                {
                    throw new HibasKulcsKivetel();
                }
            }
        }
    }
}
