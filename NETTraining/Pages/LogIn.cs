using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETTraining.Pages
{
    class LogIn
    {
        private IWebDriver driver;

        private By userName = By.Id("user_session_login");
        private By userPass = By.Id("user_session_password");
        private By btnLogin = By.CssSelector(".session-form .button.button--orange.width-100");

        public LogIn(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Home Log(String name, String pass)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(userName);
            });
            driver.FindElement(userName).SendKeys(name);
            driver.FindElement(userPass).SendKeys(pass);
            driver.FindElement(btnLogin).Click();
            return new Home(driver);
        }
    }
}
