using System;
using System.Collections.Generic;

using Foundation;
using AppKit;

namespace Walker
{
    public partial class MainWindowController : NSWindowController
    {
        public PathModel Paths { get; set; }

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
            ((MainWindow) Window).Paths = Paths;
        }

        public new MainWindow Window {
            get { return (MainWindow)base.Window; }
        }

        partial void addNewPath (NSObject sender)
        {
            Paths.CreatePath ();
        }
    }
}
