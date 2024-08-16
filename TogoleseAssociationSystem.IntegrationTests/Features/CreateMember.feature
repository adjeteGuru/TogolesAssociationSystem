Feature: CreateMember

As an admin user 
I want to create members through Togolese Associtation App
So that any member on the list can be displayed

@tag1
Scenario: When the admin user creates a member, then the newly created member is stored in the database 
	Given the following contribution to associate with member exist
	| ContributionName | Amount | DateOfContribution   | IsAnnualContribution | MemberId |
	| TestContribute   | 20     | 8/8/2024 12:00:00 AM | false                |          |
	When the user creates a member with the following properties
	| Firstname | Surname | Address         | Postcode | City       | Telephone   | Title | IsActive | IsChair | DateOfBirth | Nextofkin | Relationship | Photo | MembershipDate       |
	| TestName  | DoeTest | 34 Bentley road | BR3 1AS  | Birmingham | 07458893212 | Mr    | True     | False   | 31-Jan-2000 | Brenda    | sister       | 0x    | 8/8/2024 12:00:00 AM |
	Then an alert message indicating that the member with name 'Test'was successfully created is displayed
		And the Member with the following properties is displayed on the list page
		| Firstname | Lastname | Address         | Postcode | City       | Telephone   | Title | IsActive | IsChair | DateOfBirth | Nextofkin | Relationship | Photo | MembershipDate       | MembershipContributions |
		| TestName  | DoeTest  | 34 Bentley road | BR3 1AS  | Birmingham | 07458893212 | Miss  | True     | False   | 31-Jan-2000 | Brenda    | sister       | 0x    | 8/8/2024 12:00:00 AM | TestContribute          |
