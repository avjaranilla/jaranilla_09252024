# jaranilla_09252024
Pizza API Project
Overview
This project is a RESTful API for managing pizza records. It supports operations for adding, updating and retrieving pizza entries. Additionally, it provides functionality for bulk uploading pizza data from JSON files and logging the processing details.

FEATURES
Basic Operations: Create, Read, Update records
File Uploading: Upload JSON files to bulk add pizza records.
Logging: Track file processing details, including processing time and transaction counts.
Dynamic Filtering: Retrieve active or inactive pizzas based on query parameters.


TECHNOLOGIES USED
ASP.NET Core 8: Framework for building the API.
Entity Framework Core: ORM for database interactions.
EF Core InMemoryDB: Act as DB for storing pizza records and processing logs. Note: When Application is closed DB will be reset.
C#: Programming language used for the implementation.

PROJECT STRUCTURE
Api Layer: Contains controllers for handling API requests.
Application Layer: Contains services and interfaces for business logic.
Domain Layer: Contains entity models
Infrastructure Layer: Contains data access implementations and repositories.

STEPS TO RUN
1. Clone the repository : https://github.com/avjaranilla/jaranilla_09252024.git
2. Open sln file in Visual Studio
3. Set StartUp project to jaranilla_09252024 (api layer)
4. Build and Run the application
5. Access the API - navigate to http://localhost:5000/swagger/index.html

AUTHENTICATION
The API uses API-KEY based authentication to secure access to its endpoints. Follow these steps to authenticate:
1. Authorize using X-API-KEY : 9daeee2f-11c4-4381-a18e-aeb0a7c03a24

API ENDPOINTS
Pizza Controller
- Bulk Add Pizzas from JSON								
	-Endpoint: POST /api/pizza/upload-json	
	-Request Body: JSON file containing a list of Pizza objects.
	-Response: Returns processing time, file name, transaction count and transaction details.

- Get Pizzas
	-Endpoint: GET /api/pizza/get
	-Query Parameters: None
	-Response: Return a list of saved Pizzas

- Get Active Pizzas
	-Endpoint: GET /api/pizza/get-by-status
	-Query Parameters: isActive (optional - default to "false" if null)
	-Response: Return a list of saved Pizzas based on filtering criteria.

File ProcessingLog Controller
- Get Processed Files Logs
	-Endpoint: GET /api/fileprocessinglogs/get
	-Query Parameter: NONE
	-Response: Return a list of save processed file logs details



LOGGING
All API operations are logged for debugging and tracking purposes. Logs include details such as processing time and errors encountered during file uploads.

ERROR HANDLING
Errors are handled gracefully, returning appropriate status codes and messages.



