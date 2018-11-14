using System;
using DatabaseManagement.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class DatabaseCore
    {
        [TestMethod]
        public void CreateDb()
        {
            Main instance = new Main();
            instance.CreateDatabase("created");
        }
    }
}
