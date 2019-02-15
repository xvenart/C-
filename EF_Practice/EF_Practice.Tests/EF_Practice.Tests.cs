using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EF_Practice.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SetCategory_100and700_SecondQreterned()
        {
            // arrange
            var TestPoint = new Point { X = 100, Y = 700 };
            var Expected = new Point { X = 100, Y = 700, Category = Category.SecondQ };

            // act
            Category Actual = Program.SetCategory(TestPoint.X, TestPoint.Y);

            // assert
            Assert.AreEqual(Expected.Category, Actual);
        }

        [TestMethod]
        public void SetCategory_500and500_ZeroPreterned()
        {
            // arrange
            var TestPoint = new Point { X = 500, Y = 500 };
            var Expected = new Point { X = 500, Y = 500, Category = Category.ZeroP };

            // act
            Category Actual = Program.SetCategory(TestPoint.X, TestPoint.Y);

            // assert
            Assert.AreEqual(Expected.Category, Actual);
        }

        [TestMethod]
        public void SetGroup_100and700_1reterned()
        {
            // arrange
            var TestPoint = new Point { X = 100, Y = 700 };
            var Expected = new Point { X = 100, Y = 700, Group = 1 };

            // act
            var Actual = Program.SetGroup(TestPoint.X, TestPoint.Y);

            // assert
            Assert.AreEqual(Expected.Group, Actual);
        }
    }
}
