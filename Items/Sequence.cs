using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Go.Items
{
    public abstract class Sequence
    {
        public TypeItem Type { get; protected set; }
        public string ID { get; protected set; }
        public List<Item> items;
        public static int id { get; private set; }

        protected Form1 _form;
        protected Sequence()
        {

        }
        protected Sequence(Form1 form, TypeItem type)
        {
            id++;
            Type = type;
            _form = form;
        }
        public void SetType(TypeItem type)
        {
            Type = type;
        }
        abstract public bool Cross(Ray ray);
        abstract public void SetItems(List<Item> list);
        public static int CrossList(Ray ray, List<Item> listItems)
        {
            int countCross = 0;
            if (listItems.Count != 0)
            {
                Ray rayTemp;
                Point point_1 = Point.Empty, point_2 = Point.Empty, point_temp;
                bool IsPreviousMatch = false;

                for (int i = 1; i < listItems.Count; i++)
                {
                    rayTemp = new Ray(listItems[i - 1].CurrentPoint, listItems[i].CurrentPoint);
                    //// совпадающие лучи
                    if (ray.IsMatch(rayTemp))
                    {
                        return 1;
                        //IsPreviousMatch = true;
                    }
                    else if (IsPreviousMatch)
                    {
                        //if (items[i - 1].CurrentPoint == ray.to_P)
                        //return 1;
                    }
                    ////
                    if (ray.IsCross(rayTemp))
                    {
                        point_temp = ray.Cross(rayTemp);

                        if (rayTemp.ContainsInner(point_temp) && (point_temp != point_1 && point_temp != point_2))
                        {
                            countCross++;
                            if (point_1 == Point.Empty)
                                point_1 = point_temp;
                            else
                                point_2 = point_temp;
                        }
                    }
                }

                rayTemp = new Ray(listItems.Last().CurrentPoint, listItems.First().CurrentPoint);
                // если совпадает с последним лучем
                if (ray.IsMatch(rayTemp))
                    return 1;
                point_temp = ray.Cross(rayTemp);
                if (ray.IsCross(rayTemp) && rayTemp.ContainsInner(point_temp) && point_temp != point_1)
                    countCross++;
            }
            return countCross;
        }
        abstract public Sequence GetCopy();
    }

    public class Single : Sequence
    {
        private static int _id = 0;
        public Single()
        {

        }
        public Single(Form1 form, TypeItem type) : base(form, type)
        {

        }
        public Single(Form1 form, List<Item> items, TypeItem type) : base(form, type)
        {
            ID = "Point_" + _id++;
            SetItems(items);
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            items.First().Sequence = this;
        }
        override public Sequence GetCopy()
        {
            return new Single(_form, items, Type);
        }
        public override bool Cross(Ray ray)
        {

            if (ray.ContainsInner(items.First().CurrentPoint) && items.First().CurrentPoint != ray.to_P)
                return true;

            return false;
        }
    }
    public class Line : Sequence
    {
        private static int _id = 0;
        private Panel _panelLine;

        public Line()
        {

        }
        public Line(Form1 form, TypeItem type) : base(form, type)
        {
            ID = "Line_" + _id++;
        }
        public Line(Form1 form, List<Item> items, TypeItem type) : base(form, type)
        {
            ID = "Line_" + _id++;
            SetItems(items);
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            foreach (var item in this.items)
            {
                item.Sequence = this;
            }
            //CreatePanel();
        }
        public void CreatePanel()
        {
            if (items.Count != 0)
            {
                _panelLine = new Panel();
                GraphicsPath path = new GraphicsPath();

                Point[] p = new Point[items.Count * 2];
                for (int i = 0; i < items.Count; i++)
                {
                    p[i].X = items[i].CurrentPoint.X * _form.Scale_img / 100;
                    p[i].Y = items[i].CurrentPoint.Y * _form.Scale_img / 100;
                }
                for (int i = items.Count; i < items.Count * 2; i++)
                {
                    p[i].X = p[items.Count * 2 - i - 1].X + 2;
                    p[i].Y = p[items.Count * 2 - i - 1].Y + 2;
                }
                path.AddLines(p);

                Region Panel_Region = new Region(path);
                _panelLine.Region = Panel_Region;
                _panelLine.Name = "Line_panel_" + ID;
                _panelLine.BackColor = Type.Color;
                _panelLine.Size = _form.GetPictureBox.Size;
                _panelLine.Click += new EventHandler(_form.anyPanel_Click);
                //_form.GetPictureBox.Controls.Add(_panelLine);
            }

        }
        public void Draw(PictureBox pictureBox, int currentScale)
        {
            if (items.Count != 0)
            {
                Graphics g = pictureBox.CreateGraphics();
                var prev = items.First();
                for (int i = 1; i < items.Count - 1; i++)
                {
                    g.DrawLine(new Pen(Type.Color, 3),
                        prev.CurrentPoint.X * currentScale / 100, prev.CurrentPoint.Y * currentScale / 100,
                        items[i].CurrentPoint.X * currentScale / 100, items[i].CurrentPoint.Y * currentScale / 100);
                    prev = items[i];
                }
                g.DrawLine(new Pen(Type.Color, 3),
                        prev.CurrentPoint.X * currentScale / 100, prev.CurrentPoint.Y * currentScale / 100,
                        items.Last().CurrentPoint.X * currentScale / 100, items.Last().CurrentPoint.Y * currentScale / 100);
            }
        }

        override public Sequence GetCopy()
        {
            return new Line(_form, items, Type);
        }
        public override bool Cross(Ray ray)
        {
            Ray newRay;
            for (int i = 1; i < items.Count; i++)
            {
                if (ray.to_P == items[i - 1].CurrentPoint || ray.to_P == items[i].CurrentPoint)
                    return false;

                newRay = new Ray(items[i - 1].CurrentPoint, items[i].CurrentPoint);
                if (ray.IsCross(newRay))
                {
                    //if (ray.IsMatch(newRay))
                    //    return false;

                    if (ray.from_P == newRay.from_P)
                        return false;

                    Point crossPoint = ray.Cross(newRay);
                    if(ray.ContainsInner(crossPoint) && newRay.ContainsInner(crossPoint))
                        return true;
                }  
            }

            return false;
        }
    }
    public class Area : Sequence
    {
        private static int _id = 0;
        private Panel _panelArea;

        public Area()
        {

        }
        public Area(Form1 form, TypeItem type) : base(form, type)
        {
            ID = "Area_" + _id++;
        }
        public Area(Form1 form, List<Item> items, TypeItem type) : base(form, type)
        {
            ID = "Area_" + _id++;
            SetItems(items);
            //CreatePanel();
        }
        override public Sequence GetCopy()
        {
            return new Area(_form, items, Type);
        }
        public override bool Cross(Ray ray)
        {
            Ray newRay;
            try
            {
                for (int i = 1; i < items.Count; i++)
                {
                    newRay = new Ray(items[i - 1].CurrentPoint, items[i].CurrentPoint);
                    if (ray.IsCross(newRay))
                    {
                        Point crossPoint = ray.Cross(newRay);
                        if (newRay.ContainsInner(crossPoint) && ray.ContainsInner(crossPoint))
                            return true;
                    }

                    //if (ray.to_P == items[i - 1].CurrentPoint || ray.to_P == items[i].CurrentPoint)    //ray.ContainsInner(crossPoint) &&
                    //    return false;
                }
                newRay = new Ray(items.Last().CurrentPoint, items.First().CurrentPoint);
                if (ray.IsCross(newRay))
                {
                    Point crossPoint = ray.Cross(newRay);
                    if (ray.ContainsInner(crossPoint) && newRay.ContainsInner(crossPoint))
                        return true;
                }
            }
            catch
            {
            }
            return false;
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            foreach (var item in this.items)
            {
                item.Sequence = this;
            }
            //CreatePanel();
        }
        public void CreatePanel()
        {
            if (items.Count != 0)
            {
                _panelArea = new Panel();
                GraphicsPath path = new GraphicsPath();

                Point[] p = new Point[items.Count];
                for (int i = 0; i < items.Count; i++)
                {
                    p[i].X = items[i].CurrentPoint.X * _form.Scale_img / 100;
                    p[i].Y = items[i].CurrentPoint.Y * _form.Scale_img / 100;
                }
                path.AddLines(p);

                Region Panel_Region = new Region(path);
                _panelArea.Region = Panel_Region;
                _panelArea.Name = "Line_panel_" + ID;
                _panelArea.BackColor = Type.Color;
                _panelArea.Size = _form.GetPictureBox.Size;
                _panelArea.Click += new EventHandler(_form.anyPanel_Click);
                //_form.GetPictureBox.Controls.Add(_panelArea);
            }
        }
       
    }
}
