using System;
using System.Collections;
using System.Collections.Generic;

namespace OE.ALGA.Paradigmak
{
    public interface IVegrehajthato
    {
        public Vegrehajtas();
    }
    public class FeladatTarolo<T> where T : IVegrehajthato
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
        public void MindentVegrehajt()
        {
            foreach (var item in tarolo)
            {
                item.Vegrehajtas();
            }
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
            foreach (var item in tarolo)
            {
                if (item.FuggosegTeljesul)
                {
                    item.Vegrehajtas();
                }
            }
        }
    }

    public class TaroloMegteltKivetel : Exception
    {
        
    }

    public interface IBejarhato : IEnumerable
    {

    }
    public interface IBejaro : IEnumerator
    {

    }

