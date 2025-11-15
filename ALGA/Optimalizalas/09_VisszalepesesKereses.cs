using System;
using System.Reflection.Metadata.Ecma335;

namespace OE.ALGA.Optimalizalas
{
    public class VisszalepesesOptimalizacio<T>
    {
        public int n;
        public int[] M;
        public T[,] R;
        public Func<int, T, bool> ft;
        public Func<int, T, T[], bool> fk;
        public Func<T[], float> josag;
        public int LepesSzam { get; private set; }

        public VisszalepesesOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag)
        {
            this.n = n;
            M = m;
            R = r;
            this.ft = ft;
            this.fk = fk;
            this.josag = josag;
            LepesSzam = 0;
        }
        public virtual void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (i < M[szint] - 1)
            {
                i++;
                LepesSzam++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        if (szint == n - 1)
                        {
                            if (!van || josag(E) > josag(O))
                            {
                                E.CopyTo(O, 0);
                            }
                            van = true;
                        }
                        else
                        {
                            Backtrack(szint + 1, ref E, ref van, ref O);
                        }
                    }
                }
            }
        }
        public T[] OptimalisMegoldas()
        {
            bool van = false;
            T[] E = new T[n];
            T[] O = new T[n];
            Backtrack(0, ref E, ref van, ref O);
            return O;
        }
    }
    public class VisszalepesesHatizsakPakolas
    {
        public HatizsakProblema problema;
        public int LepesSzam { get; private set; }

        public VisszalepesesHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
            LepesSzam = 0;
        }
        public virtual bool[] OptimalisMegoldas()
        {
            int[] M = new int[problema.n];
            bool[,] R = new bool[problema.n, 2];
            for (int i = 0; i < problema.n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }
            bool ft(int szint, bool r)
            {
                return true;
            }
            bool fk (int szint, bool r, bool[] E)
            {
                int s = 0;
                if (r==false)
                {
                    return true;
                }
                else
                {
                    s = problema.w[szint];
                    int i = 0;
                    while ( i< szint && s <= problema.Wmax)
                    {
                        if (E[i])
                        {
                            s = s + problema.w[i];
                        }
                        i++;
                    }
                }
                return s <= problema.Wmax;
            }
            float josag(bool[] E)
            {
                return problema.OsszErtek(E);
            }

            VisszalepesesOptimalizacio<bool> visszalepeses = new VisszalepesesOptimalizacio<bool>(problema.n, M, R, ft, fk, josag);
            
            bool[] eredmeny = visszalepeses.OptimalisMegoldas();
            LepesSzam = visszalepeses.LepesSzam;
            return eredmeny;
        }
        public float OptimalisErtek()
        {
            bool[] eredmeny = OptimalisMegoldas();
            float vege = problema.OsszErtek(eredmeny);
            return vege;
        }
        
    }
    public class SzetvalasztasEsKorlatozasOptimalizacio<T> : VisszalepesesOptimalizacio<T>
    {
        Func<int, T[], float> fb;
        public int LepesSzam { get; private set; }
        
        public SzetvalasztasEsKorlatozasOptimalizacio(int n, int[] m, T[,] r, Func<int, T, bool> ft, Func<int, T, T[], bool> fk, Func<T[], float> josag, Func<int, T[], float> fb) 
            : base(n, m, r, ft, fk, josag)
        {
            this.fb = fb;
            LepesSzam = 0;
        }
        public override void Backtrack(int szint, ref T[] E, ref bool van, ref T[] O)
        {
            int i = -1;
            while (i < M[szint] - 1)
            {
                i++;
                LepesSzam++;
                if (ft(szint, R[szint, i]))
                {
                    if (fk(szint, R[szint, i], E))
                    {
                        E[szint] = R[szint, i];
                        
                        if (szint == n-1)
                        {
                            if(!van || josag(E) > josag(O))
                            {
                                E.CopyTo(O, 0);
                                
                            }
                            van = true;
                        }
                        else
                        {
                            if (josag(E) + fb(szint, E) > josag(O))
                            {
                                
                                Backtrack(szint + 1, ref E, ref van, ref O);
                            }
                            
                        }
                    }
                }
            }
        }
    }
    public class SzetvalasztasEsKorlatozasHatizsakPakolas : VisszalepesesHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        public SzetvalasztasEsKorlatozasHatizsakPakolas(HatizsakProblema problema) : base(problema)
        {
            LepesSzam = 0;
        }
        public override bool[] OptimalisMegoldas()
        {
            
            int[] M = new int[problema.n];
            bool[,] R = new bool[problema.n, 2];
            for (int i = 0; i < problema.n; i++)
            {
                M[i] = 2;
                R[i, 0] = true;
                R[i, 1] = false;
            }
            bool ft(int szint, bool r)
            {
                return true;
            }
            bool fk(int szint, bool r, bool[] E)
            {
                int s = 0;
                if (r == false)
                {
                    return true;
                }
                else
                {
                    s = problema.w[szint];
                    int i = 0;
                    while (i < szint && s <= problema.Wmax)
                    {
                        if (E[i])
                        {
                            s = s + problema.w[i];
                        }
                        i++;
                    }
                }
                return s <= problema.Wmax;
            }
            float josag(bool[] E)
            {
                return problema.OsszErtek(E);
            }
            float fb (int szint, bool[] E)
            {
                float b = 0;
                for (int i = szint; i < problema.n; i++)
                {
                    if (problema.OsszSuly(E) + problema.w[i]<= problema.Wmax)
                    {
                        b = b + problema.p[i];
                    }
                }
                return b;
            }

            SzetvalasztasEsKorlatozasOptimalizacio<bool> szetvalasztas = new SzetvalasztasEsKorlatozasOptimalizacio<bool>(problema.n, M, R, ft, fk, josag, fb);

            bool[] eredmeny = szetvalasztas.OptimalisMegoldas();
            LepesSzam = szetvalasztas.LepesSzam;
            return eredmeny;

        }
    }
}
