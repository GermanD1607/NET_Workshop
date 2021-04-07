using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETTraining.Pages
{
    class Results
    {
        private IWebDriver driver;

        private By minPriceInput = By.Id("minValue");
        private By maxPriceInput = By.Id("maxValue");
        private By filterPriceBtn = By.CssSelector(".facet button");
        private By priceTag = By.CssSelector(".price-display");
        private By options = By.Id("sort");
        private By option = By.CssSelector("select option");
        private By itemTitle = By.CssSelector(".grid-card__title");

        public Results(IWebDriver driver)
        {
            this.driver = driver;
        }

        public String GetItemTitle(int n)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(options);
            });
            return driver.FindElements(itemTitle)[n].Text;
        }
        public Double GetPrice(int n)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(priceTag);
            });
            return Double.Parse(driver.FindElements(priceTag)[n].Text.Replace("[^a-zA-Z0-9.]", ""));
        }
        public void Filter()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(filterPriceBtn);
            });
            driver.FindElements(minPriceInput)[0].SendKeys("1200");
            driver.FindElements(maxPriceInput)[0].SendKeys("2300");
            driver.FindElements(filterPriceBtn)[3].Click();
            wait.Until(driver =>
            {
                return driver.FindElement(itemTitle);
            });
        }
        public void Order()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(options);
            });
            driver.FindElement(options).Click();
            driver.FindElements(option)[3].Click();
        }
    }
}
