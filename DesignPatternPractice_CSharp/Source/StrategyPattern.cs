using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    internal class StrategyPattern
    {
        internal interface IStrategy
        {
            void Algorithm();
        }

        internal class ConcreteStrategyA : IStrategy
        {
            public void Algorithm()
            {
                Console.WriteLine("ConcreteStrategyA Algorithm");
            }
        }

        internal class ConcreteStrategyB : IStrategy
        {
            public void Algorithm()
            {
                Console.WriteLine("ConcreteStrategyB Algorithm");
            }
        }

        internal class Context
        {
            private IStrategy strategy;

            public Context(IStrategy _strategy)
            {
                strategy = _strategy;
            }

            internal void ContextInterface() => strategy.Algorithm();
        }

        public static void Run()
        {
            IStrategy strategy = new ConcreteStrategyA();
            Context context = new Context(strategy);
            context.ContextInterface();
        }
    }
}
