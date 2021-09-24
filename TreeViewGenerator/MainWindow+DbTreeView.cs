using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow
    {
	    public ArrayList _dirPathAnalyze(string fileFolderPath)
	    {

		    ArrayList filesArray = new ArrayList();
            
		    if (clsFile._isFile(fileFolderPath) && fileFolderPath._indexOf(".db") != -1)
		    {
			    filesArray.Add(fileFolderPath);
 
		    }else if (clsFolder._isFolder(fileFolderPath))
		    {
			    filesArray = clsFile._getFileList(fileFolderPath, ".db", null, isAllDir: true);
		    }

		    if (filesArray.Count == 0)
		    {
			    Console.WriteLine("フォルダ:{0} Dbファイルがありません", fileFolderPath);
			    return null;
		    }

		    return filesArray;
	    }
	    
	    private Gtk.ListStore DbListViewStore;

	    void _mkDbTreeView()
	    {
	
		    Gtk.TreeViewColumnEx TitleColumn = new Gtk.TreeViewColumnEx ();
		    TitleColumn.Title = "Db";
		    TitleColumn.bindingPropertyName = "title";
		    CellRendererText cell = TitleColumn._mkCellRendererText(dataBaseView , "",100,false);
		    dataBaseView._mkBinding();
		    
	    }
	    private void _mkSelect(ArrayList fileList)
	    {
		    
		    DbListViewStore = new Gtk.ListStore (typeof (DbModel));
		    
		    foreach (String dbPath in fileList)
		    {
			    DbModel dbModel = new DbModel();
			    dbModel.title = clsPath._getFileNameNoExtension(dbPath);
			    dbModel.dbPath = dbPath;
			    DbListViewStore.AppendValues (dbModel);
		    }

		    dataBaseView.Model = DbListViewStore;
	    }
		
    }
}
