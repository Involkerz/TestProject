Feature: Manage computers

Background: 
Given User starts browser and navigates to the Computers page

@AddComputer @Smoke
Scenario: Add new computer
	When User adds new computer on the Computers page
	| ComputerName | Introduced | Discontinued | Company |
	| random       |            |              | RCA     |
	Then User checks that 'add' notification message is present 
	   * User checks that computer is present on the Computers page

@AddComputer
Scenario: Add new computer with invalid data
	Given User clicks Add a new computer on the Computers page
	When User configures new computer on Add New Computer page
	| ComputerName | Introduced | Discontinued | Company |
	|              | 1          | 2            |         |
	   * User confirms new computer creation on Add New Computer page
	Then User checks that all validation errors are present on Add New Computer page

@DeleteComputer @Smoke
Scenario: Delete existing computer
	Given User adds new computer on the Computers page
	| ComputerName | Introduced | Discontinued | Company |
	| random       | 1987-12-30 | 2000-12-30   |         |
	When User deletes created computer on the Computers page
	Then User checks that 'delete' notification message is present 
	   * User checks that computer is not present on the Computers page

@ModifyComputer @Smoke
Scenario: Edit existing computer
	Given User adds new computer on the Computers page
	| ComputerName | Introduced | Discontinued | Company |
	| random       | 1987-12-30 | 2000-12-30   | RCA     |
	When User edits computer on the Computers page
	| ComputerName | Introduced | Discontinued | Company |
	| random       | 1901-05-15 | 1999-01-01   | IBM     |
	Then User checks that 'edit' notification message is present 
	   * User checks that computer is present on the Computers page
	   * User checks that old computer is not present on the Computers page
