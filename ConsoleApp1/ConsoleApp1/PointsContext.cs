namespace ConsoleApp1
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class PointsContext : DbContext
    {
        public PointsContext()
            : base("MyDB")
        {
        }

        public DbSet<Point> Points { get; set; }
    }
}