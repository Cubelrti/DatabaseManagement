using System;
using System.Collections.Generic;
using DatabaseManagement.Core;
using DatabaseManagement.Core.Entities;
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
            Assert.AreEqual(instance.databases.Count, 1);
        }

        [TestMethod]
        public void DeleteDb()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            Assert.AreEqual(instance.databases.Count, 1);
            instance.DropDatabase("data");
            Assert.AreEqual(instance.databases.Count, 0);
        }

        [TestMethod]
        public void SelectDb()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            instance.SelectDatabase("data");
            Assert.IsNotNull(instance._current);
        }
        [TestMethod]
        public void CreateTable()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            instance.SelectDatabase("data");
            Dictionary<String, Types> constraints = new Dictionary<string, Types>
            {
                { "id", Types.INTEGER }
            };
            instance.CreateTable("table", constraints);
            Assert.AreEqual(instance._current.tables.Count, 1);
        }
        [TestMethod]
        public void DropTable()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            instance.SelectDatabase("data");
            Dictionary<String, Types> constraints = new Dictionary<string, Types>
            {
                { "id", Types.INTEGER }
            };
            instance.CreateTable("table", constraints);
            Assert.AreEqual(instance._current.tables.Count, 1);
            instance.DropTable("table");
            Assert.AreEqual(instance._current.tables.Count, 0);
        }
        [TestMethod]
        public void InsertTable()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            instance.SelectDatabase("data");
            Dictionary<String, Types> constraints = new Dictionary<string, Types>
            {
                { "id", Types.INTEGER }
            };
            instance.CreateTable("table", constraints);
            instance.InsertTable("table", new Dictionary<string, string> {
                { "id", "123456" }
            });
            instance.InsertTable("table", new Dictionary<string, string> {
                { "id", "223456" }
            });
            var rows = instance.SelectAny("table");
            Assert.AreEqual(rows.Count, 2);
            Assert.AreEqual(rows[0].keyValuePairs["id"], 123456);
        }
    }
}
