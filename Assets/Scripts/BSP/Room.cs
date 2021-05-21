using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Room
    {
        public GameObject RoomGO { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public Room(Vector2 center, Vector2 size, GameObject roomPrefab)
        {
            Center = center;
            Size = size;
            RoomGO = roomPrefab;
        }

        public override string ToString()
        {
            var dataString = "";

            if (this != null)
            {
                dataString += $"pos<{Center.x},{Center.y}>:size<{Size.x},{Size.y}>";
            }

            return dataString;
        }
    }
}