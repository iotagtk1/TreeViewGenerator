﻿﻿using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
 using TreeViewGenerator.template;
 using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
	partial class MainWindow
	{
		
		ITreeModel SelectedDataBaseRow;

		private OutPutType SelectedOutPutType;

		/// <summary>
		/// DataBase TreeViewを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_dataBaseSelection_changed(object sender, EventArgs e)
		{

			if (((Gtk.TreeSelection)sender).GetSelectedRows(out SelectedDataBaseRow) != null)
			{
				_mkTalbeSelect((DbModel)SelectedDataBaseRow);
			}

		}

		ITreeModel SelectedTableViewRow;

		/// <summary>
		/// テーブルTreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_tableViewSelection_changed(object sender, EventArgs e)
		{

			if (((Gtk.TreeSelection)sender).GetSelectedRows(out SelectedTableViewRow) != null)
			{
				_mkColumnTalbeSelect((TableViewModel)SelectedTableViewRow);

				List<ColumnModel> ColumnModelArray = new List<ColumnModel>();
				ColumnListViewStore.Foreach (delegate (ITreeModel model, TreePath path, TreeIter iter)  {
					ColumnModel modelObj = model.GetValue(iter, 0) as ColumnModel;
					ColumnModelArray.Add(modelObj);
					return false;
				});

				string outPutText = _getOutPutText(ColumnModelArray,
					ListStoreEntry.Text,
					ModelViewEntry.Text,
					SubNameSpaceEntry.Text,
					TreeViewEntry.Text,
					ComboViewEntry.Text
				);

				sampleView.Buffer.Text = outPutText;

			}

		}

		ITreeModel SelectedColumnViewRow;

		/// <summary>
		/// ColumnViewをクリックする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_columnViewSelection_changed(object sender, EventArgs e)
		{
			
			if (((Gtk.TreeSelection)sender).GetSelectedRows(out SelectedColumnViewRow) != null)
			{  
				((ColumnModel)SelectedColumnViewRow).effective = ((ColumnModel)SelectedColumnViewRow).effective == true ? false : true;
				 _saveAll();
			}

		}

		/// <summary>
		/// Comoboxを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_TreeViewBtn_group_changed(object sender, EventArgs e)
		{

			if (((RadioButton)sender).Active)
			{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "true";
			}
			else
			{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "false";
			}
			
			SelectedOutPutType = _getOutPutType();

		}

		private void on_customCheckBtn_toggled(object sender , EventArgs e){
			
			if (((ToggleButton)sender).Active){
				clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "true";
			}else{
				clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "false";
			}

			SelectedOutPutType = _getOutPutType();

		}
		
		/// <summary>
		/// Comoboxを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_ComboBoxBtn_group_changed(object sender, EventArgs e)
		{

			if (((RadioButton)sender).Active)
			{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "false";
			}
			else
			{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "true";
			}
			
			SelectedOutPutType = _getOutPutType();

		}

		private void on_closeBtn_clicked(object sender, EventArgs e)
		{
			this.Close();
		}

		private void on_defualtSetBtn_clicked(object sender , EventArgs e){
			
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
					ListStoreName = TreeViewTemplate1.ListStoreName;
					SubNameSpace = TreeViewTemplate1.SubNameSpace;
					break;
				case OutPutType.TreeViewEx:
					TreeViewTemplateEx TreeViewTemplateEx = new TreeViewTemplateEx();
					ModelName = TreeViewTemplateEx.ModelName;
					TreeViewName = TreeViewTemplateEx.TreeViewName;
					ListStoreName = TreeViewTemplateEx.ListStoreName;
					SubNameSpace = TreeViewTemplateEx.SubNameSpace;
					break;  
				case OutPutType.ComboBox:
					ComboBoxTemplate ComboBoxTemplate = new ComboBoxTemplate();
					ModelName = ComboBoxTemplate.ModelName;
					TreeViewName = ComboBoxTemplate.ComboBoxName;
					ListStoreName = ComboBoxTemplate.ListStoreName;
					SubNameSpace = ComboBoxTemplate.SubNameSpace;
					break;
				case OutPutType.ComboBoxEx:
					ComboBoxTemplateEx ComboBoxTemplateEx = new ComboBoxTemplateEx();
					ModelName = ComboBoxTemplateEx.ModelName;
					TreeViewName = ComboBoxTemplateEx.ComboBoxName;
					ListStoreName = ComboBoxTemplateEx.ListStoreName;
					SubNameSpace = ComboBoxTemplateEx.SubNameSpace;
					break;
			}

			TreeViewEntry.Text = TreeViewName;
			ComboViewEntry.Text = ComboBoxName;
			ListStoreEntry.Text = ListStoreName;
			ModelViewEntry.Text = ModelName;
			SubNameSpaceEntry.Text = SubNameSpace;
			
			clsIniFile.singlton[SelectedDbTableKey,"TreeViewEntry"] = TreeViewName;
			clsIniFile.singlton[SelectedDbTableKey,"ComboViewEntry"] = ComboBoxName;
			clsIniFile.singlton[SelectedDbTableKey, "ListStoreEntry"] = ListStoreName;
			clsIniFile.singlton[SelectedDbTableKey, "ModelViewEntry"] = ModelName;
			clsIniFile.singlton[SelectedDbTableKey, "SubNameSpaceEntry"] = SubNameSpace;
		}

		private void on_TreeViewEntry_changed(object sender, EventArgs e)
		{

			string text = ((Gtk.Entry)sender).Text;

			clsIniFile.singlton[SelectedDbTableKey,"TreeViewEntry"] = text;
			
		}

		private void on_ListStoreEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;

			clsIniFile.singlton[SelectedDbTableKey, "ListStoreEntry"] = text;

		}
		
		private void on_ModelViewEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;
			
			clsIniFile.singlton[SelectedDbTableKey, "ModelViewEntry"] = text;

		}
		
		private void on_SubNameSpaceEntry_changed(object sender , EventArgs e){

			string text = ((Gtk.Entry)sender).Text;

			clsIniFile.singlton[SelectedDbTableKey, "SubNameSpaceEntry"] = text;

		}

		private void on_ComboViewEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;

			clsIniFile.singlton[SelectedDbTableKey, "ComboViewEntry"] = text;
		}
		
		
	}

}