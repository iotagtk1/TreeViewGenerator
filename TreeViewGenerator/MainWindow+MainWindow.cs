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
		TableViewModel SelectedTableViewRow;
		ColumnModel SelectedColumnViewRow;
		OutPutType SelectedOutPutType;

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
				
				_setModelNameHint(SelectedTableViewRow);

				_initTextFiled();

				_mkColumnTalbeSelect(SelectedTableViewRow);

				_outPutText();

			}

		}



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
			Environment.Exit(0);
		}

		private void on_defualtSetBtn_clicked(object sender , EventArgs e)
		{

			//OK
			_defaultTextFiledSet();

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
	
		private void on_TreeViewRadioBtn_clicked(object sender , EventArgs e){
	
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
		private void on_ComboBoxRadioBtn_clicked(object sender , EventArgs e){

			SelectedOutPutType = _getOutPutType();
			_outPutText();
			
		}

		private void on_copyBtn_clicked(object sender , EventArgs e){
	
			clsClipboard._setText(sampleView.Buffer.Text);
			
		}

		private void on_templateBtn_clicked(object sender , EventArgs e)
		{

			string absolutDirPath = clsFile._getExePath_replace(templateDir);
			
			clsDiagnosticsProcess._openDirBroser(absolutDirPath);
			
		}

	}

}