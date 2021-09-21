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
    
	    private Gtk.ListStore ColumnListViewStore;

	    void _mkColumnTableTreeView()
	    {
	
		    Gtk.TreeViewColumnEx TitleColumn = new Gtk.TreeViewColumnEx ();
		    TitleColumn.Title = "名前";
		    TitleColumn.bindingPropertyName = "title";
		    CellRendererText cell = TitleColumn._mkCellRendererText(columnView , "",100);
		    
		    Gtk.TreeViewColumnEx　isNgTimerColumn = new Gtk.TreeViewColumnEx ();
		    isNgTimerColumn.Title = "書き出し";
		    isNgTimerColumn.bindingPropertyName = "effective";
		    CellRendererToggle isNgTimerColumnToggle = isNgTimerColumn._mkCellRendererToggle(columnView ,"",60);
		    isNgTimerColumnToggle.Toggled += delegate(object o, ToggledArgs args)
		    {
			    TreeIter iter;
			    if (ColumnListViewStore.GetIterFromString(out iter, args.Path))
			    {
				   ColumnModel ColumnModel1 = (ColumnModel)ColumnListViewStore.GetValue(iter, 0);
				   ColumnModel1.effective = ColumnModel1.effective == true ? false : true;
				   _saveAll();
			    }
		    };

		    columnView._mkBinding();
	    }

	    /// <summary>
	    /// Columnの状態を保存する
	    /// </summary>
	    private void _saveAll()
	    {
		    string key = ((DbModel)SelectedDataBaseRow).dbPath._md5();
		    if (key == "")
		    {
			    Console.WriteLine("keyがない");
			    return;
		    }

		    List<ColumnModel> ColumnModelArray = new List<ColumnModel>();
		    ColumnListViewStore.Foreach((model, path, iter) =>{
			    ColumnModel testModel1 = model.GetValue(iter, 0) as ColumnModel;    
			    ColumnModelArray.Add(testModel1);
			    return false;
		    });

		    if (!dataColumnDic.TryAdd(key,ColumnModelArray))
		    {
			    dataColumnDic[key] = ColumnModelArray;
		    }
		    
		    dataColumnDic._saveXmlData(saveDataFilePath);
	    }

	    private Dictionary<string, List<ColumnModel>> dataColumnDic = null;

	    /// <summary>
	    /// TableSelect
	    /// </summary>
	    /// <param name="TableViewModel1"></param>
	    private void _mkColumnTalbeSelect(TableViewModel TableViewModel1)
	    {

		    if (TableViewModel1.title == "")
		    {
			    columnView.Model = null;
			    return;
		    }

		    if (SelectedDataBaseRow == null)
		    {
			    Console.WriteLine("SelectedDataBaseRowがない");
			    return;
		    }

		    ColumnListViewStore = new Gtk.ListStore (typeof (ColumnModel));

		    string sql = "PRAGMA table_info('" + TableViewModel1.title  +"');";
		    DataTable db = clsSqliteM.singleton._ReqDynamic(sql);

		    //確定
		    if (db != null && db.Rows.Count > 0)
		    {
			    
			    string key = ((DbModel)SelectedDataBaseRow).dbPath._md5();

			    dataColumnDic = saveDataFilePath._getSaveData<ColumnModel>();

			    List<ColumnModel> ColumnModel_OldArray = new List<ColumnModel>();
			    if (dataColumnDic != null && dataColumnDic[key] != null)
			    {
				    ColumnModel_OldArray = dataColumnDic[key];
			    }
			    
			    foreach (DataRow dr in db.Rows)
			    {
				    ColumnModel ColumnModel1 = new ColumnModel();
				    ColumnModel1.title = dr["name"].ToString();
				    ColumnModel1.type = dr["type"].ToString();
				    ColumnModel1.typeFix = _getNewKata(dr["type"].ToString());
				    
				    foreach (ColumnModel ColumnModel_old in ColumnModel_OldArray)
				    {
					    if (ColumnModel_old.title == ColumnModel1.title)
					    {
						    ColumnModel1.effective = ColumnModel_old.effective;
						    break;
					    }
				    }
				    
				    ColumnListViewStore.AppendValues (columnView);
			    }

			    columnView.Model = ColumnListViewStore;
		    }

	    }
	    
		
    }
}
