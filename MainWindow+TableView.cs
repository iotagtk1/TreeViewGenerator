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

	    void _mkTableTreeView()
	    {
	
		    Gtk.TreeViewColumnEx TitleColumn = new Gtk.TreeViewColumnEx ();
		    TitleColumn.Title = "名前";
		    TitleColumn.bindingPropertyName = "title";
		    CellRendererText cell = TitleColumn._mkCellRendererText(tableView , "",100);
		    tableView._mkBinding();
		    
	    }

	    private void _mkTalbeSelect(DbModel dbModel1)
	    {

		    clsSqliteM._remove();
		    clsSqliteM._sharedObject(dbModel1.dbPath);

		    TableListViewStore = new Gtk.ListStore (typeof (TableViewModel));

		    string sql = "select name from sqlite_master where type='table';";
		    DataTable db = clsSqliteM.singleton._ReqDynamic(sql);

		    if (db != null && db.Rows.Count > 0)
		    {
			    foreach (DataRow dr in db.Rows)
			    {
				    TableViewModel TableViewModel1 = new TableViewModel();
				    TableViewModel1.title = dr["name"].ToString();
				    TableListViewStore.AppendValues (TableViewModel1);
			    }
			    tableView.Model = TableListViewStore;
		    }

	    }
	    
		
    }
}
