The Togolese Association System is designed to enable end users or administrators to:

Create new members
Update existing members
View member details
Retrieve all members or filter members by name.
Each member of the Association can make annual contributions (membership) and donate to event-specific contributions such as birthdays, funerals, etc. The application tracks annual contributions for each member from the time they join onward.

To achieve this, an API and a Blazor WebAssembly front-end have been developed. The API exposes RESTful HTTP endpoints, allowing the Blazor WebAssembly front-end to interact with it using ASP.NET Core (Target Framework 8.0) and C#.

Installation:

Clone the repository.
Navigate to the project directory.
Restore the NuGet packages.
Optionally, run update-database in the Package Manager Console to facilitate table mapping.
Solution Structure
Back-End:

-TogoleseAssociationSystem.API
-TogoleseAssociationSystem.Application
-TogoleseAssociationSystem.Infrastructure
-TogoleseAssociationSystem.Domain
Testing Projects:

-TogoleseAssociationSystem.API.Tests
-TogoleseAssociationSystem.Application.Tests
-TogoleseAssociationSystem.Infrastructure.Tests
Front-End:

-TogoleseAssociationSystem.Core
-TogoleseAssociationSystem.APP
Testing Projects:

-TogoleseAssociationSystem.Core.Tests
-TogoleseAssociationSystem.APP.Tests
Running the Application
To run the application, set the solution property page to ‘Multiple startup projects’:

Right-click on the global project solution at the top.
Select ‘Properties’.
In the properties window, ensure both TogoleseAssociationSystem.API and TogoleseAssociationSystem.APP actions are set to ‘Start’.
Apply the changes to run both the API and UI projects simultaneously for your exploratory testing.
