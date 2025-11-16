using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Adatszerkezetek
{
    public class EgeszGrafEl : GrafEl<int>, IComparable
    {
        public EgeszGrafEl(int honnan, int hova)
        {
            Honnan = honnan;
            Hova = hova;
        }

        public int Honnan { get; }

        public int Hova { get; }

        public int CompareTo(object? obj)
        {
            EgeszGrafEl other = obj as EgeszGrafEl;
            if (Honnan != other.Honnan)
            {
                return Honnan.CompareTo(other.Honnan);
            }
            if (Honnan == other.Honnan)
            {
                return Hova.CompareTo(other.Hova);
            }
            else
            {
                return 0;
            }
        }
    }
    public class CsucsmatrixSulyozatlanEgeszGraf : SulyozatlanGraf<int, EgeszGrafEl>
    {
        int n;
        bool[,] M;

        public CsucsmatrixSulyozatlanEgeszGraf(int n)
        {
            this.n = n;
            M = new bool[n, n];
        }

        public int CsucsokSzama { get { return n; } }

        public int ElekSzama 
        {
            get 
            {
                int m = 0; 
                for (int i = 0; i< n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (M[i, j])
                        {
                            m++;
                        }
                    }
                }
                return m;
            } 
        }

        public Halmaz<int> Csucsok
        {
            get
            {
                FaHalmaz<int> faHalmaz = new FaHalmaz<int> ();
                for (int i = 0;i< n; i++)
                {
                    faHalmaz.Beszur(i);
                }
                return faHalmaz;
            }
        }

        public Halmaz<EgeszGrafEl> Elek
        {
            get
            {
                FaHalmaz<EgeszGrafEl> fahalmaz = new FaHalmaz<EgeszGrafEl> ();
                for (int i = 0; i<n; i++)
                {
                    for (int j = 0;j < n; j++)
                    {
                        if (M[i, j])
                        {
                            EgeszGrafEl grafEl = new EgeszGrafEl(i, j);
                            fahalmaz.Beszur(grafEl);
                        }
                    }
                }
                return fahalmaz;
            }
        }

        public Halmaz<int> Szomszedai(int csucs)
        {
            FaHalmaz<int> faHalmaz = new FaHalmaz<int>();
            for (int i = 0; i< n; i++)
            {
                if (M[csucs, i])
                {
                    faHalmaz.Beszur(i);
                }
            }
            return faHalmaz;
        }

        public void UjEl(int honnan, int hova)
        {
            M[honnan, hova] = true;
        }

        public bool VezetEl(int honnan, int hova)
        {
            if (M[honnan, hova])
            {
                return true;
            }
            return false;
        }
    }
    public class GrafBejarasok
    {
        public static Halmaz<V> SzelessegiBejaras<V,E> (Graf<V, E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Sor<V> S = new LancoltSor<V>();
            Halmaz<V> F = new FaHalmaz<V>();
            S.Sorba(start);
            F.Beszur(start);
            while (!S.Ures)
            {
                V k = S.Sorbol();
                muvelet(k);
                Halmaz<V> szomszedok = g.Szomszedai(k);
                szomszedok.Bejar(x =>
                {
                    if(!F.Eleme(x))
                    {
                        S.Sorba(x);
                        F.Beszur(x);
                    }
                });
            }
            return F;
        }
        public static Halmaz<V> MelysegiBejaras<V,E>(Graf<V,E> g, V start, Action<V> muvelet) where V : IComparable
        {
            Halmaz<V> F = new FaHalmaz<V>();
            
            MelysegiBejarasRekurzio(g, start,ref F, muvelet);
            return F;

        }
        public static void MelysegiBejarasRekurzio<V,E>(Graf<V,E> g, V k, ref Halmaz<V> F, Action<V> muvelet)
        {
            F.Beszur(k);
            muvelet(k);
            Halmaz<V> seged = F;
            Halmaz<V> szomszedok = g.Szomszedai(k);
            szomszedok.Bejar(x =>
            {
                if (!seged.Eleme(x))
                {
                    MelysegiBejarasRekurzio(g, x, ref seged, muvelet);
                }
            });
        }
    }
}