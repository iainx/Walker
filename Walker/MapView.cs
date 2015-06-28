using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

namespace Walker
{
    public partial class MapView : AppKit.NSView
    {
        #region Constructors

        Map.Map map;
        public Map.Map Map { 
            get {
                return map;
            } 
            set {
                map = value;

                Frame = new CoreGraphics.CGRect (Frame.X, Frame.Y, map.Width * 50, map.Height * 25);
            } 
        }

        // Called when created from unmanaged code
        public MapView (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public MapView (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        public override void DrawRect (CoreGraphics.CGRect dirtyRect)
        {
            base.DrawRect (dirtyRect);
            var path = new NSBezierPath ();
            path.AppendPathWithRect (dirtyRect);
            NSColor.Red.SetFill ();

            path.Fill ();
        }
    }
}
