namespace FlightControl.WebApi.Algorithms
{
    using System.Drawing;

    public class Bezier
    {
        public static Point GetBezierPoint(float t, Point p0, Point p1, Point p2, Point p3)
        {

            var cx = 3*(p1.X - p0.X);

            var bx = 3*(p2.X - p1.X) - cx;

            var ax = p3.X - p0.X - cx - bx;

            var cy = 3*(p1.Y - p0.Y);

            var by = 3*(p2.Y - p1.Y) - cy;

            var ay = p3.Y - p0.Y - cy - by;

            var tCubed = t*t*t;

            var tSquared = t*t;

            var resultX = (ax*tCubed) + (bx*tSquared) + (cx*t) + p0.X;

            var resultY = (ay*tCubed) + (by*tSquared) + (cy*t) + p0.Y;

            return new Point((int) resultX, (int) resultY);

        }
    }
}