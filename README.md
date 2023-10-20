# The Chat App

## Introduction

A small ASP.NET core implementation of SignalR.
As a bi-drectional channel is opened between the browser and the server,
messages can be sent from the browser to the server and messages can be sent
from the server to the page in the browser.

This application is used to help develop and validate a
WebSocket based load test with the Gatling framework.

## Scope

The following concepts are used
- .NET Model View Controller
- SignalR Hub
- Repositories (very memory store)
- Simple token based authentication

## Not in Scope
- Proper Authorization/Authentication
- Persistent data, messages, users are not persistent.

## Getting Started

### Required 

This project requires .NET Core to be installed

### To use
1. Clone this repository
2. From the root repository folder execute dotnet run

Open multiple browser tabs to the application url
- Provide a user name and sign in
- Send MEssages
- See how the messages are sent to all the opened tabs.