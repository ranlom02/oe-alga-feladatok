using System;
using System.Runtime.CompilerServices;

namespace OE.ALGA.Optimalizalas
{
    public class HatizsakProblema
    {
        public int n { get; }
        public int Wmax { get; }
        public int[] w { get; }
        public float[] p { get; }
        public HatizsakProblema(int n, int Wmax, int[] w, float[] p)
        {
            this.n = n;
            this.Wmax = Wmax;
            this.p = p;
            this.w = w;
        }
        public int OsszSuly(bool[] x)
        {
            int s = 0;
            for (int i = 0; i < n; i++)
            {
                if (x[i] == true)
                {
                    s += w[i];
                }
            }
            return s;
        }
        public float OsszErtek(bool[] x)
        {
            float s = 0;
            for (int i = 0; i < n; i++)
            {
                if (x[i] == true)
                {
                    s += p[i];
                }
            }
            return s;
        }
        public bool Ervenyes(bool[] x)
        {
            float s = OsszSuly(x);
            return s <= Wmax;
        }
    }
    public class NyersEro<T>
    {
        public int m;
        public Func<int, T> generator;
        public Func<T, float> josag;
        public int LepesSzam { private set; get; }
        public NyersEro(int m, Func<int, T> generator, Func<T, float> josag)
        {
            this.m = m;
            this.generator = generator;
            this.josag = josag;
        }
        public T OptimalisMegoldas()
        {
            T o = generator(1);
            for (int i = 2; i <= m; i++)
            {
                T x = generator(i);
                LepesSzam++;
                if (josag(x) > josag(o))
                {
                    o = x;
                }
            }
            return o;
        }
    }
    public class NyersEroHatizsakPakolas
    {
        public int LepesSzam { get; private set; }
        private HatizsakProblema problema;

        public NyersEroHatizsakPakolas(HatizsakProblema problema)
        {
            this.problema = problema;
        }
        public bool[] Generator(int i)
        {
            int szam = i - 1;
            bool[] K = new bool[problema.n];
            for (int j = 0; j < problema.n; j++)
            {
                K[j] = ((szam / (int)Math.Pow(2, j)) % 2) == 1;
            }
            return K;
        }
        public float Josag(bool[] K)
        {
            float i = 0;
            if (problema.Ervenyes(K))
            {
                i = problema.OsszErtek(K);
            }
            else
            {
                i = -1;
            }
            return i;
        }
        public bool[] OptimalisMegoldas()
        {
            NyersEro<bool[]> nyersero = new NyersEro<bool[]>((int)Math.Pow(2, problema.n), Generator, Josag);
            bool[] o = nyersero.OptimalisMegoldas();
            LepesSzam = nyersero.LepesSzam;
            return o;
        }
        public float OptimalisErtek()
        {
            bool[] o = OptimalisMegoldas();
            return problema.OsszErtek(o);
        }
    }
}
