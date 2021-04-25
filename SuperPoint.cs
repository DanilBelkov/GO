using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Go
{
    //public interface ISequence
    //{
    //    string ID { get; set; }
    //}
    public enum TypeSequence { OnePoint, Line, Area };
    public enum TypeZone { Hydrography, Flora, ArtificalObject, Landform, Stone };
    public struct PointsDistanceTo
    {
        public SuperPoint onePoint;
        public double distance;
    }
    public class SuperPoint
    {
        public Point currentPoint;
        public SuperPoint previous_Point = null, next_Point = null;
        public TypeZone type_zone;
        public TypeSequence type_seq;
        public int radius;
        public int overcome;
        public Panel newPanel;
        public List<PointsDistanceTo> NearPoints = new List<PointsDistanceTo>();
        public List<SuperPoint> VisitedNearPoints = new List<SuperPoint>();
        public int idObject = 0;
        public int ID
        {
            get
            {
                return currentID;
            }
            set
            {
                id++;
                currentID = id;
            }
        }
        public Sequence ParentObject;

        private int currentID;
        private static int id = 0;
        private Form1 _form;
        private int _sizePanel = 8;
        private Circle currentCircle;

        public SuperPoint()
        {
            currentPoint = Point.Empty;
            type_zone = 0;
            type_seq = 0;
            radius = 0;

            _form = null;
            currentCircle = new Circle();
            newPanel = null;
        }

        private void createPanel()
        {
            newPanel = new Panel();
            newPanel.Location = new Point(currentPoint.X * _form.Scale_img / 100 - _sizePanel / 2, currentPoint.Y * _form.Scale_img / 100 - _sizePanel / 2);
            newPanel.Size = new Size(_sizePanel, _sizePanel);
            newPanel.Name = "Point_panel_" + ID;
            newPanel.BackColor = Color.Black;
            newPanel.Click += new EventHandler(_form.anyPanel_Click);
        }

        public SuperPoint(Form1 form1, Point point, TypeZone tz, TypeSequence ts, int overC)
        {
            currentPoint = point;
            type_zone = tz;
            type_seq = ts;
            _form = form1;
            radius = 40;
            ID++;
            overcome = overC;
            createPanel();

            currentCircle = new Circle(currentPoint, radius);

            
            _form.GetPictureBox.Controls.Add(newPanel);

            _form.panel_list.Add(newPanel);
            //при создании ищем ближние
            FindNear();
           // FixedNear();
           
        }
        //сравнение
        public override bool Equals(Object point)
        {
            if ((point == null) || !this.GetType().Equals(point.GetType()))
                return false;
            else
            {
                SuperPoint p = (SuperPoint)point;
                return (this.ID == p.ID);
            }
        }
        //хэш код для точки
        public override int GetHashCode()
        {
            return id;
        }

        public double GetDistanceTo(SuperPoint pointTo)
        {
            return Math.Sqrt(Math.Pow((pointTo.currentPoint.X - currentPoint.X), 2) + Math.Pow((pointTo.currentPoint.Y - currentPoint.Y), 2));
        }

        public static SuperPoint findSuperPoint(List<SuperPoint> list,  int id)
        {
            foreach(SuperPoint item in list)
            {
                if (item.ID == id)
                    return item;
            }
            return null;
        }

        private void FindNear()
        {
            PointsDistanceTo _PDT = new PointsDistanceTo();
            if (_form.allPoints_list != null)
            {
                for (int i = 0; i < _form.allPoints_list.Count; i++)
                {
                    if (!_form.allPoints_list[i].Equals(currentPoint))
                    {//смотрим пересекаются ли круги у точек
                        if (this.currentCircle.Intersection(_form.allPoints_list[i].currentCircle))
                        {
                            _PDT.distance = this.GetDistanceTo(_form.allPoints_list[i]);

                            
                            //добавляем в ближние к данной точке итую точку
                            _PDT.onePoint = _form.allPoints_list[i];
                            NearPoints.Add(_PDT);

                            //добавляем в ближайшие к итой точке данную точку
                            _PDT.onePoint = this;
                            _form.allPoints_list[i].NearPoints.Add(_PDT);
                        }
                    }
                }
            }
        }

        private double getAngle(Point a, Point c, Point b)
        {
            double x1 = a.X - b.X, x2 = c.X - b.X;
            double y1 = a.Y - b.Y, y2 = c.Y - b.Y;
            double d1 = Math.Sqrt(x1 * x1 + y1 * y1);
            double d2 = Math.Sqrt(x2 * x2 + y2 * y2);

            return Math.Acos((x1 * x2 + y1 * y2) / (d1 * d2));
        }
        //-------------------------------------
        private void FixedNear()
        {
            //SuperPoint a = null;
            //double angle = 1000, otherAngle = 1000;
            //foreach (PointsDistanceTo item in NearPoints)
            //{
            //    if (type_seq == TypeSequence.Area && type_zone == 2)
            //    {
            //        if (item.onePoint is Area)
            //        {
            //            Area rel = (Area)item.onePoint;
            //            if (rel.next == this || rel.previous == this)
            //            {
            //                if (a == null)
            //                    a = rel;
            //                else
            //                    angle = getAngle(a.currentPoint, this.currentPoint, item.onePoint.currentPoint);
            //            }
            //        }
            //    }
            //}
            //if (angle != 1000)
            //{
            //    for (int i = 0; i < NearPoints.Count; i++)
            //    {
            //        if (type_seq == 2 && type_zone == 2)
            //        {
            //            otherAngle = getAngle(a.currentPoint, this.currentPoint, NearPoints[i].onePoint.currentPoint);
            //            if (otherAngle > 0 && otherAngle < angle)
            //            {
            //                NearPoints.Remove(NearPoints[i]);
            //            }
            //        }
            //    }
            //}
        }

    }

    public class Area : SuperPoint
    {
        public static Area firstPoint;
        public static int area_ID = 0;
        public Area previous, next;

        public Area(Form1 form, Point point, TypeZone tz, int overC) : base(form, point, tz, TypeSequence.Area, overC)
        {
            area_ID++;
            previous = null;
            next = null;
            overcome = overC;
        }

        public Area(Form1 form, Point point, TypeZone tz, int overC, SuperPoint prev) : base(form, point, tz, TypeSequence.Area, overC)
        {
            area_ID++;
            previous = (Area)prev;
            previous.next = this;
            next = null;
            overcome = overC;
        }

    }

    public class Line : SuperPoint
    {
        public static int line_ID = 0;

        private bool _relationship;

        public bool GetRelationship()
        {
            return _relationship;

        }

        public Line(Form1 form, Point point, TypeZone tz, int overC, bool rel) : base(form, point, tz, TypeSequence.Line, overC)
        {
            line_ID++;
            _relationship = rel;
            overcome = overC;
        }
    }

    public class OnePoint : SuperPoint
    {
        public static int point_ID = 0;



        public OnePoint(Form1 form, Point point, TypeZone tz, int overC) : base(form, point, tz, TypeSequence.OnePoint, overC)
        {
            point_ID++;
            overcome = overC;
        }
    }

    public abstract class Sequence
    {
        public List<SuperPoint> points_seq = new List<SuperPoint>();
        public float overcome;
        public string ID { get; protected set; }

        public Sequence(List<SuperPoint> LSP, float Over)
        {
            points_seq = LSP;
            overcome = Over;
            foreach (SuperPoint sp in LSP)
            {
                sp.ParentObject = this;
            }
        }
    }
    public class LineP: Sequence
    {
        public static int id = 0;
        public LineP(List<SuperPoint> LSP, float Over): base(LSP, Over)
        {
            id++;
            ID = "Line" + id;

        }
    }

    public class AreaP: Sequence
    {
        public static int id = 0;
        public AreaP(List<SuperPoint> LSP, float Over): base(LSP, Over)
        {
            id++;
            ID = "Area" + id;
        }
    }

}
