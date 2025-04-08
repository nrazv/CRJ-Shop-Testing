Feature: User



Scenario: Sign Up new user
	Given I'm on the register page
	When I enter "test@gmail.com" as the email
	And I enter "Test" as the first name
	And I enter "Joe" as the last name
	And I enter "Street Test" as the address
	And I enter "Password123!" as the password
	And I enter "Password123!" as the password confirmation
	And I submit the form data
	Then I should be redirected to the login page

Scenario: User can't sign up a new user with an existing email
	Given I am on the register page
	And I try to register a user with an existing email
	And I click register
	Then I should see an error message