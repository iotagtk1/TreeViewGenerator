using System;
using System.Collections;
using Gtk;
using UI = Gtk.Builder.ObjectAttribute;


namespace TreeViewGenerator
{
    partial class MainWindow : Window
    {
        private string saveDataFilePath = "./data.xml";
        
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

                if (clsArgsConfig.Instance()._validateCommandKey())
                {
                    _mkDbTreeView();

                    _mkTableTreeView();

                    _mkColumnTableTreeView();

                    ArrayList dbPathArray = _dirPathAnalyze(clsArgsConfig.Instance().FileDirPath);

                    _mkSelect(dbPathArray);
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

        }

    }
}