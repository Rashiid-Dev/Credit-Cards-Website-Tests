using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
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

        private readonly ITestOutputHelper output;

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
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


                //prevents accidental hovering over carousel so that it does move
                driver.Manage().Window.Minimize();

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

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Navigating to '{HomeUrl}'");
                driver.Navigate().GoToUrl(HomeUrl);

                //IWebElement CarouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));

                //CarouselNext.Click();

                //Thread.Sleep(1000);

                //WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                //CarouselNext.Click();

                //Thread.Sleep(1000);


                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Finding the element using explicit wait");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));

                IWebElement applyNowLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.ClassName("customer-service-apply-now")));

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element displayed={applyNowLink.Displayed} Enabled={applyNowLink.Enabled}");

                output.WriteLine($"{DateTime.Now.ToLongTimeString()} Clicking the element");

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

        [Fact]
        public void BeSubmittedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(ApplyUrl);

                driver.FindElement(By.Id("FirstName")).SendKeys("Rashiid");
                //IWebElement firstNameTextBox = driver.FindElement(By.Id("FirstName")).;
                //firstNameTextBox.SendKeys("Rashiid");
                driver.FindElement(By.Id("LastName")).SendKeys("Jama");
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                driver.FindElement(By.Id("Age")).SendKeys("23");
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                driver.FindElement(By.Id("Single")).Click();
                IWebElement howDidYouHearDropDown = driver.FindElement(By.Id("BusinessSource"));
                SelectElement selection = new SelectElement(howDidYouHearDropDown);

                //foreach(IWebElement option in selection.Options)
                //{
                //  if(option.GetAttribute("value") == "Internet")
                //    {
                //        option.Click();
                //    }
                //}

                // Different ways of selecting the same option
                selection.SelectByValue("Internet");
                //selection.SelectByIndex(1);
                //selection.SelectByText("Internet Search");

                driver.FindElement(By.Id("TermsAccepted")).Click();
                driver.FindElement(By.Id("SubmitApplication")).Click();

                // Alternative way of submitting a form
                //driver.FindElement(By.Id("FirstName")).Submit();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.Equal("Rashiid Jama", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("23", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("Internet", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }

        [Fact]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                const string firstName = "Rashiid";
                const string lastName = "Jama";
                const string invalidAge = "17";
                const string validAge = "23";

                driver.Navigate().GoToUrl(ApplyUrl);

                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);

                // Not entering last name

                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");

                driver.FindElement(By.Id("Age")).SendKeys(invalidAge);

                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");

                driver.FindElement(By.Id("Single")).Click();

                IWebElement howDidYouHearDropDown = driver.FindElement(By.Id("BusinessSource"));

                SelectElement selection = new SelectElement(howDidYouHearDropDown);

                selection.SelectByValue("Internet");
               
                driver.FindElement(By.Id("TermsAccepted")).Click();

                driver.FindElement(By.Id("SubmitApplication")).Click();

                // Asserting that validation failed
                var validationErrors = driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));

                Assert.Equal(2, validationErrors.Count);

                Assert.Equal("Please provide a last name", validationErrors[0].Text);

                Assert.Equal("You must be at least 18 years old", validationErrors[1].Text);

                // Fix Errors
                driver.FindElement(By.Id("LastName")).SendKeys(lastName);

                driver.FindElement(By.Id("Age")).Clear();

                driver.FindElement(By.Id("Age")).SendKeys(validAge);

                // Resubmit form
                driver.FindElement(By.Id("SubmitApplication")).Click();

                Assert.StartsWith("Application Complete", driver.Title);

                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);

                Assert.Equal("Rashiid Jama", driver.FindElement(By.Id("FullName")).Text);

                Assert.Equal("23", driver.FindElement(By.Id("Age")).Text);

                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);

                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);

                Assert.Equal("Internet", driver.FindElement(By.Id("BusinessSource")).Text);

            }
        }
        [Fact]
        public void OpenContactFooterLinkNewTab()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(HomeUrl);

                driver.FindElement(By.Id("ContactFooter")).Click();

                var allTabs = driver.WindowHandles;
                string homePageTab = allTabs[0];
                string contactTab = allTabs[1];

                driver.SwitchTo().Window(contactTab);
                
                Assert.EndsWith("/Home/Contact", driver.Url);
            }
        }

    }
}
