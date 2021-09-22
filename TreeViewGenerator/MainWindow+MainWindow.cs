﻿using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
 using TreeViewGenerator.template;
 using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
	partial class MainWindow
	{
		
		DbModel SelectedDataBaseRow;

		private OutPutType SelectedOutPutType;

		/// <summary>
		/// DataBase TreeViewを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_dataBaseSelection_changed(object sender, EventArgs e)
		{
			
			TreeIter iter;
			ITreeModel model;
			if (((Gtk.TreeSelection)sender).GetSelected(out model,out iter))
			{
				SelectedDataBaseRow = (DbModel)DbListViewStore.GetValue(iter, 0);
				
				_mkTalbeSelect(SelectedDataBaseRow);
			}
			
		}

		TableViewModel SelectedTableViewRow;

		/// <summary>
		/// テーブルTreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_tableViewSelection_changed(object sender, EventArgs e)
		{
			
			TreeIter iter;
			ITreeModel model;
			if (((Gtk.TreeSelection)sender).GetSelected(out model,out iter))
			{
				SelectedTableViewRow = (TableViewModel)TableListViewStore.GetValue(iter, 0);
				
				SelectedDbTableKey = _getDbTableKey();
	
				_mkColumnTalbeSelect(SelectedTableViewRow);

				_outPutText();

			}

		}

		ColumnModel SelectedColumnViewRow;

		/// <summary>
		/// ColumnViewをクリックする
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_columnViewSelection_changed(object sender, EventArgs e)
		{
			
			TreeIter iter;
			ITreeModel model;
			if (((Gtk.TreeSelection)sender).GetSelected(out model, out iter))
			{
				SelectedColumnViewRow  = (ColumnModel)ColumnListViewStore.GetValue(iter, 0);
				((ColumnModel)SelectedColumnViewRow).effective = ((ColumnModel)SelectedColumnViewRow).effective == true ? false : true;
				_saveAll();
				_outPutText();

			}

		}
	
		private void on_customCheckBtn_toggled(object sender , EventArgs e){
			
			if (((ToggleButton)sender).Active){
				clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "true";
			}else{
				clsIniFile.singlton["config", "customCheckBtn_customTemplate"] = "false";
			}

			SelectedOutPutType = _getOutPutType();
			
			_outPutText();

		}
		
		/// <summary>
		/// Comoboxを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
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

			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey,"TreeViewEntry"] = TreeViewName;
				clsIniFile.singlton[SelectedDbTableKey,"ComboViewEntry"] = ComboBoxName;
				clsIniFile.singlton[SelectedDbTableKey, "ListStoreEntry"] = ListStoreName;
				clsIniFile.singlton[SelectedDbTableKey, "ModelViewEntry"] = ModelName;
				clsIniFile.singlton[SelectedDbTableKey, "SubNameSpaceEntry"] = SubNameSpace;
			}
			
			_outPutText();

		}
		private void on_TreeViewEntry_changed(object sender, EventArgs e)
		{

			string text = ((Gtk.Entry)sender).Text;
			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey,"TreeViewEntry"] = text;
				_outPutText();
			}

		}

		private void on_ListStoreEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;
			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey, "ListStoreEntry"] = text;
				_outPutText();
			}

		}
		
		private void on_ModelViewEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;
			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey, "ModelViewEntry"] = text;
				_outPutText();
			}

		}
		
		private void on_SubNameSpaceEntry_changed(object sender , EventArgs e){

			string text = ((Gtk.Entry)sender).Text;
			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey, "SubNameSpaceEntry"] = text;
				_outPutText();
			}

		}

		private void on_ComboViewEntry_changed(object sender , EventArgs e){
			
			string text = ((Gtk.Entry)sender).Text;

			if (SelectedDbTableKey != "")
			{
				clsIniFile.singlton[SelectedDbTableKey, "ComboViewEntry"] = text;
				_outPutText();
			}
			
		}
		
		private void on_TreeViewRadioBtn_toggled(object sender , EventArgs e){
			
			if (((ToggleButton)sender).Active)
			{
				clsIniFile.singlton["config", "TreeViewBtn_active"] = "true";
			}
			else
			{
				clsIniFile.singlton["config", "TreeViewBtn_active"] = "false";
			}
			
			SelectedOutPutType = _getOutPutType();
			_outPutText();
		}
		
		private void on_ComboBoxRadioBtn_toggled(object sender , EventArgs e){
			
			Console.WriteLine("on_TreeViewBtn_toggled");
			
			if (((ToggleButton)sender).Active)
			{
				clsIniFile.singlton["config", "TreeViewBtn_active"] = "false";
			}
			else
			{
				clsIniFile.singlton["config", "TreeViewBtn_active"] = "true";
			}
			
			SelectedOutPutType = _getOutPutType();
			_outPutText();
			
		}



	}

}