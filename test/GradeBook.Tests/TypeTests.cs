using System;
using Xunit;

namespace GradeBook.Tests
{

    public delegate string WriteLogDelegate(string logMessage); 

    public class TypeTests
    {
        int count= 0;

         [Fact]
        public void WriteLogDelegateCanPointToMethod()
        {
            WriteLogDelegate log = ReturnMessage;

            log += ReturnMessage;
            log += IncrementCount;


            var result = log("Hiya");
            Assert.Equal(3, count);
        }

        string IncrementCount(string message)
        {
            count++;
            return message.ToUpper();
        }

        string ReturnMessage(string message)
        {
            count++;
            return message;
        }

        [Fact]
        public void BookCalculatesAnAverageGrade()
        {
            //arrange
            var finalExamResults = new InMemoryBook("");
            finalExamResults.AddGrade(89.1);
            finalExamResults.AddGrade(90.5);
            finalExamResults.AddGrade(77.3);

            //act

            var result = finalExamResults.GetStatistics();

            //assert
            Assert.Equal(85.6,result.Average,1);
            Assert.Equal(90.5,result.High,1);
            Assert.Equal(77.3,result.Low,1);
            Assert.Equal('B',result.Letter);
            
        }
         [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);
            Assert.Equal(45,x);
        }

        private void SetInt(ref int z)
        {
           z = 45;
        }

        private int GetInt()
        {
            return 3;
        }

         [Fact]//called an attribute
        public void CSharpCanPassByRef()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(ref book1, "New Name");
            //Not common, don't worry about it

           
            Assert.Equal("New Name", book1.Name);
        }
        private void GetBookSetName(ref InMemoryBook book, string name)
        {
            //ref -- I will get reference to the memory location
            //of where that variable is stored
            // in this case, you ARE making changes to the value inside
            //of var book1 since you have reference to the memory location
            book= new InMemoryBook(name);
            
        }

          [Fact]//called an attribute
        public void CSharpIsPassByValue()
        {
            var book1 = GetBook("Book 1");
            GetBookSetName(book1, "New Name");

           
            Assert.Equal("Book 1", book1.Name);
        }
        private void GetBookSetName(InMemoryBook book, string name)
        {
            book= new InMemoryBook(name);
            //constructing a completely new book object, 
            //NOT THE SAME VAR BOOK1 OBJECT ABOVE
           
        }


        [Fact]//called an attribute
        public void CanSetNameFromReference()
        {
            var book1 = GetBook("Book 1");
            SetName(book1, "Harry Potter");

           
            Assert.Equal("Harry Potter", book1.Name);
        }

        private void SetName(InMemoryBook book, string name)
        {
            //changing name
            book.Name = name;
        }
         [Fact]
        public void StringsBehaveLikeValueTypes()
        {
           string name = "Scott";
           var newName = MakeUpperCase(name);
           //since newName  

            Assert.Equal("Scott",name);
           Assert.Equal("SCOTT",newName);
        }

        private string MakeUpperCase(string parameter)
        {
            return parameter.ToUpper();
        }

        [Fact]//called an attribute
        public void GetBookReturnsDifferentObjects()
        {
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 2",book2.Name);
        }
         [Fact]//called an attribute
        public void TwoVarsCanReferenceSameObject()
        {
            var book1 = GetBook("Book 1");
            var book2 = book1;

           Assert.Same(book1,book2);
           //.Same() verifies that two objects are the same instance/object in memory
           Assert.True(Object.ReferenceEquals(book1,book2));
           //saying that object reference is true.
           //are the values in the variables pointing to the same object
        }

         InMemoryBook GetBook(string name)
        {
            return new InMemoryBook(name);
        }
    }
}
