using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Drawing.Drawing2D;
using Go.Items;


namespace Go
{

    public partial class Form1 : Form
    {
        public Bitmap img;//наша катринка
        public List<Panel> panel_list = new List<Panel>();

      
       // List<string> Way_list = new List<string> { }; //все пути

        public int Scale_img { get; private set; } // масштаб
        private int _overcome = 100;
        private bool Btn_create_P = false;// флаг на нажатие кнопки создания массива координат
        private bool Btn_item_from = false, Btn_item_to = false;
        private Item item_to, item_from;

        private ToolStripLabel _zone, _seq;
        private Button _type_B;

        private Panel _panelTemp;
       
        private TypeItem _typeItem;
        private TypeItem _typeItemInner;
        private TypeItem _typeItemOuter;
        private Item _item;

        private List<Item> _allItems = new List<Item>();
        private List<Item> _tempItems = new List<Item>();
        private List<Item> _tempItemsInner = new List<Item>();
        private List<Item> _tempItemsOuter = new List<Item>();

        private List<Items.Single> _allSingles = new List<Items.Single>();
        private List<Items.Line> _allLines = new List<Items.Line>();
        private List<Items.Area> _allAreas = new List<Items.Area>();


        public Form1()
        {
            InitializeComponent();

            Scale_img = 100;

            //Гидрография
            toolTip.SetToolTip(B_Lake, "Озеро");
            toolTip.SetToolTip(B_ImpossibleRiver, "Непреодалимая река");
            toolTip.SetToolTip(B_ImpossibleSwamp, "Нереодалимое болото");
            toolTip.SetToolTip(B_Waterlogging, "Заболоченоость");
            toolTip.SetToolTip(B_Swamp, "Болото");
            toolTip.SetToolTip(B_River, "Ручей");
            toolTip.SetToolTip(B_DisappearRiver, "Исчезающий ручей");
            toolTip.SetToolTip(B_SlimSwamp, "Узкое болото");
            toolTip.SetToolTip(B_WaterLittlePit, "Водянная воронка");
            toolTip.SetToolTip(B_WaterPit, "Колодец");
            toolTip.SetToolTip(B_WaterSource, "Источник воды");
            toolTip.SetToolTip(B_WaterObject, "Особый объект гидрографии");


            //if (!File.Exists(@"database\GODB.db")) // если базы данных нету, то...
            //{
            //    SQLiteConnection.CreateFile(@"database\GODB.db"); // создать базу данных, по указанному пути содаётся пустой файл базы данных
            //}

            //using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB.db; Version=3;")) // в строке указывается к какой базе подключаемся
            //{
            //    // строка запроса, который надо будет выполнить
            //    string commandText = "CREATE TABLE IF NOT EXISTS [dbWay] ( " +
            //        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            //        "[way] TEXT) ";// создать таблицу, если её нет
            //    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
            //    Connect.Open(); // открыть соединение
            //    Command.ExecuteNonQuery(); // выполнить запрос

            //    // таблица точек
            //    commandText = "CREATE TABLE IF NOT EXISTS [dbPoints] ( " +
            //        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            //        "[x] INTEGER NOT NULL, [y] INTEGER NOT NULL, " +
            //        "[typeZone] TEXT NOT NULL, " +// зона карты
            //        "[typeSequencePoints] TEXT NOT NULL, " +
            //        "[id_tsp] INTEGER NOT NULL," +
            //        "[oldX] INTEGER NOT NULL, [oldY] INTEGER NOT NULL)";// создать таблицу, если её нет
            //    Command = new SQLiteCommand(commandText, Connect);
            //    Command.ExecuteNonQuery(); // выполнить запрос

            //    //таблица областей
            //    commandText = "CREATE TABLE IF NOT EXISTS [dbArea] ( " +
            //        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            //        "[areaPoints] TEXT NOT NULL, " +// множество точек
            //        "[typeZone] TEXT NOT NULL)";// зона карты
            //    Command = new SQLiteCommand(commandText, Connect);
            //    Command.ExecuteNonQuery(); // выполнить запрос

            //    //таблица линий
            //    commandText = "CREATE TABLE IF NOT EXISTS [dbLine] ( " +
            //        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            //        "[linePoints] TEXT NOT NULL, " +// множество точек
            //        "[typeZone] TEXT NOT NULL)";// зона карты
            //    Command = new SQLiteCommand(commandText, Connect);
            //    Command.ExecuteNonQuery(); // выполнить запрос

            //    //таблица обьектов
            //    commandText = "CREATE TABLE IF NOT EXISTS [dbObject] ( " +
            //        "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
            //        "[x] INTEGER NOT NULL, [y] INTEGER NOT NULL, " +
            //        "[typeZone] TEXT NOT NULL)";// зона карты
            //    Command = new SQLiteCommand(commandText, Connect);
            //    Command.ExecuteNonQuery(); // выполнить запрос

            //    //commandText = "SELECT * FROM [dbTableName] WHERE [way] NOT NULL";
            //    //Command = new SQLiteCommand(commandText, Connect);
            //    //SQLiteDataReader sqlReader = Command.ExecuteReader();
            //    //while (sqlReader.Read()) // считываем и вносим в лист все параметры
            //    //{
            //    //    Way_list.Add(sqlReader["way"].ToString());
            //    //}
            //    Connect.Close(); // закрыть соединение
            //}
            //for(int i = 1; i <= Way_list.Count; i++)//заполняем комбобокс всеми путями
            //{
            //    comboBox1.Items.Add("Путь" + i);
            //}

        }

        public PictureBox GetPictureBox
        {
            get
            {
                return pictureBox1;
            }
        }

        private void B_open_Click(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG;)|*.BMP;*.JPG;*.GIF;*.PNG;|All files (*.*)|*.*";
            OPF.RestoreDirectory = true;
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    img = new Bitmap(OPF.FileName);
                    pictureBox1.Width = img.Width;
                    pictureBox1.Height = img.Height;
                    pictureBox1.Image = img;
                    if(splitContainer1.Panel1.Width / 2 - img.Width / 2 < 0 || splitContainer1.Panel1.Height / 2 - img.Height / 2 < 0)
                        pictureBox1.Location = new Point(0,0);
                    else
                        pictureBox1.Location = (new Point(splitContainer1.Panel1.Width / 2 - img.Width / 2, splitContainer1.Panel1.Height / 2 - img.Height / 2));

                    B_plus.Enabled = true;
                    B_minus.Enabled = true;
                }
                catch
                {
                    MessageBox.Show("Невозможно открыть файл", "Ошибка", MessageBoxButtons.OK);
                }
            }
        }


        private void B_plus_Click(object sender, EventArgs e)
        {
            Scale_img += 10; // увеличиваем масштаб

            pictureBox1.Width = img.Width * Scale_img / 100;
            pictureBox1.Height = img.Height * Scale_img / 100;

            if (pictureBox1.Location.X - img.Width / 20 <= 0 || pictureBox1.Location.Y - img.Height / 20 <= 0)
                pictureBox1.Location = new Point(0,0);
            else
                pictureBox1.Location = new Point(pictureBox1.Location.X - img.Width / 20, pictureBox1.Location.Y - img.Height / 20);
            
            //pictureBox1.Controls.Clear();
            
            foreach (var item in _allItems)
            {
                //item.SetPosition(item.CurrentPoint);
                item.Scale(Scale_img);
            }

            label_scale.Text = Scale_img.ToString() + "%"; // сообщаем это пользователю

            //CreateTempPanel(new Point(0, 0), 6);

        }

        private void B_minus_Click(object sender, EventArgs e) //уменьшаем
        {
            
            if (Scale_img - 10 >= 100)
            {
                Scale_img -= 10;

                pictureBox1.Width = img.Width * Scale_img / 100;
                pictureBox1.Height = img.Height * Scale_img / 100;

                if (pictureBox1.Width >= splitContainer1.Panel1.Width || pictureBox1.Height >= splitContainer1.Panel1.Height)
                    pictureBox1.Location = new Point(0, 0);
                else
                    pictureBox1.Location = new Point(pictureBox1.Location.X + img.Width / 20, pictureBox1.Location.Y + img.Height / 20);


                //pictureBox1.Controls.Clear();

                foreach (var item in _allItems)
                {
                    //item.SetPosition(item.CurrentPoint);
                    item.Scale(Scale_img);
                }
            }

            label_scale.Text = Scale_img.ToString() + "%";

            //CreateTempPanel(new Point(0,0), 6);
        }

        //рисуем граф 
        private void B_draw_Click(object sender, EventArgs e)
        {
            //draw(allPoints_list, new Pen(Brushes.Purple, 1));
            Graphics g = pictureBox1.CreateGraphics();
            foreach (var item in _allItems)
            {
                //item.DrawCircle();
                foreach (ItemDistanceTo nearItem in item.NearItems)
                {
                    g.DrawLine(new Pen(Brushes.Purple, 1), item.CurrentPoint.X * Scale_img / 100, item.CurrentPoint.Y * Scale_img / 100,
                        nearItem.CurrentItem.CurrentPoint.X * Scale_img / 100, nearItem.CurrentItem.CurrentPoint.Y * Scale_img / 100);
                }
            }
        }

        //линия с корректными кординатами
        public void FixDrawLine(Pen pen, Point P1, Point P2)
        {
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawLine(pen, P1.X * Scale_img / 100, P1.Y * Scale_img / 100, P2.X * Scale_img / 100, P2.Y * Scale_img / 100);
        }

        public void FullArea(List<Item> list, Pen pen)
        {
            GraphicsPath path = new GraphicsPath();
            SolidBrush brush = new SolidBrush(pen.Color);
            Graphics g = pictureBox1.CreateGraphics();
            Point[] p = new Point[list.Count];
            for(int i = 0; i < list.Count; i++)
            {
                p[i] = list[i].CurrentPoint;
            }
            path.AddLines(p);

            g.FillPath(brush, path);
        }

        private void FragmentSequence(List<Item> items, TypeItem type, Point to)
        {
            Ray ray = new Ray(items.Last().CurrentPoint, to);
            if (ray.Lenght() > 20)
            {
                Point betweenPoint;
                Item betweenItem;
                TypeItem generalType = type.IsImpossible ? _typeItem : null;
                while (ray.Lenght() > 20)
                {
                    betweenPoint = new Point(items.Last().CurrentPoint.X + (int)((ray.Coordinate.X / ray.Lenght()) * 20),
                        items.Last().CurrentPoint.Y + (int)((ray.Coordinate.Y / ray.Lenght()) * 20));
                    betweenItem = new Item(_allItems, this, betweenPoint, type, generalType);
                    betweenItem.Previous = items.Last();
                    items.Last().Next = betweenItem;
                    items.Add(betweenItem);
                    _allItems.Add(betweenItem);
                    type.Sequence.SetItems(items);
                    ray = new Ray(items.Last().CurrentPoint, to);
                }
            }
        }
        private void ReplaceItem()
        {
            _item = _tempItems[_tempItems.Count - 2];
            pictureBox1.Controls.Remove(_item.NewPanel);
            panel_list.Remove(_item.NewPanel);

            _item = new ImpossibleItem(_allItems, this, _typeItem, _typeItemInner, _typeItemOuter);
            ImpossibleItem item = _item as ImpossibleItem;
            _allItems.Add(item.itemInner);
            _allItems.Add(item.itemOuter);

            if (!(_typeItem.Sequence is Area && _tempItems.Count == 3))
            {

                FragmentSequence(_tempItemsInner, _typeItemInner, item.itemInner.CurrentPoint);
                FragmentSequence(_tempItemsOuter, _typeItemOuter, item.itemOuter.CurrentPoint);

                item.itemInner.Previous = _tempItemsInner.Last();
                _tempItemsInner.Last().Next = item.itemInner;

                item.itemOuter.Previous = _tempItemsOuter.Last();
                _tempItemsOuter.Last().Next = item.itemOuter;
            }
            _tempItemsInner.Add(item.itemInner);
            _tempItemsOuter.Add(item.itemOuter);

            _typeItemInner.Sequence.SetItems(_tempItemsInner);
            _typeItemOuter.Sequence.SetItems(_tempItemsOuter);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)//если есть картинка
            {
                if (Btn_create_P && _typeItem != null)//если была нажата кнопка создания координат привязки
                {
                    if (e.Button == MouseButtons.Right && _tempItems.Count != 0)
                    {
                        if (_typeItem.Sequence is Items.Line)
                        {
                            if (_typeItem.IsImpossible)
                            {
                                FragmentSequence(_tempItemsInner, _typeItemInner, _tempItems.Last().CurrentPoint);
                                FragmentSequence(_tempItemsOuter, _typeItemOuter, _tempItems.Last().CurrentPoint);

                                _tempItems[_tempItems.Count - 1] = new Item(_allItems, this, _tempItems.Last().CurrentPoint, _typeItem, _typeItem);

                                _tempItemsInner.Add(_tempItems.Last());
                                _tempItemsOuter.Add(_tempItems.Last());

                                _typeItemInner.Sequence.SetItems(_tempItemsInner);
                                _typeItemOuter.Sequence.SetItems(_tempItemsOuter);

                                _allItems.Add(_tempItems.Last());
                            }
                            _allLines.Add(_typeItem.Sequence as Items.Line);
                        }
                        else if (_typeItem.Sequence is Items.Area)
                        {
                            if (_typeItem.IsImpossible)
                            {
                                _tempItems.Add(_tempItems.First());
                                _typeItem.Sequence.SetItems(_tempItems);
                                ReplaceItem();

                                _tempItems.Add(_tempItems[1]);
                                _typeItem.Sequence.SetItems(_tempItems);
                                ReplaceItem();

                                FragmentSequence(_tempItemsInner, _typeItemInner, _tempItemsInner.First().CurrentPoint);
                                FragmentSequence(_tempItemsOuter, _typeItemOuter, _tempItemsOuter.First().CurrentPoint);

                                _tempItemsInner.Last().Next = _tempItemsInner.First();
                                _tempItemsInner.First().Previous = _tempItemsInner.Last();
                                _tempItemsOuter.Last().Next = _tempItemsOuter.First();
                                _tempItemsOuter.First().Previous = _tempItemsOuter.Last();

                                _typeItemInner.Sequence.SetItems(_tempItemsInner);
                                _typeItemOuter.Sequence.SetItems(_tempItemsOuter);

                                _allAreas.Add(_typeItemInner.Sequence as Area);
                                _allAreas.Add(_typeItemOuter.Sequence as Area);
                            }
                            else
                            {
                                FragmentSequence(_tempItems, _typeItem, _tempItems.First().CurrentPoint);

                                _tempItems.Last().Next = _tempItems.First();
                                _tempItems.First().Previous = _tempItems.Last();
                                _typeItem.Sequence.SetItems(_tempItems);

                                _allAreas.Add(_typeItem.Sequence as Items.Area);
                            }

                        }
                        _tempItems.Clear();
                        _tempItemsInner.Clear();
                        _tempItemsOuter.Clear();

                        _typeItem = _typeItem.GetCopy();
                        _typeItemInner = _typeItem.GetCopy();
                        _typeItemInner = _typeItem.GetCopy();
                        //DrawConnections(_tempItems, new Pen(Brushes.Purple, 1));
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        Point localPoint = new Point(e.Location.X * 100 / Scale_img, e.Location.Y * 100 / Scale_img);
                        //_item = new Item(_allItems, this, localPoint, _typeItem);
                        //_allItems.Add(_item);

                        if (_typeItem.Sequence is Items.Single)
                        {
                            //_item = new Item(localPoint, this, _typeItem);
                            //_tempItems.Add(_item);
                            //_typeItem.Sequence.SetItems(_tempItems);

                            _item = new Item(_allItems, this, localPoint, _typeItem);
                            _tempItems.Add(_item);
                            _typeItem.Sequence.SetItems(_tempItems);
                            _allSingles.Add(_typeItem.Sequence as Items.Single);
                            _allItems.Add(_item);

                            //DrawConnections(_tempItems, new Pen(Brushes.Purple, 1));
                            _tempItems.Clear();

                            _typeItem = _typeItem.GetCopy();
                        }
                        else
                        {
                            if (_tempItems.Count != 0)
                            {
                                if (_typeItem.IsImpossible)
                                {
                                    //_item = new Item(_allItems, this, localPoint, _typeItem, _typeItem);
                                    _item = new Item(localPoint, this, _typeItem);
                                    _tempItems.Add(_item);
                                    _typeItem.Sequence.SetItems(_tempItems);

                                    if (_tempItems.Count >= 3)
                                    {
                                        ReplaceItem();
                                    }
                                }
                                else
                                {
                                    _item = new Item(_allItems, this, localPoint, _typeItem);
                                    // Добавляем промежуточные точки 
                                    FragmentSequence(_tempItems, _typeItem, _item.CurrentPoint);

                                    _allItems.Add(_item);

                                    _item.Previous = _tempItems.Last();
                                    _tempItems.Last().Next = _item;

                                    _tempItems.Add(_item);
                                    _typeItem.Sequence.SetItems(_tempItems);

                                }
                            }
                            else
                            {
                                _item = new Item(_allItems, this, localPoint, _typeItem);
                                    
                                _tempItems.Add(_item);
                                _typeItem.Sequence.SetItems(_tempItems);

                                if (_typeItem.IsImpossible)
                                {
                                    if (_typeItem.Sequence is Line)
                                    {
                                        _allItems.Add(_item);
                                        _tempItemsInner.Add(_item);
                                        _tempItemsOuter.Add(_item);

                                        _typeItemInner.Sequence.SetItems(_tempItemsInner);
                                        _typeItemOuter.Sequence.SetItems(_tempItemsOuter);
                                    }
                                }
                                else
                                    _allItems.Add(_item);
                            }

                            //DrawConnections(_tempItems, new Pen(Brushes.Purple, 1));
                        }

                        label_Way.Text = _item.CurrentPoint + "|id = " + _item.ID;
                    }

                }

            }
        }

        //using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB.db; Version=3;"))
        //{

        //    Connect.Open(); // открыть соединение
        //    string commandText = "SELECT * FROM [dbPoints]";
        //    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
        //    SQLiteDataReader sqlReader = Command.ExecuteReader();
        //    while (sqlReader.Read()) // считываем и вносим в лист все параметры
        //    {
        //        Graphics g = pictureBox1.CreateGraphics();
        //        g.DrawRectangle(new Pen(Brushes.Red, 4), int.Parse(sqlReader["x"].ToString()) * scale_img / 100, int.Parse(sqlReader["y"].ToString()) * scale_img / 100, scale_img / 100, scale_img / 100); // рисуем квадратик
        //        //label_Way.Text = (sqlReader["id"].ToString());
        //    }
        //    Connect.Close(); // закрыть соединение
        //    //label_Way.Text = Way_str;
        //}

        //переводим структуру в строку для записи в базу
        //private void Write_to_string_Point(MyPoint point)
        //{
        //    Way_str += (point.x * 100 / scale_img) + "," + (point.y * 100 / scale_img) + ",'" + typeZoneFlag //Array.IndexOf(Enum.GetValues(typeZoneFlag.GetType()), typeZoneFlag)
        //                    + "','" + typePointFlag + "'," + point.id + "|";//записываем координату в изначальном масштабе
        //}


       
        private void B_show_points_Click(object sender, EventArgs e)// рисуем конечный маршрут
        {
            Item start = item_from;
            Item end = item_to;
            Way minWay = new Way();
            minWay.FindWay(start, end, _typeItem);
            label_Way.Text = minWay.distance.ToString();
            foreach(Item_A_Star item in minWay.pointsOnWay)
            {
                label_Way.Text += "|" + item.item.ID;
            }
            DrawWay(minWay.P_A_StoSuperPoint(minWay.pointsOnWay), new Pen(Brushes.Red, 2));
         
        }
       

        //кнопки тула
        //создание точек
        private void B_createPoints_Click(object sender, EventArgs e)
        {
            if (Btn_create_P )
            {
                //using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB.db; Version=3;"))
                //{
                //    string commandText = "INSERT INTO [dbPoints] ([x], [y], [typeZone], [typeSequencePoints], [id_tsp], [oldX], [oldY]) VALUES ";//вставляем координаты 
                //    //string a = Way_str;
                //    //int index_1 = a.IndexOf(","); //ишем первый индекс запятой и палки)
                //    //int index_2 = a.IndexOf("|");

                //    //while (index_2 != -1)//проходимся по строке до конца,  это у нас |
                //    //{
                //    //    commandText += "(";
                //    //    while (index_1 < index_2 && index_1 != -1)
                //    //    {
                //    //        commandText += a.Substring(0, index_1);

                //    //        a = a.Remove(0, ++index_1);

                //    //        index_1 = a.IndexOf(",");
                //    //        index_2 = a.IndexOf("|");
                //    //        commandText += ",";
                //    //    }
                //    //    commandText += a.Substring(0, index_2) + ")";
                //    //    a = a.Remove(0, ++index_2);
                //    //    index_1 = a.IndexOf(",");
                //    //    index_2 = a.IndexOf("|");
                //    //    if (index_2 != -1)
                //    //        {
                //    //            commandText += ",";
                //    //        }
                //    //}
                //    //foreach (MyPoint temp in points_list)
                //    //{
                //    //    commandText += "(" + temp.x + "," + temp.y + ",'" + temp.Type_Zone_Point + "','" + temp.Type_Seq_Point//тупо но ниченго не придумал лучше
                //    //        + "'," + temp.id + "," + temp.oldX + "," + temp.oldY + ((temp.x == points_list[points_list.Count - 1].x && temp.y == points_list[points_list.Count - 1].y) ? ")" : "),");
                //    //}
                //    SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                //    Connect.Open();
                //    Command.ExecuteNonQuery();//выполняем запрос
                //    Connect.Close();
                //}
                //меняем цыет на изначальный

                //_zone.BackColor = Color.DarkTurquoise;
                //_seq.BackColor = Color.DarkTurquoise;


            }
           
            Btn_create_P = !Btn_create_P;//обратное значение
            //открываем и закрываем возможность нажатия на кнопки тула
            Tool_flora.Enabled = Btn_create_P;
            Tool_hydrography.Enabled = Btn_create_P;
            Tool_artificalObject.Enabled = Btn_create_P;
            Tool_landform.Enabled = Btn_create_P;
            Tool_stone.Enabled = Btn_create_P;

            Tool_area.Enabled = Btn_create_P;
            Tool_line.Enabled = Btn_create_P;
            Tool_item.Enabled = Btn_create_P;
            //ставим значение по умолчанию
            if (Btn_create_P)
            {
                //_zone.BackColor = Color.CadetBlue;
                //_seq.BackColor = Color.CadetBlue;

                //_typeItem = new Flora("S", 0);

                CreateTempPanel(new Point(0,0), 6);
            }
        }

        //---------------------
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem temp = (ToolStripMenuItem)sender;
            label_Way.Text = temp.Text;
        }

        private void CreateTempPanel(Point point, int sizePanel)
        {
            _panelTemp = new Panel();
            _panelTemp.Location = new Point(point.X - sizePanel / 2, point.Y - sizePanel / 2);
            _panelTemp.Size = new Size(sizePanel, sizePanel);
            _panelTemp.BackgroundImage = Properties.Resources.Point;
            _panelTemp.BackgroundImageLayout = ImageLayout.Zoom;
            _panelTemp.Enabled = false;

            pictureBox1.Controls.Add(_panelTemp);
        }
        //------------------
        public void anyPanel_Click(object sender, EventArgs e)
        {
            Panel tempPanel = (Panel)sender;
            label_Way.Text = tempPanel.Name + "--";
            Item existItem = _allItems.Find(x => x.ID == int.Parse(tempPanel.Name));

            foreach (var item in existItem.Types)
            {
                label_Way.Text += " " + item.Name + ",";
            }

            if (Btn_create_P)
            {
                // пока сделаю без связей на соседние до и после
                _tempItems.Add(existItem);
                _typeItem.Sequence.SetItems(_tempItems);
                existItem.Types.Add(_typeItem);

                if (_typeItem.Sequence is Items.Single)
                {
                    _allSingles.Add(_typeItem.Sequence as Items.Single);
                    _tempItems.Clear();

                    //_typeItem.Sequence = new Items.Single(this);
                    _typeItem = _typeItem.GetCopy();
                }
            }
            else
            {
                if (Btn_item_from)
                {
                    if (item_from != null)
                        panel_list.Find(x => x.Name == item_to.ID.ToString()).BackgroundImage = Properties.Resources.Point;
                    item_from = _allItems.Find(x => x.ID == int.Parse(tempPanel.Name));//_allItems[int.Parse(tempPanel.Name)];
                    B_from.Text = item_from.ID.ToString();
                    toolB_start.Text = item_from.ID.ToString();

                    Btn_item_from = !Btn_item_from;
                    tempPanel.BackgroundImage = Properties.Resources.EnabledPoint;
                }
                if (Btn_item_to)
                {
                    if (item_to != null)
                        panel_list.Find(x => x.Name == item_to.ID.ToString()).BackgroundImage = Properties.Resources.Point;

                    item_to = _allItems.Find(x => x.ID == int.Parse(tempPanel.Name)); //_allItems[int.Parse(tempPanel.Name)];
                    B_to.Text = item_to.ID.ToString();
                    toolB_finish.Text = item_to.ID.ToString();

                    Btn_item_to = !Btn_item_to;
                    tempPanel.BackgroundImage = Properties.Resources.EnabledPoint;
                }
            }
        }

        public void anyPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if(Btn_create_P)
            {

            }
        }
        private void FragmentTempSequence(Point to)
        {
            Ray ray = new Ray(_tempItems.Last().CurrentPoint, to);
            if (ray.Lenght() > 20)
            {
                Point betweenPoint;
                List<Point> tempPoints = new List<Point> { _tempItems.Last().CurrentPoint };
                while (ray.Lenght() > 20)
                {
                    betweenPoint = new Point(tempPoints.Last().X + (int)((ray.Coordinate.X / ray.Lenght()) * 20),
                        tempPoints.Last().Y + (int)((ray.Coordinate.Y / ray.Lenght()) * 20));
                    CreateTempPanel(betweenPoint, 6);
                    tempPoints.Add(betweenPoint);
                    
                    ray = new Ray(_tempItems.Last().CurrentPoint, to);
                }
            }
        }

        private void ChangeBackColors(Button button_type, ToolStripLabel tsl_zone, ToolStripLabel tsl_seq)
        {
            if (_type_B != null && _zone != null && _seq != null)
            {
                _type_B.BackColor = Color.Azure;
                _zone.BackgroundImage = null;
                _seq.BackgroundImage = null;
            }

            _type_B = button_type;
            _type_B.BackColor = Color.CadetBlue;

            _zone = tsl_zone;
            _zone.BackgroundImage = Properties.Resources.Point;

            _seq = tsl_seq;
            _seq.BackgroundImage = Properties.Resources.Point;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if(pictureBox1.Image != null && Btn_create_P)
            {
                _panelTemp.Location = new Point(e.X - _panelTemp.Width / 2, e.Y - _panelTemp.Height / 2);
            }
            //if(!(_sequence is Items.Single))
            //{

            //}
            //if (pictureBox1.Image != null)
            //{
            //    if (isPressed)
            //    {

            //        if (e.X < pictureBox1.Width & e.X > 0 & e.Y > 0 & e.Y < pictureBox1.Height)
            //        {
            //            PrevPoint = CurrentPoint;
            //            CurrentPoint = e.Location;
            //            Graphics g = pictureBox1.CreateGraphics();
            //            g.DrawLine(new Pen(Brushes.Red, 3), PrevPoint, CurrentPoint);

            //            //img.SetPixel(e.X, e.Y, Color.Red);
            //            label_Way.Text = e.X + " ; " + e.Y;

            //        }
            //    }
            //}
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
           // isPressed = false;
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            //isPressed = true; 
            //CurrentPoint = e.Location;
        }
        private void B_from_Click(object sender, EventArgs e)
        {
            Btn_item_from = !Btn_item_from;
        }

        private void B_to_Click(object sender, EventArgs e)
        {
            Btn_item_to = !Btn_item_to;
        }
        /// <summary>
        /// Отсюда обрабатываются нажатия на кнопки с типами 
        /// </summary>
        private void B_Lake_Click(object sender, EventArgs e)
        {
            _typeItem = new Lake();
            _typeItemInner = _typeItem.GetCopy();
            _typeItemOuter = _typeItem.GetCopy();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_area);    
        }

        private void B_ImpossibleRiver_Click(object sender, EventArgs e)
        {
            _typeItem = new ImpassableRiver();
            _typeItemInner = _typeItem.GetCopy();
            _typeItemOuter = _typeItem.GetCopy();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_line);
        }

        private void B_River_Click(object sender, EventArgs e)
        {
            _typeItem = new River();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_line);
        }

        private void B_ImpossibleSwamp_Click(object sender, EventArgs e)
        {
            _typeItem = new ImpassableSwamp();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_area);
        }

        private void B_Waterlogging_Click(object sender, EventArgs e)
        {
            _typeItem = new Waterlogging();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_area);
        }

        private void B_DisappearRiver_Click(object sender, EventArgs e)
        {
            _typeItem = new DisappearRiver();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_line);
        }

        private void B_SlimSwamp_Click(object sender, EventArgs e)
        {
            _typeItem = new SlimSwamp();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_line);
        }

        private void B_WaterLittlePit_Click(object sender, EventArgs e)
        {
            _typeItem = new WaterLittlePit();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_item);
        }

        private void B_WaterPit_Click(object sender, EventArgs e)
        {
            _typeItem = new WaterPit();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_item);
        }

        private void B_WaterSource_Click(object sender, EventArgs e)
        {
            _typeItem = new WaterSource();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_item);
        }

        private void B_WaterObject_Click(object sender, EventArgs e)
        {
            _typeItem = new WaterObject();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_item);
        }

        private void B_ClaenForest_Click(object sender, EventArgs e)
        {

        }

        private void B_Swamp_Click(object sender, EventArgs e)
        {
            _typeItem = new Swamp();
            ChangeBackColors((Button)sender, Tool_hydrography, Tool_area);
        }


        public void DrawWay(List<Item> list, Pen pen)
        {
            if (list.Count == 0)
                return;
            Item temp = list[0];
            for (int i = 1; i < list.Count; i++)
            {
                FixDrawLine(pen, temp.CurrentPoint, list[i].CurrentPoint);
                temp = list[i];
            }
        }

        public void DrawConnections(List<Item> list, Pen pen)
        {
            foreach(var item in list)
            {
                foreach(var nearItem in item.NearItems)
                {
                    FixDrawLine(pen, item.CurrentPoint, nearItem.CurrentItem.CurrentPoint);
                }
            }
        }
    }
}
