using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        private bool? Mode = false;

        private bool PlayerMove = true;

        private string Player = "X";//TODO если дойдут руки, реализовать замену символов

        private string PC = "O";

        private readonly Button[,] Field = new Button[3, 3];

        /// <summary>
        /// Вертикали, горизонтали и диагонали
        /// </summary>
        private readonly List<List<Point>> FieldLines = new List<List<Point>>
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

        /// <summary>
        /// Поиск ответоного хода компьютером
        /// </summary>
        private void Search()
        {
            var variable = Finder(Field);
            int point = new Random().Next(variable.Count);
            var movesLeft = FindVoidPoint(Field);
            if (variable.Count > 0)
                Field[variable[point].x, variable[point].y].PerformClick();
            else if (movesLeft.Count >= 4)
            {//алгоритм не даст результата если уже сыграно больше 5 шагов
                var heap = Algorithm(Field);
                List<Point> bestWay = new List<Point>();
                List<Point> goodWay = new List<Point>();
                List<Point> allWay = new List<Point>();
                List<Point> badWay = new List<Point>();
                foreach (var item in heap)
                {
                    if (item.Value == 'A')
                        goodWay.Add(item.Key);
                    else if (item.Value == 'S')
                        bestWay.Add(item.Key);
                    else
                        badWay.Add(item.Key);
                    allWay.Add(item.Key);
                }
                if (Mode == true)//Hard
                {
                    if (bestWay.Count > 0)
                        Action(bestWay);
                    else
                        Action(goodWay);
                }
                else if (Mode == false)
                    Action(allWay);
                else
                {
                    if (badWay.Count > 0)
                        Action(badWay);
                    else
                        Action(); // иногда случается, что плохих ходов нет
                }
            }
            else
                Action(); // все ходы после 5го
        }

        /// <summary>
        /// Выбор случайного хода из списка переданных, либо случайный ход
        /// </summary>
        /// <param name="way"></param>
        private void Action(List<Point> way = null)
        {
            if (way == null)
                RandomizeEmpty();
            else
            {
                Point point = way[new Random().Next(way.Count)];
                Field[point.x, point.y].PerformClick();
            }
        }

        /// <summary>
        /// Проверяет вертикали, горизонтали и диагонали на предмет опасных ситуаций
        /// </summary>
        /// <param name="buttons"> Поле для проверки.</param>
        /// <param name="mode"> null- дефолтный, false- только вилки "O", true- только вилки "X".</param>
        /// <returns>Возвращает список возможных ходов, в том числе пустой.</returns>
        private List<Point> Finder(Button[,] buttons, bool? mode = null)
        {
            int countA = 0, countB = 0;
            int freeCount = 0;
            Point xy = new Point();
            List<Point> result = new List<Point>();
            foreach (var line in FieldLines)
            {
                foreach (var point in line)
                {
                    if (buttons[point.x, point.y].Text == Player)
                        countA++;
                    else if (buttons[point.x, point.y].Text == PC)
                        countB++;
                    else if(buttons[point.x, point.y].Text == "")
                    {
                        freeCount++;
                        xy.x = point.x;
                        xy.y = point.y;
                    }
                }
                if (countB == 2 && freeCount == 1 && mode == null) //  O winer
                {
                    result.Clear();
                    result.Add(new Point(xy.x, xy.y));
                    return result;
                }
                if (countB == 2 && freeCount == 1 && mode == false)
                    result.Add(new Point(xy.x, xy.y));
                if (countA == 2 & freeCount == 1 && (mode == null || mode == true))
                    result.Add(new Point(xy.x, xy.y));
                countA = 0;
                countB = 0;
                xy = new Point();
                freeCount = 0;
            }
            return result;
        }

        /// <summary>
        /// Проверят игровое поле на предмет завершения игры
        /// </summary>
        /// <param name="array">Массив для проверки</param>
        /// <param name="mode">Ключи X/O ограничивают проверку одной из сторон, b(оба) - дефолт </param>
        /// <returns>Возвращает true если игра завершена</returns>
        private bool Final(Button[,] array = null, char mode = 'b') //both , X, O
        {
            array ??= Field;
            int countA = 0, countB = 0;
            foreach (var line in FieldLines)
            {
                foreach (var item in line)
                {
                    if (array[item.x, item.y].Text == Player)
                        countA++;
                    else if (array[item.x, item.y].Text == PC)
                        countB++;
                    if (countA == 3 && (mode == 'b' || mode == 'X'))
                        return true;
                    if (countB == 3 && (mode == 'b' || mode == 'O'))
                        return true;
                }
                countA = 0;
                countB = 0;
            }
            return false;
        }


        /// <summary>
        /// Просчитывает ходы на 4 шага вперёд, рекурсионный перебор
        /// </summary>
        /// <param name="array">Массив на просчёт</param>
        /// <param name="pc">Не трогать. Аргумент хода(ИИ / Игрок) </param>
        /// <param name="loop">Не трогать. Текущее погружение рекурсии</param>
        /// <returns>Возвращает словарь возможных ходов с ключом качества данного хода</returns>
        private Dictionary<Point, char> Algorithm(Button[,] array, bool pc = true, int loop = 0)
        {
            Dictionary<Point, char> result = new Dictionary<Point, char>();
            var voidArray = FindVoidPoint(array);
            Button[,] assumption = new Button[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    assumption[i, j] = new Button
                    {
                        Text = array[i, j].Text
                    };
            var variable = Finder(assumption);
            if (pc)
            {
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    assumption[item.x, item.y].Text = PC;
                    var quality = Algorithm(assumption, false, loop + 1);
                    result.Add(item, quality[new Point(9, 9)]);
                    assumption[item.x, item.y].Text = "";
                }
                if (loop == 0)
                    return result;
                else if (result.Where(x => x.Value == 'S').Count() > 0)
                    return new Dictionary<Point, char>() { { new Point(9, 9), 'S' } };
                else if (result.Where(x => x.Value == 'A').Count() > 0)
                    return new Dictionary<Point, char>() { { new Point(9, 9), 'A' } };
                result.Add(new Point(9, 9), 'B');
                return result;
            }
            else
            {
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    var hole = Finder(assumption, false);
                    if (hole.Count > 1)
                        return new Dictionary<Point, char>() { { new Point(9, 9), 'S' } };
                    assumption[item.x, item.y].Text = Player;
                    var prediction = Finder(assumption, true);
                    if (prediction.Count > 1)
                        result.Add(item, 'B');
                    else if (loop < 3)
                    {
                        var quality = Algorithm(assumption, true, loop + 1);
                        result.Add(item, quality[new Point(9, 9)]);
                    }
                    else
                        result.Add(item, 'A');
                    assumption[item.x, item.y].Text = "";
                }
                if (result.Where(x => x.Value == 'B').Count() > 0)
                    return new Dictionary<Point, char>() { { new Point(9, 9), 'B' } };
                else if (result.Where(x => x.Value == 'A').Count() > 0)
                    return new Dictionary<Point, char>() { { new Point(9, 9), 'A' } };
                result.Add(new Point(9, 9), 'S');
                return result;
            }
        }


        /// <summary>
        /// Находит пустые клетки 
        /// </summary>
        /// <param name="array"></param>
        /// <returns>Возвращает список допустимых ходов</returns>
        private static List<Point> FindVoidPoint(Button[,] array)
        {
            List<Point> cloneArray = new List<Point>();
            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j].Text == "")
                        cloneArray.Add(new Point(i, j));
            return cloneArray;
        }

        /// <summary>
        /// Генератор случайного хода
        /// </summary>
        private void RandomizeEmpty()
        {
            List<Button> any = new List<Button>();
            foreach (var item in Field)
                if (item.Text == "")
                    any.Add(item);
            int count = any.Count;
            if (count > 0)
                any[new Random().Next(count - 1)].PerformClick();
        }

        /// <summary>
        /// Первый ход за компьютером
        /// </summary>
        private void FirstStepPC()
        {
            Field[new Random().Next(3), new Random().Next(3)].PerformClick();
        }

        /// <summary>
        /// Обработка игровой логики
        /// </summary>
        /// <param name="button">Нажатая кнопка</param>
        /// <param name="_event">Событие вызвавшее метод</param>
        private void ChangeField(Button button, EventHandler _event)
        {
            //button11.Click -= button11_Click;
            button11.Enabled = false;
            button12.Click -= button12_Click;
            button13.Click -= button13_Click;
            button14.Click -= button14_Click;
            if (PlayerMove) //установка флага игрока
            {
                button.Text = Player;
                button.ForeColor = Color.Red;
                PlayerMove = false;
            }
            else //установка флага компьютера
            {
                button.Text = PC;
                button.ForeColor = Color.Blue;
                PlayerMove = true;
            }
            if (Final()) //проверка игры на завершение
            {
                foreach (var item in Field)
                    item.Enabled = false;
            }
            button.UseVisualStyleBackColor = false;
            button.Click -= _event; //отписка события от кнопки
            if (!PlayerMove)
                Search();
        }
    }
}
