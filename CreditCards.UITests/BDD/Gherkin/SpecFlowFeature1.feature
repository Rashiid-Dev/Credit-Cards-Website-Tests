Feature: Credit Application
	In order to get credit
	as a new customer
	I want to be able to complete an application

@mytag
Scenario: Valid Application
	Given I am on the application page
	And I enter my first name
	And I enter my last name
	And I enter my frequent flyer number
	And I enter my age
	And I enter my gross annual income
	And I enter my marital status as single
	And I enter the business source as internet
	And I accept the terms
	When I submit the application form
	Then I should be taken to the complete application form
