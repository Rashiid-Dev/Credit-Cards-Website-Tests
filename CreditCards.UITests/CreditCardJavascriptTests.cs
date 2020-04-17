using System;
using Xunit;
using Xunit.Abstractions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CreditCards.UITests
{
    public class CreditCardJavascriptTests
    {
        private const string HomeUrl = "http://localhost:44108/";
        private const string jsOverlayUrl = "http://localhost:44108/jsoverlay.html";
        private const string HomeTitle = "Home Page - Credit Cards";

        [Fact]
        public void ClickOverlayedLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(jsOverlayUrl);

                //// Gets the link
                //string script = "return document.getElementById('HiddenLink').innerHTML;";
                //// Gets the link text
                //string linktext = (string)js.ExecuteScript(script);

                string script = "document.getElementById('HiddenLink').click();";

                IJavaScriptExecutor js = (IJavaScriptExecutor)driver;

                js.ExecuteScript(script);

                Assert.Equal("https://www.pluralsight.com/", driver.Url);

            }
        }
    }
}
