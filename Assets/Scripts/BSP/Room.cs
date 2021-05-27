using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Room
    {
        public GameObject RoomGO { get; set; }
        public Vector2 Center { get; set; }
        public Vector2 Size { get; set; }

        public Vector2 BottomLeftCorner => new Vector2(Center.x - Size.x / 2, Center.y - Size.y / 2);
        public Vector2 TopLeftCorner => new Vector2(Center.x - Size.x / 2, Center.y + Size.y / 2);
        public Vector2 BottomRightCorner => new Vector2(Center.x + Size.x / 2, Center.y - Size.y / 2);
        public Vector2 TopRightCorner => new Vector2(Center.x + Size.x / 2, Center.y + Size.y / 2);
        public Vector2 LeftCenterPosition => new Vector2(Center.x - Size.x / 2f, Center.y);
        public Vector2 RightCenterPosition => new Vector2(Center.x + Size.x / 2f, Center.y);
        public Vector2 TopCenterPosition => new Vector2(Center.x, Center.y + Size.y / 2f);
        public Vector2 BottomCenterPosition => new Vector2(Center.x, Center.y - Size.y / 2f);

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