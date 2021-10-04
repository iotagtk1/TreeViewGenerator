using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Gtk;
using TreeViewGenerator.template;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow
    {
	    private void _initConfigFile()
	    {

		    string configIniPath = clsFile._getExePath_replace("./config.ini");
		    
		    clsIniFile._sharedObject(configIniPath);
        		    
		    if (clsIniFile.singlton["config", "TreeViewBtn_active"] == "")
		    {
			    clsIniFile.singlton["config", "TreeViewBtn_active"] = "true";
		    }

		    if (clsIniFile.singlton["config", "customCheckBtn_customTemplate"] == "")
		    {
			    clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "false";
		    }
		    
		    Console.WriteLine(clsIniFile.singlton["config", "TreeViewBtn_active"]);

		    if (Convert.ToBoolean(clsIniFile.singlton["config", "TreeViewBtn_active"]))
		    {
			    TreeViewRadioBtn.Active = true;
		    }else
		    {
			    ComboBoxRadioBtn.Active = true;
		    }
		    customCheckBtn.Active = Convert.ToBoolean(clsIniFile.singlton["config", "customCheckBtn_customTemplate"]) ? true : false;

	    }
	    private void _initTextFiled(string SelectedDbTableKey_t){

		    if (clsIniFile.singlton[SelectedDbTableKey_t, "TreeViewEntry"] != "")
		    {
			    TreeViewEntry.Text = clsIniFile.singlton[SelectedDbTableKey_t, "TreeViewEntry"];
		    }

		    if (clsIniFile.singlton[SelectedDbTableKey_t, "ComboViewEntry"] != "")
		    {
			    ComboViewEntry.Text = clsIniFile.singlton[SelectedDbTableKey_t, "ComboViewEntry"];
		    }

		    if (clsIniFile.singlton[SelectedDbTableKey_t, "ListStoreEntry"] != "")
		    {
			    ListStoreEntry.Text = clsIniFile.singlton[SelectedDbTableKey_t, "ListStoreEntry"];
		    }

		    if (clsIniFile.singlton[SelectedDbTableKey_t, "ModelViewEntry"] != "")
		    {
			    ModelViewEntry.Text = clsIniFile.singlton[SelectedDbTableKey_t, "ModelViewEntry"];
		    }
		    if (clsIniFile.singlton[SelectedDbTableKey_t, "SubNameSpaceEntry"] != "")
		    {
			    SubNameSpaceEntry.Text = clsIniFile.singlton[SelectedDbTableKey_t, "SubNameSpaceEntry"];
		    }

	    }

	    private void _defaultTextFiledSet()
	    {
		    SelectedOutPutType = _getOutPutType();

	        string ModelName = "";
	        string TreeViewName = "";
	        string ComboBoxName = "";
	        string ListStoreName = "";
	        string SubNameSpace = "";

	        switch (SelectedOutPutType)
	        {
	            case OutPutType.TreeView:
            		TreeViewTemplate TreeViewTemplate1 = new TreeViewTemplate();
            		ModelName = TreeViewTemplate1.ModelName;
            		TreeViewName = TreeViewTemplate1.TreeViewName;
            		ComboBoxName = TreeViewTemplate1.ComboBoxName;
            		ListStoreName = TreeViewTemplate1.ListStoreName;
            		SubNameSpace = TreeViewTemplate1.SubNameSpace;
            		break;
	            case OutPutType.TreeViewEx:
            		TreeViewTemplateEx TreeViewTemplateEx = new TreeViewTemplateEx();
            		ModelName = TreeViewTemplateEx.ModelName;
            		TreeViewName = TreeViewTemplateEx.TreeViewName;
            		ComboBoxName = TreeViewTemplateEx.ComboBoxName;
            		ListStoreName = TreeViewTemplateEx.ListStoreName;
            		SubNameSpace = TreeViewTemplateEx.SubNameSpace;
            		break;  
	            case OutPutType.ComboBox:
            		ComboBoxTemplate ComboBoxTemplate = new ComboBoxTemplate();
            		ModelName = ComboBoxTemplate.ModelName;
            		TreeViewName = ComboBoxTemplate.TreeViewName;
            		ComboBoxName = ComboBoxTemplate.ComboBoxName;
            		ListStoreName = ComboBoxTemplate.ListStoreName;
            		SubNameSpace = ComboBoxTemplate.SubNameSpace;
            		break;
	            case OutPutType.ComboBoxEx:
            		ComboBoxTemplateEx ComboBoxTemplateEx = new ComboBoxTemplateEx();
            		ModelName = ComboBoxTemplateEx.ModelName;
            		TreeViewName = ComboBoxTemplateEx.TreeViewName;
            		ComboBoxName = ComboBoxTemplateEx.ComboBoxName;
            		ListStoreName = ComboBoxTemplateEx.ListStoreName;
            		SubNameSpace = ComboBoxTemplateEx.SubNameSpace;
            		break;
	        }

	        TreeViewEntry.Text = TreeViewName;
	        ComboViewEntry.Text = ComboBoxName;
	        ListStoreEntry.Text = ListStoreName;
	        ModelViewEntry.Text = ModelName;
	        SubNameSpaceEntry.Text = SubNameSpace;

	    }
	    
	    
		
    }
}
