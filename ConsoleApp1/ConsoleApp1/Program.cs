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
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    enum Category
    {
        ZeroP,
        FirstQ,
        SecondQ,
        ThirdQ,
        FourthQ
    }

    class Point
    {
        public int X;
        public int Y;
        public Category Category;
        public int Group;
        public double Length;

        public Point(int xx, int yy)
        {
            X = xx;
            Y = yy;

            if ((X == 500) && (Y == 500))
            {
                Group = 0;
            }
            else if ((X <= 500) && (Y <= 1000))
            {
                Group = 1;
            }
            else if ((X >= 500) && (Y <= 1000))
            {
                Group = 2;
            }

            if ((X >= 0 && X < 500) && (Y >= 0 && Y < 500))
            {
                Category = Category.ThirdQ;
            }
            else if ((X > 500 && X <= 1000) && (Y >= 0 && Y < 500))
            {
                Category = Category.FourthQ;
            }
            else if ((X > 500 && X <= 1000) && (Y > 500 && Y <= 1000))
            {
                Category = Category.FirstQ;
            }
            else if ((X >= 0 && X < 500) && (Y > 500 && Y <= 1000))
            {
                Category = Category.SecondQ;
            }
            else if (X == 500 && Y == 500)
            {
                Category = Category.ZeroP;
            }
        }

        public void SetLength(double ll)
        {
            Length = ll;
        }
    }

    class Program
    {
        static double GetLength(Point obj1, Point obj2)
        {
            var Length = Math.Sqrt(Math.Pow(Math.Abs(obj1.X - obj2.X), 2) + Math.Pow(Math.Abs(obj1.Y - obj2.Y), 2));
            obj1.SetLength(Length);
            return Length;
        }

        static void Main(string[] args)
        {
            var rand = new Random();

            Console.WriteLine("Случайные точки:");

            var p = new List<Point>();
            for (int i = 0; i < 5; i++)
            {
                p.Add(new Point(rand.Next(0, 1000), rand.Next(0, 1000)));
                Console.WriteLine($"x = {p[i].X}, y = {p[i].Y}, group = {p[i].Group}, category = {p[i].Category}");
            }
            Console.WriteLine();

            var zp = new Point(500, 500);

            Console.WriteLine("Нулевая точка:");
            Console.WriteLine($"x = {zp.X}, y = {zp.Y}, group = {zp.Group}, category = {zp.Category}");
            Console.WriteLine();

            Console.WriteLine("Расстояние до нулевой точки:");
            foreach (var i in p)
            {
                Console.WriteLine($"x: {i.X}, y: {i.Y} => {GetLength(i, zp)}");
            }
            Console.WriteLine();

            Console.WriteLine("Сортировка:");
            var result = p.OrderBy(x => x.Length);
            foreach (var i in result)
            {
                Console.WriteLine($"x: {i.X}, y: {i.Y} => {i.Length}");
            }

            Console.ReadKey();
        }
    }
}
