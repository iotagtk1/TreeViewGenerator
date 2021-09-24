### Introduction

### Explanation

Generate TreeView ComboView program statement from Sqlite table structure
### Environment
.net5

Rider

GtkSharp

Dapper

Dapper Extensions

Microsoft.Data.Sqlite.Core

System.CodeDom

INIFileParserDotNetCore

SQLitePCLRaw.bundle_green

### Rider Setting
ExploerPanel - right click - edit execution configuration - external tools

![alt text](./readMe/1.png)

Set up external tools. Set the arguments

![alt text](./readMe/3.png)

Uncheck Run after file sync.

![alt text](./readMe/5.png)

### Arguments Macro Required

Set the path of the program
You must specify a macro
copy perst

```
 -fileDir $FilePath$
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
