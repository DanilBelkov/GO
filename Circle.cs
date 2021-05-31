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

        public Point Cross(Ray ray, bool isFirst)
        {
            double l = Math.Sqrt(ray.A * ray.A + ray.B * ray.B);
            double dx = ray.A / l; // потому что направляющие вектора
            double dy = ray.B / l;

            double x, y;
            if (isFirst)
            {
                x = currentPoint.X + dx * radius;
                y = currentPoint.Y + dy * radius;
            }
            else
            { 
                x = currentPoint.X - dx * radius;
                y = currentPoint.Y - dy * radius;
            }

            return new Point((int)x, (int)y);
        }
    } 
}
