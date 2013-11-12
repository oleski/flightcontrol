namespace FlightControl.Algorithms
{
    using Model;

    public static class PlaneExtensions
    {
        public static bool CanLand(this Plane plane, Point landingStrip)
        {
            var currentPos = plane.Position;
            return true;
        }
    }
}
