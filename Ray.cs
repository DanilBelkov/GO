using System;
using System.Drawing;
using Go.Items;


namespace Go
{
    public class Ray
    {
        public Point from_P, to_P;
        public Point Coordinate { get; private set; }
        public int A, B, C;

        public Ray(Point fP, Point tP)
        {
            from_P = fP;
            to_P = tP;
            A = fP.Y - tP.Y;
            B = tP.X - fP.X;
            C = fP.X * tP.Y - tP.X * fP.Y;

            Coordinate = new Point(tP.X - fP.X, tP.Y - fP.Y);
        }

        public Ray(int A, int B, int C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            from_P = to_P = Coordinate = Point.Empty;
        }
        public override bool Equals(Object ray)
        {
            if ((ray == null) || !this.GetType().Equals(ray.GetType()))
                return false;
            else
            {
                Ray r = (Ray)ray;
                return (this.A == r.A && this.B == r.B && this.C == r.C);
            }
        }
        public override int GetHashCode()
        {
            return A + B + C;
        }
        public Point Cross(Ray two)
        {
            Point pCross = Point.Empty;
            if (!this.Equals(two))
            {
                try
                {
                    pCross.X = (B * two.C - two.B * C) / (A * two.B - two.A * B);
                }
                catch(DivideByZeroException)
                {
                    pCross.X = from_P.X;
                }

                try
                {
                    pCross.Y = (two.A * C - A * two.C) / (A * two.B - two.A * B);
                }
                catch(DivideByZeroException)
                {
                    pCross.Y = from_P.Y;
                }

                return pCross;
            }
            return Point.Empty;
        }
        public bool ContainsInner(Point point, bool withEdge)
        {
            if (point.X >= Math.Min(from_P.X, to_P.X) && point.X <= Math.Max(from_P.X, to_P.X) &&
                point.Y >= Math.Min(from_P.Y, to_P.Y) && point.Y <= Math.Max(from_P.Y, to_P.Y))
            {
                if (!withEdge && (point == from_P || point == to_P))
                    return false;

                return true;
            }

            return false;
        }
        public double Lenght()
        {
            return Math.Sqrt(A * A + B * B);
        }
        static public double Distance(Point from, Point to)
        {
            return Math.Sqrt((to.X - from.X) * (to.X - from.X) + (to.Y - from.Y) * (to.Y - from.Y));
        }
        static public double Angle(Ray ray_1, Ray ray_2)
        {
            return Math.Acos(Math.Abs(ray_1.A * ray_2.A - ray_1.B * ray_2.B) / (ray_1.Lenght() * ray_2.Lenght()));
        }
        public Ray Increase(int heightMap)
        {
            return new Ray(from_P, new Point(from_P.X + Coordinate.X * heightMap, from_P.Y + Coordinate.Y * heightMap));
        }
        public bool IsMatch(Ray two)
        {
            if (Math.Abs(A * two.B) == Math.Abs(two.A * B) && 
                Math.Abs(A * two.C) == Math.Abs(two.A * C) &&
                Math.Abs(C * two.B) == Math.Abs(two.C * B))
            {
                //совпадают
                return true;
            }
            return false;
        }
        public bool IsCross(Ray two)
        {
            if ((A * two.B - two.A * B) == 0)
            {
                if ((B * two.C - two.B * C) == 0)
                    //совпадают
                    return true;

               //параллельны
               return false;
            }
            //пересекаются
            return true;
        }
        public bool OnRight(Point point)
        {
            int D = (point.X - from_P.X) * A - (point.Y - from_P.Y) * B;
            if (D > 0)
                return true;
            else
                return false;
        }
        public bool Include(Point point, bool withEdge)
        {
            int D = (point.X - from_P.X) * A - (point.Y - from_P.Y) * B;
            if (D == 0)
            {
                if (!withEdge && (point == from_P || point == to_P))
                    return false;
                return true;
            }
            else
                return false;
        }
        public Ray Move(Point newPoint)
        {
            int x1, y1, x2, y2;

            x1 = newPoint.X - Math.Abs(to_P.X - from_P.X) / 2;
            x2 = newPoint.X + Math.Abs(to_P.X - from_P.X) / 2;

            if(from_P.X > to_P.X)
            {
                int xTemp = x1;
                x1 = x2;
                x2 = xTemp;
            }
            y1 = newPoint.Y - Math.Abs(to_P.Y - from_P.Y) / 2;
            y2 = newPoint.Y + Math.Abs(to_P.Y - from_P.Y) / 2;

            if (from_P.Y > to_P.Y)
            {
                int yTemp = y1;
                y1 = y2;
                y2 = yTemp;
            }

            return new Ray(new Point(x1, y1), new Point(x2, y2));
        }
        public Ray Rotate_90(Point rotatePoint)
        {
            this.Move(Point.Empty);
            Point fromPoint = new Point(-from_P.Y, from_P.X);
            Point toPoint = new Point(-to_P.Y, to_P.X);
            Ray ray = new Ray(fromPoint, toPoint);

            return ray.Move(rotatePoint);
        }
        public Ray GetParallelRay(float distance, bool left)
        {
            int otherC = (int)(distance * Math.Sqrt(A * A + B * B) * (left ? -1 : 1) + C);
            return new Ray(A, B, otherC);
        }
    }
}
