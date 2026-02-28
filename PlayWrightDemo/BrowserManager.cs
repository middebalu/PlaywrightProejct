using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlayWrightDemo
{
    internal class BrowserManager
    {
        private IPlaywright playwright;
        private IBrowser browser;
        private IBrowserContext context;
        private IPage page;

        public async Task<IPage> getPageAsync()
        {
            page = await context.NewPageAsync();
            return page;
        }

        public async Task<IBrowserContext> getBrowserContxtAsync()
        {
            context = await browser.NewContextAsync();

            return context;
        }
        public async Task InitiBrowserAsync(String browser) {
            
                playwright = await Playwright.CreateAsync();
                if (browser == "chrome") {
                    this.browser= await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
                } else if (browser == "webkit"){
                    this.browser =await playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false, SlowMo = 1000 });
                }
           
                
        }

        public async Task DisposeBrowserAynsc()
        {
            await page.CloseAsync();
            await context.CloseAsync();
            await browser.CloseAsync();
            playwright.Dispose();
        }
    }
}
