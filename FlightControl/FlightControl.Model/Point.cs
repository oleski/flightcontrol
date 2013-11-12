namespace FlightControl.Model
{
    using System.Runtime.Serialization;
    using System.Drawing;

    [DataContract]
    public class Point
    {
        [DataMember(Name = "x")]
        public double X { get; set; }

        [DataMember(Name = "y")]
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(System.Drawing.Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public static Point operator -(Point p1, Point p2)
        {
            System.Drawing.Point pp1 = new System.Drawing.Point((int)p1.X, (int)p1.Y);
            System.Drawing.Point pp2 = new System.Drawing.Point((int)p2.X, (int)p2.Y);
            return new Point(pp2.X - pp1.X, pp2.Y - pp1.Y);
        }

        public static Point operator +(Point p1, Point p2)
        {
            System.Drawing.Point pp1 = new System.Drawing.Point((int)p1.X, (int)p1.Y);
            System.Drawing.Point pp2 = new System.Drawing.Point((int)p2.X, (int)p2.Y);
            return new Point(pp2.X + pp1.X, pp2.Y + pp1.Y);
        }
    }
}