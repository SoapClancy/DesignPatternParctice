using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    /* 版本1：所有都implement interface IComponent，
     但是Decorator里面的Operation没有标记为virtual，
     ConcreteDecorator里面的Operation也没有标记为override，
     输出为Exec ConcreteComponent Operation
          Decorated ConcreteComponent Operation
    internal class DecoratorPattern
    {
        internal interface IComponent
        {
            void Operation();
        }

        internal class ConcreteComponent : IComponent
        {
            public void Operation()
            {
                Console.WriteLine("Exec ConcreteComponent Operation");
            }
        }

        internal abstract class Decorator : IComponent
        {
            private IComponent component;  // the object being decorated

            protected Decorator(IComponent _component)
            {
                component = _component;
            }

            public void Operation()
            {
                component.Operation();
                Console.WriteLine("Decorated ConcreteComponent Operation");
            }
        }

        internal class ConcreteDecorator : Decorator
        {
            public ConcreteDecorator(IComponent _component) : base(_component)
            {
            }

            public void Operation()
            {
                base.Operation();
                ExtraStuff();
            }

            private static void ExtraStuff()
            {
                Console.WriteLine("Extra decoration in ConcreteDecorator");
            }
        }


        public static void Run()
        {
            IComponent component = new ConcreteComponent();
            IComponent decorator = new ConcreteDecorator(component);
            decorator.Operation();
        }
    }
    */

    /* 版本2：所有都implement interface IComponent，
     Decorator里面的Operation标记为virtual，
     ConcreteDecorator里面的Operation标记为override，
     输出为Exec ConcreteComponent Operation
          Decorated ConcreteComponent Operation
          Extra decoration in ConcreteDecorator
     (注意如果只标记为virtual结果也不对)
    */
    internal class DecoratorPattern
    {
        internal interface IComponent
        {
            void Operation();
        }

        internal class ConcreteComponent : IComponent
        {
            public void Operation()
            {
                Console.WriteLine("Exec ConcreteComponent Operation");
            }
        }

        internal abstract class Decorator : IComponent
        {
            private IComponent component;  // the object being decorated

            protected Decorator(IComponent _component)
            {
                component = _component;
            }

            public virtual void Operation()
            {
                component.Operation();
                Console.WriteLine("Decorated ConcreteComponent Operation");
            }
        }

        internal class ConcreteDecorator : Decorator
        {
            public ConcreteDecorator(IComponent _component) : base(_component)
            {
            }

            public override void Operation()
            {
                base.Operation();
                ExtraStuff();
            }

            private static void ExtraStuff()
            {
                Console.WriteLine("Extra decoration in ConcreteDecorator");
            }
        }


        public static void Run()
        {
            IComponent component = new ConcreteComponent();
            IComponent decorator = new ConcreteDecorator(component);
            decorator.Operation();
        }
    }

    /* 版本3：用abstract class，全部用override，
     输出为Exec ConcreteComponent Operation
          Decorated ConcreteComponent Operation
          Extra decoration in ConcreteDecorator
    internal class DecoratorPattern
    {
        internal abstract class Component
        {
            internal abstract void Operation();
        }

        internal class ConcreteComponent : Component
        {
            internal override void Operation()
            {
                Console.WriteLine("Exec ConcreteComponent Operation");
            }
        }

        internal abstract class Decorator : Component
        {
            private Component component;  // the object being decorated

            protected Decorator(Component _component)
            {
                component = _component;
            }

            internal override void Operation()
            {
                component.Operation();
                Console.WriteLine("Decorated ConcreteComponent Operation");
            }
        }

        internal class ConcreteDecorator : Decorator
        {
            public ConcreteDecorator(Component _component) : base(_component)
            {
            }

            internal override void Operation()
            {
                base.Operation();
                ExtraStuff();
            }

            private static void ExtraStuff()
            {
                Console.WriteLine("Extra decoration in ConcreteDecorator");
            }
        }


        public static void Run()
        {
            Component component = new ConcreteComponent();
            Component decorator = new ConcreteDecorator(component);
            decorator.Operation();
        }
    }
    */
}
