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
        Bitmap img;//наша катринка
        public List<Panel> panel_list = new List<Panel> { };

      
       // List<string> Way_list = new List<string> { }; //все пути

        public int Scale_img { get; private set; } // масштаб
        int overcome = 100;
        bool Btn_create_P = false;// флаг на нажатие кнопки создания массива координат

        ToolStripButton B_zone, B_seq;

        ListView listSubTypes;



        private TypeItem _typeItem;
        private Items.Sequence _sequence;
        private Item _item;

        private List<Item> _allItems = new List<Item>();
        private List<Item> _tempItems = new List<Item>();
        private List<Items.Single> _allSingles = new List<Items.Single>();
        private List<Items.Line> _allLines = new List<Items.Line>();
        private List<Items.Area> _allAreas = new List<Items.Area>();


        public Form1()
        {
            InitializeComponent();

            listSubTypes = ListSubTypes;
            B_zone = Tool_B_create_flora;
            B_seq = Tool_B_item;
            comboBox1.SelectedItem = 0;

            Scale_img = 100;
            _typeItem = new Flora("F", 0);
            _sequence = new Items.Single(this, _typeItem);
           
            //----------------------------
            ToolStripMenuItem toolTemp;
            for (int i = 0; i < 25; i++)
            {
                toolTemp = new ToolStripMenuItem();
                toolTemp.BackgroundImageLayout = ImageLayout.None;
                toolTemp.Image = Properties.Resources.building;
                toolTemp.ImageAlign = ContentAlignment.MiddleLeft;
                toolTemp.ImageScaling = ToolStripItemImageScaling.None;
                toolTemp.Name = "TS" + i;
                toolTemp.Size = new Size(230, 28);
                toolTemp.Text = "TS" + i;
                toolTemp.Click += new EventHandler(toolStripMenuItem_Click);

                tool_B_subtype.DropDownItems.Add(toolTemp);
            }

            //ListSubTypes.SmallImageList.Images.Add(Bitmap.FromFile(@"D:\Visual PROJECTS\Go\icons\garden.jpg"));
            ListViewItem itemSubTypes;
            for (int i = 0; i < 85; i++)
            {
                itemSubTypes = new ListViewItem();
                itemSubTypes.Text = "tata";
                itemSubTypes.ImageIndex = i/10;
                

                listSubTypes.Items.Add(itemSubTypes);
            }

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
            
            pictureBox1.Controls.Clear();
            
            foreach (var item in _allItems)
            {
                item.SetPosition(item.CurrentPoint);
            }
            //foreach (var item in _allAreas)
            //{
            //    item.CreatePanel();
            //}
            //foreach (var item in _allLines)
            //{
            //    item.CreatePanel();
            //}

            label_scale.Text = Scale_img.ToString() + "%"; // сообщаем это пользователю

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


                pictureBox1.Controls.Clear();

                foreach (var item in _allItems)
                {
                    item.SetPosition(item.CurrentPoint);
                }
                //foreach (var item in _allAreas)
                //{
                //    item.CreatePanel();
                //}
                //foreach (var item in _allLines)
                //{
                //    item.CreatePanel();
                //}
            }

            label_scale.Text = Scale_img.ToString() + "%";
        }

        //рисуем граф 
        private void B_draw_Click(object sender, EventArgs e)
        {
            //draw(allPoints_list, new Pen(Brushes.Purple, 1));
            Graphics g = pictureBox1.CreateGraphics();
            foreach (var item in _allItems)
            {
                item.FixNearItems();
                foreach (ItemDistanceTo nearItem in item.NearItems)
                {
                    g.DrawLine(new Pen(Brushes.Purple, 1), item.CurrentPoint.X * Scale_img / 100, item.CurrentPoint.Y * Scale_img / 100,
                        nearItem.CurrentItem.CurrentPoint.X * Scale_img / 100, nearItem.CurrentItem.CurrentPoint.Y * Scale_img / 100);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //label_Way.Text = Way_list[comboBox1.SelectedIndex];//выводим выбранный путь из комбобокса
            //string a = Way_list[comboBox1.SelectedIndex];//заносим выбранный путь в новую строку для удобства пользования
            //int index_1 = a.IndexOf(","); //ишем первый индекс запятой и палки)
            //int index_2 = a.IndexOf("|");
            ////получаем из строки первую координату путем обрезания строки до палки и приводим ее кнужному масштабу
            //Point oldItem = new Point((int)(int.Parse(a.Substring(0, index_1)) * scale_img / 100), (int)(int.Parse(a.Substring(++index_1, index_2 - index_1)) * scale_img / 100));
            //while (index_2 != a.Length - 1)//проходимся по строке до конца,  это у нас |
            //{
            //    a = a.Remove(0, ++index_2);//обрезаем строку до палки включая ее
            //    //ищем новые индексы в ноавой строке
            //    index_1 = a.IndexOf(",");
            //    index_2 = a.IndexOf("|");
            //    //достаем следующую координату таким же способом
            //    Point item = new Point(int.Parse(a.Substring(0, index_1)) * scale_img / 100, int.Parse(a.Substring(++index_1, index_2 - index_1)) * scale_img / 100);
            //    //рисуем линию между координатами oldItem и item
            //    Graphics g = pictureBox1.CreateGraphics();
            //    g.DrawLine(new Pen(Brushes.Red, 4), oldItem, item);
            //    oldItem = item;//двигаем oldItem на следующую координату
            //}
        }//получить координаты!!!!

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
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (pictureBox1.Image != null)//если есть картинка
            {
                if (Btn_create_P)//если была нажата кнопка создания координат привязки
                {
                    //overcome = int.Parse(comboBox1.SelectedItem.ToString());
                    //выбираем цвет ручки
                    Pen currentPen = new Pen(_typeItem.Color,3);

                    if(e.Button == MouseButtons.Right && _tempItems.Count != 0 )
                    {
                        if(_sequence is Items.Line)
                        {
                            //Items.Line line = _sequence as Items.Line;
                            _allLines.Add(new Items.Line(this, _tempItems, _typeItem));

                            //line.Draw(pictureBox1, Scale_img);
                        }
                        else if(_sequence is Items.Area)
                        {
                            Ray ray = new Ray(_tempItems.Last().CurrentPoint, _tempItems.First().CurrentPoint);
                            if (ray.Lenght() > 20)
                            {
                                Point betweenPoint;
                                Item betweenItem;
                                while (ray.Lenght() > 20)
                                {
                                    betweenPoint = new Point(_tempItems.Last().CurrentPoint.X + (int)((ray.Coordinate.X / ray.Lenght()) * 20),
                                        _tempItems.Last().CurrentPoint.Y + (int)((ray.Coordinate.Y / ray.Lenght()) * 20));
                                    betweenItem = new Item(_allItems, this, betweenPoint, _typeItem, _sequence);
                                    betweenItem.Previous = _tempItems.Last();
                                    _tempItems.Last().Next = betweenItem;
                                    _tempItems.Add(betweenItem);
                                    _allItems.Add(betweenItem);
                                    ray = new Ray(_tempItems.Last().CurrentPoint, _tempItems.First().CurrentPoint);
                                }
                            }

                            _tempItems.Last().Next = _tempItems.First();
                            _tempItems.First().Previous = _tempItems.Last();
                            //Items.Area area = _sequence as Items.Area;
                            _allAreas.Add(new Items.Area(this, _tempItems, _typeItem));
                        }
                        //_sequence.SetItems(_tempItems);
                       // _sequence = _sequence.GetCopy();
                        _tempItems.Clear();
                    }
                    else if (e.Button == MouseButtons.Left)
                    {
                        //SuperPoint superPoint = null;
                        Point localPoint = new Point(e.Location.X * 100 / Scale_img, e.Location.Y * 100 / Scale_img);
                        _item = new Item(_allItems, this, localPoint, _typeItem, _sequence);
                        //superPoint = new SuperPoint(this, localPoint, type_zone, type_seq, overcome);
                        //_item.SetPosition(localPoint);

                        _allItems.Add(_item);


                        if (_sequence is Items.Single)
                        {
                            _tempItems.Add(_item);
                            //_sequence.SetItems(_tempItems);
                            //_sequence = _sequence.GetCopy();
                            _allSingles.Add(new Items.Single(this, _tempItems, _typeItem));
                            _tempItems.Clear();
                        }
                        else
                        {
                           
                            if (_tempItems.Count != 0)
                            {
                                // Добавляем промежуточные точки 
                                Ray ray = new Ray(_tempItems.Last().CurrentPoint, localPoint);
                                if (ray.Lenght() > 20)
                                {
                                    Point betweenPoint;
                                    Item betweenItem;
                                    while (ray.Lenght() > 20)
                                    {
                                        betweenPoint = new Point(_tempItems.Last().CurrentPoint.X + (int)((ray.Coordinate.X / ray.Lenght()) * 20 ),
                                            _tempItems.Last().CurrentPoint.Y + (int)((ray.Coordinate.Y / ray.Lenght()) * 20));
                                        betweenItem = new Item(_allItems, this, betweenPoint, _typeItem, _sequence);
                                        betweenItem.Previous = _tempItems.Last();
                                        _tempItems.Last().Next = betweenItem;
                                        _tempItems.Add(betweenItem);
                                        _allItems.Add(betweenItem);
                                        ray = new Ray(_tempItems.Last().CurrentPoint, localPoint);
                                    }
                                }
                                
                                _item.Previous = _tempItems.Last();
                                _tempItems.Last().Next = _item;
                            }
                            _tempItems.Add(_item);
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
            Item start = _allItems[0];
            Item end = _allItems.Last();
            Way minWay = new Way();
            minWay.FindWay(start, end);
            label_Way.Text = minWay.distance.ToString();
            foreach(Point_A_Star item in minWay.pointOnWay)
            {
                label_Way.Text += "|" + item.starPoint.ID;
            }
            draw(minWay.P_A_StoSuperPoint(minWay.pointOnWay), new Pen(Brushes.Red, 2));
         
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

                B_zone.BackColor = Color.DarkTurquoise;
                B_seq.BackColor = Color.DarkTurquoise;


            }
           
            Btn_create_P = !Btn_create_P;//обратное значение
            //открываем и закрываем возможность нажатия на кнопки тула
            Tool_B_create_flora.Enabled = Btn_create_P;
            Tool_B_create_hydrography.Enabled = Btn_create_P;
            Tool_B_create_artificalObject.Enabled = Btn_create_P;
            Tool_B_create_landform.Enabled = Btn_create_P;
            Tool_B_create_stone.Enabled = Btn_create_P;

            Tool_B_area.Enabled = Btn_create_P;
            Tool_B_line.Enabled = Btn_create_P;
            Tool_B_item.Enabled = Btn_create_P;
            //ставим значение по умолчанию
            if (Btn_create_P)
            {
                B_zone.BackColor = Color.CadetBlue;
                B_seq.BackColor = Color.CadetBlue;

                _typeItem = new Flora("H", 0);
               // currentPanel.Visible = true;
               // currentPanel.Parent = pictureBox1;
            }
        }



        //гидрография
        private void Tool_B_create_hydrography_Click(object sender, EventArgs e)
        {
            B_zone.BackColor = Color.DarkTurquoise;
            B_zone = Tool_B_create_hydrography;
            B_zone.BackColor = Color.CadetBlue;

            _typeItem = new Hydrography("H",0);
            _sequence.SetType(_typeItem);
        }
        //растительность
        private void Tool_B_create_flora_Click(object sender, EventArgs e)
        {
          
            B_zone.BackColor = Color.DarkTurquoise;
            B_zone = Tool_B_create_flora;
            B_zone.BackColor = Color.CadetBlue;

            _typeItem = new Flora("H",0);
            _sequence.SetType(_typeItem);
        }
        //искусственные обьекты
        private void Tool_B_create_artificalObject_Click(object sender, EventArgs e)
        {
         
            B_zone.BackColor = Color.DarkTurquoise;
            B_zone = Tool_B_create_artificalObject;
            B_zone.BackColor = Color.CadetBlue;

            _typeItem = new ArtificalObject("H",0);
            _sequence.SetType(_typeItem);
        }
        //рельеф
        private void Tool_B_create_landform_Click(object sender, EventArgs e)
        {
          
            B_zone.BackColor = Color.DarkTurquoise;
            B_zone = Tool_B_create_landform;
            B_zone.BackColor = Color.CadetBlue;

            _typeItem = new Landform("H",0);
            _sequence.SetType(_typeItem);
        }
        //камни(скалы)
        private void Tool_B_create_stone_Click(object sender, EventArgs e)
        {
           
            B_zone.BackColor = Color.DarkTurquoise;
            B_zone = Tool_B_create_stone;
            B_zone.BackColor = Color.CadetBlue;

            _typeItem = new Stone("H",0);
            _sequence.SetType(_typeItem);
        }
        
        
        //область
        private void Tool_B_area_Click(object sender, EventArgs e)
        {

            B_seq.BackColor = Color.DarkTurquoise;
            B_seq = Tool_B_area;
            B_seq.BackColor = Color.CadetBlue;

            _sequence = new Items.Area();
        }
        //линия
        private void Tool_B_line_Click(object sender, EventArgs e)
        {
            
            B_seq.BackColor = Color.DarkTurquoise;
            B_seq = Tool_B_line;
            B_seq.BackColor = Color.CadetBlue;

            _sequence = new Items.Line();
        }
        //обьект
        private void Tool_B_item_Click(object sender, EventArgs e)
        {
          
            B_seq.BackColor = Color.DarkTurquoise;
            B_seq = Tool_B_item;
            B_seq.BackColor = Color.CadetBlue;

            _sequence = new Items.Single();
        }
     

        //---------------------
        private void toolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem temp = (ToolStripMenuItem)sender;
            label_Way.Text = temp.Text;
        }


        //------------------
        public void anyPanel_Click(object sender, EventArgs e)
        {
            Panel tempPanel = (Panel)sender;
            label_Way.Text = tempPanel.Name + "--" + _allItems[int.Parse(tempPanel.Name)].ID;
            
            //int id = int.Parse(tempPanel.Name.Substring(12));
            //SuperPoint tempSP =  SuperPoint.findSuperPoint(id);
            //if(tempSP.type_seq == 2)
            //{
            //    tempSP = (Area)tempSP;
                
            //}
        }

        //рисовать кривую 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
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

        public void draw(List<Item> list, Pen pen)
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

    }
}
