/*  
    Юнит-тесты.

    На этот метод (SetCategory)
    На пограничные значения и правильное определение групп

    Главное: отталкивайся при тестировании от того, как должно быть, а не как будет при текущем коде

    И пограничные значения все, если что
*/

using System;
using System.Collections.Generic;
using System.Linq;

namespace EF_Practice
{
    public class Program
    {
        public static double SetLength(Point obj)
        {
            return Math.Sqrt(Math.Pow(obj.X - 500, 2) + Math.Pow(obj.Y - 500, 2));
        }

        public static int SetGroup(int xx, int yy)
        {
            return ((xx == 500 && yy == 500) ? 0 : (xx <= 500 && yy <= 1000) ? 1 : 2);
        }

        public static Category SetCategory(int xx, int yy)
        {
            if (xx < 0 || xx > 1000 || yy < 0 || yy > 1000)
            {
                throw new ArgumentOutOfRangeException("X и/или Y выходит за границы интервалов.");
            }

            if (xx == 500 && yy == 500)
            {
                return Category.ZeroP;
            }
            else
            {
                return ((xx <= 500) ? (yy <= 500 ? Category.ThirdQ : Category.SecondQ) : (yy <= 500 ? Category.FourthQ : Category.FirstQ));
            }
        }

        public static void Main(string[] args)
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
            finally
            {
                Console.Read();
            }
        }
    }
}