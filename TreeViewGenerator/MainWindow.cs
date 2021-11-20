using System;
using System.Collections;
using System.Text.RegularExpressions;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


namespace TreeViewGenerator
{
    partial class MainWindow : Window
    {
        private string saveDataFilePath = "./data.json";

        private string templateDir = "template";
        
        enum OutPutType
        {
            TreeView = 0,     
            TreeViewEx = 1,
            ComboBox = 2,
            ComboBoxEx = 3,
        }
        public MainWindow() : this(new Builder("MainWindow.glade"))
        {
            try
            {

                     _initConfigFile();
     
                     saveDataFilePath = clsFile._getExePath_replace(saveDataFilePath);
     
                     if (clsArgsConfig.Instance()._validateCommandKey())
                     {
                       
                         _mkDbTreeView();
                         
                         _mkTableTreeView();
     
                         _mkColumnTableTreeView();
     
                         ArrayList dbPathArray = _dirPathAnalyze(clsArgsConfig.Instance().FileDirPath);
                         if (dbPathArray != null && dbPathArray.Count > 0)
                         {
                             _mkSelect(dbPathArray);
                         }
             
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);
            DeleteEvent += Window_DeleteEvent;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Application.Quit();
        }

    }
}