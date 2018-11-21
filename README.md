# TiDatabase
一个简单的，以学习数据库底层操作为目的的DBMS实现。

## 核心功能
- 核心(Database.Core.Main)
- 转译器(Database.Core.Executor)
- 数据库的CRUD(CREATE DATABASE, USE, DROP DATABASE)
- 表的CRUD(CREATE TABLE, SELECT, DELETE, DROP TABLE)
- 基础查询(WHERE)
- GUI查询、命令、构造功能

## 技术
深度依赖于LINQ以及List<T>，自己实现了一些简单的封装。
转译器实现存在很多不完整的地方。没有使用AST和其他的数据结构。

## 测试
见**UnitTest**。现在共有17个可用的测试，分别对应了Database.Core.Main与Database.Core.Executor。
覆盖了小部分常用的数据库操作。可使用NUnit或者Visual Studio Test来运行。

# References

- [halfvim/minidb](https://github.com/halfvim/minidb) C++/Boost，无makefile
- [codewdy/mymymysql](https://github.com/codewdy/mymymysql) VC++，cmake



