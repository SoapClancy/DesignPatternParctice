using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace DesignPatternPractice.Source
{
    internal class AbstractFactoryPattern
    {
        internal abstract class Animal
        {
            internal abstract void Speak();
        }

        internal abstract class Dog : Animal
        {
            internal abstract void PlayBall();
        }

        internal class DogXY : Dog
        {
            internal override void Speak() => Console.WriteLine("I am a XY dog");
            internal override void PlayBall() => Console.WriteLine("XY Dog plays ball");
        }

        internal class DogXX : Dog
        {
            internal override void Speak() => Console.WriteLine("I am a XX dog");
            internal override void PlayBall() => Console.WriteLine("XX Dog plays ball");
        }

        internal interface IAnimalFactory
        {
            public Dog CreateDog();
        }

        internal class AnimalXYFactory : IAnimalFactory
        {
            public Dog CreateDog() => new DogXY();
        }

        internal class AnimalXXFactory : IAnimalFactory
        {
            public Dog CreateDog() => new DogXX();
        }

        public static void Run()
        {
            IAnimalFactory dogXYFactory = new AnimalXYFactory();
            Animal dogXY = dogXYFactory.CreateDog();
            dogXY.Speak();
            (dogXY as DogXY)?.PlayBall();

            IAnimalFactory dogXXFactory = new AnimalXXFactory();
            Animal dogXX = dogXXFactory.CreateDog();
            dogXX.Speak();
            (dogXX as DogXX)?.PlayBall();
        }
    }
}
