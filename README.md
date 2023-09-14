# CognitoAuthAPI

CognitoAuthAPI is a .NET Core API that integrates with Amazon Cognito Identity Provider to handle user authentication and related actions. This API allows users to sign up, confirm their account, sign in, reset their password, change their password, and retrieve their user information.

## Table of Contents

- [Controller](#controller)
- [Global Error Handling Middleware](#global-error-handling-middleware)
- [Service Extensions](#service-extensions)
- [Service](#service)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Further Considerations](#further-considerations)
- [Contact](#contact)

## Controller

The `AuthController` is the main controller for this API, handling various user authentication and account-related actions. It includes the following actions:

- `SignUp`: Allows users to sign up for a new account.
- `ConfirmSignUp`: Confirms a user's sign-up by validating a confirmation code.
- `SignIn`: Allows users to sign in to their account.
- `SendForgotPasswordCode`: Sends a code to reset the password.
- `ConfirmForgotPassword`: Confirms a password reset request.
- `ChangePasswordAsync`: Allows authenticated users to change their password.
- `GetMe`: Retrieves user information for an authenticated user.

## Global Error Handling Middleware

The `GlobalErrorHandlingMiddleware` is used to handle exceptions that may occur during API operations. Any exception not caught in the service layer will be caught here, logged, and turned into a 500 response.

## Service Extensions

The `ServiceExtensions` class includes extension methods for configuring services in the ASP.NET Core application. It sets up services related to the API, JWT authentication, and Swagger documentation.

## Service

The `CognitoAuthService` class is responsible for interacting with Amazon Cognito Identity Provider. It provides methods for signing up users, confirming sign-up, signing in, resetting passwords, changing passwords, and handling user-related operations.

## Getting Started

### Prerequisites

Before using this API, ensure that you have the following prerequisites installed:

- .NET Core SDK
- Amazon Cognito Identity Provider configuration (AWS Cognito)

### Installation

1. Clone the repository:

   ```bash
   git clone <repository-url>
   ```

2. Configure your AWS Cognito settings in the `appsettings.json` file.

3. Build and run the application:

   ```bash
   dotnet build
   dotnet run
   ```

The API should now be running and accessible at the specified endpoints.

## Usage

To use this API, make HTTP requests to the defined endpoints in the `AuthController` based on your authentication and account management needs. Be sure to handle authentication using JWT tokens when required.

## API Endpoints

The following are the main API endpoints provided by the `AuthController`:

- `POST /api/Auth/signUp`: Sign up for a new account.
- `POST /api/Auth/confirmSignUp`: Confirm a user's sign-up.
- `POST /api/Auth/signIn`: Sign in to an existing account.
- `GET /api/Auth/forgotPasswordCode`: Send a code to reset the password.
- `POST /api/Auth/forgotPassword`: Confirm a forgot password reset request.
- `POST /api/Auth/changePassword`: Change the user's password (requires authentication).
- `GET /api/Auth/me`: Retrieve user information (requires authentication).
- `DELETE /api/Auth/signOut`: Invalidates all identity, access, and refresh tokens that AWS has issued to the user.

## Further Considerations
- Identity Provider Flexibility - Consider abstracting the identity provider interactions further to allow for easier switching between different identity providers in the future

## Contact

If you have any questions, feedback, or issues related to this API, please feel free to contact the project maintainers:

- Matt Weiss
- mattirvingweiss@gmail.com

We appreciate your interest and contributions to this project!

---
