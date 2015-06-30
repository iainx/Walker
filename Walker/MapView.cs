using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using AppKit;
using CoreGraphics;

using Walker.Map;

namespace Walker
{
    public partial class MapView : AppKit.NSView
    {
        #region Constructors

        Map.Map.Position? highlight;

        Map.Map map;
        public Map.Map Map { 
            get {
                return map;
            } 
            set {
                map = value;
                InvalidateIntrinsicContentSize ();
                UpdateMapViews ();
            } 
        }

        public override CGSize IntrinsicContentSize {
            get {
                if (map != null) {
                    return new CGSize (map.Width * 100, map.Height * 50 + 15);
                } else {
                    return new CGSize (100, 65);
                }
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
            NSTrackingArea tracker = new NSTrackingArea (CoreGraphics.CGRect.Empty, 
                NSTrackingAreaOptions.ActiveInActiveApp | NSTrackingAreaOptions.InVisibleRect | NSTrackingAreaOptions.MouseMoved, 
                this, null);
            TranslatesAutoresizingMaskIntoConstraints = false;
            AddTrackingArea (tracker);
        }

        #endregion

        public override bool IsFlipped {
            get {
                return true;
            }
        }

        void UpdateMapViews ()
        {
            if (map == null) {
                return;
            }

            for (int row = 0; row < map.Height; row++) {
                for (int column = 0; column < map.Width; column++) {
                    Map.Map.Position position = new Map.Map.Position { Row = row, Column = column };
                    CGPoint point = map.PositionToPoint (position);
                    Tile t = map.TileAtPosition (position);

                    var bbox = map.BoundingBoxForTileAtPosition (position);

                    Console.WriteLine ("{0}", point);
                    Console.WriteLine ("bbox: {0}", bbox);
                    NSImageView tileView = new NSImageView (bbox) {Image = t.Image, TranslatesAutoresizingMaskIntoConstraints = false};
                    AddSubview (tileView);

                    var constraint = NSLayoutConstraint.Create (this,
                                         NSLayoutAttribute.CenterX, NSLayoutRelation.Equal,
                                         tileView, NSLayoutAttribute.CenterX, 1, -point.X);
                    AddConstraint (constraint);

                    constraint = NSLayoutConstraint.Create (this,
                        NSLayoutAttribute.Top, NSLayoutRelation.Equal,
                        tileView, NSLayoutAttribute.Bottom, 1, -65 + point.Y);
                    AddConstraint (constraint);

                    constraint = NSLayoutConstraint.Create (tileView, NSLayoutAttribute.Width, NSLayoutRelation.Equal,
                        null, NSLayoutAttribute.NoAttribute, 1, bbox.Width);
                    tileView.AddConstraint (constraint);

                    constraint = NSLayoutConstraint.Create (tileView, NSLayoutAttribute.Height, NSLayoutRelation.Equal,
                        null, NSLayoutAttribute.NoAttribute, 1, bbox.Height);
                    tileView.AddConstraint (constraint);
                }
            }
        }

        /*
        public override void MouseMoved (NSEvent theEvent)
        {
            var locationInView = ConvertPointToView (theEvent.LocationInWindow, null);
            locationInView.X -= (Bounds.Size.Width / 2);
            locationInView.Y -= (Bounds.Size.Height - 15);

            var possiblePosition = map.PointToPosition (locationInView);

            Console.WriteLine ("{0} -> {1}", locationInView, possiblePosition);
            if (map.PositionIsValid (possiblePosition)) {
                highlight = possiblePosition;
                NeedsDisplay = true;
            }
        }
        */
    }
}
