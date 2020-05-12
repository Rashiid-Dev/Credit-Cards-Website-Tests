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
        
        [Given(@"I enter a first name of (.*)")]
        public void GivenIEnterMyFirstName(string firstName)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterFirstName(firstName);
        }
        
        [Given(@"I enter a last name of (.*)")]
        public void GivenIEnterMyLastName(string lastName)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterLastName(lastName);
        }
        
        [Given(@"I enter a frequent flyer number of (.*)")]
        public void GivenIEnterMyFrequentFlyerNumber(string number)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterFrequentFlyerNumber(number);
        }
        
        [Given(@"I enter an age of (.*)")]
        public void GivenIEnterMyAge(string age)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterAge(age);
        }
        
        [Given(@"I enter a gross annual income of (.*)")]
        public void GivenIEnterMyGrossAnnualIncome(string income)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterGrossAnnualIncome(income);
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

        [Given(@"I don't enter a last name")]
        public void GivenIDonTEnterALastName()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterLastName("");
        }

        [Then(@"I should see two errors")]
        public void ThenIShouldSeeTwoErrors()
        {
            var applicationPage = new ApplicationPage(driver);
            Assert.Equal(2, applicationPage.ValidationErrorMessages.Count);

            Assert.Contains("Please provide a last name", applicationPage.ValidationErrorMessages);

            Assert.Contains("You must be at least 18 years old", applicationPage.ValidationErrorMessages);
        }

        [Then(@"I enter a last name of (.*)")]
        public void ThenIEnterALastNameOfJama(string lastName)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterLastName(lastName);
        }

        [Then(@"I remove the entered age")]
        public void ThenIRemoveTheEnteredAge()
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.ClearAge();
        }

        [Then(@"I enter an age of (.*)")]
        public void ThenIEnterAnAgeOf(string validAge)
        {
            var applicationPage = new ApplicationPage(driver);
            applicationPage.EnterAge(validAge);
        }


        [AfterScenario]
        public void DisposeWebDriver()
        {
            driver.Dispose();
        }
    }
}
