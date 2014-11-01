using UnityEngine;
using System.Collections;

namespace Common
{
    public class Point
    {
        public int x = 0;
        public int y = 0;

        public Point(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        /// <summary>
        /// 디버깅 시 좌표를 좀 더 쉽게 보기 위해
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return "Point[x: " + x + ", y: " + y +"]";
        }

        /// <summary>
        /// 자신과 동일한 Point 객체를 하나 더 만든다.
        /// </summary>
        /// <returns></returns>
        public Point Clone()
        {
            return new Common.Point(x, y);
        }

        /// <summary>
        /// Point끼리 비교하기 위해서
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool isEqual(Point p)
        {
            return (p.x == x) && (p.y == y);
        }

        public static bool operator==(Point p1, Point p2)
        {
            if (object.ReferenceEquals(null, p1) )
            {
                return object.ReferenceEquals(null, p2);
            }
            if (object.ReferenceEquals(null, p2) )
            {
                return object.ReferenceEquals(null, p1);
            }
            return p1.isEqual(p2);
        }

        public static bool operator!=(Point p1, Point p2)
        {
            if (object.ReferenceEquals(null, p1) )
            {
                return !object.ReferenceEquals(null, p2);
            }
            if (object.ReferenceEquals(null, p2) )
            {
                return !object.ReferenceEquals(null, p1);
            }
            return !p1.isEqual(p2);
        }

        public override bool Equals(object o)
        {
            return this.isEqual((Point)o);
        }

        /// <summary>
        /// Dictionary에서 키값으로 Point를 쓰기 위해서
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}


