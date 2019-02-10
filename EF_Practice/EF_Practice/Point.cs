namespace EF_Practice
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
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Group { get; set; }
        public Category Category { get; set; }
        public double Length { get; set; }
    }
}
