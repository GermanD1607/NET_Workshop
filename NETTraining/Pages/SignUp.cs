using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace NETTraining.Pages
{
    class SignUp
    {
        private IWebDriver driver;

        private By firstName = By.Id("user_first_name");
        private By lastName = By.Id("user_last_name");
        private By email = By.Id("user_email");
        private By password = By.Id("user_password");
        private By btnSignUp = By.CssSelector(".session-form .button.button--orange.width-100");

        public SignUp(IWebDriver driver)
        {
            this.driver = driver;
        }

        public Home Register(String fn, String ln, String pass, String mail)
        {

            driver.FindElement(firstName).SendKeys(fn);
            driver.FindElement(lastName).SendKeys(ln);
            driver.FindElement(email).SendKeys(mail);
            driver.FindElement(password).SendKeys(pass);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(btnSignUp);
            });
            driver.FindElement(btnSignUp).Click();
            return new Home(driver);
        }
    }
}
