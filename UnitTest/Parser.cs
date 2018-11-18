using System;
using DatabaseManagement.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void ParseCreateDatabase()
        {
            Main instance = new Main();
            Expr expr = new Expr("CREATE DATABASE fuck");
            expr.Excute(instance);
            Assert.AreEqual(instance.databases.Count, 1);
        }
        [TestMethod]
        public void ParseCreateTable()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            instance.SelectDatabase("data");
            Expr expr = new Expr(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20)
            )");
            expr.Excute(instance);
        }
    }
}
