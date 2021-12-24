using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    enum Difficulty
    {
        Easy,
        Meddium,
        Hard,
        HardX
    }

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
        private Difficulty Mode = Difficulty.Meddium;

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
        /// Ответный ход компьютера
        /// </summary>
        private void StepPC()
        {
            switch (Mode)
            {
                case Difficulty.Easy:
                    Action();
                    break;
                case Difficulty.Meddium:
                    MeddiumWay();
                    break;
                case Difficulty.Hard:
                    HardWay();
                    break;
                default:
                    throw new Exception("Mode содержит недопустимое для него значение Difficulty.HardX");
            }
        }

        private void MeddiumWay()
        {
            var variable = Finder(Field);
            if (variable.Count > 0)
            {
                int point = new Random().Next(variable.Count);
                Field[variable[point].x, variable[point].y].PerformClick();
            }
            else
                Action();
        }

        private void HardWay()
        {
            var variable = Finder(Field);
            if (variable.Count > 0)
            {
                int point = new Random().Next(variable.Count);
                Field[variable[point].x, variable[point].y].PerformClick();
            }
            else if (FindVoidPoint(Field).Count >= 4)
            {
                var heap = Algorithm(CopyField());
                var bestWay = heap.Where(x => x.Value == Difficulty.HardX).Select(x => x.Key).ToList();
                var goodWay = heap.Where(x => x.Value == Difficulty.Hard).Select(x => x.Key).ToList();
                if (bestWay.Count > 0)
                    Action(bestWay);
                else
                    Action(goodWay);
            }
            else
                Action();
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
                    else if (buttons[point.x, point.y].Text == "")
                    {
                        freeCount++;
                        xy.x = point.x;
                        xy.y = point.y;
                    }
                }
                if (countB == 2 && freeCount == 1 && mode == null) //  PC winer
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

        private Button[,] CopyField()
        {
            Button[,] result = new Button[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    result[i, j] = new Button
                    {
                        Text = Field[i, j].Text
                    };
            return result;
        }

        /// Просчитывает ходы на 4 шага вперёд, рекурсионный перебор
        /// </summary>
        /// <param name="array">Массив на просчёт</param>
        /// <param name="pc">Не трогать. Аргумент хода(ИИ / Игрок) </param>
        /// <param name="loop">Не трогать. Текущее погружение рекурсии</param>
        /// <returns>Возвращает словарь возможных ходов с ключом качества данного хода</returns>
        private Dictionary<Point, Difficulty> Algorithm(Button[,] assumption)
        {
            return AnalysisPartOne(assumption);



            Dictionary<Point, Difficulty> AnalysisPartOne(Button[,] assumption, int loop = 0)
            {
                Dictionary<Point, Difficulty> result = new Dictionary<Point, Difficulty>();
                var voidArray = FindVoidPoint(assumption);
                var variable = Finder(assumption);
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    assumption[item.x, item.y].Text = PC;
                    var quality = AnalysisPartTwo(assumption, loop + 1);
                    result.Add(item, quality[new Point(9, 9)]);
                    assumption[item.x, item.y].Text = "";
                }
                if (loop == 0)
                    return result;
                else if (result.Where(x => x.Value == Difficulty.HardX).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };// вернуть ключ атаки
                else if (result.Where(x => x.Value == Difficulty.Hard).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Hard } };// вернуть ключ защиты
                return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Meddium } };// вернуть ключ слабости
            }



            Dictionary<Point, Difficulty> AnalysisPartTwo(Button[,] assumption, int loop = 0)
            {
                Dictionary<Point, Difficulty> result = new Dictionary<Point, Difficulty>();
                var voidArray = FindVoidPoint(assumption);
                var variable = Finder(assumption);
                var hole = Finder(assumption, false);
                if (hole.Count > 1)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    assumption[item.x, item.y].Text = Player;
                    var prediction = Finder(assumption, true);
                    if (prediction.Count > 1)
                        result.Add(item, Difficulty.Meddium);
                    else if (loop < 3)
                    {
                        var quality = AnalysisPartOne(assumption, loop + 1);
                        result.Add(item, quality[new Point(9, 9)]);
                    }
                    else
                        result.Add(item, Difficulty.Hard);
                    assumption[item.x, item.y].Text = "";
                }
                if (result.Where(x => x.Value == Difficulty.Meddium).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Meddium } };// вернуть ключ слабости
                else if (result.Where(x => x.Value == Difficulty.Hard).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Hard } };// вернуть ключ защиты
                return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };// вернуть ключ акаки
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
                StepPC();
        }
    }
}


/*/// <summary>
        /// Просчитывает ходы на 4 шага вперёд, рекурсионный перебор
        /// </summary>
        /// <param name="array">Массив на просчёт</param>
        /// <param name="pc">Не трогать. Аргумент хода(ИИ / Игрок) </param>
        /// <param name="loop">Не трогать. Текущее погружение рекурсии</param>
        /// <returns>Возвращает словарь возможных ходов с ключом качества данного хода</returns>
        private Dictionary<Point, Difficulty> Algorithm(Button[,] assumption, bool pc = true, int loop = 0)
        {
            Dictionary<Point, Difficulty> result = new Dictionary<Point, Difficulty>();
            var voidArray = FindVoidPoint(assumption);
            var variable = Finder(assumption);
            if (pc)
            {
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    assumption[item.x, item.y].Text = PC;//1
                    var quality = Algorithm(assumption, false, loop + 1);//2
                    result.Add(item, quality[new Point(9, 9)]);//3
                    assumption[item.x, item.y].Text = "";//4
                }
                if (loop == 0)
                    return result;
                else if (result.Where(x => x.Value == Difficulty.HardX).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };// вернуть ключ атаки
                else if (result.Where(x => x.Value == Difficulty.Hard).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Hard } };// вернуть ключ защиты
                return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Meddium } };// вернуть ключ слабости
            }
            else
            {
                var hole = Finder(assumption, false);
                if (hole.Count > 1)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };
                foreach (var item in variable.Count == 0 ? voidArray : variable)
                {
                    assumption[item.x, item.y].Text = Player;//1
                    var prediction = Finder(assumption, true);
                    if (prediction.Count > 1)
                        result.Add(item, Difficulty.Meddium);//3
                    else if (loop < 3) 
                    {
                        var quality = Algorithm(assumption, true, loop + 1);//2
                        result.Add(item, quality[new Point(9, 9)]);//3
                    }
                    else
                        result.Add(item, Difficulty.Hard);//3
                    assumption[item.x, item.y].Text = "";//4
                }
                if (result.Where(x => x.Value == Difficulty.Meddium).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Meddium } };// вернуть ключ слабости
                else if (result.Where(x => x.Value == Difficulty.Hard).Count() > 0)
                    return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.Hard } };// вернуть ключ защиты
                return new Dictionary<Point, Difficulty>() { { new Point(9, 9), Difficulty.HardX } };// вернуть ключ акаки
            }
        }// координата 9 9 не является игровым полем и используется для обмена данными */ 