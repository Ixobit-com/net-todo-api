# ToDo List API

## Table of Contents

- [Description](#description)
- [General Information](#general-information)
- [Main Features](#main-features)
- [Installation](#installation)
- [System Requirements](#system-requirements)
- [Third-Party Components](#third-party-components)

## Description

API for one of the common tasks — ToDo List for managing tasks and users.

## General Information

- 3-tier architecture is used;
- 2 user roles: admin and user;
- JWT token authorization;
- Unit Of Work and Repository patterns are used.

## Main Features

- OAuth authorization for clients and users (client_credentials, password)
- CRUD for clients
- CRUD for users 
- CRUD for email templates
- CRUD for labels
- CRUD for sprints
- CRUD for tickets
- Data validation 
- Email creation and sending according to the template 
- Two access levels: client scopes and user roles
- Full API documentation via Swagger
- Requests and errors are logged

## Installation

1. Open solution in Visual Studio
2. Change connection string and SMTP settings in the appsettings.json
3. Compile and run

## System Requirements

- Visual Studio 2022
- .NET 8
- MS SQL Server 2019 

## Third-Party Components

- AutoMapper 13.0.1
- FluentValidation 11.9.2
- Handlebars.Net 2.1.6
- MailKit 4.6.0
- Newtonsoft.Json 13.0.3
- NLog 5.3.2
- Swashbuckle.AspNetCore 6.6.2