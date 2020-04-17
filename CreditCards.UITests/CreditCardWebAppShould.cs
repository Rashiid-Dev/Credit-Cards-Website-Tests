using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string HomeTitle = "Home Page - Credit Cards";
        private const string AboutUrl = "http://localhost:44108/Home/About";


        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
        {   using (IWebDriver driver = new ChromeDriver())
            {
                
                driver.Navigate().GoToUrl(HomeUrl);

                driver.Manage().Window.Maximize();

                // Window manipulations
                //driver.Manage().Window.Size(300, 400);
                //driver.Manage().Window.Position = new System.Drawing.Size(1,1);
                //driver.Manage().Window.FullScreen();

                Assert.Equal(HomeTitle, driver.Title);

                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {

                driver.Navigate().GoToUrl(HomeUrl);

                driver.Navigate().Refresh();

                Assert.Equal(HomeTitle, driver.Title);

                Assert.Equal(HomeUrl, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement generationTokenElement = driver.FindElement(By.Id("GenerationToken"));

                string initialToken = generationTokenElement.Text;

                driver.Navigate().GoToUrl(AboutUrl);

                driver.Navigate().Back();

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnForward()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(AboutUrl);           

                driver.Navigate().GoToUrl(HomeUrl);

                string initialToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().Back();

                driver.Navigate().Forward();

                string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.NotEqual(initialToken, reloadedToken);

                //Assert.Equal(HomeTitle, driver.Title);

                //Assert.Equal(HomeUrl, driver.Url);

            }
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);


                ReadOnlyCollection<IWebElement> tableCells = driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);

                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
               
            }
        }

        [Fact]
        public void NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));

                driver.Navigate().Refresh();

                //returns empty rather than throw an exeption if there is none
                ReadOnlyCollection<IWebElement> message = driver.FindElements(By.Id("CookiesBeingUsed"));

                Assert.Empty(message);

                // Gets cookie
                //Cookie cookieValue = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                //Assert.Equal("true", cookieValue.Value);

                // Deletes cookie
                //driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");


            }
        }
    }
}
