using System;
using System.Threading;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using CreditCards.UITests.POM;

namespace CreditCards.UITests
{
    [Trait("Category", "Applications")]
    public class CreditCardApplicationShould
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private const string HomeTitle = "Home Page - Credit Cards";
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
                var homePage = new HomePage(driver);

                homePage.NavigateTo();

                ApplicationPage applicationPage = homePage.ClickApplyNewLowRate();

                applicationPage.EnsurePageLoaded();
            } 
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);

                homePage.NavigateTo();

                homePage.WaitForEasyApplicationCarouselPage();

                ApplicationPage applicationPage = homePage.ClickEasyApplicationLink();

                applicationPage.EnsurePageLoaded();
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
                const string FirstName = "Rashiid";
                const string LastName = "Jama";
                const string Number = "123456-A";
                const string Age = "23";
                const string Income = "50000";

                var applicationPage = new ApplicationPage(driver);

                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(FirstName);
                applicationPage.EnterLastName(LastName);
                applicationPage.EnterFrequentFlyerNumber(Number);
                applicationPage.EnterAge(Age);
                applicationPage.EnterGrossAnnualIncome(Income);
                applicationPage.ChooseMaritalStatusSingle();
                applicationPage.ChooseBusinessSourceIntenet();
                applicationPage.ClickAcceptTerms();

                ApplicationCompletePage applicationCompletePage = applicationPage.SumbitApplication();

                //applicationCompletePage.EnsurePageLoaded();

                Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
                Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
                Assert.Equal($"{FirstName} {LastName}", applicationCompletePage.FullName);
                Assert.Equal(Age, applicationCompletePage.Age);
                Assert.Equal(Income, applicationCompletePage.Income);
                Assert.Equal("Single", applicationCompletePage.RelationShipStatus);
                Assert.Equal("Internet", applicationCompletePage.BusinessSource);
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
                const string number = "123456-A";
                const string income = "50000";

                var applicationPage = new ApplicationPage(driver);

                applicationPage.NavigateTo();

                applicationPage.EnterFirstName(firstName);

                // Not entering last name

                applicationPage.EnterFrequentFlyerNumber(number);

                applicationPage.EnterAge(invalidAge);

                applicationPage.EnterGrossAnnualIncome(income);

                applicationPage.ChooseMaritalStatusSingle();

                applicationPage.ChooseBusinessSourceIntenet();

                applicationPage.ClickAcceptTerms();

                applicationPage.SumbitApplication();

                // Asserting that validation failed

                Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);

                Assert.Contains("Please provide a last name", applicationPage.ValidationErrorMessages);

                Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);

                // Fix Errors
                applicationPage.EnterLastName(lastName);

                applicationPage.ClearAge();

                applicationPage.EnterAge(validAge);

                // Resubmit form
                ApplicationCompletePage applicationCompletePage =  applicationPage.SumbitApplication();

                //applicationCompletePage.EnsurePageLoaded();

            }
        }

    }
}
