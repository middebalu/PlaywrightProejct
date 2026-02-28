namespace PlayWrightDemo;

public class LamdaTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        //(paramentes) => expression;
        //(a)=>return "";
        //(a) => { Console.WriteLine("Hello"); return ""; };
        // Action -- no return type;
        // Func-- return type
        Func<int, int, int, int> add1 = (a, b, c) => (a + b + c);  // decalration for return type
        
      
        Func<int,int,int, double> add = (a,b,c) => { 
            Console.WriteLine("In function");
            return (a + b + c);
        };
      
        Console.WriteLine(add(2, 3, 4));
        
        add += (a, b, c) => a * b * c;  // update the function;
        
        Console.WriteLine(add(2, 3, 4));
        Func<int, int> square = x => x * x;
        Console.WriteLine(square(4));

        Action<string> print = (message) => Console.WriteLine(message);  // doesn't have any return type
        print("I am in class");

    }
    public int  Add(int a,int s, int c)
    {
        List<Book> booksList = new List<Book>();
       
        for(int i = 0; i < 10; i++)
        {
            Book book = new Book();
           // add infor book;
           //
            booksList.Add(book);
        }
        return a+s+c;
    }
}
