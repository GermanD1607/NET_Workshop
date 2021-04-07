using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETTraining.Pages
{
    class Categories
    {
        private IWebDriver driver;

        private By title = By.CssSelector("h1");
        private By menu = By.CssSelector(".cms-facets");

        public Categories(IWebDriver driver)
        {
            this.driver = driver;
        }

        public String GetTitle()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(menu);
            });
            return driver.FindElement(title).Text;
        }
    }
}
