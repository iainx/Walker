using System;

using Foundation;
using AppKit;

using Walker.Map;
namespace Walker
{
    public partial class MainWindow : NSWindow
    {
        public Map.Map Map { 
            get { 
                return mapView.Map;
            }

            set { 
                mapView.Map = value;
            } 
        }

        public MainWindow (IntPtr handle) : base (handle)
        {
        }

        [Export ("initWithCoder:")]
        public MainWindow (NSCoder coder) : base (coder)
        {
        }

        public override void AwakeFromNib ()
        {
            base.AwakeFromNib ();
        }

        
    }
}
