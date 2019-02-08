/*
1. У тебя генерируются 300+ точек с случайными координатами x от 0 до 1000, y от 0 до 1000, они должны относиться
к одной из четырех категорий (Категории прописать в enum'е) и к одной из двух групп (пусть 1 и 3 категории 
относятся к первой группе, 2 и 4 - ко второй) 

2. Создается центральная точка с координатами (500, 500), третьей (нулевой) группой и пятой (нулевой) категорией

3. Для каждой точки нужно будет считать расстояние до центральной точки

4. Затем сортировать список точек по увеличению дистанции до центральной точки

5. Пока что просто вывести

+ 6. Так что сделай добавление точек в базу
*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    public enum Category
    {
        ZeroP,
        FirstQ,
        SecondQ,
        ThirdQ,
        FourthQ
    }

    public class Point
    {
        public int X;
        public int Y;
        public Category Category;
        public int Group;
        public double Length;
    }

    class Program
    {
        static double SetLength(Point obj)
        {
            return Math.Sqrt(Math.Pow(Math.Abs(obj.X - 500), 2) + Math.Pow(Math.Abs(obj.Y - 500), 2));
        }

        static int SetGroup(int xx, int yy)
        {
            if ((xx == 500) && (yy == 500))
            {
                return 0;
            }
            else if ((xx <= 500) && (yy <= 1000))
            {
                return 1;
            }
            else 
            {
                return 2;
            }
        }

        static Category SetCategory(int xx, int yy)
        {
            if ((xx >= 0 && xx < 500) && (yy >= 0 && yy < 500))
            {
                return Category.ThirdQ;
            }
            else if ((xx > 500 && xx <= 1000) && (yy >= 0 && yy < 500))
            {
                return Category.FourthQ;
            }
            else if ((xx > 500 && xx <= 1000) && (yy > 500 && yy <= 1000))
            {
                return Category.FirstQ;
            }
            else if ((xx >= 0 && xx < 500) && (yy > 500 && yy <= 1000))
            {
                return Category.SecondQ;
            }
            else 
            {
                return Category.ZeroP;
            }
        }

        static void Main(string[] args)
        {
            var rand = new Random();

            Console.WriteLine("Случайные точки:");

            var p = new List<Point>();
            for (int i = 0; i < 5; i++)
            {
                var xx = rand.Next(0, 1000);
                var yy = rand.Next(0, 1000);
                p.Add(new Point { X = xx, Y = yy, Group = SetGroup(xx, yy), Category = SetCategory(xx, yy) });
                Console.WriteLine($"x = {p[i].X}, y = {p[i].Y}, group = {p[i].Group}, category = {p[i].Category}");
            }
            Console.WriteLine();

            var zp = new Point { X = 500, Y = 500, Group = SetGroup(500, 500), Category = SetCategory(500, 500) };

            Console.WriteLine("Нулевая точка:");
            Console.WriteLine($"x = {zp.X}, y = {zp.Y}, group = {zp.Group}, category = {zp.Category}");
            Console.WriteLine();

            Console.WriteLine("Расстояние до нулевой точки:");
            foreach (var i in p)
            {
                i.Length = SetLength(i);
                Console.WriteLine($"x: {i.X}, y: {i.Y} => {i.Length}");
            }
            Console.WriteLine();

            Console.WriteLine("Сортировка:");
            var result = p.OrderBy(x => x.Length);
            foreach (var i in result)
            {
                Console.WriteLine($"x: {i.X}, y: {i.Y} => {i.Length}");
            }

            using (PointsContext db = new PointsContext())
            {
                /*foreach (var i in p)
                {
                    db.DB_Points.Add(i);
                }*/
                db.Points.AddRange(p);   //виснет туть. 
                db.SaveChanges();

                Console.WriteLine("Данные сохранены в базе данных.");
                Console.WriteLine();

                Console.WriteLine("Объекты БД:");
                foreach (var i in db.Points)
                {
                    Console.WriteLine($"{i.X}, {i.Y}: {i.Length}");
                }
            }

            Console.ReadKey();
        }
    }
}
