Feature: LoginUser

A short summary of the feature

Scenario: Login the new user
	Given I'm on the login page
	When I enter "test@gmail.com" as the email of the new user
	And I enter "Password123!" as the password of the new user
	And I submit the form
	Then I should be redirected to the home page
