using System;
using SimpleInjector;
using Container;

namespace UsageExample
{
    static class DIContainer
    {
        /*
        public static SimpleInjector.Container container;
        public static void CompositionRoot()
        {
            container = new SimpleInjector.Container();
            container.Register<House, HouseWithoutDoors>(SimpleInjector.Lifestyle.Singleton);
            container.Register<Window, SimpleWindow>();
        }
        */

        public static Container.Container container;
        public static void CompositionRoot()
        {
            container = new Container.Container();
            container.Register<House, HouseWithoutDoors>(Container.Lifestyle.Singleton);
            container.Register<Window, SimpleWindow>();
            Console.WriteLine("Container is configured");
        }
    }

    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Run main");
            DIContainer.CompositionRoot();
            var w = DIContainer.container.GetInstance<Window>();
            w.Open();
            w.Print();
            House house = DIContainer.container.GetInstance<House>();
            house.Print();
        }
    }
}
