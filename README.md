# Geolocation

A simple API for getting geolocation details based on IP or URL using ipstack API (https://ipstack.com/).

Technologies used: ASP.NET Web API, C#, Entity Framework, NUnit, Moq, NLog, Ninject.

Only a single IP/URL is supported per request.
An IP parameter can be in an IPv4 or IPv6 format.
A URL parameter should contain a hostname without scheme.

- A GET request means details are acquired from a database if there are any.
- A POST request means an IP/URL is sent to IP Stack and received details are stored in a database.
- A DELETE request means an IP/URL and its geolocation details are removed from a database.