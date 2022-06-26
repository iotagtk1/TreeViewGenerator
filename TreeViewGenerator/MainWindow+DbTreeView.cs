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
            
		    if (clsFile._isFile(fileFolderPath) && (fileFolderPath._indexOf(".db") != -1 || fileFolderPath._indexOf(".sqlite") != -1))
		    {
			    filesArray.Add(fileFolderPath);
 
		    }else if (clsFolder._isFolder(fileFolderPath))
		    {
			    ArrayList filesArray1 = clsFile._getFileList(fileFolderPath, ".db", null, isAllDir: true);
			    ArrayList filesArray2 = clsFile._getFileList(fileFolderPath, ".db3", null, isAllDir: true);
			    ArrayList filesArray3 = clsFile._getFileList(fileFolderPath, ".sqlite", null, isAllDir: true);
			    filesArray.AddRange(filesArray1);
			    filesArray.AddRange(filesArray2);  
			    filesArray.AddRange(filesArray3);   
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
		    CellRendererText cell = TitleColumn._mkCellRendererText(dataBaseView , "",100,100,false);
		    dataBaseView._mkBinding();
		    
	    }
	    private void _mkSelect(ArrayList fileList)
	    {
		    
		    DbListViewStore = new Gtk.ListStore (typeof (dbModel));
		    
		    foreach (String dbPath in fileList)
		    {
			    dbModel dbModel = new dbModel();
			    dbModel.title = clsPath._getFileNameNoExtension(dbPath);
			    dbModel.dbPath = dbPath;
			    DbListViewStore.AppendValues (dbModel);
		    }

		    dataBaseView.Model = DbListViewStore;
	    }
	    
	    private void _mkSelect_mySql()
	    {
		    
		    DbListViewStore = new Gtk.ListStore (typeof (dbModel));

		    dbModel dbModel = new dbModel();
		    dbModel.title = clsPath._getFileNameNoExtension("mySql");
		    dbModel.dbPath = "mySql";
		    DbListViewStore.AppendValues (dbModel);		   

		    dataBaseView.Model = DbListViewStore;
	    }
	    
	    
		
    }
}
