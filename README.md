CoreFileManager
Overview

CoreFileManager is a hands-on project designed to build a deep understanding of ASP.NET Core fundamentals. Rather than focusing on features, the project emphasizes how the framework works internally—from request entry to response generation.

The main goal is to fully understand the request–response lifecycle, middleware pipeline, and cross-cutting concerns such as logging and exception handling.

Objectives

Understand the ASP.NET Core request–response pipeline

Learn how middleware works and how it is composed

Apply dependency injection correctly

Centralize logging and error handling

Core Concepts Covered

Middleware
Custom middleware implementation to intercept, process, and manipulate HTTP requests and responses.

Dependency Injection (DI)
Proper usage of ASP.NET Core’s built-in DI container for clean and maintainable service registration.

Logging
Structured logging using Serilog for consistent and centralized log management.

Global Exception Handling
A single, centralized exception handler to manage errors across the entire application.

Implementation Highlights

Custom middleware integrated into the request pipeline

Centralized exception handling for consistent error responses

Serilog-based structured logging

Clear separation of cross-cutting concerns

Clean and maintainable startup configuration

Outcome

This project results in an application that:

Clearly demonstrates the request–response flow

Handles errors from a single, centralized point

Manages logs consistently across the system

Provides a solid foundation for more advanced ASP.NET Core architectures

CoreFileManager represents the first step toward building reliable, maintainable, and production-ready ASP.NET Core applications.
