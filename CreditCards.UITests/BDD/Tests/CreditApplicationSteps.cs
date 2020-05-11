using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections.ObjectModel;
using ApprovalTests.Reporters;
using ApprovalTests.Reporters.Windows;
using System.IO;
using ApprovalTests;
using CreditCards.UITests.POM;
using Xunit;
using System.Security.Cryptography;
using System.Threading;
using System.Globalization;

namespace CreditCards.UITests
{
    
    [Binding]
    public class CreditApplicationSteps
    {
        private const string ApplyUrl = "http://localhost:44108/Apply";
        private IWebDriver driver;
        const string FirstName = "Rashiid";
        const string LastName = "Jama";
        const string Number = "123456-A";
        const string Age = "23";
        const string Income = "50000";
        

        [Given(@"I am on the application page")]
        public void GivenIAmOnTheApplicationPage()
        {
            driver = new ChromeDriver();
            var applicationPage = new ApplicationPage(driver);            
            applicationPage.NavigateTo();
        }
        
        [Given(@"I enter my first name")]
        public void GivenIEnterMyFirstName()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterFirstName(FirstName);
        }
        
        [Given(@"I enter my last name")]
        public void GivenIEnterMyLastName()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterLastName(LastName);
        }
        
        [Given(@"I enter my frequent flyer number")]
        public void GivenIEnterMyFrequentFlyerNumber()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterFrequentFlyerNumber(Number);
        }
        
        [Given(@"I enter my age")]
        public void GivenIEnterMyAge()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterAge(Age);
        }
        
        [Given(@"I enter my gross annual income")]
        public void GivenIEnterMyGrossAnnualIncome()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterGrossAnnualIncome(Income);
        }
        
        [Given(@"I enter my marital status as single")]
        public void GivenIEnterMyMaritalStatusAsSingle()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.ChooseMaritalStatusSingle();
        }
        
        [Given(@"I enter the business source as internet")]
        public void GivenIEnterTheBusinessSourceAsInternet()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.ChooseBusinessSourceIntenet();
        }
        
        [Given(@"I accept the terms")]
        public void GivenIAcceptTheTerms()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.ClickAcceptTerms();
        }
        
        [When(@"I submit the application form")]
        public void WhenISubmitTheApplicationForm()
        {
            var applicationPage = new ApplicationPage(driver);
            ApplicationCompletePage applicationCompletePage = applicationPage.SumbitApplication();

        }
        
        [Then(@"I should be taken to the complete application form")]
        public void ThenIShouldBeTakenToTheCompleteApplicationForm()
        {
            //Assert.Equal(driver.Url, ApplyUrl); 
            var applicationPage = new ApplicationPage(driver);
            ApplicationCompletePage applicationCompletePage = new ApplicationCompletePage(driver);
            Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
            Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
            Assert.Equal($"{FirstName} {LastName}", applicationCompletePage.FullName);
            Assert.Equal(Age, applicationCompletePage.Age);
            Assert.Equal(Income, applicationCompletePage.Income);
            Assert.Equal("Single", applicationCompletePage.RelationShipStatus);
            Assert.Equal("Internet", applicationCompletePage.BusinessSource);
        }


        [AfterScenario]
        public void DisposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
