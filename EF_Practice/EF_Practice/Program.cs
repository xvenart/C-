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

namespace EF_Practice
{
    class Program
    {
        static double SetLength(Point obj)
        {
            return Math.Sqrt(Math.Pow(obj.X - 500, 2) + Math.Pow(obj.Y - 500, 2));
        }

        static int SetGroup(int xx, int yy)
        {
            return ((xx == 500 && yy == 500) ? 0 : (xx <= 500 && yy <= 1000) ? 1 : 2);
        }

        static Category SetCategory(int xx, int yy)
        {
            if (xx < 0 || xx > 1000 || yy < 0 || yy > 1000)
            {
                throw new ArgumentOutOfRangeException("X и/или Y выходит за границы интервалов.");
            }

            return ((xx < 500 &&  yy < 500) ? Category.ThirdQ :
                (xx > 500 && yy < 500) ? Category.FourthQ :
                (xx > 500 && yy > 500) ? Category.FirstQ :
                (xx < 500 && yy > 500) ? Category.SecondQ : Category.ZeroP);
        }

        static void Main(string[] args)
        {
            try
            {
                var rand = new Random();

                Console.WriteLine("Случайные точки:");

                var p = new List<Point>();
                for (int i = 0; i < 5; i++)
                {
                    var xx = rand.Next(0, 1000);
                    var yy = rand.Next(0, 1000);
                    p.Add(new Point { Id = i + 1, X = xx, Y = yy, Group = SetGroup(xx, yy), Category = SetCategory(xx, yy) });
                    Console.WriteLine($"x = {p[i].X}, y = {p[i].Y}, group = {p[i].Group}, category = {p[i].Category}");
                }
                Console.WriteLine();

                /*//Проверка на выходы за границы
                var CheckX = 1001;
                var CheckY = -9;
                p.Add(new Point { Id = 9999, X = CheckX, Y = CheckY, Group = SetGroup(CheckX, CheckY), Category = SetCategory(CheckX, CheckY) });*/

                var zp = new Point { Id = 0, X = 500, Y = 500, Group = SetGroup(500, 500), Category = SetCategory(500, 500) };

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

                using (PointContext db = new PointContext())
                {
                    if (db.Points.Count() != 0)
                    {
                        db.Points.RemoveRange(db.Points);
                        db.SaveChanges();
                    }

                    foreach (var i in p)
                    {
                        db.Points.Add(i);
                    }

                    db.SaveChanges();
                    Console.WriteLine("Объект сохранён.");

                    Console.WriteLine("Список объектов:");
                    foreach (var i in db.Points)
                    {
                        Console.WriteLine($"{i.Id} | {i.X}, {i.Y} => {i.Length} | {i.Group} | {i.Category}");
                    }
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine($"Ошибка: {e.Message}");
            }
            Console.Read();
        }
    }
}