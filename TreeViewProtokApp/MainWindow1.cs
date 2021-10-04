using System;
using System.Collections.Generic;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;
using TreeViewProtokApp.template;

namespace TreeViewProtokApp
{
    class MainWindow1 : Window
    {

        [UI] private readonly Gtk.TreeView treeView1 = null;
        private void _mkTreeView()
        {

            Gtk.TreeViewColumn artistColumn = new Gtk.TreeViewColumn ();
            artistColumn.Title = "artist";
            Gtk.CellRendererText artistCell = new Gtk.CellRendererText();
            artistColumn.PackStart(artistCell, true);

            Gtk.TreeViewColumn SongNameColumn = new Gtk.TreeViewColumn ();
            SongNameColumn.Title = "SongName";
            Gtk.CellRendererText SongNameCell = new Gtk.CellRendererText();
            SongNameColumn.PackStart(SongNameCell, true);

            Gtk.ListStore ListStore = new Gtk.ListStore (typeof (object));
            List<object> classObjArray = new List<object>();
            foreach (object classObj in classObjArray) {
                ListStore.AppendValues (classObj);
            }

            treeView1.Model = ListStore;

            treeView1.AppendColumn(artistColumn);
            treeView1.AppendColumn(SongNameColumn);

            artistColumn.SetCellDataFunc (artistCell, new Gtk.TreeCellDataFunc (Renderartist));
            SongNameColumn.SetCellDataFunc (SongNameCell, new Gtk.TreeCellDataFunc (RenderSongName));
        }


        private void Renderartist(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.ITreeModel model, Gtk.TreeIter iter)
        {
            object classObj = (object) model.GetValue (iter, 0);
            //(cell as Gtk.CellRendererText).Text = song.artist;
        }

        private void RenderSongName(Gtk.TreeViewColumn column, Gtk.CellRenderer cell, Gtk.ITreeModel model, Gtk.TreeIter iter)
        {
            object classObj = (object) model.GetValue (iter, 0);
            //(cell as Gtk.CellRendererText).Text = song.SongName;
        }
        
        [UI] private readonly Gtk.ComboBox comboBox1 = null;
        private void _mkComboBox()
        {
            comboBox1.Clear();
            CellRendererText cell = new CellRendererText();
            comboBox1.PackStart(cell, false);
            comboBox1.AddAttribute(cell, "text", 0);

            ListStore ListStore1 = new ListStore(typeof (string));

            comboBox1.Model = ListStore1;

            ListStore1.AppendValues ("artist");   
            ListStore1.AppendValues ("SongName");   
        }

        public MainWindow1() : this(new Builder("MainWindow1.glade"))
        {
            List<columnModel> columnModelArray = new List<columnModel>();
            
            columnModel columnModel1 = new columnModel();
            columnModel1.title = "artist";
            columnModel1.effective = true;
            columnModelArray.Add(columnModel1);

            columnModel columnModel2 = new columnModel();
            columnModel2.title = "SongName";
            columnModel2.effective = true;
            columnModelArray.Add(columnModel2);

            TreeViewTemplate TreeViewTemplate1 = new TreeViewTemplate();
            TreeViewTemplate1.columnModelArray = columnModelArray;
            var b = TreeViewTemplate1.TransformText();

            ComboBoxTemplate ComboBoxTemplate1 = new ComboBoxTemplate();
            ComboBoxTemplate1.columnModelArray = columnModelArray;
            var c = ComboBoxTemplate1.TransformText();

           _mkTreeView();

           _mkComboBox();
           
           TreeViewTemplateEx TreeViewTemplateEx1 = new TreeViewTemplateEx();
           TreeViewTemplateEx1.columnModelArray = columnModelArray;
           var b2 = TreeViewTemplateEx1.TransformText();

           ComboBoxTemplateEx ComboBoxTemplateEx1 = new ComboBoxTemplateEx();
           ComboBoxTemplateEx1.columnModelArray = columnModelArray;
           var c2 = ComboBoxTemplateEx1.TransformText();
           
           Console.WriteLine(c2);

        }

        private MainWindow1(Builder builder) : base(builder.GetRawOwnedObject("MainWindow1"))
        {
            builder.Autoconnect(this);

        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }
}