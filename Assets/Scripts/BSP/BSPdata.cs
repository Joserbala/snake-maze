using UnityEngine;

namespace SnakeMaze.BSP
{
    public class BSPdata
    {
        public Vector2 position;
        public Vector2 size;
        public Vector2 Center
        {
            get
            {
                return position + size / 2f;
            }
        }

        public BSPdata(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        public override string ToString()
        {
            string datastring = "";
            if (this != null)
            {
                datastring += string.Format("pos<{0},{1}>:size<{2},{3}>", position.x, position.y, size.x, size.y);
            }
            return string.Format("{0}", datastring);
        }

    }
}