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
        		    
		    if (clsIniFile.singlton["config", "TreeViewBtn_group_active"] == "")
		    {
			    clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "true";
		    }

		    TreeViewRadioBtn.Active = Convert.ToBoolean(clsIniFile.singlton["config", "TreeViewBtn_group_active"]) ? true : false;

		    customCheckBtn.Active = Convert.ToBoolean(clsIniFile.singlton["config", "customCheckBtn_customTemplate"]) ? true : false;

		    
		  
	    }
	    
		
    }
}
