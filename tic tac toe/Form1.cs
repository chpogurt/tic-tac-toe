using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    struct Point
    {
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int x;
        public int y;
    }

    public partial class Game : Form//game logic part
    {
        public Game()
        {
            InitializeComponent();
            Field[0, 0] = button1;
            Field[0, 1] = button2;
            Field[0, 2] = button3;
            Field[1, 0] = button4;
            Field[1, 1] = button5;
            Field[1, 2] = button6;
            Field[2, 0] = button7;
            Field[2, 1] = button8;
            Field[2, 2] = button9;
        }

        private bool Mode = false;

        private bool PlayerMove = true;

        private string PlayerS = "X";

        private string PC = "O";

        private readonly Button[,] Field = new Button[3, 3];

        private readonly List<List<Point>> Points = new List<List<Point>>
        {
            new List<Point>
            {
                new Point(2, 0), new Point(1, 1), new Point(0, 2)
            },
            new List<Point>
            {
                new Point(0, 0), new Point(1, 1), new Point(2, 2)
            },
            new List<Point>
            {
                new Point(0, 0), new Point(0, 1), new Point(0, 2)
            },
            new List<Point>
            {
                new Point(1, 0), new Point(1, 1), new Point(1, 2)
            },
            new List<Point>
            {
                new Point(2, 0), new Point(2, 1), new Point(2, 2)
            },
            new List<Point>
            {
                new Point(0, 0), new Point(1, 0), new Point(2, 0)
            },
            new List<Point>
            {
                new Point(0, 1), new Point(1, 1), new Point(2, 1)
            },
            new List<Point>
            {
                new Point(0, 2), new Point(1, 2), new Point(2, 2)
            }
        };

        private void Search()
        {
            var variable = Finder(Field);
            int point = new Random().Next(variable.Count);
            if (variable.Count > 0)
                Field[variable[point].x, variable[point].y].PerformClick();
            else
            {
                var heap = Algorithm(Field);
                List<Point> Hard = new List<Point>();
                List<Point> Medium = new List<Point>();
                List<Point> Easy = new List<Point>();
                foreach (var item in heap)
                {
                    if (item.Value == 'A')
                        Hard.Add(item.Key);
                    else
                        Easy.Add(item.Key);
                    Medium.Add(item.Key);
                }
                if (Mode)//Hard
                {
                    if (Hard.Count > 0)
                    {
                        Point hard = Hard[new Random().Next(Hard.Count)];
                        Field[hard.x, hard.y].PerformClick();
                    }
                    else
                        RandomizeEmpty();
                }
                else
                {
                    if (Medium.Count > 0)
                    {
                        Point medium = Medium[new Random().Next(Medium.Count)];
                        Field[medium.x, medium.y].PerformClick();
                    }
                    else
                        RandomizeEmpty();
                }
            }
        }

        private List<Point> Finder(Button[,] buttons, bool defence = false)
        {
            int countA = 0, countB = 0;
            int freeCount = 0;
            Point xy = new Point();
            List<Point> result = new List<Point>();
            foreach (var line in Points)
            {
                foreach (var point in line)
                    switch (buttons[point.x, point.y].Text)
                    {
                        case "X":
                            countA++;
                            break;
                        case "O":
                            countB++;
                            break;
                        case "":
                            {
                                freeCount++;
                                xy.x = point.x;
                                xy.y = point.y;
                            }
                            break;
                    }
                if (countB == 2 && freeCount == 1 && !defence) //  O winer
                {
                    result.Clear();
                    result.Add(new Point(xy.x, xy.y));
                    return result;
                }
                if (countA == 2 & freeCount == 1)
                    result.Add(new Point(xy.x, xy.y));
                countA = 0;
                countB = 0;
                xy = new Point();
                freeCount = 0;
            }
            return result;
        }

        private bool Final(Button[,] array = null, char mode = 'b') //both , X, Y
        {
            array ??= Field;
            int countA = 0, countB = 0;
            foreach (var line in Points)
            {
                foreach (var item in line)
                {
                    switch (array[item.x, item.y].Text)
                    {
                        case "X":
                            countA++;
                            break;
                        case "O":
                            countB++;
                            break;
                        default:
                            break;
                    }
                    if (countA == 3 && (mode == 'b' || mode == 'X'))
                        return true;
                    if (countB == 3 && (mode == 'b' || mode == 'Y'))
                        return true;
                }
                countA = 0;
                countB = 0;
            }
            return false;
        }
        
        /*
         * Исходя из моих наблюдений любая игра играется ровно до 5 хода,
         * начиная со второго хода решается будет построена вилка или нет
         * далее на 5 ходу она либо построена либо заблокирована 
         * дальнейший ход игры не влияет на исход
         * 
         * что касательно логики АТАКИ, то алгоритм должен быть аналогичен
         * этому, но просчитывать нужно с 3го по 5 ходы, то есть 3, а не 4 хода как тут
         * а благоприятнымм считаются варианты при которых существую варианты построить вилку
         */
        private Dictionary<Point, char> Algorithm(Button[,] array, string symbol = "O", int loop = 0)
        {
            Dictionary<Point, char> result = new Dictionary<Point, char>();
            var voidArray = CopyArray(array);
            Button[,] assumption = new Button[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    assumption[i, j] = new Button
                    {
                        Text = array[i, j].Text
                    };
            if (symbol == PC)
            {
                foreach (var item in Finder(assumption).Count == 0 ? voidArray : Finder(assumption))
                {
                    assumption[item.x, item.y].Text = symbol;
                    char s;
                    s = Algorithm(assumption, PlayerS, loop + 1)[new Point(9, 9)];
                    result.Add(item, s);
                    assumption[item.x, item.y].Text = "";
                }
                if (loop == 0)
                    return result;
                foreach (var item in result)
                    if (item.Value == 'A')
                    {
                        result.Add(new Point(9, 9), 'A');
                        return result;
                    }
                result.Add(new Point(9, 9), 'B');
                return result;
            }
            else
            {
                
                foreach (var item in Finder(assumption).Count == 0 ? voidArray : Finder(assumption))
                {
                    assumption[item.x, item.y].Text = symbol;
                    var prediction = Finder(assumption, true);
                    if (prediction.Count > 1)
                        result.Add(item, 'B'); 
                    else if (loop < 3)
                    {
                        var s = Algorithm(assumption, PC, loop + 1);
                        result.Add(item, s[new Point(9, 9)]);
                    }
                    else
                        result.Add(item, 'A');
                    assumption[item.x, item.y].Text = "";
                }
                foreach (var item in result)
                    if (item.Value == 'B')
                    {
                        result.Add(new Point(9, 9), 'B');
                        return result;
                    }
                result.Add(new Point(9, 9), 'A');
                return result;
            }
        }

        private static List<Point> CopyArray(Button[,] array)
        {
            List<Point> cloneArray = new List<Point>();
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j].Text == "")
                        cloneArray.Add(new Point(i, j));
            return cloneArray;
        }

        private void RandomizeEmpty()
        {
            List<Button> Fake = new List<Button>();
            foreach (var item in Field)
                if (item.Text == "")
                    Fake.Add(item);
            int count = Fake.Count;
            if (count > 0)
                Fake[new Random().Next(count - 1)].PerformClick();
        } //самый вспомогательный костыль, всё ещё рабочий

        private void FirstStepPC()
        {
            Field[new Random().Next(3), new Random().Next(3)].PerformClick();
        }

        private void ChangeField(Button button, EventHandler _event)
        {
            button11.Click -= button11_Click;
            button11.Enabled = false;
            button12.Click -= button12_Click;
            button13.Click -= button13_Click;
            if (PlayerMove)
            {
                button.Text = PlayerS;
                button.ForeColor = Color.Red;
                PlayerMove = false;
            }
            else
            {
                button.Text = PC;
                button.ForeColor = Color.Blue;
                PlayerMove = true;
            }
            if (Final(Field))
            {
                foreach (var item in Field)
                    item.Enabled = false;
            }
            button.UseVisualStyleBackColor = false;
            button.Click -= _event;
            if (!PlayerMove)
                Search();
        }
    }
}
