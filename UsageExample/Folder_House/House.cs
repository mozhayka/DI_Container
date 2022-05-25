﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsageExample
{
    interface House
    {
        public void AddWindow();
        public void SetDoor();
        public void Print();
    }

    interface UnprotectedHouse : House
    {
        public void RobThisHouse();
    }

    class HouseWithoutDoors : House
    {
        List<Window> windows;

        public HouseWithoutDoors() : this(3)
        { }

        public HouseWithoutDoors(int windowsCnt)
        {
            windows = new();
            for (int i = 0; i < windowsCnt; i++)
                AddWindow();
        }

        public void AddWindow()
        {
            windows.Add(DIContainer.container.GetInstance<Window>());
        }

        public void Print()
        {
            Console.WriteLine($"This house has {windows.Count} windows");
            windows.ForEach(w => w.Print());
        }

        public void SetDoor()
        {
            throw new NotImplementedException();
        }
    }

    class SimpleHouse : House, UnprotectedHouse
    {
        List<Window> windows;
        Door door;

        public SimpleHouse()
        {
            windows = new();
        }

        public void AddWindow()
        {
            windows.Add(DIContainer.container.GetInstance<Window>());
        }

        public void Print()
        {
            Console.WriteLine($"This house has door");
            door.Print();
            Console.WriteLine($"and {windows.Count} windows");
            windows.ForEach(w => w.Print());
        }

        public void SetDoor()
        {
            door = DIContainer.container.GetInstance<Door>();
        }

        public void RobThisHouse()
        {
            try
            {
                door.KnockDown();
                Console.WriteLine("Inside the house");
                windows[0].Open();
                Console.WriteLine("Success");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Fail");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
