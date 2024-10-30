// See https://aka.ms/new-console-template for more information

// 1
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    // Strategy 
    public interface SSugarStrat
    {
        void Sugar();
    }
    public class LightSugar : SSugarStrat
    {
        public void Sugar()
        {
            Console.WriteLine("Чуть чуть сахара");
        }
    }

    public class MediumSugar : SSugarStrat
    {
        public void Sugar()
        {
            Console.WriteLine("Нормально сахара");
        }
    }

    public class SSoda
    {
        private SSugarStrat sugarcount;
        public SSoda(SSugarStrat sugarStrat)
        {
            sugarcount = sugarStrat;
        }

        public void SetSugarStrat(SSugarStrat sugarStrat)
        {
            sugarcount = sugarStrat;
        }

        public void ApplySugar()
        {
            sugarcount.Sugar();
        }
    }

    // Responsibility chain
    public abstract class SodaHandler
    {
        protected SodaHandler nextHandler;

        public void SetNextHandler(SodaHandler nextHandler)
        {
            nextHandler = nextHandler;
        }

        public abstract void HandleRequest(string request);
    }
    public class MixingHandler : SodaHandler
    {
        public override void HandleRequest(string request)
        {
            if (request == "mix")
            {
                Console.WriteLine("Смешиваем ингридиенты");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }

    public class BottlingHandler : SodaHandler
    {
        public override void HandleRequest(string request)
        {
            if (request == "bottle")
            {
                Console.WriteLine("Упаковывем");
            }
            else if (nextHandler != null)
            {
                nextHandler.HandleRequest(request);
            }
        }
    }
    // Iterator 
    public class ISoda
    {
        public string Name { get; set; }

        public ISoda(string name)
        {
            Name = name;
        }
    }
    public interface IIterator<T>
    {
        bool HasNext();
        T Next();
    }
    public class ArrayIterator<T> : IIterator<T>
    {
        private T[] items;
        private int position;

        public ArrayIterator(T[] items)
        {
            this.items = items;
            this.position = 0;
        }

        public bool HasNext()
        {
            return position < items.Length;
        }

        public T Next()
        {
            if (this.HasNext())
            {
                return items[position++];
            }
            throw new IndexOutOfRangeException("Кончились");
        }
    }


    internal class Lab4
    {
        static void Main(string[] args)
        {

            // Strategy 
            SSugarStrat light = new LightSugar();
            SSugarStrat medium = new MediumSugar();
            SSoda soda = new SSoda(light);
            soda.ApplySugar();

            soda.SetSugarStrat(medium);
            soda.ApplySugar();

            // Responsibility chain
            SodaHandler mixingHandler = new MixingHandler();
            SodaHandler bottlingHandler = new BottlingHandler();
            mixingHandler.SetNextHandler(bottlingHandler);

            mixingHandler.HandleRequest("mix");     
            mixingHandler.HandleRequest("bottle");    
            mixingHandler.HandleRequest("something other");

            // Iterator 
            ISoda[] sodas = new ISoda[]
            {
            new ISoda("Cola"),
            new ISoda("Fanta"),
            new ISoda("Pepsi")
            };

            IIterator<ISoda> iterator = new ArrayIterator<ISoda>(sodas);
            while (iterator.HasNext())
            {
                ISoda isoda = iterator.Next();
                Console.WriteLine(isoda.Name);
            }

        }
    }
}