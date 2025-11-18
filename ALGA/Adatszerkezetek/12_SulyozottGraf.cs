using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace OE.ALGA.Adatszerkezetek
{
    public class SulyozottEgeszGrafEl : EgeszGrafEl, SulyozottGrafEl<int>
    {
        public SulyozottEgeszGrafEl(int honnan, int hova, float suly) : base(honnan, hova)
        {
            Suly = suly;
        }

        public float Suly { get; }
    }
    public class CsucsmatrixSulyozottEgeszGraf : SulyozottGraf<int, SulyozottEgeszGrafEl>
    {
        public int n { get; set; }
        public float[,] M { get; set; }
        public CsucsmatrixSulyozottEgeszGraf(int n)
        {
            this.n = n;
            this.M = new float[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.M[i, j] = float.NaN;
                }
            }
        }


        public int CsucsokSzama => n;

        public int ElekSzama
        {
            get
            {
                int Elszam = 0;
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!float.IsNaN(M[i, j]))
                        {
                            Elszam++;
                        }
                    }
                }
                return Elszam;
            }
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                FaHalmaz<int> Csucs = new FaHalmaz<int>();
                for (int i = 0; i < n; i++)
                {
                    Csucs.Beszur(i);
                }
                return Csucs;
            }
        }

        public Halmaz<SulyozottEgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<SulyozottEgeszGrafEl> El = new FaHalmaz<SulyozottEgeszGrafEl>();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (!float.IsNaN(M[i, j]))
                        {
                            El.Beszur(new SulyozottEgeszGrafEl(i, j, M[i, j]));
                        }
                    }
                }
                return El;
            }
        }

        public float Suly(int honnan, int hova)
        {
            if (float.IsNaN(M[honnan, hova]))
            {
                throw new NincsElKivetel();
            }
            return M[honnan, hova];
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> faHalmaz = new FaHalmaz<int>();
            for (int i = 0; i < n; i++)
            {
                if (!float.IsNaN(M[csucs, i]))
                {
                    faHalmaz.Beszur(i);
                }
            }
            return faHalmaz;
        }

        public void UjEl(int honnan, int hova, float suly)
        {

            M[honnan, hova] = suly;
        }

        public bool VezetEl(int honnan, int hova)
        {
            if (!float.IsNaN(M[honnan, hova]))
            {
                return true;
            }
            return false;
        }

    }
    public class Utkereses
    {
        public static Szotar<V, float> Dijkstra<V, E>(SulyozottGraf<V, E> g, V start)
        {
            Szotar<V, float> L = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            PrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (a, b) => L.Kiolvas(a) < L.Kiolvas(b));
            g.Csucsok.Bejar(x =>
            {
                L.Beir(x, float.PositiveInfinity);
                S.Sorba(x);
            });
            L.Beir(start, 0f);
            S.Frissit(start);
            while (!S.Ures)
            {
                V u = S.Sorbol();
                g.Szomszedai(u).Bejar(x =>
                {
                    if (L.Kiolvas(u) + g.Suly(u, x) < L.Kiolvas(x))
                    {
                        L.Beir(x, L.Kiolvas(u) + g.Suly(u, x));
                    }
                });
            }
            return L;
        }
    }
    public class FeszitofaKereses
    {
        public static Szotar<V, V> Prim<V, E>(SulyozottGraf<V, E> g, V start) where V : IComparable
        {
            Szotar<V, float> K = new HasitoSzotarTulcsordulasiTerulettel<V, float>(g.CsucsokSzama);
            Szotar<V, V> P = new HasitoSzotarTulcsordulasiTerulettel<V, V>(g.CsucsokSzama);
            PrioritasosSor<V> S = new KupacPrioritasosSor<V>(g.CsucsokSzama, (a, b) => K.Kiolvas(a) < K.Kiolvas(b));

            g.Csucsok.Bejar(x =>
            {
                K.Beir(x, float.PositiveInfinity);
                P.Beir(x, default!);
                S.Sorba(x);



            });
            K.Beir(start, 0f);
            S.Frissit(start);
            FaHalmaz<V> SEleme = new FaHalmaz<V>();
            while (!S.Ures)
            {
                V u = S.Sorbol();
                SEleme.Beszur(u);
                g.Szomszedai(u).Bejar(x =>
                {
                    if (!SEleme.Eleme(x) && g.Suly(u, x) < K.Kiolvas(x))
                    {
                        K.Beir(x, g.Suly(u, x));
                        P.Beir(x, u);
                        S.Frissit(x);
                    }
                });
            }
            return P;
        }
        public static Halmaz<E> Kruskal<V, E>(SulyozottGraf<V, E> g) where E : SulyozottGrafEl<V>, IComparable where V : IComparable
        {
            Halmaz<E> A = new FaHalmaz<E>();
            PrioritasosSor<E> S = new KupacPrioritasosSor<E>(g.ElekSzama, (a, b) => a.Suly < b.Suly);
            List<FaHalmaz<V>> Halmazok = new List<FaHalmaz<V>>();
            
            g.Csucsok.Bejar(x =>
            {
                FaHalmaz<V> seged = new FaHalmaz<V>();
                seged.Beszur(x);
                Halmazok.Add(seged);
            });
            g.Elek.Bejar(u => S.Sorba(u));
            while (!S.Ures)
            {
                E v = S.Sorbol();
                if(Tartalmaz(v.Honnan)!= Tartalmaz(v.Hova))
                {
                    A.Beszur(v);
                    Osszevon(Tartalmaz(v.Honnan), Tartalmaz(v.Hova));
                }
                
            }
            return A;
            int Tartalmaz(V v)
            {
                for(int i=0; i<Halmazok.Count; i++)
                {
                    if (Halmazok[i].Eleme(v))
                    {
                        return i;
                    }
                }
                return -1;
            }
            void Osszevon(int a, int b)
            {
                List<V> tagok = new List<V>();
                Halmazok[b].Bejar(x=> {  tagok.Add(x); });
                foreach (var x in tagok)
                {
                    Halmazok[a].Beszur(x);
                    Halmazok[b].Torol(x);
                }
                Halmazok.RemoveAt(b);
            }
        }
    }
}
