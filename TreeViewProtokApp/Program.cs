using System;
using Gtk;

namespace TreeViewProtokApp
{
    class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Application.Init();

            var app = new Application("org.TreeViewProtokApp.TreeViewProtokApp", GLib.ApplicationFlags.None);
            app.Register(GLib.Cancellable.Current);

            var win = new MainWindow1();
            app.AddWindow(win);

            win.Show();
            Application.Run();
        }
    }
}