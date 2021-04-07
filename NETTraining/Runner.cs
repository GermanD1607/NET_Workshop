using NETTraining.Pages;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using RestSharp;
using System;

namespace NETTraining
{
    [TestFixture]
    public class APITestSuite
    {
        public string texto { get; set; }

        [Test]
        public void APITest()
        {
            var client = new RestClient();
            var request = new RestRequest("joke/Any", Method.GET);
            client.BaseUrl = new Uri("https://jokeapi.dev/");
            var response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }

    [TestFixture]
    public class UITestSuite
    {
        public IWebDriver driver;
        private String url="https://reverb.com";

        [OneTimeSetUp]
        public void BeforeTest()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [OneTimeTearDown]
        public void AfterTest()
        {
            driver.Quit();
        }

        public void CloseCookies()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver =>
            {
                return driver.FindElement(By.CssSelector(".intl-settings-nag__close"));
            });
        }

        [Test]
        public void SignUpTest()
        {
            driver.Navigate().GoToUrl(url + "/signup");
            SignUp signUp = new SignUp(driver);
            CloseCookies();
            Home home = signUp.Register("Pepito", "Perez",
                    "contrase;a", "pepito@endava.com");
            Assert.NotNull(home.GetUserIcon());

            home.LogOut();
        }
        [Test]
        public void ValidLogInTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            LogIn login = home.OpenLogin();
            home = login.Log("pepito@endava.com", "contrase;a");
            Assert.NotNull(home.GetUserIcon());

            home.LogOut();
        }
        [Test]
        public void InvalidLogInTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            LogIn login = home.OpenLogin();
            string email = "invalid@mail";
            home = login.Log(email, "contrase;a");
            Assert.IsTrue(home.GetLoginButton().Displayed);
        }
        [Test]
        public void SearchTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            Results results = home.Search("Fender Stratocaster");
            CloseCookies();
            Assert.IsTrue(results.GetItemTitle(0).Contains("Fender Stratocaster"));
        }
        [Test]
        public void CategoriesTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            home.SelectCategory();
            Categories categories = home.SelectElement();
            Assert.AreEqual(categories.GetTitle(), "Acoustic Drums");
        }
        [Test]
        public void FilterPriceTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            Results results = home.Search("Gibson Les Paul");
            CloseCookies();
            results.Filter();
            Assert.IsTrue(driver.Url.Contains("price_min=1200&price_max=2300"));
        }
        [Test]
        public void OrderPriceTest()
        {
            driver.Navigate().GoToUrl(url);
            Home home = new Home(driver);
            Results results = home.Search("Condenser Microphones");
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver =>
            {
                return driver.FindElement(By.CssSelector(".price-display"));
            });
            CloseCookies();
            results.Order();
            Assert.IsTrue(results.GetPrice(4) > results.GetPrice(5));
        }
    }
}
