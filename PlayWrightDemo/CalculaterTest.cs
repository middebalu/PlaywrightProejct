using NUnit.Framework;
using PlayWrightDemo;

namespace CalculatorTests
{
    [TestFixture(2,3,5)]
    [TestFixture(2, 3, -1)]
    internal class CalculatorTests
    {
        int number1;
        int number2;
        int expectedResult;
        public CalculatorTests(int number1,int number2,int expectedResult)
        {
            this.number1 = number1;
            this.number2 = number2;
            this.expectedResult = expectedResult;
        }
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

       [Test]
        public void Add_WhenTwoNumbers_ReturnsSum()
        {
            int result = calculator.Add(number1, number2);

            Assert.That(result,Is.EqualTo(expectedResult));
            
           // Assert.AreE(result, expecedResult);
           
        }

        [Test]
        public void Subtract_WhenTwoNumbers_ReturnsDifference()
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