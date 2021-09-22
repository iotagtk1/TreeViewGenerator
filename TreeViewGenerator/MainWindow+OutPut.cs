using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Net.Mime;
using Gtk;
using TreeViewGenerator.template;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow
    {
	    private void _outPutText()
	    {
		    List<ColumnModel> ColumnModelArray_OutPut = new List<ColumnModel>();

		    if (ColumnListViewStore != null)
		    {
			    ColumnListViewStore.Foreach (delegate (ITreeModel model, TreePath path, TreeIter iter)  {
				    ColumnModel modelObj = model.GetValue(iter, 0) as ColumnModel;
				    ColumnModelArray_OutPut.Add(modelObj);
				    return false;
			    });

			    string outPutText = _getOutPutText(ColumnModelArray_OutPut,
				    ListStoreEntry.Text,
				    ModelViewEntry.Text,
				    SubNameSpaceEntry.Text,
				    TreeViewEntry.Text,
				    ComboViewEntry.Text
			    );

			    sampleView.Buffer.Text = outPutText;
		    }
	    }

	    private string _getOutPutText(List<ColumnModel> columnModelArray,string listStoreText,string modelText,string subNameText,string treeViewText = "",string comboViewText = "")
	    {
		    string txt = "";
		    switch (SelectedOutPutType)
		    {
			    case OutPutType.TreeView:
					TreeViewTemplate t = new TreeViewTemplate();
					t.ModelName = modelText;
					t.TreeViewName = treeViewText;
					t.ListStoreName = listStoreText;
					t.SubNameSpace = subNameText;
					t.ColumnModelArray = columnModelArray;
					
					txt = t.TransformText();
					break;
			    case OutPutType.TreeViewEx:
				    TreeViewTemplateEx t2 = new TreeViewTemplateEx();
				    t2.ModelName = modelText;
				    t2.TreeViewName = treeViewText;
				    t2.ListStoreName = listStoreText;
				    t2.SubNameSpace = subNameText;
				    t2.ColumnModelArray = columnModelArray;
				    
				    txt = t2.TransformText();
				    break;  
			    case OutPutType.ComboBox:
				    ComboBoxTemplate c = new ComboBoxTemplate();
				    c.ModelName = modelText;
				    c.ComboBoxName = comboViewText;
				    c.ListStoreName = listStoreText;
				    c.SubNameSpace = subNameText;
				    c.ColumnModelArray = columnModelArray;
				    txt = c.TransformText();
				    break;
			    case OutPutType.ComboBoxEx:
				    ComboBoxTemplateEx c2 = new ComboBoxTemplateEx();
				    c2.ModelName = modelText;
				    c2.ComboBoxName = comboViewText;
				    c2.ListStoreName = listStoreText;
				    c2.SubNameSpace = subNameText;
				    c2.ColumnModelArray = columnModelArray;
				    txt = c2.TransformText();
				    break;
		    }

		    return txt;
	    }
	    
		
    }
}
