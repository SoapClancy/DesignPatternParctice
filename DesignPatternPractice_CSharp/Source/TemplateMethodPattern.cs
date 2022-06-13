using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    internal class TemplateMethodPattern
    {
        internal abstract class AbstractDish
        {
            // Use protect to follow LoD
            protected abstract void Prepare();
            protected abstract void Cook();
            protected abstract void ApplyCumin();
            protected abstract void Eat();
            protected abstract void Clear();

            // Note: this is Hook Method, i.e., the method that have impacts on the template method
            protected virtual bool AllowApplyCumin() => true;  // default, apply cumin

            internal void HappyTime()
            {
                int i = 0;
                Console.WriteLine($"Step {++i}: ");
                Prepare();
                Console.WriteLine($"Step {++i}: ");
                Cook();
                if (AllowApplyCumin())
                {
                    Console.WriteLine($"Step {++i}: ");
                    ApplyCumin();
                }
                Console.WriteLine($"Step {++i}: ");
                Eat();
                Console.WriteLine($"Step {++i}: ");
                Clear();
            }
        }

        internal class GrillBeef : AbstractDish
        {
            private bool allowApplyCuminFlag = false;
            protected override void Prepare() => Console.WriteLine("Prepare grill beef");
            protected override void Cook() => Console.WriteLine("Cook grill beef");
            protected override void ApplyCumin() => Console.WriteLine("Add some cumin to beef");
            protected override void Eat() => Console.WriteLine("Eat grill beef");
            protected override void Clear() => Console.WriteLine("Clear grill beef\n");

            public void SetAllowApplyCuminFlag(bool flag) => allowApplyCuminFlag = flag;  // Customizable
            protected override bool AllowApplyCumin() => allowApplyCuminFlag;
        }

        internal class GrillLamb : AbstractDish
        {
            protected override void Prepare() => Console.WriteLine("Prepare grill lamb");
            protected override void Cook() => Console.WriteLine("Cook grill lamb");
            protected override void ApplyCumin() => Console.WriteLine("Add some cumin to lamb");
            protected override void Eat() => Console.WriteLine("Eat grill lamb");
            protected override void Clear() => Console.WriteLine("Clear grill lamb\n");

            protected override bool AllowApplyCumin() => true;  // Always apply cumin for lamb
        }

        public static void Run()
        {
            AbstractDish grillBeef = new GrillBeef();
            grillBeef.HappyTime();
            AbstractDish grillLamb = new GrillLamb();
            grillLamb.HappyTime();
        }
    }
}
