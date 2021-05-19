using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Corridor
    {
        public Vector2 startPoint, endPoint;

        private float _internalwitdh;
        private Vector2 _center;

        public Vector2 Center
        {
            get
            {
                return (startPoint + endPoint) / 2;
                /*                if (startPoint.x == endPoint.x){
                                    _center.x = startPoint.x;
                                    _center.y = (startPoint.y<endPoint.y)?(endPoint.y-startPoint.y)/2:(startPoint.y+endPoint.y)/2;
                                }
                                else if (startPoint.y == endPoint.y){
                                    _center.x = (startPoint.x<endPoint.x)?(endPoint.x-startPoint.x)/2:(startPoint.x-endPoint.x)/2;
                                    _center.y = startPoint.y;
                                }
                                return _center;
                */
            }
        }

        public float Width
        {
            get
            {
                if (startPoint.y == endPoint.y)
                    return Vector2.Distance(startPoint, endPoint);
                else
                    return _internalwitdh;
            }
        }

        public float Height
        {
            get
            {
                if (startPoint.y == endPoint.y)
                    return _internalwitdh;
                else
                    return Vector2.Distance(startPoint, endPoint);
            }
        }


        public Corridor(Vector2 star, Vector2 end, float width)
        {
            startPoint = star;
            endPoint = end;
            _internalwitdh = width;
        }

        public override string ToString()
        {
            string datastring;
            datastring = "<" + startPoint + ">,<" + endPoint + ">";
            return string.Format("{0}", datastring);
        }
    }
}