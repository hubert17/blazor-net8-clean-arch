
# Blazor InteractiveAuto Clean Architecture in .NET 8

A clean architecture template for Blazor InteractiveAuto applications using .NET 8.

## Demo:
- https://gablazor.somee.com

## Prerequisites

- .NET 8
- MudBlazor
- CodeBeam.MudExtensions

## Features

- Clean architecture design pattern
- MudBlazor and CodeBeam.MudExtensions integration
- JWT authentication with refresh tokens

## Smart Components (Hot!!!)

Smart Components are prebuilt end-to-end AI features that you can drop into your existing UIs to upgrade them, truly making your app more productive for your end users. You need to provide access to a language model backend. See: [Configure the OpenAI backend](https://github.com/dotnet-smartcomponents/smartcomponents/blob/main/docs/configure-openai-backend.md).

## Docker Publish Command

    dotnet publish --os linux --arch x64 -c Release /p:PublishProfile=DefaultContainer

