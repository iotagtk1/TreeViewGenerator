﻿using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
	partial class MainWindow
	{
		ITreeModel SelectedDataBaseRow;

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
				
				
				
			}

			Console.WriteLine(SelectedTableViewRow);

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
				((ColumnModel)SelectedColumnViewRow).effective = ((ColumnModel)SelectedColumnViewRow) .effective == true ? false : true;
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

		}

		private void on_closeBtn_clicked(object sender, EventArgs e)
		{
			this.Close();
		}

	
		
		
	}

}