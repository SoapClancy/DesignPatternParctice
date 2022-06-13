using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    internal class FactoryMethodPattern
    {
        internal abstract class Animal
        {
            internal abstract void Speak();
        }

        internal class Dog : Animal
        {
            private readonly int age;
            public Dog()
            {
            }

            public Dog(int age)
            {
                this.age = age;
            }

            internal override void Speak() => Console.WriteLine("I am a dog");
            internal void PlayBall() => Console.WriteLine("Dog plays ball");
            internal void SayAge() => Console.WriteLine($"Dog with age = {age}");
        }

        internal class Cat : Animal
        {
            internal override void Speak() => Console.WriteLine("I am a cat");
            internal void CatchMouse() => Console.WriteLine("Cat catches mouse");
        }

        internal abstract class AbstractAnimalFactory
        {
            // This will use parameterless constructor 
            internal abstract T Create<T>() where T : Animal;
            // This will use the constructor that best matches the specified parameters.
            internal abstract T Create<T>(params object[] parameter) where T : Animal;
        }

        internal class AnimalFactory : AbstractAnimalFactory
        {
            internal override T Create<T>()
            {
                T animal;
                try
                {
                    animal = Activator.CreateInstance<T>();
                }
                catch
                {
                    Console.WriteLine($"Fail to create {typeof(T)}");
                    throw;
                }

                return animal;
            }

            internal override T Create<T>(params object[] parameter)
            {
                T? animal;
                try
                {
                    object? obj = Activator.CreateInstance(typeof(T), parameter);
                    animal = obj as T;
                    // animal = (T?)Activator.CreateInstance(typeof(T), parameter); is also OK
                    if (animal == null)  // To avoid CS8603 warning 
                    {
                        throw new Exception();
                    }
                }
                catch
                {
                    Console.WriteLine($"Fail to create {typeof(T)}");
                    throw;
                }

                return animal;
            }
        }

        public static void Run()
        {
            AnimalFactory animalFactory = new AnimalFactory();

            /* To create dog object, using parameterless constructor */
            Animal dog = animalFactory.Create<Dog>();
            dog.Speak();
            // Note:
            // Dog.GetType() is wrong, since it should be obj.GetType().
            // Also,
            // typeof(dog) is also wrong, because:
            // 1) it should be determined as compile time;
            // 2) it should be typeof(type)
            // Be aware, Dog is a type, and it is NOT "class object" 
            // (C# is not Python, only Python has concept like "a cls is obj of its mcs"
            Console.WriteLine($"dog.GetType().Name = {dog.GetType().Name}");

            // Note:
            // The code below show type is still Dog
            Animal animal = (Animal)dog;
            Console.WriteLine($"animal.GetType().Name = {animal.GetType().Name}");

            // Note:
            // dog.PlayBall(); gives compile error, even though dog.GetType().Name == Dog
            // But the following can work
            (dog as Dog)?.PlayBall();

            // However, using reflection can have access to the "PlayBall" method
            // Because dog.GetType().Name == Dog
            // Also, BindingFlags should be used to access internal instance method
            Console.Write("Use reflection for dog variable: ");
            dog.GetType().GetMethod(
                "PlayBall",
                BindingFlags.Instance | BindingFlags.NonPublic
            )?.Invoke(dog, null);

            // Even animal (basically, also Dog) can also have PlayBall method of Dog type
            Console.Write("Use reflection for animal variable: ");
            animal.GetType().GetMethod(
                "PlayBall",
                BindingFlags.Instance | BindingFlags.NonPublic
            )?.Invoke(dog, null);

            /* To create cat object, using parameterless constructor */
            Console.WriteLine();
            Animal cat = animalFactory.Create<Cat>();
            cat.Speak();
            Console.WriteLine($"cat.GetType().Name = {cat.GetType().Name}");
            (cat as Cat)?.CatchMouse();

            /* To create dog object, using parameterless constructor */
            // Note:
            // The code below will go into "T Create<T>()" instead of "T Create<T>(params object[] parameter)"
            Animal anotherDog = animalFactory.Create<Dog>();

            /* To create dog object with Age */
            Console.WriteLine();
            Animal dogWithAge = animalFactory.Create<Dog>(10);
            Console.WriteLine($"dogWithAge.GetType().Name = {dogWithAge.GetType().Name}");
            (dogWithAge as Dog)?.SayAge();
        }
    }
}
