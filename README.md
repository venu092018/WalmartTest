Prerequisites:
---

- Install Node.js
- Install .Net Core SDK 2.1 from https://www.microsoft.com/net/download/dotnet-core/2.1

I used the following versions locally  
 - Core Sdk Version 2.1.301
 - Core Runtime Version Microsoft.NETCore.App 2.1.1

The project  uses Angular 5 on the client side.

Steps to Build/Run the app:
---
1)	Clone the project from GitHub repository
2)	Change working directory to [repository local path root] 
3)	Change working directory to the web application project
      - cd WalmartTest\WalmarteShopDemo\WalmarteShopDemo
5)	***Open the file appsettings.json and set the ApiKey value to your secret key
6)	Run command - dotnet build
7)	Verify no compile errors are reported
8)	Run command - dotnet run
9)	Open browser and navigate to http://localhost:59074/
10)	In case you want to use a different port number update the value in 
    Properties\launchSettings.json and then re-build the project.

Steps to run Unit tests:
---
There are two Unit test projects in the solution.
 - Walmart.Api.Client.Tests project performs assertions on code which calls the Walmart API.
 - WalmarteShopDemo.Tests project has unit tests for the controllers in my project.
1)	Change working directory to [repository local path root] 
2)	Change working directory to WalmartTest\WalmarteShopDemo\Walmart.Api.Client.Tests
3)	Run command – dotnet test
4)	Change working directory to WalmartTest\WalmarteShopDemo\WalmarteShopDemo.Tests
5)	Run command – dotnet test
