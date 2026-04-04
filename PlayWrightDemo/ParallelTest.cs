namespace PlayWrightDemo;

[Parallelizable(ParallelScope.All)]
[Category("all")]
public class ParallelTest1
{
    //ThreadLocal<int> count;
    ParallelObjects objet = new();

    [SetUp]
    public void Setup()
    {
        objet.InitCount();
        objet.setCount(3);
        
        Console.WriteLine($"Setup executed {DateTime.Now:yyyyMMdd_HHmmssfff} ");
    }

    [Test]
    public void Test1()
    {
       
        Console.WriteLine($"Test 1 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {objet.getCount()}");
        objet.setCount(4);
        Console.WriteLine($"After incrrement on test 1 {objet.getCount().Value}");
    }

    [Test]
    public void Test2()
    {
        Console.WriteLine($"Test 2 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {objet.getCount()}");
        objet.setCount(5);
        Console.WriteLine($"After incrrement on test 2 {objet.getCount().Value}");
    }

    [Test]
    public void Test3()
    {
        Console.WriteLine($"Test 3 executed {DateTime.Now:yyyyMMdd_HHmmssfff} {objet.getCount().Value}");
    }

    [TearDown]
    public void tearDown()
    {
        Console.WriteLine("Tear down executed ");
        objet.DisposeCount();
    }
}
