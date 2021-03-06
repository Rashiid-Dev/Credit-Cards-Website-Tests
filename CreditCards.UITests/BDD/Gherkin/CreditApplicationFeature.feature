﻿Feature: Credit Application
	In order to get credit
	as a new customer
	I want to be able to complete an application


Scenario: Valid Application
	Given I am on the application page
	And I enter a first name of Rashiid
	And I enter a last name of Jama
	And I enter a frequent flyer number of 123456-A
	And I enter an age of 23
	And I enter a gross annual income of 50000
	And I enter my marital status as single
	And I enter the business source as internet
	And I accept the terms
	When I submit the application form
	Then I should be taken to the complete application form

Scenario: Invalid Application
	Given I am on the application page
	And I enter a first name of Rashiid
	And I don't enter a last name
	And I enter a frequent flyer number of 123456-A
	And I enter an age of 14
	And I enter a gross annual income of 50000
	And I enter my marital status as single
	And I enter the business source as internet
	And I accept the terms
	When I submit the application form
	Then I should see two errors
	Then I enter a last name of Jama
	Then I remove the entered age
	Then I enter an age of 23
	When I submit the application form
	Then I should be taken to the complete application form
	
