using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETTraining.Pages
{
    class Home
    {
        private IWebDriver driver;

        private By searchBar = By.CssSelector(".site-search__controls__input");
        private By btnSearch = By.CssSelector(".site-search__controls__submit");
        private By btnLogin = By.CssSelector(".site-header__nav__link.site-header__nav__link--login");
        private By icon = By.XPath("//li/div[@class='site-header__nav__link__icon']");
        private By btnCategory = By.CssSelector(".category-flyout-header__link");
        private By btnOut = By.XPath("//li/a/div[@class='size-80 opacity-70']");
        private By result = By.XPath("//div[@class='category-flyout__column__section']/h3/a");

        public Home(IWebDriver driver)
        {
            this.driver = driver;
        }
        public LogIn OpenLogin()
        {
            driver.FindElement(btnLogin).Click();
            return new LogIn(driver);
        }
        public void SelectCategory()
        {
            driver.FindElements(btnCategory)[4].Click();
        }
        public Categories SelectElement()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(result);
            });
            driver.FindElement(result).Click();
            return new Categories(driver);
        }
        public Results Search(String words)
        {
            IWebElement bar = driver.FindElement(searchBar);
            bar.SendKeys(words);
            driver.FindElement(btnSearch).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(By.CssSelector(".facet-container"));
            });
            return new Results(driver);
        }
        public IWebElement GetUserIcon()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(icon);
            });
            return driver.FindElement(icon);
        }
        public IWebElement GetLoginButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(btnLogin);
            });
            return driver.FindElement(btnLogin);
        }
        public void LogOut()
        {
            driver.FindElement(icon).Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(btnOut);
            });
            driver.FindElement(btnOut).Click();
        }
    }
}
