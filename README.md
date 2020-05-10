# Credit Card Website Test Project

> Created By Abdirashiid Jama

## About
This is a Selenium test project that Tests a local Credit Card website

## Project Goal
Setup a Selenium test project that tests the pages/links/buttons/forms etc.

## To Run
- Download & Install [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- Clone the [repo](https://github.com/Rashiid-Dev/Credit-Cards-Website-Tests)
- Open the solution
- Run the program without debugging to start the IIS Express and launch the website
- Run the desired tests

### You will need:
- [Visual Studio](https://visualstudio.microsoft.com/downloads/)
- Automatically Installed Dependecies
- - Selenium.Webdriver
- - Selenium.Webdriver.Chromedriver
- - XUnit
- - XUnit.Runner.Visualstudio
- - ApprovalTests
- - ApprovalUtilities

## Project
- CreditCards Project (Pre-made website to test)
- CreditCards.UITests Project (Tests)
- - POM (Page Object Model) Folder
- - - ApplicationCompletePage.cs (POM for the complete application page)
- - - ApplicationPage.cs (POM for the application page)
- - - HomePage.cs (POM for the homepage)
- - CreditCardApplicationShould.cs (Test navigation to the Application page and completion/submission of form)
- - CreditJavaScriptTests.cs (Contains 1 test to access a link layered over by JS)
- - CreditCardWebAppShould.cs (Tests Various aspects of the Homepage(Links/Buttons/Tables etc.))

## Jira Board
![The Board](https://i.imgur.com/sWO6AQS.jpg)
[Link to board](https://spartaacademyhub.atlassian.net/jira/software/projects/CCWT/boards/2)

## Test Demo Video
[Video](https://drive.google.com/open?id=1G8UDbznXnQIiErA5JhbfN0LPIghrx8UP)

## Refactoring
Most of the project has been refactored.
What is left in CreditCardWebAppShould.cs and CreditCardJavaScriptTests.cs will be refactored in a coming update.

