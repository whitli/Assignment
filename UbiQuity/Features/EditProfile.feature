Feature: EditProfile
@mytag
Scenario: Update profile details
	Given I am on Edit Profile page
	When Update the following details <emailAdress> <dateOfBirth> <password> in Edit Profile page
	And I click on Submit button
	Then I should be able to see successfully message 'Update is successful! Please see updated information below' on the page
	And I should be able to see updated Data <emailAdress> <dateOfBirth> <password> in in Edit Profile page
	Examples: 
	| emailAdress | dateOfBirth | password |
	| trytest@gamilc.om   | 12/01/2001 | 123      |

