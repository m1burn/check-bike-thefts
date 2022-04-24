# Check Bike Thefts Coding Assignment

Solution for Check Bike Thefts Coding Assignment

The solution is developed as a web application with 1 page and 1 API endpoint.
To see the web page please run the application and open https://localhost:7026/.
The API is located under https://localhost:7026/StolenBike. Please open Swagger page (https://localhost:7026/swagger) to see more details about the API.

## Layers
Although this coding assignment is relatively small, let's imagine that this is just a beginning and it will grow significantly in the future. In this case, we better make things more organized from the beginning. Especially, because future requirements are not yet known, I decided to go with the so-called Onion Layered Architecture. It let us keep things more organized and less coupled. Especially it keeps things that are highly probable to change in the future from the things that are less likely to change in the future.

I've extracted the following layers:


- **CheckBikeThefts.Web** is ASP.NET MVC application which exposes UI for users. It contains only logic related to web framework
- **CheckBikeThefts.Domain** contains domain model (data and behavior) which represents real-world objects and completely uncoupled of specific application logic
- **CheckBikeThefts.Infrastructure** contains logic to communicate with infrustructure systems, like data storage, web services, etc. For this code assignment, we have only 1 CSV file (cities.csv) and BikeIndexApi as an external web service
- **CheckBikeThefts.UseCases** contains use cases (or scenarios) that the application is supposed to perform. This component connects all other components to do the work
- **CheckBikeThefts.Interfaces** contains interfaces for use across other layers

## Unit Tests
The solution doesn't have 100% unit test coverage, but existing tests rather verify the correctness of the most important pieces. You will find unit tests in projects with the prefix UnitTests.

## Requirements
- .NET 6
- Jetbrains Rider or Microsoft Visual Studio