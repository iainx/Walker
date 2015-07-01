using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CoreGraphics;

using Walker.Map;

namespace Walker
{
    [Foundation.Register ("MapViewOverlay")]
    public class MapViewOverlay : AppKit.NSView
    {
        public Map.Map Map {
            get;
            set;
        }

        CGRect highlightRect;
        public Map.Map.Position? Highlight {
            set {
                highlightRect = Map.BoundingBoxForTileAtPosition ((Map.Map.Position)value);
                highlightRect.X += (Bounds.Width / 2);

                NeedsDisplay = true;
            }
        }

        #region Constructors

        public MapViewOverlay ()
        {
            Initialize ();
        }

        // Called when created from unmanaged code
        public MapViewOverlay (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        // Called when created directly from a XIB file
        [Export ("initWithCoder:")]
        public MapViewOverlay (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        // Shared initialization code
        void Initialize ()
        {
        }

        #endregion

        public override bool IsFlipped {
            get {
                return true;
            }
        }

        public override void DrawRect (CGRect dirtyRect)
        {
            var path = new NSBezierPath ();

            path.AppendPathWithRect (dirtyRect);
            NSColor.Clear.SetFill ();
            path.Fill ();

            path.AppendPathWithRect (highlightRect);
            NSColor.Red.SetStroke ();
            path.Stroke ();
        }
    }
}
