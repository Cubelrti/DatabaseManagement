using System;
using DatabaseManagement.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static DatabaseManagement.Core.Entities.Types;

namespace UnitTest
{
    [TestClass]
    public class Parser
    {
        [TestMethod]
        public void ParseCreateDatabase()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            Assert.AreEqual(1, instance.databases.Count);
        }
        [TestMethod]
        public void ParseDropDatabase()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            Assert.AreEqual(1, instance.databases.Count);
            expr.Run("DROP DATABASE hug");
            Assert.AreEqual(0, instance.databases.Count);
        }
        [TestMethod]
        public void ParseCreateTable()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20)
            )");
            Assert.AreEqual(4, instance._current.tables[0].ColumnDefinitions.Count);
            Assert.AreEqual(VARCHAR, instance._current.tables[0].ColumnDefinitions["classno"]);
        }
        [TestMethod]
        public void ParseNotNull()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6) not null
            )");
            Assert.AreEqual(VARCHAR, instance._current.tables[0].ColumnDefinitions["classno"]);
            
            Assert.ThrowsException<NotNullException>(() =>
            {
                expr.Run("insert into class (classno) values (null)");
            });
        }
        [TestMethod]
        public void ParsePrimaryKey()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6) primary key
            )");
            Assert.AreEqual(VARCHAR, instance._current.tables[0].ColumnDefinitions["classno"]);

            Assert.ThrowsException<PrimaryKeyConflictException>(() =>
            {
                expr.Run("insert into class (classno) values ('233')");
                expr.Run("insert into class (classno) values ('233')");
            });
        }
        [TestMethod]
        public void ParseDropTable()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20)
            )");
            Assert.AreEqual(4, instance._current.tables[0].ColumnDefinitions.Count);
            Assert.AreEqual(VARCHAR, instance._current.tables[0].ColumnDefinitions["classno"]);
            expr.Run("DROP TABLE class");
            Assert.AreEqual(0, instance._current.tables.Count);
        }
        [TestMethod]
        public void ParseShowDatabase()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("CREATE DATABASE xcz");
            var result = expr.Run("SHOW DATABASES");
            Assert.AreEqual("hug, xcz", result);
        }
        [TestMethod]
        public void ParseShowTable()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"CREATE TABLE student(
	            sno varchar(8),
                sname varchar(8),
                ssex varchar(2),
                sbirthday date
            )");
            var result = expr.Run("SHOW TABLES");
            Assert.AreEqual("class, student", result);
        }
        [TestMethod]
        public void ParseInsertTable()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"insert into class (classno, classname, classmajor, classdept, studentnumber)
                    values 
                    ('Rj0806', '软件0806', '软件工程', '软件开发', 24)");
            Assert.AreEqual(1, instance._current.tables[0].rows.Count);
        }
        [TestMethod]
        public void ParseSelectAny()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"insert into class (classno, classname, classmajor, classdept, studentnumber)
                    values 
                    ('Rj0806', '软件0806', '软件工程', '软件开发', 24)");
            expr.Run(@"insert into class (classname, classno, classmajor, classdept, studentnumber)
                    values 
                    ('软件0801', 'Rj0801', '软件工程', '软件开发', 24)");
            var result = expr.Run("select * from class;");
            Assert.AreEqual("rj0806, 软件0806, 软件工程, 软件开发, 24\n软件0801, rj0801, 软件工程, 软件开发, 24", result);
        }
        [TestMethod]
        public void ParseSelectColumn()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"insert into class (classno, classname, classmajor, classdept, studentnumber)
                    values 
                    ('Rj0806', '软件0806', '软件工程', '软件开发', 24)");
            var result = expr.Run("select classno, classname from class where classno='Rj0806'");
            Assert.AreEqual("rj0806, 软件0806", result);
        }
        [TestMethod]
        public void ParseDelete()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"insert into class (classno, classname, classmajor, classdept, studentnumber)
                    values 
                    ('Rj0806', '软件0806', '软件工程', '软件开发', 24)");
            Assert.AreEqual(1, instance._current.tables[0].rows.Count);
            expr.Run("delete from class where classno='Rj0806'");
            Assert.AreEqual(0, instance._current.tables[0].rows.Count);
        }
        [TestMethod]
        public void ParseWhere()
        {
            Main instance = new Main();
            Executor expr = new Executor(instance);
            expr.Run("CREATE DATABASE hug");
            expr.Run("USE hug");
            expr.Run(@"CREATE TABLE class(
                classno varchar(6),
                classname varchar(20),
                classmajor varchar(20),
                classdept varchar(20),
                studentnumber integer
            )");
            expr.Run(@"insert into class (classno, classname, classmajor, classdept, studentnumber)
                    values 
                    ('Rj0806', '软件0806', '软件工程', '软件开发', 24)");
            expr.Run(@"insert into class (classname, classno, classmajor, classdept, studentnumber)
                    values 
                    ('软件0801', 'Rj0801', '软件工程', '软件开发', 24)");
            var result = expr.Run("select * from class where classno='Rj0801';");
            Assert.AreEqual("软件0801, rj0801, 软件工程, 软件开发, 24", result);
        }
    }
}
