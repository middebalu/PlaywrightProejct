using System;
using System.Collections.Generic;
using System.Text;

namespace PlayWrightDemo
{
    [Parallelizable(ParallelScope.Children)]
    [Category("children")]
    public class ParallelTest
    {
        private ThreadLocal<int> count;
        [OneTimeSetUp]
        public void Setup2()
        {
              count = new ThreadLocal<int>();
        }
        


        [SetUp]
        public void Setup()
        {
           
            count.Value = 1;
            Console.WriteLine($"Setup executed {DateTime.Now:yyyyMMdd_HHmmssfff} {count.Value}");
        }

        [Test]
        public void Test1()
        {

            Console.WriteLine($"Test 1 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {count.Value++}");

            Console.WriteLine($"After incrrement on test 1 {count.Value}");
        }

        [Test]
        public void Test2()
        {
            Console.WriteLine($"Test 2 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {count.Value++}");
            Console.WriteLine($"After incrrement on test 2 {count.Value}");
        }

        [Test]
        public void Test3()
        {
            Console.WriteLine($"Test 3 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {count.Value}");
        }

        [TearDown]
        public void tearDown()
        {
            Console.WriteLine($"Tear down executed {count.Value}");
            //count.Dispose();
        }

        [OneTimeTearDown]
        public void OneTileTearDown()
        {
            count.Dispose();
        }
    }
}

