using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CreditCards.UITests.POM;


namespace CreditCards.UITests.BDD.Tests
{
  
    [Binding]
    public class SeeHomepageSteps
    {
        private IWebDriver driver;

        [Given(@"I have navigated to the homepage")]
        public void GivenIHaveNavigatedToTheHomepage()
        {
            driver = new ChromeDriver();
            var homePage = new HomePage(driver);
            homePage.NavigateTo();
        }
        
        [Then(@"I should see the homepage")]
        public void ThenIShouldSeeTheHomepage()
        {
            var homePage = new HomePage(driver);
            homePage.EnsurePageLoaded();            
        }

        //[AfterScenario]
        //public void DisposeWebDriver()
        //{
        //    driver.Dispose();
        //}


    }
}
