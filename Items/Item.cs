using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Linq;

namespace Go.Items
{
    public struct ItemDistanceTo
    {
        public Item CurrentItem;
        public double distance;

        static public bool RemoveItem(List<ItemDistanceTo> IDT, Item item)
        {
            for (int i = 0; i< IDT.Count; i++)
            {
                if (IDT[i].CurrentItem.Equals(item))
                {
                    IDT.Remove(IDT[i]);
                    return true;
                }
            }
            return false;
        }
        static public ItemDistanceTo GetItemDT(List<ItemDistanceTo> IDT, Item item)
        {
            foreach(var itemIDT in IDT)
            {
                if (itemIDT.CurrentItem.Equals(item))
                    return itemIDT;
            }
            return IDT.Last();
        }
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
        public List<TypeItem> Types = new List<TypeItem>();
        public int ID { get; protected set; }
        public Panel NewPanel;
        public Item Next = null, Previous = null;
        public Point CurrentPoint { get; private set; }
        public List<ItemDistanceTo> NearItems = new List<ItemDistanceTo>();
        public Circle CurrentCircle;

        protected Form1 _form;

        private static int _id = 0;
        private static int _sizePanel = 6;

        public Item(Point point)
        {
            ID = _id++;
            CurrentPoint = point;
        }
        public Item(Point point, Form1 form, TypeItem type)
        {
            ID = _id++;
            CurrentPoint = point;
            _form = form;
            Types.Add(type);
            CreatePanel();
            //FindNearItems(allItems);
        }
        public Item(List<Item> allItems, Form1 form, Point point, TypeItem type, TypeItem ImpassibleType = null)
        {
            ID = _id++;
            CurrentPoint = point;
            CurrentCircle = new Circle(point, 40);
            _form = form;
            Types.Add(type);
            CreatePanel();
            FindNearItems(allItems);
            FixNearItems(ImpassibleType, type);
        }

        public void SetPosition(Point point)
        {
            CurrentPoint = point;
            NewPanel.Location = new Point(CurrentPoint.X * _form.Scale_img / 100 - _sizePanel / 2, CurrentPoint.Y * _form.Scale_img / 100 - _sizePanel / 2);
            CurrentCircle = new Circle(point, 40);

        }
        public void Scale(int scale)
        {
            NewPanel.Location = new Point(CurrentPoint.X * scale / 100 - _sizePanel / 2, CurrentPoint.Y * scale / 100 - _sizePanel / 2);
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
        public int GetOvercome(Item previousItem, bool withoutSeq)
        {
            //if (Types.Count == 1 && Types.First().Sequence is Single)
            //    return previousItem.Overcome;

            foreach(var type in Types)
            {
                foreach (var typePrev in previousItem.Types)
                {
                    if (type.Equals(typePrev) || (withoutSeq && type.Name == typePrev.Name && type.Overcome == typePrev.Overcome))
                        return type.Overcome;
                }
            }
            return 1;
        }
        private void CreatePanel()
        {
            NewPanel = new Panel();
            NewPanel.Location = new Point(CurrentPoint.X * _form.Scale_img / 100 - _sizePanel / 2, CurrentPoint.Y * _form.Scale_img / 100 - _sizePanel / 2);
            NewPanel.Size = new Size(_sizePanel, _sizePanel);
            NewPanel.Name = ID.ToString();
            NewPanel.BackgroundImage = Properties.Resources.Point;
            NewPanel.BackgroundImageLayout = ImageLayout.Zoom;
            NewPanel.Click += new EventHandler(_form.anyPanel_Click);
            _form.panel_list.Add(NewPanel);

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
        private void FixNearItems(TypeItem ImpassibleType, TypeItem typeself)
        {
            List<ItemDistanceTo> nearFixedList = new List<ItemDistanceTo>();

            foreach (var near in NearItems)
            {
                Ray FromMainToFix = new Ray(CurrentPoint, near.CurrentItem.CurrentPoint);
                if ((typeself.Sequence.items != null && typeself.Sequence.Cross(FromMainToFix, false)) || (ImpassibleType != null && ImpassibleType.Sequence.Cross(FromMainToFix, false)))
                {
                    nearFixedList.Add(near);
                }
                else
                {
                    foreach (var near2 in NearItems)
                    {
                        foreach (var type in near2.CurrentItem.Types)
                        {
                            if (type.Sequence.Cross(FromMainToFix, false))
                            {
                                nearFixedList.Add(near);
                                break;
                            }
                        }
                    }
                }
            }
            DifferenceNears(nearFixedList);
        }
        private void DifferenceNears(List<ItemDistanceTo> listFixed)
        {
            foreach(var itemFixed in listFixed)
            {
                NearItems.Remove(itemFixed);
                ItemDistanceTo.RemoveItem(itemFixed.CurrentItem.NearItems, this);
            }
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
        public void DrawCircle()
        {

            Graphics g = _form.GetPictureBox.CreateGraphics();
            g.DrawEllipse(new Pen(Color.DarkBlue, 1), CurrentPoint.X - CurrentCircle.radius, CurrentPoint.Y - CurrentCircle.radius, CurrentCircle.radius * 2, CurrentCircle.radius * 2);
        }
    }

    public class ImpossibleItem : Item
    {
        public Item itemInner, itemOuter;

        public ImpossibleItem(List<Item> allItems, Form1 form, TypeItem generalType, TypeItem typeInner, TypeItem typeOuter) : base(generalType.Sequence.items[generalType.Sequence.items.Count - 2].CurrentPoint)
        {
            itemInner = new Item(allItems, form, TakePoint(generalType.Sequence.items, true), typeInner, generalType);
            itemOuter = new Item(allItems, form, TakePoint(generalType.Sequence.items, false), typeOuter, generalType);
            
        }

        private Point TakePoint(List<Item> generalItems, bool isInner)
        {
            //if (isInner)
            //    return new Point(generalItems[generalItems.Count - 2].CurrentPoint.X - 5, generalItems[generalItems.Count - 2].CurrentPoint.Y);
            //else
            //    return new Point(generalItems[generalItems.Count - 2].CurrentPoint.X + 5, generalItems[generalItems.Count - 2].CurrentPoint.Y);
            if (generalItems.Count > 2)
            {
                Ray beforeRay = new Ray(generalItems[generalItems.Count - 3].CurrentPoint, generalItems[generalItems.Count - 2].CurrentPoint);
                Ray afterRay = new Ray(generalItems[generalItems.Count - 2].CurrentPoint, generalItems[generalItems.Count - 1].CurrentPoint);
                Ray parallelBeforeRay = beforeRay.GetParallelRay(5.0f, isInner);
                Ray parallelAfterRay = afterRay.GetParallelRay(5.0f, isInner);

                return parallelBeforeRay.Cross(parallelAfterRay);

                //Ray ray = new Ray(generalItems[generalItems.Count - 3].CurrentPoint, newPoint);
                //Circle circle = new Circle(generalItems[generalItems.Count - 2].CurrentPoint, 5);
                //return circle.Cross(ray, isInner);
            }
            return Point.Empty;
        }
    }

}
