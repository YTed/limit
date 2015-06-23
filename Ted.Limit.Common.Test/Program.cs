using System;
using System.Collections.Generic;
using System.Text;

using Ted.Limit.ExtLoader.Test;

namespace Ted.Limit.Common.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //string clsName = Console.ReadLine();
            System.Reflection.Assembly assembly = typeof(Program).Assembly;
            ITestRun run = assembly.CreateInstance("Ted.Limit.Common.Test.LogManagerTest") as ITestRun;
            run.Run();
            Console.Read();
        }
    }
}
