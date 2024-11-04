# UrlShorteningService
UrlShorteningService

# URL Shortening Service

A simple URL shortening service built using ASP.NET Core. This service allows users to shorten long URLs and retrieve the original URLs using a unique short ID.

## Features

- Shorten long URLs and generate a unique short ID.
- Retrieve the original long URL using the short ID.
- Basic URL validation to ensure well-formed URLs are processed.
- In-memory storage for URL mappings (temporary, suitable for development/testing).

## Requirements

- [.NET 8.0](https://dotnet.microsoft.com/download/dotnet/8.0) or later
- A suitable IDE or text editor (e.g., Visual Studio, Visual Studio Code)

## Installation

1. **Clone the repository**:
   ```bash
   [git clone https://github.com/yourusername/UrlShortener.git](https://github.com/sryeruva/UrlShorteningService.git)
   cd UrlShortener
2. dotnet restore
3. dotnet run

The API will be accessible at https://localhost:7055/api/UrlShortener.


Design Choices for the URL Shortening Service
1.	Modeling with Classes:
o	The UrlMapping class is used to represent the mapping between the original long URL and its shortened version. This class contains properties for the short ID, long URL, access count, and the full short URL for easy retrieval.
2.	In-Memory Storage:
o	A ConcurrentDictionary<string, UrlMapping> is utilized to store the mappings in memory. This choice allows for thread-safe access to the data structure, making it suitable for handling multiple concurrent requests. However, it's important to note that this approach will lose all mappings if the application restarts, which may not be acceptable for a production-level service.
3.	Short ID Generation:
o	The short IDs are generated using a simple method that encodes the original URL using Base64 encoding, followed by truncating the result to the first 6 characters. This approach provides a quick and straightforward way to generate unique identifiers. However, it is not collision-proof and could lead to duplicates if different long URLs generate the same base64-encoded substring. For a production system, a more robust method (like a hash function with collision handling or a sequential ID generator) would be advisable.
4.	URL Validation:
o	The service includes URL validation to ensure that only well-formed HTTP or HTTPS URLs are processed. This prevents invalid URLs from being stored and improves the overall reliability of the service.
5.	API Design:
o	The API exposes two endpoints: one for shortening URLs and another for retrieving the long URL from the short ID. The design emphasizes simplicity and clarity, making it easy for clients to interact with the service.
Considerations for Scalability
1.	Data Persistence:
o	Since the current implementation uses in-memory storage, it lacks data persistence, which poses a scalability issue. Transitioning to a persistent database (like SQL Server, MongoDB, or Redis) would enable the service to store mappings permanently and scale to handle a larger number of users and requests.
2.	Load Balancing:
o	For a scalable architecture, implementing load balancing would be essential. This can be achieved by deploying multiple instances of the service behind a load balancer, which would distribute incoming requests evenly across instances.
3.	Unique Short ID Generation:
o	To avoid collisions and ensure uniqueness in short IDs, a more sophisticated generation strategy could be implemented. This could include using a sequential ID with base conversion or a UUID, combined with a database to keep track of existing IDs.
4.	Caching:
o	Implementing caching mechanisms (e.g., Redis) can help reduce database load and improve response times for frequently accessed URLs. This would be particularly beneficial for high-traffic scenarios.
5.	Analytics and Monitoring:
o	Adding logging and analytics capabilities can provide insights into usage patterns, helping to inform further scalability and performance optimizations.
6.	Microservices Architecture:
o	For larger applications, considering a microservices architecture may facilitate scaling specific components independently (e.g., a dedicated service for URL generation or analytics).
By considering these design choices and scalability factors, the URL shortening service can be effectively built to handle both current needs and future growth. If you have any questions or need further details, feel free to ask!


Explanation of Tests
1.	ShortenUrl_ValidUrl_ReturnsMapping:
o	Tests that a valid URL can be shortened and that the returned mapping contains the correct properties.
2.	GetLongUrl_ValidShortId_ReturnsLongUrl:
o	Tests that a valid short ID correctly returns the original long URL.
3.	GetLongUrl_InvalidShortId_ReturnsNull:
o	Tests that an invalid short ID returns null, indicating that the mapping does not exist.
	
Running the Tests
To run the tests, Right click on test file , click run tests, it will run all tests in the file:







