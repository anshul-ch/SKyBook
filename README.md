# SkyBook - Flight Search Engine

A simple flight search web app built with **ASP.NET Core MVC (.NET 10)** for a college assignment.

## What it does

- Search flights by city, date, passengers, and class
- View flight results with sorting (price / duration)
- See detailed info for each flight
- Supports Economy, Business, and First Class

## Routes available

- Delhi ↔ Mumbai
- Delhi ↔ Bangalore
- Mumbai ↔ Bangalore

## How to run

1. Open the solution in **Visual Studio 2022**
2. Press **F5** to run
3. Go to the **Flights** tab and search

## Tech used

- ASP.NET Core MVC
- C#
- Bootstrap 5
- Bootstrap Icons

## Project structure

- `Models/` – Flight, SearchCriteria, FlightSearchResult
- `Services/` – FlightService (handles search, sort, sample data)
- `Controllers/` – FlightController, HomeController
- `Views/Flight/` – Search, SearchResults, Details, Index pages

## Note

This project uses hardcoded sample data (no database). Booking feature is not implemented yet.
