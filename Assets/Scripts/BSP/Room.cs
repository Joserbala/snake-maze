using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Room
    {
        public Vector2 center;
        public Vector2 size;

        public Room(Vector2 center, Vector2 size)
        {
            this.center = center;
            this.size = size;
        }

        public override string ToString()
        {
            string datastring = "";
            if (this != null)
            {
                datastring += string.Format("pos<{0},{1}>:size<{2},{3}>", center.x, center.y, size.x, size.y);
            }
            return string.Format("{0}", datastring);
        }
    }
}