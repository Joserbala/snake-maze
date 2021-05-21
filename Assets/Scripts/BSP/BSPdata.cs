﻿using UnityEngine;

namespace SnakeMaze.BSP
{
    public class BSPData
    {
        public Vector2 Position { get; set; }
        public Vector2 Size { get; set; }
        public Vector2 Center
        {
            get
            {
                return Position + Size / 2f;
            }
        }

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
                dataString += string.Format("pos<{0},{1}>:size<{2},{3}>", Position.x, Position.y, Size.x, Size.y);
            }
            return string.Format("{0}", dataString);
        }
    }
}