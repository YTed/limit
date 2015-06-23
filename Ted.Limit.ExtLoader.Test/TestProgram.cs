using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Ted.Limit.ExtLoader.Test
{
    class TestProgram
    {
        static void Main(string[] argv)
        {
            string clsName = Console.ReadLine();
            Assembly assembly = typeof(TestProgram).Assembly;
            ITestRun run = assembly.CreateInstance("Ted.Limit.ExtLoader.Test." + clsName) as ITestRun;
            run.Run();
            Console.Read();
        }
    }
}
