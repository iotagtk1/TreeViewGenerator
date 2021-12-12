using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;
using MySql.Data.MySqlClient;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow
    {
    
	    private Gtk.ListStore TableListViewStore;
	    
	    private string SelectedDbTableKey = "";

	    void _mkTableTreeView()
	    {
	
		    Gtk.TreeViewColumnEx TitleColumn = new Gtk.TreeViewColumnEx ();
		    TitleColumn.Title = "Table";
		    TitleColumn.bindingPropertyName = "title";
		    CellRendererText cell = TitleColumn._mkCellRendererText(tableView , "",100,false);
		    tableView._mkBinding();
		    
	    }

	    private void _mkTalbeSelect(dbModel dbModel1)
	    {
		    
		    SelectedDbTableKey = "";

		    DataTable db = null;

		    if (!clsArgsConfig.Instance().isDbType_Sqlite)
		    {
			    MySqlConnectionStringBuilder b = new MySqlConnectionStringBuilder();
			    b.Server = "localhost";
			    b.Port = 3306;
			    b.Database = clsArgsConfig.Instance().MySql_DataBase;
			    b.UserID = clsArgsConfig.Instance().MySql_UserId;
			    b.Password = clsArgsConfig.Instance().MySql_Password;

			    clsMySqlM._sharedObject(b);
			    clsDapper._initMySql();

			    string sql = "SELECT table_name as name FROM information_schema.tables WHERE table_schema = '" +  b.Database +"';";
			    db = clsMySqlM.singleton._ReqDynamic(sql,new List<string>(){"name"});  
		    }
		    else
		    {
			    clsSqliteM._remove();
			    clsSqliteM._sharedObject(dbModel1.dbPath);
			    clsDapper._initSqlite();

			    string sql = "select name from sqlite_master where type='table';";
			    db = clsSqliteM.singleton._ReqDynamic(sql,new List<string>(){"name"});  
		    }

		    TableListViewStore = new Gtk.ListStore (typeof (tableViewModel));

		    if (db != null && db.Rows.Count > 0)
		    {
			    foreach (DataRow dr in db.Rows)
			    {
				    tableViewModel tableViewModel1 = new tableViewModel();
				    tableViewModel1.title = dr["name"].ToString();
				    TableListViewStore.AppendValues (tableViewModel1);
			    }
			    tableView.Model = TableListViewStore;
		    }

	    }
	    
		
    }
}
