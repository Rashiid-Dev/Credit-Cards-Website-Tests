using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));

                applyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            }
        }
    }
}
