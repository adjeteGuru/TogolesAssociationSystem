Feature: 1.GetMembers
As an admin user 
I want to retrieve all member list
So that I can display them on the UI

@tag1
Scenario: 1.1. When admin sends request to get members, then the member list is returned
	Given the following contribution to associate with member exist	
		| id                                   | firstname | lastname | address         | postcode | city       | telephone   | title | isActive | isChair | dateOfBirth | nextofkin | relationship | photo | membershipDate       |
		| 41acf485-1607-4931-8234-2c6821386698 | Test  | DoeTest  | 34 Bentley road | BR3 1AS  | Birmingham | 07458893212 | Mr    | True     | False   | 31-Jan-2000 | Brenda    | sister       | 0x    | 8/8/2024 12:00:00 AM |
	When the user sends request to get members
	Then the response with 'Ok' status code is returned
		And the list of members is returned