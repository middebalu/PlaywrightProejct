using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Playwright;

namespace PlayWrightDemo
{
    internal class SwagLabLoginPage
    {
        private IPage page;
        private ILocator userNameTextBox;
        private ILocator passwordTextbox;
        private ILocator loginBtn;

        public SwagLabLoginPage(IPage page)
        {
            this.page = page;
            userNameTextBox=page.GetByPlaceholder("Username");
            passwordTextbox = page.GetByPlaceholder("Password");
            loginBtn = page.GetByText("Login");

        }

        public async Task LoginAsync(String userName,String password)
        {
            await userNameTextBox.FillAsync(userName);
            await passwordTextbox.FillAsync(password);
            await loginBtn.ClickAsync();
        }
    }
}
