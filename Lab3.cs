// See https://aka.ms/new-console-template for more information

// 1
using System;

namespace MyApp // Note: actual namespace depends on the project name.
{
    // Singleton 
    public class Soda
    {
        private static Soda instance = null;

        public string Flavor { get; private set; }
        private Soda(string flavor)
        {
            Flavor = flavor;
        }
        public static Soda getInstance(string flavor)
        {
            if (instance == null)
            {
                instance = new Soda(flavor);
            }
            return instance;
        }
    }

    // Factory method
    public interface FSoda
    {
        string getFlavor();
    }
    public class Cola : FSoda
    {
        public string getFlavor()
        {
            return "Cola";
        }
    }

    public class Fanta : FSoda
    {
        public string getFlavor()
        {
            return "Fanta";
        }
    }
    public abstract class SodaFactory
    {
        public abstract FSoda CreateSoda();
    }
    public class ColaFactory : SodaFactory
    {
        public override FSoda CreateSoda()
        {
            return new Cola();
        }
    }

    public class FantaFactory : SodaFactory
    {
        public override FSoda CreateSoda()
        {
            return new Fanta();
        }
    }
    // Abstract factory
    public interface ASoda
    {
        string GetFlavor();
        string GetColor();
    }

    public interface ABottle
    {
        string GetMaterial();
        int GetCapacity();
    }
    public class ACola : ASoda
    {
        public string GetFlavor() => "Cola";
        public string GetColor() => "Brown";
    }

    public class AFantaSoda : ASoda
    {
        public string GetFlavor() => "Fanta";
        public string GetColor() => "Orange";
    }

    public class APlasticBottle : ABottle
    {
        public string GetMaterial() => "Plastic";
        public int GetCapacity() => 500;
    }

    public class AGlassBottle : ABottle
    {
        public string GetMaterial() => "Glass";
        public int GetCapacity() => 330;
    }
    public interface ASodaFactory
    {
        ASoda CreateSoda();
        ABottle CreateBottle();
    }
    public class AColaFactory : ASodaFactory
    {
        public ASoda CreateSoda() => new ACola();
        public ABottle CreateBottle() => new APlasticBottle();
    }

    public class AFantaSodaFactory : ASodaFactory
    {
        public ASoda CreateSoda() => new AFantaSoda();
        public ABottle CreateBottle() => new AGlassBottle();
    }

    // Builder 
    public class BSoda
    {
        public string Flavor { get; set; }
        public string Color { get; set; }
        public int Sugar { get; set; }

        public override string ToString()
        {
            return $"Flavor: {Flavor}, Color: {Color}, Sugar: {Sugar}";
        }
    }
    public interface BSodaBuilder
    {
        void BSetFlavor();
        void BSetColor();
        void BSetSugar();
        BSoda BGetSoda();
    }
    public class BColaBuilder : BSodaBuilder
    {
        private BSoda soda = new BSoda();

        public void BSetFlavor() => soda.Flavor = "Cola";
        public void BSetColor() => soda.Color = "Brown";
        public void BSetSugar() => soda.Sugar = 5;

        public BSoda BGetSoda() => soda;
    }

    public class BFantaSodaBuilder : BSodaBuilder
    {
        private BSoda soda = new BSoda();

        public void BSetFlavor() => soda.Flavor = "Fanta";
        public void BSetColor() => soda.Color = "Orange";
        public void BSetSugar() => soda.Sugar = 4;

        public BSoda BGetSoda() => soda;
    }

    public interface BSodaFactory
    {
        BSodaBuilder CreateSodaBuilder();
    }

    public class BColaFactory : BSodaFactory
    {
        public BSodaBuilder CreateSodaBuilder()
        {
            return new BColaBuilder();
        }
    }

    public class BFantaSodaFactory : BSodaFactory
    {
        public BSodaBuilder CreateSodaBuilder()
        {
            return new BFantaSodaBuilder();
        }
    }
    public class BSodaDirector
    {
        public void Construct(BSodaBuilder sodaBuilder)
        {
            sodaBuilder.BSetFlavor();
            sodaBuilder.BSetColor();
            sodaBuilder.BSetSugar();
        }
    }



    internal class Lab3
    {
        public static void Main()
        {
            // Singleton
            Soda mySoda = Soda.getInstance("Cola");
            Console.WriteLine(mySoda.Flavor);

            Soda anotherSoda = Soda.getInstance("Fanta");
            Console.WriteLine(anotherSoda.Flavor);

            // Factory method
            SodaFactory colaFactory = new ColaFactory();
            FSoda cola = colaFactory.CreateSoda();
            Console.WriteLine(cola.getFlavor());

            SodaFactory fantaFactory = new FantaFactory();
            FSoda fanta = fantaFactory.CreateSoda();
            Console.WriteLine(fanta.getFlavor());

            // Abstract factorya
            ASodaFactory acolaFactory = new AColaFactory();
            ASoda acola = acolaFactory.CreateSoda();
            ABottle acolaBottle = acolaFactory.CreateBottle();
            Console.WriteLine($"Тип: {acola.GetFlavor()}, Цвет: {acola.GetColor()}, Бутылка: {acolaBottle.GetMaterial()}, Capacity: {acolaBottle.GetCapacity()} ml");

            ASodaFactory afantaSodaFactory = new AFantaSodaFactory();
            ASoda afantaSoda = afantaSodaFactory.CreateSoda();
            ABottle afantaSodaBottle = afantaSodaFactory.CreateBottle();
            Console.WriteLine($"Тип: {afantaSoda.GetFlavor()}, Цвет: {afantaSoda.GetColor()}, Бутылка: {afantaSodaBottle.GetMaterial()}, Capacity: {afantaSodaBottle.GetCapacity()} ml");

            // Builder 
            BSodaDirector director = new BSodaDirector();
            BSodaFactory bcolaFactory = new BColaFactory();
            BSodaBuilder bcolaBuilder = bcolaFactory.CreateSodaBuilder();
            director.Construct(bcolaBuilder);
            BSoda bcola = bcolaBuilder.BGetSoda();
            Console.WriteLine(bcola);

            BSodaFactory bfantaFactory = new BFantaSodaFactory();
            BSodaBuilder bfantaBuilder = bfantaFactory.CreateSodaBuilder();
            director.Construct(bfantaBuilder);
            BSoda bfantaSoda = bfantaBuilder.BGetSoda();
            Console.WriteLine(bfantaSoda);

        }
    }
}