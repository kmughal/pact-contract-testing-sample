
```markdown
# API Testing Sample Project with Pact-Net

## Introduction

This is a work-in-progress (WIP) sample project for API testing using Pact-Net. The project includes consumer and provider testing, interacting with the Hacker News API to retrieve sample data.

This project also leverages the MediatR library for handling requests and responses in a clean and decoupled manner.

## Project Structure

The project is organized into the following components:

- **HackerNewsApi:** Contains the API implementation for interacting with the Hacker News API.

- **ContractTesting:** Holds the contract tests for consumer/provider interactions using Pact-Net.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) installed on your machine.

### Setup

1. Clone this repository to your local machine:

   ```bash
   git clone <repository-url>
   ```

2. Navigate to the project root:

   ```bash
   cd <repository-directory>
   ```

### Running the Contract Tests

1. Navigate to the ContractTesting project:

   ```bash
   cd ContractTesting
   ```

2. Run the Pact contract tests:

   ```bash 
   dotnet test
   ```

### Running the API

1. Navigate to the HackerNewsApi project:

   ```bash
   cd HackerNewsApi
   ```

2. Run the API:

   ```bash
   dotnet run
   ```

3. The API will be hosted at `http://localhost:7201/`. You can test its endpoints using your preferred API client.

4. In order to get the top post ids use `http://localhost:7201/api/top-news` end point.


## MediatR Integration

This project utilizes the MediatR library for handling requests and responses. MediatR promotes a clean and decoupled architecture by allowing components to communicate through a mediator.

## Contributing

Feel free to contribute to this project by submitting issues or pull requests. Your feedback and contributions are highly appreciated.

## License

This project is licensed under the [MIT License](LICENSE).
```