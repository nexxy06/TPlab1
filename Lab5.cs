
using System;
using static MyApp.Lab5;
using static MyApp.Lab5.SweetsProxy;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Lab5
    {   
        // Proxy
        public interface Sweets
        {
            void eat();

        }
        public class RealSweets : Sweets
        {
            public void eat()
            {
                Console.WriteLine("съел");
            }
        }
        public class SweetsProxy : Sweets
        {
            private RealSweets realSweets;
            private int age;

            public SweetsProxy(int age)
            {
                this.realSweets = new RealSweets();
                this.age = age;
            }

            public void eat()
            {
                if (age > 10)
                {
                    realSweets.eat();
                }
                else
                {
                    Console.WriteLine("Мама не разрешает");
                }
            }
            // Adapter
            public class ReturnInfo
            {
                public string retMessage(String msg)
                {
                    return "Адаптировал сообщение: " + msg;
                }
            }
            public interface Consol
            {
                void gets(String message);
            }

            public class ConsolAdapter : Consol
            {
                private ReturnInfo returnInfo;

                public ConsolAdapter(ReturnInfo returnInfo)
                {
                    this.returnInfo = returnInfo;
                }

                public void gets(String message)
                {
                    Console.WriteLine(returnInfo.retMessage(message)); // Адаптируем метод
                }
            }

            // Bridge
            public interface Device
            {
                void print(String data);
            }
            public class Monitor : Device
            {
                public void print(String data)
                {
                    Console.WriteLine("Displaying on monitor: " + data);
                }
            }

            public class Printer : Device
            {
                public void print(String data)
                {
                    Console.WriteLine("Printing to paper: " + data);
                }
            }
            public abstract class Output
            {
                protected Device device;
                public Output(Device device)
                {
                    this.device = device;
                }
                public abstract void render(String data);
            }
            public class TextOutput : Output    
            {
                public TextOutput(Device device) : base(device)
                {
                }
                public override void render(String data)
                {
                    device.print("Text: " + data);
                }
            }
            public class ImageOutput : Output
            {
                public ImageOutput(Device device) : base(device)
                {
                }

                public override void render(String data)
                {
                    device.print("Image: [Binary data: " + data + "]");
                }
            }
    static void Main(string[] args)
            {   
                // Proxy
                Sweets kid = new SweetsProxy(7);
                Sweets adult = new SweetsProxy(37);
                kid.eat();
                adult.eat();

                // Adapter
                ReturnInfo returnInfo = new ReturnInfo();
                Consol consol = new ConsolAdapter(returnInfo);
                consol.gets("1010101002");

                // Bridge
                Device monitor = new Monitor();
                Device printer = new Printer();

                Output textOnMonitor = new TextOutput(monitor);
                Output textOnPrinter = new TextOutput(printer);

                textOnMonitor.render("Hello, world!"); 
                textOnPrinter.render("Hello, world!");

                Output imageOnMonitor = new ImageOutput(monitor);
                imageOnMonitor.render("101010101");
            }
        }
    }
}