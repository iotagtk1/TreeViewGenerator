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
		    TitleColumn.Title = "Column";
		    TitleColumn.bindingPropertyName = "title";
		    CellRendererText cell = TitleColumn._mkCellRendererText(columnView , "",100,false);
		    
		    Gtk.TreeViewColumnEx　isNgTimerColumn = new Gtk.TreeViewColumnEx ();
		    isNgTimerColumn.Title = "OutPut";
		    isNgTimerColumn.bindingPropertyName = "effective";
		    CellRendererToggle isNgTimerColumnToggle = isNgTimerColumn._mkCellRendererToggle(columnView ,"",40);
		    isNgTimerColumnToggle.Toggled += delegate(object o, ToggledArgs args)
		    {
			    TreeIter iter;
			    if (ColumnListViewStore.GetIterFromString(out iter, args.Path))
			    {
				   columnModel columnModel1 = (columnModel)ColumnListViewStore.GetValue(iter, 0);
				   columnModel1.effective = columnModel1.effective == true ? false : true;
				   _saveAll();
				   _outPutText();
			    }
		    };

		    columnView._mkBinding();
	    }

	    /// <summary>
	    /// Columnの状態を保存する
	    /// </summary>
	    private void _saveAll()
	    {

		    if (SelectedDbTableKey == "")
		    {
			    Console.WriteLine("keyがない");
			    return;
		    }

		    List<columnModel> columnModelArray = new List<columnModel>();
		    ColumnListViewStore.Foreach((model, path, iter) =>{
			    columnModel testModel1 = model.GetValue(iter, 0) as columnModel;    
			    columnModelArray.Add(testModel1);
			    return false;
		    });
		    
		    if (dataColumnDic.ContainsKey(SelectedDbTableKey))
		    {
			    dataColumnDic[SelectedDbTableKey] = columnModelArray;
		    }
		    else
		    {
			    dataColumnDic.Add(SelectedDbTableKey,columnModelArray);
		    }

		    if (dataColumnDic != null)
		    {
			    clsFile._saveJsonData<Dictionary<string, List<columnModel>>>(saveDataFilePath, dataColumnDic);
		    }

	    }

	    private Dictionary<string, List<columnModel>> dataColumnDic = null;

	    /// <summary>
	    /// TableSelect
	    /// </summary>
	    /// <param name="TableViewModel1"></param>
	    private void _mkColumnTalbeSelect(tableViewModel tableViewModel1)
	    {

		    if (tableViewModel1 == null || tableViewModel1.title == "")
		    {
			    columnView.Model = null;
			    return;
		    }

		    if (SelectedDataBaseRow == null)
		    {
			    Console.WriteLine("SelectedDataBaseRowがない");
			    return;
		    }

		    ColumnListViewStore = new Gtk.ListStore (typeof (columnModel));

		    DataTable columnDb = null;
		    if (!clsArgsConfig.Instance().isDbType_Sqlite)
		    {
			    string sql = "SHOW COLUMNS FROM " + tableViewModel1.title  + ";";
			    columnDb = clsMySqlM.singleton._ReqDynamic(sql,new List<string>(){"Field","Type"});
			    columnDb.Columns.Add("name");
			    foreach (DataRow dr in columnDb.Rows)
			    {
				    dr["name"] = dr["Field"];
			    }
		    }
		    else
		    {
			    string sql = "PRAGMA table_info('" + tableViewModel1.title  +"');";
			    columnDb = clsSqliteM.singleton._ReqDynamic(sql,new List<string>(){"name","type"});	    
		    }

		    //確定
		    if (columnDb != null && columnDb.Rows.Count > 0)
		    {
			    dataColumnDic = clsFile._getJsonData<Dictionary<string, List<columnModel>>>(saveDataFilePath);

			    if (dataColumnDic == null)
			    {
				    dataColumnDic = new Dictionary<string, List<columnModel>>();
			    }
			   
			    List<columnModel> columnModel_OldArray = new List<columnModel>();
			    if (dataColumnDic != null && dataColumnDic.ContainsKey(SelectedDbTableKey) && dataColumnDic[SelectedDbTableKey] != null)
			    {
				    columnModel_OldArray = dataColumnDic[SelectedDbTableKey];
			    }
			    
			    foreach (DataRow dr in columnDb.Rows)
			    {
				    columnModel columnModel1 = new columnModel();
				    columnModel1.title = dr["name"].ToString();
				    columnModel1.type = dr["type"].ToString();
				    columnModel1.typeFix = _getNewKata(dr["type"].ToString()).ToLower();
				    columnModel1.effective = true;
				    
				    foreach (columnModel columnModel_old in columnModel_OldArray)
				    {
					    if (columnModel_old.title == columnModel1.title)
					    {
						    columnModel1.effective = columnModel_old.effective;
						    break;
					    }
				    }
				    
				    ColumnListViewStore.AppendValues (columnModel1);
			    }

			    columnView.Model = ColumnListViewStore;
		    }

	    }
	    
		
    }
}
