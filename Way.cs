using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.Items;

namespace Go
{
    class Point_A_Star
    {
        public Item starPoint;
        public double currentDistance;
        public double heuristic;
        public double finallyDistance;
        public Point_A_Star previousPoint;
    }
    class Way
    {
        public double distance;
        public List<Point_A_Star> pointOnWay = new List<Point_A_Star>();
    
        public List<Point_A_Star> visitedPointGraph = new List<Point_A_Star>();
        List<Point_A_Star> openPoinst = new List<Point_A_Star>();

        public Way() { }

        public List<Item> P_A_StoSuperPoint(List<Point_A_Star> list_PAS)
        {
            if (list_PAS != null)
            {
                List<Item> list = new List<Item>();
                foreach (Point_A_Star item in list_PAS)
                {
                    list.Add(item.starPoint);
                }
                return list;
            }
            return null;
        }

        public bool findPointAStar(List<Point_A_Star> list, Point_A_Star point)
        {
            foreach(Point_A_Star item in list)
            {
                if (item.starPoint.Equals(point.starPoint))
                    return true;
            }
            return false;
        }
        public void FindWay(Item startP, Item endP)
        {
            
            List<int> overcomes = new List<int>();
            ///////// основой карты всегда белая область(лес)
            //overcomes.Add(10);
            Point_A_Star _tempPoint;//= new Point_A_Star();
            Point_A_Star _curP = new Point_A_Star();
            _curP.starPoint = startP;
            _curP.currentDistance = 0;
            _curP.heuristic = 0;
            _curP.finallyDistance = 50000;
            _curP.previousPoint = null;

            //overcomes.Add(_curP.starPoint.overcome);// добавляем стартовую проходимость
            //int MinOvercome = overcomes.Last();

            try
            {
                //пока текущий не стал конечным
                while (!_curP.starPoint.Equals(endP))
                {
                    //записываем в просмотренные
                    visitedPointGraph.Add(_curP);
                    //перебираем ближние
                    foreach (ItemDistanceTo item in _curP.starPoint.NearItems)
                    {
                        _tempPoint = new Point_A_Star();
                        _tempPoint.starPoint = item.CurrentItem;
                        _tempPoint.currentDistance = _curP.currentDistance + item.distance;// * overcomes.Last(); ///////add overcome

                        ////если проходимость одна и таже
                        //if (overcomes.Last() == item.CurrentItem.Overcome)
                        //{
                        //    // и обе точки относятся к классу Область
                        //    if(_curP.starPoint is Area && item.onePoint is Area)
                        //    {
                        //        Area temp = _curP.starPoint as Area;//опускаем до области
                        //        //если точки идут последовательно 
                        //        if(temp.next.Equals(item.onePoint) || temp.previous.Equals(item.onePoint))
                        //            _tempPoint.currentDistance = _curP.currentDistance + item.distance * MinOvercome;
                        //    }

                        //}


                        _tempPoint.heuristic = item.CurrentItem.GetDistanceTo(endP);
                        _tempPoint.finallyDistance = _tempPoint.currentDistance + _tempPoint.heuristic;
                        _tempPoint.previousPoint = _curP;

                        //смотрим есть ли точка в каком-то из списков
                        if (!findPointAStar(openPoinst, _tempPoint) && !findPointAStar(visitedPointGraph, _tempPoint))
                        {
                            //записываем в открытый список
                            openPoinst.Add(_tempPoint);
                        }
                    }
                    // если открытый список опустел, закругляемся
                    if (openPoinst.Count == 0)
                    {
                        return;
                    }
                    Point_A_Star Temp = _curP;
                    _curP = openPoinst[0];//теперь это пока минимальный
                    foreach (Point_A_Star item in openPoinst)
                    {
                        //ищем минмальный из ближайших  
                        if (item.finallyDistance <= _curP.finallyDistance)
                        {
                            _curP = item;
                        }
                    }
                    //добавляем "скобку"
                    //overcomes.Add(_curP.starPoint.overcome);
                    // если это "скобка закрывает, то удаляем", то есть мы вошли и вышли из одной области
                    //if(Temp.starPoint.overcome == _curP.starPoint.overcome) //overcomes[overcomes.Count - 1] == overcomes[overcomes.Count - 2])
                    //{
                    //    if(Temp.starPoint is Area && _curP.starPoint is Area)
                    //        overcomes.RemoveRange(overcomes.Count - 2, 2);
                    //}
                    //else  // если разные коэффы, то ищем минимальный 
                    //    MinOvercome = Math.Min(overcomes[overcomes.Count - 1], overcomes[overcomes.Count - 2]);
                    //убираем из открытого списка, потом занесем его в посещеные
                    openPoinst.Remove(_curP);
                    //openPoinst.Clear();

                }
                visitedPointGraph.Add(_curP);
                this.distance = _curP.finallyDistance;

                //восстанавливаем путь по ссылкам на предыдущие 
                while (_curP != null)
                {
                    pointOnWay.Add(_curP);
                    _curP = _curP.previousPoint;
                }
                pointOnWay.Reverse();
            }
            catch
            {
                Console.WriteLine("Путь не удалось найти");
            }
        }
    }
}
