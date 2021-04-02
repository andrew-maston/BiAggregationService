## Synopsis

This project was written as a code sample around Building Information aggregation 

## Motivation

This project was created with the sole purpose of it being reviewed by xbim

## Installation

Clone this repository, build and run the solution in Visual Studio, Swagger UI will be displayed at the application root.

## API Reference

The Swagger UI/documentation for the API can be found at the root of the running application, we provide two endpoints

- GET /Rooms
- GET /Summary

## Challenges / Future Enhancements

- I didn't make any real effort at all to avoid passing the Xbim.Essentials objects about.
- There are no unit tests.
- There are no postman tests.
- I couldn't find a way of accessing the store asynchronously, I was expecting some implementation of Task in IfcStore.
- XbimEditorCredentials which is required to init IfcStore didn't use properties, was unable to bind from config to this, had to create a config object.
- Found it difficult to understand the data structure, not sure why am I querying into underlying models for properties which are present, but that's how examples
seem to do it, and I'm getting numbers back, further to this not sure why I've got to parse e.g. see GetArea(this IIfcSpace space)
- I couldn't see any obvious way of retrieving the label for the type of object e.g. "Door", "Wall", "Window"; the names are specific to each item, could have written a switch statement
with hardcoded labels
-- Suspect that I've missed an enum, though did look for one around ObjectType
-- Instead iterated over and counted the interface types of a given item and used the Type name as a key for grouping/counting
- Logging is underwhelming, It's not implemented everywhere, cloud logging and/or some trace pattern would be better.
- I would never add credentials in appsettings but added from sample here so that values were populated 
- I could have allowed the user to upload the Sample file (or any other ifc file), I didn't do that. The file is in the repo and it's hardcoded in appsettings.
- I retrieved data when using Ifc4, but saw equivilant interfaces for Ifc2x3, assumed that I did have the interfaces correct for this file.
-- Think there's probably something else to do here in terms of detecting which standard the file is of, I didn't do that.

## Contributors

Andrew Maston