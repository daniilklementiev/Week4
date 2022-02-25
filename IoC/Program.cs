using System;
using Microsoft.Practices.Unity;

namespace IOC
{
    class Greeter
    {
        public String Hello { get => "Hello"; }
    }

    class TimeService
    {
        public string GetTime => DateTime.Now.ToShortTimeString();
        public string GetDate => DateTime.Now.ToShortDateString();
    }
    sealed class Program
    {
        public static UnityContainer container;

        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello IoC!");
            container = new UnityContainer();

            var globalRandom = new Random();
            container.RegisterInstance<Random>(globalRandom);
            container.RegisterType<Greeter>();
            container.RegisterType<TimeService>();


            //var rndI = new RandomInt();
            //rndI.rnd = globalRandom;
            //rndI.Print();

            var time = container.Resolve<TimeService>();
            var rndInt = container.Resolve<RandomInt>();
            var rndDouble = container.Resolve<RandomDouble>();
            var rndChar = container.Resolve<RandomChar>();
            rndInt.Print();
            rndDouble.Print();

            rndChar.Print();

            
        }
    }

    class RandomInt
    {
        [Dependency]
        public Random rnd { get; set; }
        [Dependency] public Greeter greeter { get; set; }
        [Dependency] public TimeService ts { get; set; }
        public void Print()
        {
            Console.WriteLine($"[{ts.GetTime} {ts.GetDate}] {greeter.Hello} int: {rnd.Next()}");
        }
    }

    class RandomDouble
    {
        [Dependency]
        public Random rnd { get; set; }
        [Dependency] public Greeter greeter { get; set; }
        [Dependency] public TimeService ts { get; set; }
        public void Print()
        {
            Console.WriteLine($"[{ts.GetTime} {ts.GetDate}] {greeter.Hello} double: {rnd.NextDouble()}");
        }
    }

    class RandomChar
    {
        private readonly Greeter _greeter;
        private readonly Random _rnd;
        private readonly TimeService _ts;

        public RandomChar(Greeter greeter, Random rnd, TimeService ts)
        {
            _greeter = greeter;
            _rnd = rnd;
            _ts = ts;
        }
        public void Print()
        {
            Console.WriteLine($"[{_ts.GetTime} {_ts.GetDate}] {_greeter.Hello}, symb: {(char)_rnd.Next(32,128)} ");
        }
    }

    
}