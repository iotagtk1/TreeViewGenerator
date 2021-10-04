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
		    List<columnModel> columnModelArray_OutPut = new List<columnModel>();

		    if (ColumnListViewStore != null)
		    {
			    ColumnListViewStore.Foreach (delegate (ITreeModel model, TreePath path, TreeIter iter)  {
				    columnModel modelObj = model.GetValue(iter, 0) as columnModel;
				    columnModelArray_OutPut.Add(modelObj);
				    return false;
			    });

			    string outPutText = _getOutPutText(columnModelArray_OutPut,
				    ListStoreEntry.Text,
				    ModelViewEntry.Text,
				    SubNameSpaceEntry.Text,
				    TreeViewEntry.Text,
				    ComboViewEntry.Text
			    );

			    sampleView.Buffer.Text = outPutText;
		    }
	    }

	    private string _getOutPutText(List<columnModel> columnModelArray,string listStoreText,string modelText,string subNameText,string treeViewText = "",string comboViewText = "")
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
					t.columnModelArray = columnModelArray;
					t.ProjectName = clsArgsConfig.Instance().ProjectName;
					txt = t.TransformText();
					break;
			    case OutPutType.TreeViewEx:
				    TreeViewTemplateEx t2 = new TreeViewTemplateEx();
				    t2.ModelName = modelText;
				    t2.TreeViewName = treeViewText;
				    t2.ListStoreName = listStoreText;
				    t2.SubNameSpace = subNameText;
				    t2.columnModelArray = columnModelArray;
				    t2.ProjectName = clsArgsConfig.Instance().ProjectName;
				    txt = t2.TransformText();
				    break;  
			    case OutPutType.ComboBox:
				    ComboBoxTemplate c = new ComboBoxTemplate();
				    c.ModelName = modelText;
				    c.ComboBoxName = comboViewText;
				    c.ListStoreName = listStoreText;
				    c.SubNameSpace = subNameText;
				    c.columnModelArray = columnModelArray;
				    c.ProjectName = clsArgsConfig.Instance().ProjectName;
				    txt = c.TransformText();
				    break;
			    case OutPutType.ComboBoxEx:
				    ComboBoxTemplateEx c2 = new ComboBoxTemplateEx();
				    c2.ModelName = modelText;
				    c2.ComboBoxName = comboViewText;
				    c2.ListStoreName = listStoreText;
				    c2.SubNameSpace = subNameText;
				    c2.columnModelArray = columnModelArray;
				    c2.ProjectName = clsArgsConfig.Instance().ProjectName;
				    txt = c2.TransformText();
				    break;
		    }

		    return txt;
	    }
	    
		
    }
}
