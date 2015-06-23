using System;
using System.IO;

using Ted.Limit.ExtLoader;
using Ted.Limit.Core;

namespace Ted.Limit.ExtLoader.Test
{
    public class ExtManagerTest : ITestRun
    {
        private IExtManager m_ExtMgr;

        #region single method test

        private void LoadShareUnitTest()
        {
            string suPath = @"F:\Ted\Limit\Ted.Limit.ExtLoader.Test\bin\config\ted.loves.su";
            m_ExtMgr.LoadShareUnit(suPath);
        }

        private void SaveShareUnitTest()
        {
            string suPath = Directory.GetCurrentDirectory();
            suPath = suPath.Substring(0, suPath.LastIndexOf("\\"));
            suPath += "\\config\\ted.loves.su";
            m_ExtMgr.SaveShareUnit(suPath);
        }

        private void LoadExtDefTest()
        {
            string tedPath = @"F:\Ted\Limit\Ted.Limit.ExtLoader.Test\TestCase\Empty.ted";
            string ExtKey = string.Empty;
            IMake make = m_ExtMgr.LoadExtDef(tedPath,ref ExtKey);
            Console.WriteLine("The Key is " + ExtKey);
        }

        private void LoadModuleTest()
        {
            string moduleKey = "Empty";
            m_ExtMgr.LoadModule(moduleKey);
        }

        private void GetToolTest()
        {
            string toolKey = "Ted.Limit.Core.Empty.EmptyTool";
            object obj = m_ExtMgr.GetTool(toolKey);
        }

        private void GetResourceTest()
        {
            string toolKey = "Ted.Limit.Core.Empty.EmptyTool";
            string rerxPath = m_ExtMgr.GetResource(toolKey);
        }

        #endregion //single method test

        #region test order

        private void FirstCreate()
        {
            LoadExtDefTest();
            SaveShareUnitTest();
        }

        private void LoadAndGet()
        {
            LoadShareUnitTest();
            Console.WriteLine("Load .su file successfully!");
            LoadModuleTest();
            Console.WriteLine("Load module successfully!");
            GetToolTest();
            Console.WriteLine("Get tool successfully!");
            GetResourceTest();
            Console.WriteLine("Get resources path successfully!");
        }

        #endregion //test in order

        public ExtManagerTest()
        {
            m_ExtMgr = new ExtManager();
        }

        #region ITestRun 成员

        void ITestRun.Run()
        {
            LoadExtDefTest();
        }

        #endregion
    }
}
