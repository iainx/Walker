using System;

namespace Walker
{
    public class Path
    {
        Map.Map map;
        public PathNode[] nodes;

        public delegate void PathChangedHandler (object o, EventArgs args);
        public event PathChangedHandler Changed;

        public Path (Map.Map _map)
        {
            map = _map;
            nodes = new PathNode[map.Width * map.Height];
        }

        protected void OnPathChanged ()
        {
            if (Changed != null) {
                Changed (this, EventArgs.Empty);
            }
        }

        public void ConnectToNode (Map.Map.Position fromPosition, Map.Map.Position toPosition)
        {
            PathNode fromNode, toNode;
            Map.Direction fromDirection, toDirection;
            int dr, dc;

            dr = fromPosition.Row - toPosition.Row;
            dc = fromPosition.Column - toPosition.Column;

            if (dr == 0) {
                fromDirection = dc > 0 ? Map.Direction.North : Map.Direction.South;
                toDirection = dc > 0 ? Map.Direction.South : Map.Direction.North;
            } else {
                fromDirection = dr > 0 ? Map.Direction.East : Map.Direction.West;
                toDirection = dr > 0 ? Map.Direction.West : Map.Direction.East;
            }

            fromNode = nodes[map.PositionToIndex (fromPosition)];
            toNode = nodes[map.PositionToIndex (toPosition)];

            fromNode.ValidExits |= fromDirection;
            toNode.ValidExits |= toDirection;

            OnPathChanged ();
        }
    }
}

