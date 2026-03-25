using Microsoft.Playwright;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static Microsoft.Playwright.Assertions;
using static System.Console;

namespace PlayWrightDemo
{
    public class Tests
    {
       
        IPage page;
        BrowserManager browserManager;
        IBrowserContext context;
        [SetUp]
        public async  Task Setup()
        {
            browserManager = new BrowserManager();
            await browserManager.InitiBrowserAsync("chrome");
            context = await browserManager.getBrowserContxtAsync();
            page =await browserManager.getPageAsync();
            
        }
        

        [Test]
        public async Task Test1()
        {
            
            await page.GotoAsync("https://google.com");
            GooglePage googlepage = new GooglePage(page);
            String title = await googlepage.GetTitleAsync();
            Assert.That(title, Does.Match("Kumar"));
        }
        [Test]
        public async Task SwaglabDemoTest()
        {
           // page.SetDefaultTimeout(60000);
            await page.GotoAsync("https://www.saucedemo.com/");
            SwagLabLoginPage swagLabLoginPage = new SwagLabLoginPage(page);
            SwaglabHome swaglabHome = new SwaglabHome(page);
            await swagLabLoginPage.LoginAsync("standard_user", "secret_sauc");
            Assert.That(await swaglabHome.getPageLogo(), Does.Match("Swag Labs1"));
            await Expect(page).ToHaveTitleAsync("Swag Labs");
            ILocator usernameLocator = page.GetByPlaceholder("Username");
          
        }
        [Test]
        public async Task RoleTest()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator button = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions {NameRegex= new Regex( "Primary*")});
            ILocator menuitems = page.GetByRole(AriaRole.Menuitem, new() {Name="Home" });
            ILocator username = page.GetByLabel("Usernam:");
            
            ILocator menuLocator = page.GetByRole(AriaRole.Menuitem);  //3 elements
            IReadOnlyList<string> names=await  menuLocator.AllInnerTextsAsync(); //3 string
             
            foreach(string s in names)
            {
                Console.WriteLine("Menu :"+s);
            }
            
            
            
            ILocator mouseHover = page.GetByText("Point M");
            await mouseHover.HoverAsync();
           // await mouseHover.ScrollIntoViewIfNeededAsync();
            await username.TypeAsync("username");

            await page.Keyboard.PressAsync("ControlOrMeta+A");

           
            await button.ClickAsync();
            await menuitems.ClickAsync();
           

        }
        [Test]
        public async Task ListOfElements()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator button = page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { NameRegex = new Regex("Primary*") });
            ILocator menuList = page.GetByRole(AriaRole.Menuitem);
           
            
        }
        [Test]
        public async Task DropDown()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/");
            ILocator dropDown = page.GetByLabel("Country:");
            await dropDown.ScrollIntoViewIfNeededAsync();
            await dropDown.SelectOptionAsync(new SelectOptionValue { Label="India"}); // select by Visible text
            await dropDown.SelectOptionAsync("japan"); // select by value
            IReadOnlyList<String> items=await dropDown.AllTextContentsAsync();
            foreach(string s in items)
            {
                Console.WriteLine("Item: "+s);
            }


            // Console.WriteLine(dropDown.Locator("option"));
            for (int i = 0; i < await dropDown.CountAsync(); i++)
            {
                Console.WriteLine("Attribute"+await dropDown.Nth(i).GetAttributeAsync("value"));
            }
            //Console.WriteLine(option);
            await dropDown.SelectOptionAsync(new SelectOptionValue { Index = 5 }); // select by index
            string selectedCountry=await dropDown.InputValueAsync();    // get selected value
            Console.WriteLine("Selected country" + selectedCountry);

        }
        [Test]
        public async Task Chekcbox()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/");
            ILocator checkbox=page.GetByRole(AriaRole.Radio);
            await checkbox.Nth(0).CheckAsync();
            for(int i = 0; i < await checkbox.CountAsync(); i++)
            {
                // WriteLine("Chekbox: Value"+await checkbox.Nth(i).GetAttributeAsync("value"));
                //WriteLine("Chekbox: id" + await checkbox.Nth(i).GetAttributeAsync("id"));
                string s = await checkbox.Nth(i).GetAttributeAsync("id");
                
                
                WriteLine("Day " + s); 
                if (s.Equals("monday"))
                {
                   
                    await checkbox.Nth(i).CheckAsync();
                    await Expect(checkbox.Nth(i)).ToBeCheckedAsync();
                    break;
                }
            }
           
        }
        [Test]
        public async Task AletTest()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/");
            page.Dialog += async (_, dialog) =>
            {
                WriteLine(dialog.Message);
                await dialog.AcceptAsync();
            };
            ILocator alert=  page.GetByRole(AriaRole.Button, new() { Name = "Simple Alert" });
            await alert.ClickAsync();
            
           
           
        }
        [Test]
        public async Task Alerts()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator simpleAlert=page.GetByText("Simple Alert");
            page.Dialog += async (_, dailog) => {
                WriteLine(dailog.Message);
                await dailog.AcceptAsync();
            };
            await simpleAlert.ClickAsync();
            Console.WriteLine("After alert");
        }
        [Test]
        public async Task Popups()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator popupLoc = page.GetByText("Popup Windows");
            await page.WaitForLoadStateAsync();
            List<IPage> popups = new List<IPage>();
            page.Popup += async (_, popup) =>
            {
                popups.Add(popup);
            };
            await popupLoc.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.Load);
            popups.ForEach(p => Console.WriteLine(p.Url));
            WriteLine(popups[0].Url);
            WriteLine(popups[1].Url);
        }

        [Test]
        public async Task HandelTabs()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            WriteLine(page.Url);

            ILocator newtab = page.GetByText("New Tab");
            var newPage = await context.RunAndWaitForPageAsync(async () =>
            {
                await newtab.ClickAsync();
            });
            // await newPage.GotoAsync("https://google.com");
            await newPage.WaitForLoadStateAsync();
            WriteLine(newPage.Url);
            await newPage.CloseAsync();

        }

        [Test]
        public async Task MutiplePopups()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            WriteLine(page.Url);
            ILocator popupLoc = page.GetByText("Popup Windows");
            List<IPage> popupWindows = new List<IPage>();
            page.Popup += async (_, popup) =>  // register to capture popups
            {
                popupWindows.Add(popup);
               
            };
            await popupLoc.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.NetworkIdle);  // wait till nwtwork is idle
            for(int i = 0; i < popupWindows.Count; i++)
            {
                WriteLine(await popupWindows[i].TitleAsync());
                await popupWindows[i].CloseAsync();
            }
            popupWindows.ForEach(async p => {    // Iterate with LINQ ForEach
                WriteLine(p.Url);
                WriteLine(await p.TitleAsync());
            });

        }
        [Test]
        public async Task Clicks()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator loc = page.GetByText("Kumar");
            await loc.WaitForAsync(new() { Timeout = 60000 });
            await loc.ClickAsync(new LocatorClickOptions { ClickCount = 2 });   // Doudble click
            string s = await page.Locator("#field2").TextContentAsync();
            Assert.That(s,Does.Match("Hello World!"));  //N Unit assertion
            await page.ClickAsync("", new PageClickOptions { ClickCount = 2 });
            
        }
       
        [Test]
        public async Task fileUpload()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator fileLoc = page.Locator("#singleFileInput");
            var fileUploader = await page.RunAndWaitForFileChooserAsync(async () =>
            {
                await fileLoc.ClickAsync();
            });
           
            await fileUploader.SetFilesAsync("C:\\Users\\Administrator\\source\\repos\\PlayWrightDemo\\PlayWrightDemo\\TextData.txt");
            await page.GetByText("Upload Single File").ClickAsync();
            String message = await page.GetByText(new Regex("Single file selected:*")).TextContentAsync();
            Assert.That(message, Does.Contain("TextData.txt"));
            
           
        }
        [Test]
        public async Task MultiFuleUpload(){
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator fileLoc = page.Locator("#multipleFilesInput");
            
            var fileUploader = await page.RunAndWaitForFileChooserAsync(async () =>
            {
                await fileLoc.ClickAsync();
            });
            String[] filespath = { "C:\\Users\\Administrator\\source\\repos\\PlayWrightDemo\\PlayWrightDemo\\TestDataFile.txt"
                    , "C:\\Users\\Administrator\\source\\repos\\PlayWrightDemo\\PlayWrightDemo\\TextData.txt" };
            await fileUploader.SetFilesAsync(filespath);
            await page.GetByText("Upload Multiple Files").ClickAsync();
            ILocator messageLoc = page.Locator("#multipleFilesStatus");
            IReadOnlyList<String> message = await messageLoc.AllTextContentsAsync();
            Assert.That(message[0], Does.Contain("TestDataFile.txt"));
            Assert.That(message[0], Does.Contain("TextData.txt"));

        }
        [Test]
        public async Task LImitScopeOfPage()
        {
            await page.GotoAsync("https://testautomationpractice.blogspot.com/p/playwrightpractice.html#");
            ILocator laptops = page.Locator("#laptops");
            ILocator laptopsLinkLoc = laptops.Locator("a[href]");
            for(int i=0;i<await laptopsLinkLoc.CountAsync(); i++)
            {
                WriteLine(await laptopsLinkLoc.Nth(i).TextContentAsync());
            }


        }
        [TearDown]
        public async Task CloseBrowser()
        {
           
            
                await browserManager.TakeScreenshot();
            
            
            await browserManager.DisposeBrowserAynsc();

        }
        

    }
}
