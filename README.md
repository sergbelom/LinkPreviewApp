# LinkPreviewApp - MAUI Application

**LinkPreviewApp** is a MAUI application for generating website previews. The app uses the **LinkPreview API** to retrieve images and descriptions for the preview. [Documentation for LinkPreview API](https://docs.linkpreview.net/)

### Features

- The app allows users to create a website preview and navigate to the site by clicking on the preview. The default browser is opened for the link.

### Architecture

- **MVVM Architecture**: The application follows the MVVM (Model-View-ViewModel) design pattern.
- **API Requests**: DataService is responsible for making API calls using `HttpClient` for sending requests and receiving responses from the service.
- **Dependency Injection**: The app utilizes the built-in DI tool provided by `MauiAppBuilder`.
- **SOLID**: Development was strictly based on SOLID principles.
- **Logging**: Logging is implemented using the `Serilog` package.
- **Error Handling**: Comprehensive error and exception handling is in place.
- **Global Configuration**: All necessary parameters for working with **LinkPreview API** are managed in `GlobalConfiguration`.
- **Localization**: The app supports localization, and all text resources are stored in `AppResources`.

### Testing

- **Unit Testing**: All services and view models are covered by unit tests using the `NUnit` and `NSubstitute` packages.
- **Mocking for Automated Tests**: A `MockHttpService` is implemented to provide mock responses for automated tests without the need for `HttpClient`.