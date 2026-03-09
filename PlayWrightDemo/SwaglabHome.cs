using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlayWrightDemo
{
    internal class SwaglabHome
    {
        IPage page;
        ILocator pageLogo;
        ILocator productTitle;
        public SwaglabHome(IPage page)
        {
            this.page = page;
            pageLogo = page.Locator(".app_logo");
            productTitle = page.Locator(".title");
           

        }
        public async Task<String> getPageLogo()
        {
           return await  pageLogo.TextContentAsync();
        }

        public async Task<String> getProductPageTitle()
        {
            return await productTitle.TextContentAsync();
        }
    }
}
