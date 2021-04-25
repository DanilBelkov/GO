using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Go
{
    public class Circle
    {
        public Point currentPoint;
        public int radius;


        public Circle()
        {
            currentPoint = Point.Empty;
            radius = 0;
        }

        public Circle(Point point, int rad)
        {
            currentPoint = point;
            radius = rad;
        }

        public bool Intersection(Circle circle)
        {
            double distance = Math.Sqrt(Math.Pow((circle.currentPoint.X - currentPoint.X),2) + Math.Pow((circle.currentPoint.Y - currentPoint.Y),2));
            if (distance > (radius + circle.radius) || distance < Math.Abs(radius - circle.radius))
                return false;
            else
                return true;
        }
    } 
}
