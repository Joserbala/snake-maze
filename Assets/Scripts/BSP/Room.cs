using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Room
    {
        public GameObject RoomGO { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public Vector2 BottomLeftCorner => new Vector2(Center.x - Size.x / 2, Center.y - Size.y / 2);
        public Vector2 TopLeftPosition => new Vector2(Center.x - Size.x / 2, Center.y + Size.y / 2);
        public Vector2 BottomRightPosition => new Vector2(Center.x + Size.x / 2, Center.y - Size.y / 2);
        public Vector2 TopRightPosition => new Vector2(Center.x + Size.x / 2, Center.y + Size.y / 2);

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