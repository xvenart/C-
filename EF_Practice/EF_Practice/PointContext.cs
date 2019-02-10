using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace EF_Practice
{
    class PointContext : DbContext
    {
        public PointContext() : base("MyDB")
        { }

        public DbSet<Point> Points { get; set; }
    }
}
