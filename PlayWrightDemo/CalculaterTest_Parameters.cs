using NUnit.Framework;
using PlayWrightDemo;

namespace CalculatorTests
{
   
    internal class CalculatorTests_Test_Para
    {
       
        private Calculator calculator;

        [OneTimeSetUp]
        public void GlobalSetup()
        {
            Console.WriteLine("Starting Calculator Tests");
        }

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Calculatorr object created");
            calculator = new Calculator();
        }

       [TestCase (2,3,5)]
        public void Add_WhenTwoNumbers_ReturnsSum(int number1,int number2,int expectedResult)
        {
            int result = calculator.Add(number1, number2);

            Assert.That(result,Is.EqualTo(expectedResult));
            
           // Assert.AreE(result, expecedResult);
           
        }
        [TestCase(2, 3, -1)]
        public void Subtract_WhenTwoNumbers_ReturnsDifference(int number1, int number2, int expectedResult)
        {
            int result = calculator.Subtract(number1, number2);

            Assert.That(expectedResult,Is.EqualTo(result));
        }

        [TearDown]
        public void Cleanup()
        {
            Console.WriteLine("Test Completed");
        }

        [OneTimeTearDown]
        public void GlobalCleanup()
        {
            Console.WriteLine("All tests finished");
        }
    }
}