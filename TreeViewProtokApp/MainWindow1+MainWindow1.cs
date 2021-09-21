using System;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow1
    {
    
    
		private void on_treeViewSelection_changed(object sender , EventArgs e){
			ITreeModel model;	
			if (((Gtk.TreeSelection)sender).GetSelectedRows(out model) != null)
			{
			    // ((type_MainWindow1)model)    
			}
			
			/*
			TreeIter iter;
			ITreeModel model;
			if (((Gtk.TreeSelection)sender).GetSelected(out model,out iter))
			{
			    type_MainWindow1 modelData = (type_MainWindow1)ListStore_MainWindow1.GetValue(iter, 0);
			}
			*/

			
		}}
}
