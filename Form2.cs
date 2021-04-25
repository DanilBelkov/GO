using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Go
{
    enum type_zone1 { flora, hydrography, artificalObject, landform, stone };
    enum type_sequence_points1 { area, line, item };

    struct MyPoint1
    {
        public int x, y;
        public type_zone1 Type_Zone_Point; // типп знака карты
        //public type_sequence_points Type_Seq_Point;
        public type_sequence_points1 Type_Seq_Point;
        public int id;
        public int oldX, oldY;
    }

    public partial class Form2 : Form
    {
        List<MyPoint1> massP = new List<MyPoint1>();
       // MyPoint1[] massP = new MyPoint1[1000];
        string str;
        public Form2()
        {
            InitializeComponent();

            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB1.db; Version=3;")) // в строке указывается к какой базе подключаемся
            {
                // строка запроса, который надо будет выполнить
                string commandText = "CREATE TABLE IF NOT EXISTS [dbWay] ( " +
                    "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "[way] TEXT) ";// создать таблицу, если её нет
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open(); // открыть соединение
                Command.ExecuteNonQuery(); // выполнить запрос

                // таблица точек
                commandText = "CREATE TABLE IF NOT EXISTS [dbPoints] ( " +
                    "[id] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, " +
                    "[x] INTEGER NOT NULL, [y] INTEGER NOT NULL, " +
                    "[typeZone] TEXT NOT NULL, " +// зона карты
                    "[typeSequencePoints] TEXT NOT NULL, " +
                    "[id_tsp] INTEGER NOT NULL," +
                    "[oldX] INTEGER NOT NULL, [oldY] INTEGER NOT NULL)";// создать таблицу, если её нет
                Command = new SQLiteCommand(commandText, Connect);
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB1.db; Version=3;"))
            {
                string commandText = "INSERT INTO [dbPoints] ([x], [y], [typeZone], [typeSequencePoints], [id_tsp], [oldX], [oldY]) VALUES ";//вставляем координаты 
                int index_1 = str.IndexOf(","); //ишем первый индекс запятой и палки)
                int index_2 = str.IndexOf("|");

                while (index_2 != -1)//проходимся по строке до конца,  это у нас |
                {
                    commandText += "(";
                    while (index_1 < index_2 && index_1 != -1)
                    {
                        commandText += str.Substring(0, index_1);

                        str = str.Remove(0, ++index_1);

                        index_1 = str.IndexOf(",");
                        index_2 = str.IndexOf("|");
                        commandText += ",";
                    }
                    commandText += str.Substring(0, index_2) + ")";
                    str = str.Remove(0, ++index_2);
                    index_1 = str.IndexOf(",");
                    index_2 = str.IndexOf("|");
                    if (index_2 != -1)
                    {
                        commandText += ",";
                    }
                }
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery();//выполняем запрос
                Connect.Close();
            }
            watch.Stop();
            label1.Text = watch.ElapsedMilliseconds + " ms|||";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            using (SQLiteConnection Connect = new SQLiteConnection(@"Data Source=database\GODB1.db; Version=3;"))
            {
                string commandText = "INSERT INTO [dbPoints] ([x], [y], [typeZone], [typeSequencePoints], [id_tsp], [oldX], [oldY]) VALUES ";//вставляем координаты
                foreach (MyPoint1 temp in massP)
                {
                    commandText += "(" + temp.x + "," + temp.y + ",'" + temp.Type_Zone_Point + "','" + temp.Type_Seq_Point
                        + "'," + temp.id + "," + temp.oldX + "," + temp.oldY + ((temp.id == massP[massP.Count-1].id) ? ")": "),");
                }
                SQLiteCommand Command = new SQLiteCommand(commandText, Connect);
                Connect.Open();
                Command.ExecuteNonQuery(); // выполнить запрос
                Connect.Close();
            }
            watch.Stop();
            label2.Text = watch.ElapsedMilliseconds + " ms|||" + massP.Capacity;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Ox, Oy;
            Ox = Oy = -1;
            for(int i = 0; i< 1000; i++)
            {
                str += i + "," + (i+1) + ",'" + type_zone1.flora //Array.IndexOf(Enum.GetValues(typeZoneFlag.GetType()), typeZoneFlag)
                            + "','" + type_sequence_points1.item + "'," + (i-1) + "," + Ox + "," + Oy + "|";
                MyPoint1 temp = new MyPoint1
                {
                    x = i,
                    y = i + 1,
                    Type_Zone_Point = type_zone1.flora,
                    Type_Seq_Point = type_sequence_points1.item,
                    id = i - 1,
                    oldX = Ox,
                    oldY = Oy
                };
                massP.Add(temp);
                Ox = i; Oy = i + 1;

            }
        }
    }
}
