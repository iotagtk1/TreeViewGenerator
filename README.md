### Introduction

### Explanation

Generate TreeView ComboView program statement from Sqlite table structure

### Sqlite Setting

Please prepare a Sqlite file. The file extension should be .db3.

Place it in Rider's Explorer.

### Rider Setting
ExploerPanel - right click - edit execution configuration - external tools

![alt text](./readMe/1.png)

Set up external tools. Set the arguments

![alt text](./readMe/3.png)

Uncheck Run after file sync.

![alt text](./readMe/5.png)

### Arguments Macro Required

Set the path of the program
copy perst


### Select Sqlite
```
 -fileDir $FilePath$ -projectName $FilePath$ 
```
**DbType default is sqlite**

### Select MySql
****
```
 -fileDir $FilePath$ -projectName $FilePath$ -dbType mysql -dataBase mySqlDataBaseName -userId mySqlUserId -passWord mySqlPassWord
```

### Execution

Select the sqlite file in the explorer bar

![alt text](./readMe/7.png)


You can run it from an external tool

TopMenu - Tool - ExternalTool


Start the TreeViewGenerator.

![alt text](./readMe/6.png)

Select the table to display the sample script.
Copy it and use it.

### Template UI library
The UI library for the template is used here

You can find the UI library in the Custom UI folder.

### Rider Code Format

Menu - Code - codeReFormat

### Environment
.net6

Rider

GtkSharp

Nuget

Dapper

Dapper Extensions

Microsoft.Data.Sqlite.Core

MySql

System.CodeDom

INIFileParserDotNetCore

SQLitePCLRaw.bundle_green

#### Use Rider for Free

If you are an open source developer, you can use all JetBrains products for free!

https://www.jetbrains.com/community/opensource

