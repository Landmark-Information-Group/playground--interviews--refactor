# Landmark Developer Coding Test

#### Scenario:
One of your colleagues has written a simple and rather dirty Proof of Concept Web-API application. Your task is to refactor the application to production level quality. This includes the following:
- Ensure the code follows SOLID principles
- Ensure the code can be easily unit testable
- Change the current XML API response to JSON
- Add some unit tests

#### Application description
The application gets flood data from the open-source Environment Agency API, transforms it into a different model and serves it in a simple RESTful API. There are two endpoints; one returns all the flood alerts and one that filters them by EaAreaName.

#### Application code
The code can be downloaded from the main branch of this repository.

#### Additional information
- The unit tests can be written using any .NET compatible framework
- This is not a timed task. We are interested in determining a candidates idea of well written, clean code and the ability to determine and code sensible, isolated unit tests.
- In the unlikely event that the Environment Agency API does not return any flood alerts we have included an example of the response data with the application code.
