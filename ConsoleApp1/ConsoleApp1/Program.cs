/*
1. У тебя генерируются 300+ точек с случайными координатами x от 0 до 1000, y от 0 до 1000, они должны относиться
к одной из четырех категорий (Категории прописать в enum'е) и к одной из двух групп (пусть 1 и 3 категории 
относятся к первой группе, 2 и 4 - ко второй) 

2. Создается центральная точка с координатами (500, 500), третьей (нулевой) группой и пятой (нулевой) категорией

3. Для каждой точки нужно будет считать расстояние до центральной точки

4. Затем сортировать список точек по увеличению дистанции до центральной точки

5. Пока что просто вывести
*/
using System;

namespace ConsoleApp1
{
    enum Category
    {
        FirstQ = 1,
        SecondQ = 2,
        ThirdQ = 3,
        FourthQ = 4,
        ZeroP = 0
    }

    class Point
    {
        public int x;
        public int y;
        public Category category;
        public int group;
        public double length;

        public Point(int xx, int yy)
        {
            x = xx;
            y = yy;

            if ((x == 500) && (y == 500))
            {
                group = 0;
            }
            else if ((x <= 500) && (y <= 1000))
            {
                group = 1;
            }
            else if ((x >= 500) && (y <= 1000))
            {
                group = 2;
            }

            if ((x >= 0 && x < 500) && (y >= 0 && y < 500))
            {
                category = Category.ThirdQ;
            }
            else if ((x > 500 && x <= 1000) && (y >= 0 && y < 500))
            {
                category = Category.FourthQ;
            }
            else if ((x > 500 && x <= 1000) && (y > 500 && y <= 1000))
            {
                category = Category.FirstQ;
            }
            else if ((x >= 0 && x < 500) && (y > 500 && y <= 1000))
            {
                category = Category.SecondQ;
            }
            else if (x == 500 && y == 500)
            {
                category = Category.ZeroP;
            }
        }

        public double Length(Point obj)
        {
            length = Math.Sqrt(Math.Pow(Math.Abs(x - obj.x), 2) + Math.Pow(Math.Abs(y - obj.y), 2));
            return length;
        }
    }

    class Program
    {
        static void BubbleSort(Point[] objs)
        {
            Point temp;

            for (int i = 0; i < objs.Length; i++)
            {
                for (int j = i + 1; j < objs.Length; j++)
                {
                    if (objs[i].length > objs[j].length)
                    {
                        temp = objs[i];
                        objs[i] = objs[j];
                        objs[j] = temp;
                    }
                }
            }

            foreach (var i in objs)
            {
                Console.WriteLine($"x: {i.x}, y: {i.y} => {i.length}");
            }
        }

        static void Main(string[] args)
        {
            var rand = new Random();

            Console.WriteLine("Случайные точки:");

            Point[] p = new Point[5];
            for (int i = 0; i < 5; i++)
            {
                p[i] = new Point(rand.Next(0, 1000), rand.Next(0,1000));
                Console.WriteLine($"x = {p[i].x}, y = {p[i].y}, group = {p[i].group}, category = {p[i].category}");
            }
            Console.WriteLine("");

            var zp = new Point(500, 500);

            Console.WriteLine("Нулевая точка:");
            Console.WriteLine($"x = {zp.x}, y = {zp.y}, group = {zp.group}, category = {zp.category}");
            Console.WriteLine("");

            Console.WriteLine("Расстояние до нулевой точки:");
            foreach (var i in p)
            {
                Console.WriteLine($"x: {i.x}, y: {i.y} => {i.Length(zp)}");
            }
            Console.WriteLine("");

            Console.WriteLine("Сортировка:");
            BubbleSort(p);

            Console.ReadKey();
        }
    }
}
