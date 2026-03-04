using Microsoft.Playwright;
using System;
using System.Reflection.Metadata;

namespace PlayWrightDemo;

public class WithXpath
{
   
    IPage page;
    BrowserManager browserManager;
    IBrowserContext context;
  
    [SetUp]
    public async Task Setup()
    {
        browserManager = new BrowserManager();
        await browserManager.InitiBrowserAsync("chrome");
        context = await browserManager.getBrowserContxtAsync();
        page = await browserManager.getPageAsync();

     }

    [Test]
    public async Task InputTest()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");

        await page.Locator("//textarea").FillAsync("Automatio testing with playwright");
        await page.Locator("//button[@id='generatePdf']").ClickAsync();

        //tagName[@attribute='value' or @attribute1=value]   - Relative -- attrubute
        //button[text()='Generate and Download Text File']  == Exact text match.
        //button[contains(text(),'Download Text')]  == contains text

        //button[not(contains(text(),'Download Text'))]  == contains not text

        //div[@id='laptops']/a[2]  -- Parent to Child
        //a[@id='apple']/..    child to parent
        //a[@id='apple']/parent::div  child to parent

        //a[@id='apple']/../.. child to grand parent or ancestor
        //a[@id='apple']/ancestor::div[@class='container']

        //a[@id='lenovo']/following-sibling::a  -- child another child
        //a[@id='dell']/preceding-sibling::a  

        //*[@id='laptops']/*[@class]   -- ignoreing the tag


       // contains(@attribute,'text')
        /// tagname[@attribute = 'value']/tagname/ 

        //div -body - intpu-text

    }

    [Test]
    public async Task CSSLocator()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator cssId = page.Locator("button#generateTxt");
        ILocator xapthid1 = page.Locator("//button[@id='generateTxt']");
        Console.WriteLine(await cssId.TextContentAsync());
        IReadOnlyList<ILocator> bbbutns= await page.Locator("//button[@id]").AllAsync();
        foreach (var item in bbbutns)
        {
            Console.WriteLine(await item.TextContentAsync());
        }
        Console.WriteLine(await xapthid1.TextContentAsync());

        // Css by tag name and id value
        //     tagname#id
        // ex: button#generateTxt

        // css with by tag and class attribute value
        //   tagname.class
        // a.link

        // css with by tag and any attrinute
        //  tagname[attribute='value]
        //ex div[data-version='1']
        //  button[id='generateTxt']

    }
    [Test]
    public async Task printTableRows()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator bookTable = page.Locator("//table[@name='BookTable']");
        IReadOnlyList<ILocator> tableRows = await bookTable.Locator("//tr").AllAsync();
        for (int i = 0; i <  tableRows.Count; i++)
        {
            Console.WriteLine(await tableRows[i].TextContentAsync());
        }
       
    }
    [Test]
    public async Task printTableCells()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator bookTable = page.Locator("//table[@name='BookTable']");
        IReadOnlyList<ILocator> tableRows = await bookTable.Locator("//tr").AllAsync();
        List<Book> books = new List<Book>();

        for (int i = 1; i < tableRows.Count; i++)
        {
            IReadOnlyList<ILocator> cells = await tableRows[i].Locator("//td").AllAsync();

            foreach (ILocator cell in cells)
            {
                Console.Write(" " + await cell.TextContentAsync());
            }
            Console.WriteLine();
        }

        
    }
    [Test]
    public async Task writeIntoBookObject()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator bookTable = page.Locator("//table[@name='BookTable']");
        IReadOnlyList<ILocator> tableRows = await bookTable.Locator("//tr[position()>1]").AllAsync();
        List<Book> books = new List<Book>();

       
        foreach (ILocator row in tableRows)   // //table[@name='BookTable']//tr[position()>1]  this locator to skip first tr which has table header
        {
            IReadOnlyList<ILocator> cells = await row.Locator("//td").AllAsync();
            Book book = new();
            book.bookName = await cells[0].TextContentAsync();
            book.authoName = await cells[1].TextContentAsync();
            book.subject = await cells[2].TextContentAsync();
            book.price = await cells[3].TextContentAsync();
            books.Add(book);
        }
        Console.WriteLine("From book object");
        foreach (Book book in books)
        {

            Console.WriteLine(book.bookName);
            Console.WriteLine(book.authoName);
            Console.WriteLine(book.subject);
            Console.WriteLine(book.price);
        }
    }
    [Test]
    public async Task TableByGetByRole()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator table=page.Locator("table[name='BookTable']");
        IReadOnlyList<String> tableData = await table.AllTextContentsAsync();
        ILocator tableHeader = table.GetByRole(AriaRole.Columnheader);
        for(int i = 0; i < await tableHeader.CountAsync(); i++)
        {
            Console.WriteLine(await tableHeader.Nth(i).TextContentAsync());
        }
        IReadOnlyList<ILocator> tableHeaders = await table.GetByRole(AriaRole.Columnheader).AllAsync();
        foreach(ILocator hedaerCell in tableHeaders)
        {
            Console.WriteLine(await hedaerCell.TextContentAsync());
        }
        
        ILocator tablerows = table.GetByRole(AriaRole.Row);
        for(int i=1;i<await tablerows.CountAsync(); i++)
        {
            ILocator cells=tablerows.Nth(i).GetByRole(AriaRole.Cell);
            for(int j = 0;  j < await cells.CountAsync(); j++)
            {
                Console.Write(" " + await cells.Nth(j).TextContentAsync());
            }
            Console.WriteLine();
        }
        Console.WriteLine(await tablerows.CountAsync());
        Console.WriteLine(await tableHeader.Nth(0).AllInnerTextsAsync());
       

    }

    [Test]
    public async Task DragAndDrop()
    {
        //tagName[attribute*=value]    contains
        // ex: div[class*='ui-draggable']

        //tagName[attribute^=value]  start with
        // ex: div[class^='ui-widget-conten']

        // tagname.class value     // space need replace with .
        //ex:div.ui-widget-content.ui-draggable.ui-draggable-handle
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator source = page.Locator("div[id='draggable']");
        ILocator target = page.Locator("div[id='droppable']");
        await source.ScrollIntoViewIfNeededAsync();
        //await source.DragToAsync(target);  -- with webelement
        LocatorBoundingBoxResult sourceBoundBox=await   source.BoundingBoxAsync();
        LocatorBoundingBoxResult targetBoundBox = await target.BoundingBoxAsync();
        //x+width/2 , y+height/2
        await page.Mouse.MoveAsync(sourceBoundBox.X + sourceBoundBox.Width / 2, sourceBoundBox.Y + sourceBoundBox.Height / 2);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(targetBoundBox.X + targetBoundBox.Width / 2, targetBoundBox.Y + targetBoundBox.Height / 2,new MouseMoveOptions{Steps=50});
        await page.Mouse.UpAsync();
        
        Console.WriteLine();
        // Locate slider and handles
        var slider = page.Locator("#slider-range");
        var handles = slider.Locator(".ui-slider-handle");
        await slider.ScrollIntoViewIfNeededAsync();
        // Get bounding box of slider
        var sliderBox = await slider.BoundingBoxAsync();

        // Move first handle (index 0)
        var firstHandle = handles.Nth(0);
        var handleBox = await firstHandle.BoundingBoxAsync();

        // Calculate target position (example: 30%)
        double  targetX = sliderBox.X + (sliderBox.Width * 0.40);
        double centerY = handleBox.Y + handleBox.Height / 2;

        // Drag
        await page.Mouse.MoveAsync(handleBox.X + handleBox.Width / 2, (float)centerY);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync((float)targetX, (float)centerY);
        await page.Mouse.UpAsync();

    }
    [Test]
    public async Task Slider()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator sliderRange = page.Locator("div.ui-slider");
        ILocator sliderHandl = page.Locator("//span[contains(@class,'ui-slider-handle')]");
      //  await sliderRange.ScrollIntoViewIfNeededAsync();
        var sliderDim=await sliderRange.BoundingBoxAsync();
        float xAxis = sliderDim.X + sliderDim.Width * 0.40f;
        //left slider
        var sliderHandelDim = await sliderHandl.Nth(0).BoundingBoxAsync();
        await page.Mouse.MoveAsync(sliderHandelDim.X+sliderHandelDim.Width/2, sliderHandelDim.Y+sliderHandelDim.Height/2);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(xAxis, sliderDim.Y);
        await page.Mouse.UpAsync();
        String actualleft = await sliderHandl.Nth(0).GetAttributeAsync("style");
        Assert.That(actualleft, Does.Contain("40%"));
        // right slider
        xAxis = sliderDim.X + sliderDim.Width * 0.70f;
        var sliderHandelRightDim = await sliderHandl.Nth(1).BoundingBoxAsync();
        await page.Mouse.MoveAsync(sliderHandelRightDim.X + sliderHandelRightDim.Width / 2, sliderHandelRightDim.Y + sliderHandelRightDim.Height / 2);
        await page.Mouse.DownAsync();
        await page.Mouse.MoveAsync(xAxis, sliderDim.Y);
        String actualRight=await sliderHandl.Nth(1).GetAttributeAsync("style");
        Assert.That(actualRight, Does.Contain("70%"));

    }
    [Test]
    public async Task ShadowElements()
    {
        await page.GotoAsync("https://testautomationpractice.blogspot.com/p/download-files_25.html");
        ILocator shadowElement = page.Locator("//div[@id='shadow_host']");
        ILocator input =shadowElement.Locator("input[type='text']");
        await input.FillAsync("testing");
        String inputContent=await input.TextContentAsync();
        Assert.That(inputContent, Does.Match("testing"));

    }

    [TearDown]
    public async Task Teardown()
    {
        await browserManager.DisposeBrowserAynsc();
    }
}





















