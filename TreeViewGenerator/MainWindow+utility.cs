using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;

namespace TreeViewGenerator
{
    partial class MainWindow
    {

	    /// <summary>
	    /// 書き出すタイプを取得する
	    /// </summary>
	    /// <returns></returns>
	    private OutPutType _getOutPutType(){

		    if (TreeViewRadioBtn.Active && !customCheckBtn.Active)
		    {
			    return OutPutType.TreeView;
		    }else if (TreeViewRadioBtn.Active && customCheckBtn.Active)
		    {
			    return OutPutType.TreeViewEx;
		    }else if (ComboBoxRadioBtn.Active && !customCheckBtn.Active)
		    {
			    return OutPutType.ComboBox;
		    }else if (ComboBoxRadioBtn.Active && customCheckBtn.Active)
		    {
			    return OutPutType.ComboBoxEx;
		    }

		    return OutPutType.TreeView;
	    }

	    /// <summary>
	    /// tablekeyとDbKeyを保存用に使う
	    /// </summary>
	    /// <returns></returns>
	    private string _getDbTableKey()
	    {
		    if (((DbModel)SelectedDataBaseRow).dbPath == "" || ((TableViewModel)SelectedTableViewRow).title == "")
		    {
			    return "";
		    } 
		    
		    string dbPathKey = ((DbModel)SelectedDataBaseRow).dbPath._md5();
		    string titleKey = ((TableViewModel)SelectedTableViewRow).title._md5();
		    
		    return dbPathKey + titleKey;
	    }
	    
	    Dictionary<string,string> kataCheckDic =
		    new Dictionary<string,string> (){
			    {"(~/(?i)^bit|bool$/)", "bool"},
			    {"(~/(?i)^tinyint$/)","byte"},
			    {"(~/(?i)^uniqueidentifier| uuid$/)", "Guid"},
			    {"(~/(?i)^int | integer | number$/)", "long"},
			    {"(~/(?i)^bigint$/)", "long"},
			    {"(~/(?i)^varbinary | image$/)", "byte[]"},
			    {"(~/(?i)^double | float | real$/)", "double"},
			    {"(~/(?i)^decimal | money | numeric | smallmoney$/)","decimal"},
			    {"(~/(?i)^datetimeoffset$/)", "DateTimeOffset"},
			    {"(~/(?i)^datetime | datetime2 | timestamp | date | time$/)", "DateTime"},
			    {"(~/(?i)^char$/)", "char"}
		    };

	    /// <summary>
	    /// DB側の型をコンバートする
	    /// </summary>
	    /// <param name="kataOld"></param>
	    /// <returns></returns>
	    private string _getNewKata(string kataOld)
	    {
		    foreach (string paternStr in kataCheckDic.Keys)
		    {
			    Match match = Regex.Match(kataOld, paternStr);
			    if (match.Index > 0)
			    {
				    string kataNew = kataCheckDic[paternStr];
				    return kataNew;
					break;
			    }
		    }
		    return kataOld;
	    }
		
    }
}
