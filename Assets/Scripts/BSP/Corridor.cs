using UnityEngine;

namespace SnakeMaze.BSP
{
    public class Corridor
    {
        private float _internalWitdh;
        private Vector2 _center;
        private Vector2 _endPoint;
        private Vector2 _startPoint;

        public Vector2 Center
        {
            get
            {
                return (_startPoint + _endPoint) / 2;
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

        public float Height
        {
            get
            {
                if (_startPoint.y == _endPoint.y)
                    return _internalWitdh;
                else
                    return Vector2.Distance(_startPoint, _endPoint);
            }
        }

        public float Width
        {
            get
            {
                if (_startPoint.y == _endPoint.y)
                    return Vector2.Distance(_startPoint, _endPoint);
                else
                    return _internalWitdh;
            }
        }

        public Corridor(Vector2 start, Vector2 end, float width)
        {
            _startPoint = start;
            _endPoint = end;
            _internalWitdh = width;
        }

        public override string ToString()
        {
            string datastring;
            datastring = "<" + _startPoint + ">,<" + _endPoint + ">";
            return string.Format("{0}", datastring);
        }
    }
}