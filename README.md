# Swasthya - A Medical Record Management System
Swasthya is a web-based application that allows medical centers to upload medical reports directly to a patient's account. The system is designed to improve the efficiency and accuracy of medical record keeping, while also providing patients with easy access to their medical reports and enabling doctors to access a patient's complete medical history.

## Features
- Patient account creation and management
- Healthcare center account creation and management
- Medical practitioner account creation and management
- Upload medical reports to a patient's account
- Grant or deny access to healthcare providers
- View medical reports

## Technologies Used
- ASP.NET Core Web API Project for backend development
- Blazor WebAssembly for frontend development
- Azure CosmosDB for database management

## Installation
To run Swasthya on your local machine, follow these steps:
- Clone the repository to your local machine using the following command:
    `git clone git@github.com:PriyaDebo/Swasthya.git`
- Open the solution file in Visual Studio or Visual Studio Code.
- In the appsettings.json file in API Project, replace the EndpointUri and PrimaryKey values with your own CosmosDB values.
- Connect Azure Storage and create a container named 'reports'. 
- Build and run the application.

## Contributions
Contributions to Swasthya are welcome and encouraged. To contribute, please fork the repository, make your changes, and submit a pull request.
