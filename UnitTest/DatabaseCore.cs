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
        public void CreateDatabase()
        {
            Main instance = new Main(); 
            instance.CreateDatabase("created");
            Assert.AreEqual(1, instance.databases.Count);
        }

        [TestMethod]
        public void DeleteDatabase()
        {
            Main instance = new Main();
            instance.CreateDatabase("data");
            Assert.AreEqual(1, instance.databases.Count);
            instance.DropDatabase("data");
            Assert.AreEqual(0, instance.databases.Count);
        }

        [TestMethod]
        public void SelectDatabase()
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
            Assert.AreEqual(1, instance._current.tables.Count);
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
            Assert.AreEqual(1, instance._current.tables.Count);
            instance.DropTable("table");
            Assert.AreEqual(0, instance._current.tables.Count);
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
            Assert.AreEqual(2, rows.Count);
            Assert.AreEqual(123456, rows[0].keyValuePairs["id"]);
        }
    }
}
