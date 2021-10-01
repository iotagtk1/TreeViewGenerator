using System;
using System.Collections.Generic;
using Gtk;

namespace TreeViewGenerator
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.TreeViewGenerator.TreeViewGenerator", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);
            
            try
            {
                clsArgsConfig.Instance();
                
                List<string> a = new List<string>();
                a.Add("-fileDir");
                a.Add("/home/ita/C#/App_GitHub/gladeGenerator/gladeGenerator/gladeGenerator/GladeGeneratorGUI");
                a.Add("-projectName");
                a.Add("/home/ita/C#/App_GitHub/gladeGenerator/gladeGenerator/gladeGenerator/GladeGeneratorGUI"); 
                args = a.ToArray();


   
                clsArgsConfig.Instance()._setArgs(args);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            var win = new MainWindow();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}