using System;
using System.Reflection;
using Gdk;
using Gtk;

namespace Gtk
{
    public class TreeViewColumnEx : TreeViewColumn
    {
        public String bindingPropertyName = "";
        private Gtk.ListStore listStore1 = null;
        private Gtk.TreeView treeView = null;
        
        public CellRendererText _mkCellRendererText(TreeView treeView1, string title = "", int minWidth = 0, int maxWidth = 0,
            bool isEditable = true, bool isExpand = false, bool isPackStart = true , bool isAutoEdit = true,bool isAutoSize = false)
        {

            treeView = treeView1;

            if (title != "")
            {
                this.Title = title;
            }
            Gtk.CellRendererTextEx CellRendererText1 = new Gtk.CellRendererTextEx();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            this.PackStart(CellRendererText1, isPackStart);
           
            if (isEditable)
            {
                CellRendererText1.Editable = isAutoEdit;
                CellRendererText1.Edited += delegate(object o, EditedArgs args)
                {
                    TreeIter iter;
                    listStore1 = (ListStore)treeView.Model;
                    if (listStore1.GetIterFromString(out iter, args.Path))
                    {
                        object modelData = (object)listStore1.GetValue(iter, 0);
                        _setModelData(modelData, bindingPropertyName, args.NewText);
                    }
                };
            }

            treeView.AppendColumn(this);
            return CellRendererText1;
        }

        public CellRendererPixbuf _mkCellRendererPixbuf(TreeView treeView, string title = "",int minWidth = 0,int maxWidth = 0,
            bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererPixbuf CellRendererPixbuf1 = new Gtk.CellRendererPixbuf();

            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            listStore1 = (ListStore)treeView.Model;
            this.PackStart(CellRendererPixbuf1, isPackStart);
            //this.AddAttribute (CellRendererPixbuf1, "pixbuf", 0);  
            treeView.AppendColumn(this);
            return CellRendererPixbuf1;
        }

        public CellRendererToggle _mkCellRendererToggle(TreeView treeView1, string title = "", int minWidth = 0,int maxWidth = 0,
            bool isToggled = false, bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            treeView = treeView1;
            
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererToggle CellRendererToggle1 = new Gtk.CellRendererToggle();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }
            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            
            if (isToggled)
            {
                CellRendererToggle1.Toggled += delegate(object o, ToggledArgs args)
                {
                    listStore1 = (ListStore)treeView.Model;
                    TreeIter iter;
                    if (listStore1.GetIterFromString(out iter, args.Path))
                    {
                        object object1 = (object)listStore1.GetValue(iter, 0);
                        object value = object1._performSelector_Property(bindingPropertyName);
                        String val = Convert.ToBoolean(value) == true ? "false" : "true";
                        _setModelData(object1, bindingPropertyName, val);
                    }
                };
            }

            this.PackStart(CellRendererToggle1, isPackStart);
            treeView1.AppendColumn(this);
            return CellRendererToggle1;
        }

        public CellRendererProgress _mkCellRendererProgress(TreeView treeView, string title = "" , 
            int minWidth = 0 , int maxWidth = 0, bool isExpand = false, bool isPackStart = true,bool isAutoSize = false)
        {
            if (title != "")
            {
                this.Title = title;
            }

            Gtk.CellRendererProgress CellRendererProgress1 = new Gtk.CellRendererProgress();
            if (minWidth != 0)
            {
                this.MinWidth = minWidth;
            }
            if (maxWidth != 0)
            {
                this.MaxWidth = maxWidth;
            }

            this.Expand = isExpand;
            this.Sizing = isAutoSize ? TreeViewColumnSizing.Autosize : TreeViewColumnSizing.Fixed;
            listStore1 = (ListStore)treeView.Model;
            this.PackStart(CellRendererProgress1, isPackStart);
            treeView.AppendColumn(this);
            return CellRendererProgress1;
        }
        public void _mkBinding()
        {
            if (this.Cells.Length > 0)
            {
                this.SetCellDataFunc(this.Cells[0], new Gtk.TreeCellDataFunc(_RenderCellDo));
            }
        }
        
        private void _RenderCellDo(Gtk.TreeViewColumn column, Gtk.CellRenderer cellRender,
            Gtk.ITreeModel model, Gtk.TreeIter iter){
            try
            {
                if (!(column is TreeViewColumnEx))
                {
                    Console.WriteLine("_RenderCellDo");
                    return;
                }
                TreeViewColumnEx column1 = (column as TreeViewColumnEx);
                if (column1.bindingPropertyName == null || column1.bindingPropertyName == "")
                {
                    Console.WriteLine("PropertyNameがない");
                    return;
                }  
                object modelData = (object)model.GetValue(iter, 0);
                object value = modelData._performSelector_Property(column1.bindingPropertyName);

                _setCellData(value, cellRender);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void _setCellData(object value, Gtk.CellRenderer cellRender)
        {
            if (value != null && cellRender is Gtk.CellRendererText && (value is String))
            {
                (cellRender as Gtk.CellRendererText).Text = value as String;
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererText && (value is int))
            {
                (cellRender as Gtk.CellRendererText).Text = ((int)value).ToString();
            }else if (value != null && value != "" && cellRender is Gtk.CellRendererText && (value is double))
            {
                (cellRender as Gtk.CellRendererText).Text = ((double)value).ToString();
            }else if (value != null && value != "" && cellRender is Gtk.CellRendererText && (value is long))
            {
                (cellRender as Gtk.CellRendererText).Text = ((long)value).ToString();
            }else if (value != null && value != "" && cellRender is Gtk.CellRendererText && (value is decimal))
            {
                (cellRender as Gtk.CellRendererText).Text = ((decimal)value).ToString();
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererText && (value is DateTime))
            {
                (cellRender as Gtk.CellRendererText).Text = ((DateTime)value).ToString();
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererPixbuf && (value is String))
            {
                (cellRender as Gtk.CellRendererPixbuf).Pixbuf = new Pixbuf(null, (value as String));
            }
            else if (value != null && cellRender is Gtk.CellRendererToggle && (value is String))
            {
                (cellRender as Gtk.CellRendererToggle).Active = Convert.ToBoolean((value is String));
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererProgress && (value is String))
            {
                (cellRender as Gtk.CellRendererProgress).Value = Convert.ToInt32((value is String));
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererPixbuf && (value is byte[]))
            {
                (cellRender as Gtk.CellRendererPixbuf).Pixbuf = new Pixbuf((byte[])value);
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererToggle && (value is Boolean))
            {
                (cellRender as Gtk.CellRendererToggle).Active = (Boolean)value;
            }
            else if (value != null && value != "" && cellRender is Gtk.CellRendererProgress && (value is int))
            {
                (cellRender as Gtk.CellRendererProgress).Value = (int)value;
            }
        }
        private void _setModelData(object modelData1, String bindingPropertyName1, String value)
        {
            Type t = modelData1._getKata(bindingPropertyName1);

            if (t.ToString().IndexOf("System.Nullable") != -1)
            {
                t = Nullable.GetUnderlyingType(t);
            }

            if (value != null &&  t.Equals(typeof(String)))
            {
                modelData1._setSelector_Property(bindingPropertyName1, Convert.ToString(value));
            }
            else if (t.Equals(typeof(int)) && int.TryParse(value,out int in1))
            {
                modelData1._setSelector_Property(bindingPropertyName1, in1);
            }
            else if (t.Equals(typeof(double)) && double.TryParse(value,out double do1))
            {
                modelData1._setSelector_Property(bindingPropertyName1, do1);
            }
            else if (t.Equals(typeof(long)) && long.TryParse(value,out long lo))
            {
                modelData1._setSelector_Property(bindingPropertyName1, lo);
            }
            else if (t.Equals(typeof(decimal)) && decimal.TryParse(value,out decimal de))
            {
                modelData1._setSelector_Property(bindingPropertyName1, de);
            }
            else if (t.Equals(typeof(Boolean)) && Boolean.TryParse(value,out bool b))
            {
                modelData1._setSelector_Property(bindingPropertyName1, b);
            }
            else if (t.Equals(typeof(bool)) && bool.TryParse(value,out bool b2))
            {
                modelData1._setSelector_Property(bindingPropertyName1, b2);
            }
            else if (t.Equals(typeof(DateTime)) && DateTime.TryParse(value, out DateTime d))
            {
                modelData1._setSelector_Property(bindingPropertyName1, d);
            }
            else if (t.Equals(typeof(char)) && char.TryParse(value, out char c))
            {
                modelData1._setSelector_Property(bindingPropertyName1, c);
            }
            else if (t.Equals(typeof(byte)) && byte.TryParse(value, out byte by))
            {
                modelData1._setSelector_Property(bindingPropertyName1, by);
            }
        }
    }
}