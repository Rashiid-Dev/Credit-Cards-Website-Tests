using System;
using System.Threading;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement CarouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));

                CarouselNext.Click();

                // will click the button if it shows up less than a second and timeout & throw an exception if it takes more than a second
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                //Thread.Sleep(1000); // allow carousel time to scroll

                IWebElement easyApplyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));

                easyApplyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_Prebuilt_Conditions()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement CarouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));

                CarouselNext.Click();

                // will click the button if it shows up less than a second and timeout & throw an exception if it takes more than a second
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                //Thread.Sleep(1000); // allow carousel time to scroll

                IWebElement easyApplyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));

                easyApplyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement CarouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));

                CarouselNext.Click();

                Thread.Sleep(1000);

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                CarouselNext.Click();

                Thread.Sleep(1000);

                IWebElement applyNowLink = driver.FindElement(By.ClassName("customer-service-apply-now"));

                applyNowLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);

            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("- Apply Now!"));

                randomGreetingApplyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);

            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("/html/body/div/div[4]/div/p/a"));

                randomGreetingApplyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);

            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_RG_XPATH()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                IWebElement randomGreetingApplyLink = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));

                randomGreetingApplyLink.Click();

                Assert.Equal(driver.Url, ApplyUrl);

                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);

            }
        }
    }

    
}
