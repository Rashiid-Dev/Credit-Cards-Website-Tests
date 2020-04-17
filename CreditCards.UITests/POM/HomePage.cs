﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CreditCards.UITests.POM
{
    class HomePage
    {
        private readonly IWebDriver Driver;
        private const string PageUrl = "http://localhost:44108/";
        private const string PageTitle = "Home Page - Credit Cards";


        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();

                var productCells = Driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count - 1; i += 2)
                {
                    string name = productCells[i].Text;

                    string interestRate = productCells[i + 1].Text;

                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;

            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }

            bool pageHasLoaded = urlIsCorrect && (Driver.Title == PageTitle);

            if(!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = '{Driver.Url}' Page Source: \r\n {Driver.PageSource}");
            }
        }

        public void WaitForEasyApplicationCarouselPage()
        {

            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(11));

            IWebElement easyApplyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));

        }

        public string GenerationToken => Driver.FindElement(By.Id("GenerationToken")).Text;

        public void ClickContactFooterLink() => Driver.FindElement(By.Id("ContactFooter")).Click();

        public void ClickLiveChatFooterLink() => Driver.FindElement(By.Id("LiveChat")).Click();

        public void ClickLearnMoreAboutUsLink() => Driver.FindElement(By.Id("LearnAboutUs")).Click();

        public bool IsCookieMeesagePresent() => Driver.FindElements(By.Id("CookiesBeingUsed")).Any();

        public ApplicationPage ClickApplyNewLowRate() 
        {
            Driver.FindElement(By.Name("ApplyLowRate")).Click();
            return new ApplicationPage(Driver);
        }
            

        public ApplicationPage ClickEasyApplicationLink()
        {
            Driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();
            return new ApplicationPage(Driver);
        }

    }

    //easy app navigate carousel
    //IWebElement CarouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
    //CarouselNext.Click();



}
