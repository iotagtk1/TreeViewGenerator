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
	    private void _initConfigFile()
	    {
        		                
		    clsIniFile._sharedObject("./config.ini");
        		    
		    if (clsIniFile.singlton["config", "TreeViewBtn_active"] == "")
		    {
			    clsIniFile.singlton["config", "TreeViewBtn_active"] = "true";
		    }
		    
		    if (clsIniFile.singlton["config", "customCheckBtn_customTemplate"] == "")
		    {
			    clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "false";
		    }   

		    TreeViewRadioBtn.Active = Convert.ToBoolean(clsIniFile.singlton["config", "TreeViewBtn_active"]) ? true : false;
		    ComboBoxRadioBtn.Active = TreeViewRadioBtn.Active ? false : true;
		    customCheckBtn.Active = Convert.ToBoolean(clsIniFile.singlton["config", "customCheckBtn_customTemplate"]) ? true : false;

	    }
	    
		
    }
}
