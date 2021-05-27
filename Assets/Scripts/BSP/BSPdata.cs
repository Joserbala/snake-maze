using UnityEngine;

namespace SnakeMaze.BSP
{
    public class BSPData
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }

        public Vector2 LeftCenterPosition => new Vector2(Center.x - Size.x / 2f, Center.y);
        public Vector2 RightCenterPosition => new Vector2(Center.x + Size.x / 2f, Center.y);
        public Vector2 TopCenterPosition => new Vector2(Center.x, Center.y + Size.y / 2f);
        public Vector2 BottomCenterPosition => new Vector2(Center.x, Center.y - Size.y / 2f);
        public Vector2 BottomLeftCorner => new Vector2(Center.x - Size.x / 2f, Center.y - Size.y / 2f);
        public Vector2 TopLeftCorner => new Vector2(Center.x - Size.x / 2f, Center.y + Size.y / 2f);
        public Vector2 BottomRightCorner => new Vector2(Center.x + Size.x / 2f, Center.y - Size.y / 2f);
        public Vector2 TopRightCorner => new Vector2(Center.x + Size.x / 2f, Center.y + Size.y / 2f);

        public Vector2 Center
        {
            get
            {
                return Position + Size / 2f;
            }
        }

        public BSPData() { }

        public BSPData(Vector2 position, Vector2 size)
        {
            this.Position = position;
            this.Size = size;
        }

        public override string ToString()
        {
            string dataString = "";

            if (this != null)
            {
                dataString += $"pos<{Position.x},{Position.y}>:size<{Size.x},{Size.y}>";
            }

            return dataString;
        }
    }
}