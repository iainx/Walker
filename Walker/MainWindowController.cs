using System;
using System.Collections.Generic;

using Foundation;
using AppKit;

namespace Walker
{
    public partial class MainWindowController : NSWindowController
    {
        PathModel paths;
        public PathModel Paths {
            get {
                return paths;
            }

            set{
                paths = value;
                Window.Paths = value;
            }
        }

        public MainWindowController (IntPtr handle) : base (handle)
        {
        }

        [Export ("initWithCoder:")]
        public MainWindowController (NSCoder coder) : base (coder)
        {
        }

        public MainWindowController () : base ("MainWindow")
        {
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();
        }

        public new MainWindow Window {
            get { return (MainWindow)base.Window; }
        }

        partial void addNewPath (NSObject sender)
        {
            Paths.CreatePath ();
        }

        [Export ("validateToolbarItem:")]
        public bool ValidateToolbarItem(NSToolbarItem item)
        {
            if (item.Identifier == "AddPath") {
                return (Window.Map != null);
            }
            return true;
        }
    }
}
