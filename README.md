# DatabaseManagement

目前看过的Repo：
- [halfvim/minidb](https://github.com/halfvim/minidb) C++/Boost，无makefile
- [codewdy/mymymysql](https://github.com/codewdy/mymymysql) VC++，cmake

# minidb

有完整的Lexer，本质是通过正则表达式，可以参考。
Lexer里面只能传入一次分号，也就是不能多个语句。个人认为这个实现很蠢。
有完整的Indexer，没怎么看。
代码比较整洁简短，API部分不超过400行。
个人感觉这个实现的鲁棒性很差，随处可见的字符串比对。
`Intepreter`这个类就是可以参考的解释器。

# mymymysql

没有完整的Lexer，用的是SQLite自带的Lemon。
有比较简洁的执行器。
有一个文件读写的实现，用的是LRU的缓存策略。
代码鲁棒性较好，带有test。
VC++的实现。

# 随记

感觉这个用TDD是最好的。
毕竟是引擎类的东西，没有个三两年的积累很难写好。
TDD: UnitTest项目

## SQLBuilder

什么怪物，搞不懂。


