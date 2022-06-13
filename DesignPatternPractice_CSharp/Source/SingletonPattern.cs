using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    internal class SingletonPattern
    {
        private static SingletonPattern instance;

        static SingletonPattern()
        {
            instance = new SingletonPattern();
        }

        private SingletonPattern()
        {
        }

        // =>用于Expression body definition
        public static SingletonPattern GetInstance() => instance;  

        public void Say() => Console.WriteLine($"I am a singleton with hashcode = {GetHashCode():X}");

        // Note, the code in the line below is wrong, 
        // Because ActionSay is a *instance* member, and this is unavailable until created
        // public Action ActionSay = () => { this };

        public static void Run()
        {
            for (int i = 0; i < 3; ++i)
            {
                GetInstance().Say();
            }
        }
    }
}
