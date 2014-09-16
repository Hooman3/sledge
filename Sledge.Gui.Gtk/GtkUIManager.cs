﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gtk;
using Sledge.Gui.Attributes;
using Sledge.Gui.Gtk.Shell;
using Sledge.Gui.Shell;

namespace Sledge.Gui.Gtk
{
    [UIImplementation("GTK", 20, UIPlatform.Windows, UIPlatform.Linux)]
    public class GtkUIManager : IUIManager
    {
        private readonly GtkShell _shell;

        public IShell Shell
        {
            get { return _shell; }
        }

        public GtkUIManager()
        {
            Application.Init();
            _shell = new GtkShell();
        }

        public void Start()
        {
            _shell.Show();
            Application.Run();
        }
    }
}