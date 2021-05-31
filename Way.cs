using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go.Items;

namespace Go
{
    class Item_A_Star
    {
        public Item item;
        public double currentDistance;
        public double heuristic;
        public double finallyDistance;
        public Item_A_Star previousStar;
    }
    class Way
    {
        public double distance;
        public List<Item_A_Star> pointsOnWay = new List<Item_A_Star>();
    
        public List<Item_A_Star> visitedPointGraph = new List<Item_A_Star>();
        List<Item_A_Star> openPoinst = new List<Item_A_Star>();

        public Way() { }

        public List<Item> P_A_StoSuperPoint(List<Item_A_Star> list_PAS)
        {
            if (list_PAS != null)
            {
                List<Item> list = new List<Item>();
                foreach (Item_A_Star item in list_PAS)
                {
                    list.Add(item.item);
                }
                return list;
            }
            return null;
        }

        public bool FindPointAStar_ID(List<Item_A_Star> list, Item_A_Star point)
        {
            foreach(Item_A_Star itemAStar in list)
            {
                if (itemAStar.item.ID == point.item.ID)
                    return true;
            }
            return false;
        }
        public void FindWay(Item start, Item finish, TypeItem startType)
        {   
            List<int> overcomes = new List<int>();
            TypeItem _tempType = startType;
            ///////// основой карты всегда белая область(лес)
            //overcomes.Add(startOvercome);
            Item_A_Star _tempPoint;//= new Point_A_Star();
            Item_A_Star _curentStar = new Item_A_Star();
            _curentStar.item = start;
            _curentStar.currentDistance = 0;
            _curentStar.heuristic = 0;
            _curentStar.finallyDistance = double.MaxValue;
            _curentStar.previousStar = null;

            //overcomes.Add(_curP.starPoint.overcome);// добавляем стартовую проходимость
            //int MinOvercome = overcomes.Last();

            try
            {
                //пока текущий не стал конечным
                while (!_curentStar.item.Equals(finish))
                {
                    //записываем в просмотренные
                    visitedPointGraph.Add(_curentStar);
                    //перебираем ближние
                    foreach (ItemDistanceTo item in _curentStar.item.NearItems)
                    {
                        _tempPoint = new Item_A_Star();
                        _tempPoint.item = item.CurrentItem;
                        _tempPoint.currentDistance = _curentStar.currentDistance + item.distance;// * item.CurrentItem.GetOvercome(_tempType, first); ///////add overcome

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


                        _tempPoint.heuristic = item.CurrentItem.GetDistanceTo(finish);
                        _tempPoint.finallyDistance = _tempPoint.currentDistance + _tempPoint.heuristic;
                        _tempPoint.previousStar = _curentStar;

                        //смотрим есть ли точка в каком-то из списков   !findPointAStar(visitedPointGraph, _tempPoint) && !findPointAStar(openPoinst, _tempPoint)
                        if (!FindPointAStar_ID(visitedPointGraph, _tempPoint))
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
                    Item_A_Star Temp = _curentStar;
                    _curentStar = openPoinst[0];//теперь это пока минимальный
                    foreach (Item_A_Star star in openPoinst)
                    {
                        //ищем минмальный из ближайших  
                        if (star.finallyDistance <= _curentStar.finallyDistance)
                        {
                            _curentStar = star;
                        }
                    }
                    //if(_curentStar.item.NearItems.Exists(x => x.CurrentItem.Equals(_curentStar.previousStar.previousStar)))
                    //{

                    //}
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
                    openPoinst.Remove(_curentStar);
                    //openPoinst.Clear();

                }
                visitedPointGraph.Add(_curentStar);
                this.distance = _curentStar.finallyDistance;

                //восстанавливаем путь по ссылкам на предыдущие 
                while (_curentStar != null)
                {
                    pointsOnWay.Add(_curentStar);
                    _curentStar = _curentStar.previousStar;
                }
                pointsOnWay.Reverse();
            }
            catch
            {
                Console.WriteLine("Путь не удалось найти");
            }
        }
    }
}
