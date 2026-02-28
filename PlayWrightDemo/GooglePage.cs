using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlayWrightDemo
{
    internal class GooglePage
    {                          
        IPage page;
        public GooglePage(IPage page)
        {
            this.page = page;
        }
        
        public async Task<String> GetTitleAsync()
        {
            return await page.Locator("xpath=/html/head/title").TextContentAsync();
        }
    }
}
