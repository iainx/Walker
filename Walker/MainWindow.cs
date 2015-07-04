using System;
using System.Collections.Generic;
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
                if (Paths != null) {
                    Paths.Map = value;
                }
            }
        }

        PathModel paths;
        public PathModel Paths { 
            get { 
                return paths;
            }

            set { 
                paths = value;
                mapView.overlay.Paths = value; 
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
