using System;
using System.Collections;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
	partial class MainWindow
	{
		
		/// <summary>
		/// DataBase TreeViewを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_dataBaseSelection_changed(object sender , EventArgs e){

			ITreeModel model;	
			if (((Gtk.TreeSelection)sender).GetSelectedRows(out model) != null)
			{
				_mkTalbeSelect((DbModel)model);
			}

		}
		
		/// <summary>
		/// テーブルTreeView
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_tableViewSelection_changed(object sender , EventArgs e){
		
			ITreeModel model0;	
			if (((Gtk.TreeSelection)sender).GetSelectedRows(out model0) != null)
			{
			   
				// (TableViewModel)model0

			}

		}
		
		
		/// <summary>
		/// Comoboxを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_TreeViewBtn_group_changed(object sender , EventArgs e){

			if (((RadioButton)sender).Active)
			{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "true";
			}else{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "false";
			}
		
		}
		/// <summary>
		/// Comoboxを選択
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void on_ComboBoxBtn_group_changed(object sender , EventArgs e){
			
			if (((RadioButton)sender).Active){
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "false";
			}else{
				clsIniFile.singlton["config", "TreeViewBtn_group_active"] = "true";
			}
			
		}

		private void on_closeBtn_clicked(object sender , EventArgs e){
			this.Close();
		}
		
		
		
	}
}
