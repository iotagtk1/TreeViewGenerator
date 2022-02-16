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

                if (System.Diagnostics.Debugger.IsAttached)
                {
                    
                }


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