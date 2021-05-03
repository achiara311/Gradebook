using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]//called an attribute
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            InMemoryBook bookUno = new InMemoryBook("");
            bookUno.AddGrade(89.1);
            bookUno.AddGrade(90.5);
            bookUno.AddGrade(77.3);
            //act
            var result = bookUno.GetStatistics();

            //assert
            Assert.Equal(85.6,result.Average,1);
            Assert.Equal(90.5,result.High,1);
            Assert.Equal(77.3,result.Low,1);
        }
    }
}
