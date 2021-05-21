using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Room
    {
        public GameObject RoomPrefab { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public Room(Vector2 center, Vector2 size, GameObject roomPrefab)
        {
            this.Center = center;
            this.Size = size;
            RoomPrefab = roomPrefab;
        }

        public override string ToString()
        {
            string datastring = "";
            if (this != null)
            {
                datastring += string.Format("pos<{0},{1}>:size<{2},{3}>", Center.x, Center.y, Size.x, Size.y);
            }
            return string.Format("{0}", datastring);
        }
    }
}