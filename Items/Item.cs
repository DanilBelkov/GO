using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace Go.Items
{
    public struct ItemDistanceTo
    {
        public Item CurrentItem;
        public double distance;
        static public List<Item> IDT_to_Items(List<ItemDistanceTo> IDT)
        {
            if (IDT != null)
            {
                List<Item> list = new List<Item>();
                foreach (var item in IDT)
                {
                    list.Add(item.CurrentItem);
                }
                return list;
            }
            return null;
        }
    }
    public class Item
    {
        public Sequence Sequence { get; set; }
        public TypeItem TypeItem { get; private set; }
        public int ID { get; protected set; }
        public Panel NewPanel;
        public Item Next = null, Previous = null;
        public Point CurrentPoint { get; private set; }
        public List<ItemDistanceTo> NearItems = new List<ItemDistanceTo>();
        public Circle CurrentCircle;
        public int Overcome { get; private set; }

        protected Form1 _form;

        private static int _id = 0;

        public Item(Point point)
        {
            CurrentPoint = point;
        }
        public Item(List<Item> allItems, Form1 form, TypeItem type, Sequence seq)
        {
            ID = _id++;
            _form = form;
            Sequence = seq;
            TypeItem = type;
            FindNearItems(allItems);
        }
        public Item(List<Item> allItems, Form1 form, Point point, TypeItem type, Sequence seq)
        {
            ID = _id++;
            CurrentPoint = point;
            CurrentCircle = new Circle(point, 40);
            _form = form;
            Sequence = seq;
            TypeItem = type;
            CreatePanel(4);
            FindNearItems(allItems);
            //FixNearItems();
        }

        public void SetPosition(Point point)
        {
            CurrentPoint = point;
            CreatePanel(4);
            CurrentCircle = new Circle(point, 40);
        }
        public void RemoveNearItem(Item item)
        {
            for(int i = 0; i < NearItems.Count; i++)
            {
                if (item.Equals(NearItems[i].CurrentItem))
                {
                    NearItems.Remove(NearItems[i]);
                }
            }
        }

        private void CreatePanel(int sizePanel)
        {
            NewPanel = new Panel();
            NewPanel.Location = new Point(CurrentPoint.X * _form.Scale_img / 100 - sizePanel / 2, CurrentPoint.Y * _form.Scale_img / 100 - sizePanel / 2);
            NewPanel.Size = new Size(sizePanel, sizePanel);
            NewPanel.Name = "Point_panel_" + ID;
            NewPanel.BackColor = Color.Black;
            NewPanel.Click += new EventHandler(_form.anyPanel_Click);

            _form.GetPictureBox.Controls.Add(NewPanel);
        }

        public double GetDistanceTo(Item pointTo)
        {
            return Math.Sqrt(Math.Pow((pointTo.CurrentPoint.X - CurrentPoint.X), 2) + Math.Pow((pointTo.CurrentPoint.Y - CurrentPoint.Y), 2));
        }
        private void FindNearItems(List<Item> allItems)
        {
            ItemDistanceTo _IDT = new ItemDistanceTo();
            if (allItems != null)
            {
                for (int i = 0; i < allItems.Count; i++)
                {
                    if (!allItems[i].Equals(CurrentPoint))
                    {//смотрим пересекаются ли круги у точек
                        if (this.CurrentCircle.Intersection(allItems[i].CurrentCircle))
                        {
                            _IDT.distance = this.GetDistanceTo(allItems[i]);


                            //добавляем в ближние к данной точке итую точку
                            _IDT.CurrentItem = allItems[i];
                            NearItems.Add(_IDT);

                            //добавляем в ближайшие к итой точке данную точку
                            _IDT.CurrentItem = this;
                            allItems[i].NearItems.Add(_IDT);
                        }
                    }
                }
            }
        }
        public void FixNearItems()//здесь можно оптимизировать создав проверку на повторимость пересечения с оной и той же последовательностью 
        {
            List<ItemDistanceTo> nearFixedList = new List<ItemDistanceTo>();
            foreach (var near in NearItems)
            {
                Ray FromMainToFix = new Ray(CurrentPoint, near.CurrentItem.CurrentPoint);
                foreach(var near2 in NearItems)
                {
                    if(near2.CurrentItem.Sequence.Cross(FromMainToFix))
                    {
                        nearFixedList.Add(near);
                        break;
                    }
                }
            }
            DifferenceNears(nearFixedList);
        }
        public void DifferenceNears(List<ItemDistanceTo> listFixed)
        {
            foreach(var itemFixed in listFixed)
            {
                NearItems.Remove(itemFixed);
            }
        }
        public List<ItemDistanceTo> Difference(List<ItemDistanceTo> list, List<Item> zone)
        {
            GraphicsPath path = new GraphicsPath();
            Point[] p = new Point[zone.Count];
            for (int i = 0; i < zone.Count; i++)
            {
                p[i] = zone[i].CurrentPoint;
            }
            path.AddLines(p);
            for(int i = 0; i < list.Count ; i++)
            {
                if(path.IsVisible(list[i].CurrentItem.CurrentPoint) && !InvisibleZone.ContainsInBorder(zone, list[i].CurrentItem))
                {
                    //list[i].CurrentItem.RemoveNearItem(this); 
                    list.Remove(list[i]);
                    i--;
                }
            }

            return list;
        }
        public override bool Equals(Object point)
        {
            if ((point == null) || !this.GetType().Equals(point.GetType()))
                return false;
            else
            {
                Item p = (Item)point;
                return (this.ID == p.ID && this.CurrentPoint == p.CurrentPoint);
            }
        }
        public override int GetHashCode()
        {
            return ID;
        }
        static public List<Point> Items_To_Points(List<Item> list)
        {
            List<Point> points = new List<Point>();
            foreach(var item in list)
            {
                points.Add(item.CurrentPoint);
            }
            return points;
        }
    }
}
