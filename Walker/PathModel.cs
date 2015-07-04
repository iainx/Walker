using System;
using System.Collections.Generic;

namespace Walker
{
    public class PathModel
    {
        public Map.Map Map { get; set; }
        public List<Path> Paths { get; private set; }

        public delegate void PathsChangedHandler (object o, EventArgs args);
        public event PathsChangedHandler PathsChanged;

        public PathModel ()
        {
            Paths = new List<Path> ();
        }

        void OnChanged (Path p)
        {
            if (PathsChanged != null) {
                PathsChanged (this, EventArgs.Empty);
            }
        }

        public Path CreatePath ()
        {
            Path p = new Path (Map);
            p.Changed += (o, args) => OnChanged (p);
            Paths.Add (p);

            OnChanged (p);

            return p;
        }
    }
}

