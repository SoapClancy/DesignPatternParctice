using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatternPractice.Source
{
    internal class BuilderPattern
    {
        internal class Director
        {
            //private List<string> procedures = new List<string>();

            internal static AbstractDish GetSpicySaltGrillBeef(Builder grillBeefBuilder)
            {
                //procedures.Clear();
                //procedures.Add("AddSpicy");
                //procedures.Add("AddSalt");
                //grillBeefBuilder.SetProcedures(procedures);
                /* If use the commented codes above instead,
                 Section 4 in Run will have impacts on Section 5*/
                grillBeefBuilder.SetProcedures(new List<string> { "AddSpicy", "AddSalt" });

                return grillBeefBuilder.GetDish();
            }

            internal static AbstractDish GetSpicySaltGrillCuminBeef(Builder grillBeefBuilder)
            {
                grillBeefBuilder.SetProcedures(new List<string> { "AddSpicy", "AddSalt", "AddCumin" });

                return grillBeefBuilder.GetDish();
            }

            internal static AbstractDish GetCuminLamb(Builder grillLambBuilder)
            {
                //procedures.Clear();
                //procedures.Add("AddCumin");
                //grillLambBuilder.SetProcedures(procedures);
                /* If use the commented codes above instead,
                 Section 4 in Run will have impacts on Section 5*/
                grillLambBuilder.SetProcedures(new List<string> { "AddCumin" });

                return grillLambBuilder.GetDish();
            }
        }

        internal abstract class Builder
        {
            internal abstract AbstractDish GetDish();
            // This is also a template method
            internal void SetProcedures(List<string> procedures) => GetDish().SetProcedures(procedures);
        }

        internal class GrillBeefBuilder : Builder
        {
            private readonly GrillBeef grillBeef = new GrillBeef();
            internal override AbstractDish GetDish() => grillBeef;
        }

        internal class GrillLambBuilder : Builder
        {
            private readonly GrillLamb grillLamb = new GrillLamb();
            internal override AbstractDish GetDish() => grillLamb;
        }

        internal abstract class AbstractDish
        {
            private List<string> procedures = new List<string>();
            protected abstract void AddSpicy();
            protected abstract void AddSalt();
            protected abstract void AddCumin();

            // Template method
            internal void HappyTime()
            {
                foreach (string procedure in procedures)
                {
                    switch (procedure)
                    {
                        case ("AddSpicy"):
                            {
                                AddSpicy();
                                break;
                            }
                        case ("AddSalt"):
                            {
                                AddSalt();
                                break;
                            }
                        case ("AddCumin"):
                            {
                                AddCumin();
                                break;
                            }
                        default:
                            {
                                throw new ArgumentException();
                            }
                    }
                }
            }

            // Another template method
            internal void SetProcedures(List<string> _procedures) => procedures = _procedures;
        }

        internal class GrillBeef : AbstractDish
        {
            protected override void AddSpicy() => Console.WriteLine("Add spicy to beef");
            protected override void AddSalt() => Console.WriteLine("Add salt to beef");
            protected override void AddCumin() => Console.WriteLine("Add cumin to beef");
        }

        internal class GrillLamb : AbstractDish
        {
            protected override void AddSpicy() => Console.WriteLine("Add spicy to lamb");
            protected override void AddSalt() => Console.WriteLine("Add salt to lamb");
            protected override void AddCumin() => Console.WriteLine("Add cumin to lamb");
        }

        public static void Run()
        {
            // Section 1
            AbstractDish spicySaltGrillBeef = Director.GetSpicySaltGrillBeef(new GrillBeefBuilder());
            spicySaltGrillBeef.HappyTime();
            Console.WriteLine(new string('=', 79));

            // Section 2
            AbstractDish spicySaltCuminGrillBeef = Director.GetSpicySaltGrillCuminBeef(new GrillBeefBuilder());
            spicySaltCuminGrillBeef.HappyTime();
            Console.WriteLine(new string('=', 79));

            // Section 3
            spicySaltGrillBeef.HappyTime();
            Console.WriteLine(new string('=', 79));

            // Section 4
            AbstractDish cuminGrillLamb = Director.GetCuminLamb(new GrillLambBuilder());
            cuminGrillLamb.HappyTime();
            Console.WriteLine(new string('=', 79));

            // Section 5
            spicySaltGrillBeef.HappyTime();
            Console.WriteLine(new string('=', 79));
        }
    }
}
