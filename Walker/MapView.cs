using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;

using Walker.Map;

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

        public override bool IsFlipped {
            get {
                return false;
            }
        }

        public override void DrawRect (CoreGraphics.CGRect dirtyRect)
        {
            base.DrawRect (dirtyRect);

            if (map == null) {
                return;
            }

            for (int row = 0; row < map.Height; row++) {
                for (int column = 0; column < map.Width; column++) {
                    Map.Map.Position position = new Map.Map.Position { Row = row, Column = column };
                    CoreGraphics.CGPoint point = map.PositionToPoint (position);
                    Tile t = map.TileAtPosition (position);

                    var bbox = map.BoundingBoxForTileAtPosition (position);
                    Console.WriteLine ("Bounding box 1: {0}", bbox);

                    bbox.X += (Bounds.Size.Width / 2);
                    bbox.Y += (Bounds.Size.Height / 2);

                    Console.WriteLine ("Bounding box: {0}", bbox);
                    t.Image.DrawInRect (bbox, new CoreGraphics.CGRect (0.0, 0.0, t.Image.Size.Width, t.Image.Size.Height), 
                        NSCompositingOperation.SourceOver, 1.0f);
                }
            }
            /*
            var path = new NSBezierPath ();
            path.AppendPathWithRect (bbox);
            NSColor.Red.SetStroke ();

            path.Stroke ();
*/
        }
    }
}
