using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Go.Items;

namespace Go
{
    class InvisibleZone
    {
        private List<Item> _invisibleZone;
        private List<Item> _areaBorderItems;
        private Form1 _form;
        public InvisibleZone()
        {

        }

        public InvisibleZone(Form1 form, List<Item> nearestWall)
        {
            _form = form;
            _areaBorderItems = new List<Item>
            {
                new Item(new Point(0, 0)),
                new Item(new Point(_form.GetPictureBox.Width, 0)),
                new Item(new Point(_form.GetPictureBox.Height, _form.GetPictureBox.Width)),
                new Item(new Point(0, _form.GetPictureBox.Height))
            };

            _invisibleZone = new List<Item>(nearestWall);
        }

        public List<Item> GetInvisibleZone(Item item, List<Item> items)
        {
            Ray ray_1 = new Ray(item.CurrentPoint, _invisibleZone.First().CurrentPoint);
            Ray ray_2 = new Ray(item.CurrentPoint, _invisibleZone.Last().CurrentPoint);

            //создаем луч из крайних
            Ray betweenRay = new Ray(_invisibleZone.First().CurrentPoint, _invisibleZone.Last().CurrentPoint);

            //добавляем краевые точки с увеличенных лучей
            _invisibleZone.Insert(0, new Item(ray_1.Increase(_form.GetPictureBox.Width).to_P));
            _invisibleZone.Add(new Item(ray_2.Increase(_form.GetPictureBox.Width).to_P));

            //final Point
            Point normalTo = Point.Empty;
            normalTo.X = item.CurrentPoint.X - betweenRay.A * _form.GetPictureBox.Width;
            normalTo.Y = item.CurrentPoint.Y + betweenRay.B * _form.GetPictureBox.Width;

            Ray normalRay = new Ray(item.CurrentPoint, normalTo);
            //if (Items.Sequence.Cross(normalRay, items) != 2)
            //{
            //    normalTo = new Point(-normalTo.X, -normalTo.Y);
            //}
            _invisibleZone.Add(new Item(normalTo));
            //AddCrossBorderItem(ray_2.Increase(_form.GetPictureBox.Width), true);
            //AddCrossBorderItem(ray_1.Increase(_form.GetPictureBox.Width), false);

            //Point betweenP = new Point(_invisibleZone.First().CurrentPoint.X + _invisibleZone.Last().CurrentPoint.X, _invisibleZone.First().CurrentPoint.Y + _invisibleZone.Last().CurrentPoint.Y);
            //Ray betweenRay = new Ray(item.CurrentPoint, betweenP);
            //betweenRay.Increase(_form.GetPictureBox.Width);
            //Item betweenItem = new Item(betweenRay.to_P);
            //_invisibleZone.Add(betweenItem);

            return _invisibleZone;
        }
        private List<Item> GetSortBorderItemsByAngle(Ray ray, Item item, double maxAngle)
        {
            List<double> angles = new List<double>();
            List<Item> sortItemsByAngle = new List<Item>();
            foreach (var itemBorder in _areaBorderItems)
            {
                Ray ray_B = new Ray(item.CurrentPoint, itemBorder.CurrentPoint);
                double angle = Ray.Angle(ray, ray_B);
                int j = 0;
                for (int i = 0; i < angles.Count; i++)
                {
                    if (angle < angles[i])
                        j = i;
                }
                if (angle < maxAngle)
                {
                    angles.Insert(j, angle);
                    sortItemsByAngle.Insert(j, itemBorder);
                }
            }
            return sortItemsByAngle;
        }
       
        private void AddCrossBorderItem(Ray ray, bool end)
        {
            for (int i = 1; i < _areaBorderItems.Count; i++)
            {
                Ray rayBorder = new Ray(_areaBorderItems[i - 1].CurrentPoint, _areaBorderItems[i].CurrentPoint);
                if (ray.IsCross(rayBorder))
                {
                    Point crossBorderPoint = ray.Cross(rayBorder);
                    if (ray.ContainsInner(crossBorderPoint))
                    {
                        if (end)
                            _invisibleZone.Add(new Item(crossBorderPoint));
                        else
                            _invisibleZone.Insert(0, new Item(crossBorderPoint));
                    }
                }
            }
        }
        public static bool ContainsInBorder(List<Item> list, Item item)
        {
            foreach(var itemBorder in list)
            {
                if (item.Equals(itemBorder))
                    return true;
            }
            return false;
        }
    }
}
