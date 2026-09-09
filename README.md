# Cybersecurity Awareness Bot

## Project Overview

The Cybersecurity Awareness Bot is a C# console application developed to provide basic cybersecurity awareness to South African citizens.

The chatbot provides simple and practical information about common cybersecurity threats and safe online behaviour. It allows users to interact with the application through text-based questions and provides personalised responses using the user's name.

The project was developed as Part 1 of the C# Programming Portfolio of Evidence (POE).

## Project Objectives

The main objectives of the project are to:

* Create a functional console-based cybersecurity awareness chatbot.
* Educate users about common cybersecurity threats.
* Provide practical advice for safer online behaviour.
* Personalise the conversation using the user's name.
* Demonstrate input validation and exception handling.
* Create a professional and user-friendly console interface.
* Demonstrate good C# programming structure and code organisation.
* Use GitHub for version control and GitHub Actions for continuous integration.

## Features

The application includes the following features:

* Voice welcome greeting using a WAV audio file.
* Custom cybersecurity ASCII logo.
* Personalised text greeting.
* Interactive text-based conversation.
* Cybersecurity questions and responses.
* Password security awareness.
* Phishing awareness.
* Suspicious link awareness.
* Malware awareness.
* Scam and online fraud awareness.
* Multi-factor authentication awareness.
* Social engineering awareness.
* Safe browsing guidance.
* Public Wi-Fi safety guidance.
* Privacy and personal information guidance.
* Social media security guidance.
* Hacked account guidance.
* South African cybersecurity awareness.
* Help command with suggested topics.
* Exit and quit commands.
* Empty input validation.
* Long input validation.
* Unexpected input and error handling.
* Colour-coded console interface.
* Borders, spacing and section headings.
* Typing effect for chatbot responses.

## Technologies Used

* C#
* .NET 8.0
* Visual Studio 2022
* GitHub
* GitHub Desktop
* GitHub Actions
* WAV audio

## Project Structure

```text
CybersecurityAwarenessBot/
│
├── .github/
│   └── workflows/
│       └── dotnet-ci.yml
│
├── CybersecurityAwarenessBot/
│   ├── Media/
│   │   ├── cybersecurity-logo.txt
│   │   └── welcome.wav
│   │
│   ├── Models/
│   │   └── User.cs
│   │
│   ├── Services/
│   │   ├── AudioServices.cs
│   │   ├── ChatbotServices.cs
│   │   └── ConsoleUIServices.cs
│   │
│   ├── Program.cs
│   └── CybersecurityAwarenessBot.csproj
│
├── CybersecurityAwarenessBot.sln
└── README.md
```

## Application Structure

The application is divided into separate components to improve readability, maintainability and separation of responsibilities.

### Program.cs

The main entry point of the application.

It is responsible for:

* Starting the application.
* Creating the required services.
* Creating the user object.
* Playing the welcome audio.
* Displaying the ASCII logo.
* Asking for the user's name.
* Starting the chatbot conversation.
* Handling the conversation loop.
* Processing exit commands.

### User Model

The `User` class stores information about the person interacting with the chatbot.

The current application stores the user's name so that responses and prompts can be personalised.

### Audio Service

The `AudioService` handles the welcome voice greeting.

The application locates the WAV file in the `Media` folder and plays it when the program starts. Error handling is included so that the application can continue if the audio file cannot be found or played.

### Chatbot Service

The `ChatbotService` contains the chatbot's cybersecurity knowledge and response logic.

It analyses the user's question and identifies relevant cybersecurity topics such as:

* Passwords
* Phishing
* Malware
* Scams
* Suspicious links
* Multi-factor authentication
* Social engineering
* Privacy
* Social media
* Public Wi-Fi
* Hacked accounts
* Online safety

The service also includes a fallback response when the chatbot does not recognise a question.

### Console UI Service

The `ConsoleUIService` controls the visual presentation of the application.

It provides:

* ASCII logo display.
* Section headers.
* Personalised welcome messages.
* User prompts.
* Bot response formatting.
* Warning messages.
* Error messages.
* Goodbye messages.
* Console colours.
* Borders and spacing.
* Typing effect.

## Input Validation

The application validates user input before processing it.

The chatbot handles:

* Empty names.
* Names that are too long.
* Empty questions.
* Questions exceeding the allowed length.
* Unsupported questions.
* Unexpected console input.
* Unexpected runtime errors.

Instead of terminating immediately when an invalid input is received, the application provides an informative message and allows the user to continue where possible.

## Chatbot Interaction

The chatbot uses a conversational approach rather than a traditional numbered menu.

Users can type questions naturally, for example:

```text
What is phishing?
How can I make my password safer?
What is malware?
What is social engineering?
How can I stay safe on public Wi-Fi?
What should I do if my account was hacked?
```

Users can also type:

```text
help
```

to receive a list of suggested cybersecurity topics.

To end the conversation, users can type:

```text
exit
```

or:

```text
quit
```

## Voice Greeting

When the application starts, it attempts to play a recorded WAV welcome message.

The audio file is stored in:

```text
CybersecurityAwarenessBot/Media/welcome.wav
```

The project configuration ensures that the audio file is copied to the application's output directory when the project is built.

## ASCII Visual Design

The application displays a custom cybersecurity-themed ASCII logo when it starts.

The logo is stored as:

```text
Media/cybersecurity-logo.txt
```

This provides a visual identity for the application while keeping the project suitable for a console environment.

## Continuous Integration

GitHub Actions is used to automatically build the project when changes are pushed to the `main` branch.

The workflow is located at:

```text
.github/workflows/dotnet-ci.yml
```

The workflow performs the following tasks:

1. Checks out the repository.
2. Sets up .NET 8.
3. Restores project dependencies.
4. Builds the project in Release configuration.

Successful workflow runs provide confirmation that the project builds correctly after changes are pushed to GitHub.

## Version Control

GitHub and GitHub Desktop are used to manage the project's source code and development history.

Meaningful commits were created throughout development to document major improvements, including:

* Adding the GitHub Actions CI workflow.
* Improving cybersecurity chatbot responses.
* Strengthening input validation and error handling.
* Polishing the console interface and keyword matching.
* Adding a typing effect to chatbot responses.
* Adding project documentation.

This development approach helps track changes and provides a clear history of the project's progress.

## How to Run the Application

### Requirements

The following software is required:

* Windows operating system.
* .NET 8 SDK.
* Visual Studio 2022 or another compatible C# development environment.

### Steps

1. Clone or download the repository.
2. Open the solution file:

```text
CybersecurityAwarenessBot.sln
```

3. Restore the project dependencies.
4. Build the solution.
5. Run the application.

The application will:

1. Play the welcome greeting.
2. Display the cybersecurity ASCII logo.
3. Ask for the user's name.
4. Display a personalised welcome.
5. Start the cybersecurity conversation.

## Example Interaction

```text
What should I call you? Denny

Welcome, Denny!

I'm your Cybersecurity Awareness Bot.
My goal is to help you stay safe and confident
while using the digital world.

Denny > What is phishing?

BOT
Phishing is an attempt to trick you into revealing
sensitive information, such as passwords or banking details.

Denny > What is malware?

BOT
Malware is malicious software designed to damage systems,
steal information, or disrupt operations.

Denny > exit

Thank you for using the Cybersecurity Awareness Bot!
Stay alert, stay informed, and stay safe online!
Goodbye, Denny!
```

## Testing

The application was tested during development to confirm that the main functionality operates correctly.

Testing included:

* Application startup.
* Welcome audio playback.
* ASCII logo display.
* User name input.
* Personalised responses.
* Cybersecurity topic responses.
* Empty input handling.
* Long input handling.
* Unsupported questions.
* Help command.
* Exit command.
* Quit command.
* Typing effect.
* Successful project builds.
* Successful GitHub Actions CI checks.

## Future Improvements

Possible future improvements include:

* A graphical user interface using WPF or Windows Forms.
* More advanced natural-language processing.
* Additional cybersecurity topics.
* Interactive quizzes.
* Progress tracking.
* More detailed South African cybersecurity resources.
* Improved accessibility features.
* More advanced automated testing.
* Additional user interaction features.

These improvements can be explored in later parts of the project.

## Author

**Dennisious Moima**

C# Programming Portfolio of Evidence

Part 1 – Cybersecurity Awareness Chatbot for South African Citizens
