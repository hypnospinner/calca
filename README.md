# Calca

## Description

Simple web application that provides interface for calculating:

- factorial 
- n Fibonacci number

## Solution Structure

As a clean microservice architecture would be an overkill for this application it actually consists of only 2 projects: 

- ASP.NET Core project with gRPC preset for back-end 
- Angular 8 project for front-end

In order not to duplicate `.proto` contracts they are stored separately in `src/Protos` folder.

## Services

Services are implemented as proto contracts & controllers on back-end

## Data Layer Structure

Clean microservice architecture suggests that we have one database-per-service & this is one of the points I decided to follow the traditional guideline as for database I decided to use default .NET solution - MS SQL with EF Core ORM