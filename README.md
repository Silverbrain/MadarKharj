
# PersonalFinance WebApp

This repository contains the source code for the PersonalFinance API, which provides various endpoints for managing personal financial data, such as accounts, entries, and transactions. The API is built using ASP.NET Core and follows RESTful principles.

## Table of Contents

- [Getting Started](#getting-started)
- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Getting Started

To get a local copy of the project up and running, follow the steps below.

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:
   ```sh
   git clone https://github.com/yourusername/PersonalFinance.git
   cd PersonalFinance
   ```

2. Restore the .NET dependencies:
   ```sh
   dotnet restore
   ```

3. Update the database connection string in `appsettings.json` to point to your SQL Server instance:
   ```json
   "ConnectionStrings": {
     "Default": "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;"
   }
   ```

4. Run the database migrations:
   ```sh
   dotnet ef database update
   ```

5. Build and run the application:
   ```sh
   dotnet build
   dotnet run
   ```

## Usage

Once the application is running, you can access the API at `https://localhost:5001`.

## Contributing

Contributions are what makes the open-source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## License

Distributed under the MIT License. See [`MIT License`](https://github.com/Silverbrain/MadarKharj?tab=MIT-1-ov-file) for more information.
