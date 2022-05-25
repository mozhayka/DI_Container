using System;
using SimpleInjector;
using Container;
using System.Diagnostics;

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

        public static SimpleInjector.Container container;
        public static void CompositionRoot()
        {
            container = new SimpleInjector.Container();
            container.Register<House, SimpleHouse>(SimpleInjector.Lifestyle.Singleton);
            container.Register<Door, SimpleDoor>(SimpleInjector.Lifestyle.Singleton);
            container.Register<Window, SimpleWindow>();
        }

        //EXAMPLE 3
        //public static Container.Container container;
        //public static void CompositionRoot()
        //{
        //    container = new Container.Container();
        //    container.Register<UnprotectedHouse, SimpleHouse>(Container.Lifestyle.Singleton);
        //    container.Register<Door, DurableDoor>(Container.Lifestyle.Singleton);
        //    container.Register<Window, SimpleWindow>();
        //}
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

        static void Example4()
        {            
            Stopwatch stopWatch = new();
            stopWatch.Start();

            DIContainer.CompositionRoot();
            House house = DIContainer.container.GetInstance<House>();

            int n = (int)1e7;
            for (int i = 0; i < n; i++)
            {
                if (i % (int)1e6 == 0)
                    Console.Write("|");
                house.AddWindow();
            }

            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            TimeSpan ts = stopWatch.Elapsed;

            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds,
                ts.Milliseconds / 10);
            Console.WriteLine($"\nRunTime {elapsedTime} container {DIContainer.container.GetType()}");
        }

        static void Main(string[] args)
        {
            Example4();
        }
    }
}
