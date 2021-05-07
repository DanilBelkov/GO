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
        public string ID { get; protected set; }
        public List<Item> items;
        public static int id { get; private set; }

        protected Form1 _form;
        protected Sequence()
        {
            id++;
        }
        protected Sequence(Form1 form)
        {
            id++;
            _form = form;
        }
        abstract public bool Cross(Ray ray);
        abstract public void SetItems(List<Item> list);
        abstract public Sequence GetCopy();

        public override bool Equals(Object seq)
        {
            if ((seq == null) || !this.GetType().Equals(seq.GetType()))
                return false;
            else
            {
                Sequence p = (Sequence)seq;
                return (this.ID == p.ID);
            }
        }
        public override int GetHashCode()
        {
            return id;
        }
    }

    public class Single : Sequence
    {
        private static int _id = 0;
        public Single()
        {
            ID = "Point_" + _id++;
        }
        public Single(Form1 form) : base(form)
        {
            ID = "Point_" + _id++;
        }
        public Single(Form1 form, List<Item> items) : base(form)
        {
            ID = "Point_" + _id++;
            SetItems(items);
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            //if(!items.First().Sequences.Contains(this))
            //    items.First().Sequences.Add(this);
        }
        override public Sequence GetCopy()
        {
            return new Single(_form, items);
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
            ID = "Line_" + _id++;
        }
        public Line(Form1 form) : base(form)
        {
            ID = "Line_" + _id++;
        }
        public Line(Form1 form, List<Item> items) : base(form)
        {
            ID = "Line_" + _id++;
            SetItems(items);
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            //foreach (var item in this.items)
            //{
            //    if (!item.Types.Contains(this))
            //        item.Sequences.Add(this);
            //}
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
                //_panelLine.BackColor = Type.Color;
                _panelLine.Size = _form.GetPictureBox.Size;
                _panelLine.Click += new EventHandler(_form.anyPanel_Click);
                //_form.GetPictureBox.Controls.Add(_panelLine);
            }

        }
        //public void Draw(PictureBox pictureBox, int currentScale)
        //{
        //    if (items.Count != 0)
        //    {
        //        Graphics g = pictureBox.CreateGraphics();
        //        var prev = items.First();
        //        for (int i = 1; i < items.Count - 1; i++)
        //        {
        //            g.DrawLine(new Pen(Type.Color, 3),
        //                prev.CurrentPoint.X * currentScale / 100, prev.CurrentPoint.Y * currentScale / 100,
        //                items[i].CurrentPoint.X * currentScale / 100, items[i].CurrentPoint.Y * currentScale / 100);
        //            prev = items[i];
        //        }
        //        g.DrawLine(new Pen(Type.Color, 3),
        //                prev.CurrentPoint.X * currentScale / 100, prev.CurrentPoint.Y * currentScale / 100,
        //                items.Last().CurrentPoint.X * currentScale / 100, items.Last().CurrentPoint.Y * currentScale / 100);
        //    }
        //}

        override public Sequence GetCopy()
        {
            return new Line(_form, items);
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
                }
            }
            catch
            {
                Console.WriteLine("Ошибка с пересечением Линии");
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
            ID = "Area_" + _id++;
        }
        public Area(Form1 form) : base(form)
        {
            ID = "Area_" + _id++;
        }
        public Area(Form1 form, List<Item> items) : base(form)
        {
            ID = "Area_" + _id++;
            SetItems(items);
            //CreatePanel();
        }
        override public Sequence GetCopy()
        {
            return new Area(_form, items);
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
                }
                if (items.Last().Next == null)
                    return false;
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
                Console.WriteLine("Ошибка с пересечением Области");
            }
            return false;
        }
        override public void SetItems(List<Item> items)
        {
            this.items = new List<Item>(items);
            //foreach (var item in this.items)
            //{
            //    if (!item.Sequences.Contains(this))
            //        item.Sequences.Add(this);
            //}
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
                //_panelArea.BackColor = Type.Color;
                _panelArea.Size = _form.GetPictureBox.Size;
                _panelArea.Click += new EventHandler(_form.anyPanel_Click);
                //_form.GetPictureBox.Controls.Add(_panelArea);
            }
        }
       
    }
}
