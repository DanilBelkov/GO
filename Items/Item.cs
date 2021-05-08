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
        public int Overcome { get; private set; }

        protected Form1 _form;

        private static int _id = 0;
        private static int _sizePanel = 6;

        public Item(Point point)
        {
            CurrentPoint = point;
        }
        public Item(List<Item> allItems, Form1 form, TypeItem type)
        {
            ID = _id++;
            _form = form;
            Types.Add(type);
            FindNearItems(allItems);
        }
        public Item(List<Item> allItems, Form1 form, Point point, TypeItem type)
        {
            ID = _id++;
            CurrentPoint = point;
            CurrentCircle = new Circle(point, 40);
            _form = form;
            Types.Add(type);
            CreatePanel();
            FindNearItems(allItems);
            FixNearItems();
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
        public void FixNearItems()//здесь можно оптимизировать создав проверку на повторимость пересечения с оной и той же последовательностью 
        {
            List<ItemDistanceTo> nearFixedList = new List<ItemDistanceTo>();
            foreach (var near in NearItems)
            {
                Ray FromMainToFix = new Ray(CurrentPoint, near.CurrentItem.CurrentPoint);
                foreach(var near2 in NearItems)
                {
                    foreach (var type in near2.CurrentItem.Types)
                    {
                        if (type.Sequence.Cross(FromMainToFix))
                        {
                            nearFixedList.Add(near);
                            break;
                        }
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
    }
}
