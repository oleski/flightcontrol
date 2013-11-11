using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FlightControl.Services.Algorithms
{
    using System.Drawing;

    public class Bezier
    {
        public static Point GetBezierPoint(float t, Point p0, Point p1, Point p2, Point p3)
        {

            float cx = 3 * (p1.X - p0.X);

            float bx = 3 * (p2.X - p1.X) - cx;

            float ax = p3.X - p0.X - cx - bx;

            float cy = 3 * (p1.Y - p0.Y);

            float by = 3 * (p2.Y - p1.Y) - cy;

            float ay = p3.Y - p0.Y - cy - by;

            float tCubed = t * t * t;

            float tSquared = t * t;

            float resultX = (ax * tCubed) + (bx * tSquared) + (cx * t) + p0.X;

            float resultY = (ay * tCubed) + (by * tSquared) + (cy * t) + p0.Y;

            return new Point((int)resultX, (int)resultY);

        }
    }
}