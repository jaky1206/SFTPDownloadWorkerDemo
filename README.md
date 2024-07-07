# SFTP Downloader Using Worker Service

## Overview
This demo project showcases a backend service using .NET Core 6 for downloading files from an SFTP server using a worker service.

## Requirements
- **Automated File Download**: The service connects to the SFTP server every minute to check for new files and downloads them to a local path.
- **Configuration**: SFTP server details and file paths are configurable outside of the code.
- **Database Integration**: Downloaded file paths are stored in a PostgreSQL database.
- **Resilience**: The service is designed to handle connection issues and continue operating without crashing.
- **Code Comments**: The codebase includes comments explaining the functionality.
- **Logging**: Includes configurable logging and tracing for monitoring service activities.
- **Dependency Injection**: Utilizes dependency injection for better code management.

## Setup
1. Disable antivirus software to prevent issues with folder creation.
2. To create a service from the executable, run: sc.exe \myserver create NewService binpath= c:\windows\system32\NewServ.exe
3. Configure file paths, database credentials, SFTP credentials, and preferences in `appsettings.json`.

## Database
- The database schema is defined using the code-first principle with Entity Framework.

## Logging
- The service includes logging with configurable tracing levels.

## Disclaimer
Please ensure to configure the service correctly and handle any security implications when disabling antivirus software.

## About
This project demonstrates the use of a worker service in .NET Core 6 for SFTP file downloading, PostgreSQL database, database integration using Entity Framework , and service resilience.

