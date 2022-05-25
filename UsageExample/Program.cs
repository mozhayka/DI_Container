using System;
using SimpleInjector;
using Container;

namespace UsageExample
{
    static class DIContainer
    {
        //Чтобы быстро раскоментировать, надо выделить текст, затем ctrl+k ctrl+u, закомментировать ctrl+k ctrl+c

        //EXAMPLE 1
        //public static SimpleInjector.Container container;
        //public static void CompositionRoot()
        //{
        //    container = new SimpleInjector.Container();
        //    container.Register<House, HouseWithoutDoors>(SimpleInjector.Lifestyle.Singleton); // SimpleInjector не поддерживает классы с несколькими конструкторами
        //    container.Register<Window, SimpleWindow>();
        //}


        //public static Container.Container container;
        //public static void CompositionRoot()
        //{
        //    container = new Container.Container();
        //    container.Register<House, HouseWithoutDoors>(Container.Lifestyle.Singleton);
        //    container.Register<Window, SimpleWindow>();
        //}

        //EXAMPLE 2
        //public static Container.Container container;
        //public static void CompositionRoot()
        //{
        //    container = new Container.Container();
        //    container.Register<House, SimpleHouse>(Container.Lifestyle.Singleton);
        //    container.Register<Door, SimpleDoor>(Container.Lifestyle.Singleton);
        //    container.Register<Window, SimpleWindow>();
        //}

        //public static SimpleInjector.Container container;
        //public static void CompositionRoot()
        //{
        //    container = new SimpleInjector.Container();
        //    container.Register<House, SimpleHouse>(SimpleInjector.Lifestyle.Singleton);
        //    container.Register<Door, SimpleDoor>(SimpleInjector.Lifestyle.Singleton);
        //    container.Register<Window, SimpleWindow>();
        //}

        //EXAMPLE 3
        public static Container.Container container;
        public static void CompositionRoot()
        {
            container = new Container.Container();
            container.Register<UnprotectedHouse, SimpleHouse>(Container.Lifestyle.Singleton);
            container.Register<Door, DurableDoor>(Container.Lifestyle.Singleton);
            container.Register<Window, SimpleWindow>();
        }
    }

    static class Program
    {
        static void Example1()
        {
            DIContainer.CompositionRoot();

            var w = DIContainer.container.GetInstance<Window>();
            w.Open();
            w.Print();

            House house = DIContainer.container.GetInstance<House>();
            house.Print();
        }

        static void Example2()
        {
            DIContainer.CompositionRoot();
            House house = DIContainer.container.GetInstance<House>();
            house.SetDoor();
            house.Print();
        }

        static void Example3()
        {
            DIContainer.CompositionRoot();
            UnprotectedHouse house = DIContainer.container.GetInstance<UnprotectedHouse>();
            house.SetDoor();
            house.Print();
            house.RobThisHouse();
        }

        static void Main(string[] args)
        {
            Example3();
        }
    }
}
