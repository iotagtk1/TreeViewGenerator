using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;
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

		    clsSqliteM._remove();
		    clsSqliteM._sharedObject(dbModel1.dbPath);

		    TableListViewStore = new Gtk.ListStore (typeof (tableViewModel));

		    string sql = "select name from sqlite_master where type='table';";
		    DataTable db = clsSqliteM.singleton._ReqDynamic(sql,new List<string>(){"name"});

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
