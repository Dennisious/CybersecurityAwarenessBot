# Cybersecurity Awareness Bot

## Project Overview

The Cybersecurity Awareness Bot is a C# application developed to promote cybersecurity awareness among South African citizens. It provides practical guidance on common online threats and encourages safer digital behaviour.

The project was developed as part of the C# Programming Portfolio of Evidence (POE) and consists of two applications:

- **Part 1 – Console Application:** A text-based chatbot featuring a voice greeting, personalised responses, cybersecurity advice and input validation.
- **Part 2 – WPF GUI Application:** A graphical chatbot featuring dynamic responses, random cybersecurity tips, conversation flow, session-based memory and sentiment detection.

## Project Objectives

The objectives of the project are to:

- Educate users about common cybersecurity threats.
- Provide practical advice for safer online behaviour.
- Recognise cybersecurity keywords and user questions.
- Generate varied responses to encourage continued interaction.
- Personalise conversations using remembered information.
- Detect user sentiment and respond empathetically.
- Apply input validation and error handling.
- Demonstrate good C# programming practices and code organisation.
- Use GitHub for version control and GitHub Actions for continuous integration.

## Features

### Part 1 – Console Application

- Voice welcome greeting using a WAV audio file.
- Cybersecurity-themed ASCII artwork.
- Personalised welcome messages.
- Interactive text-based conversation.
- Password and phishing awareness.
- Scam and online fraud prevention.
- Malware and suspicious-link awareness.
- Multi-factor authentication guidance.
- Social engineering awareness.
- Safe browsing and public Wi-Fi guidance.
- Privacy and social media security.
- Hacked-account guidance.
- Help, exit and quit commands.
- Empty and excessively long input validation.
- Fallback responses and error handling.
- Colour-coded interface, borders and spacing.
- Typing effect for chatbot responses.

### Part 2 – WPF GUI Application

- Graphical interface developed using Windows Presentation Foundation (WPF).
- Dark-themed interface with contrasting colours.
- Chat-style conversation area.
- Suggested cybersecurity topic buttons.
- Text input and send button.
- Voice welcome greeting.
- ASCII artwork loaded from a text file.
- Keyword recognition for supported cybersecurity topics.
- Random cybersecurity tips stored in collections.
- Conversation flow supporting requests such as "Give me another tip" and "Explain more".
- Memory for the user's name and favourite cybersecurity topic during the current session.
- Memory recall through follow-up questions.
- Sentiment detection for worried, curious and frustrated users.
- Empathetic responses combined with relevant cybersecurity guidance.
- Fallback responses for unsupported questions.
- Input validation and exception handling.
- Delegate and event-based communication between chatbot logic and the interface.

Memory in the current GUI implementation is maintained during the running application session and is not guaranteed to persist after the application closes.

## Technologies Used

- C#
- .NET 8
- WPF (Windows Presentation Foundation)
- XAML
- Visual Studio 2022
- Git and GitHub
- GitHub Actions
- WAV audio files

## Project Structure

The repository contains the original console application and the additional GUI application.

```text
CybersecurityAwarenessBot/
├── .github/
│   └── workflows/
│       └── dotnet-ci.yml
├── CybersecurityAwarenessBot/
│   ├── Media/
│   │   ├── cybersecurity-logo.txt
│   │   └── Welcome.wav
│   ├── Models/
│   │   └── User.cs
│   ├── Services/
│   │   ├── AudioService.cs
│   │   ├── ChatbotService.cs
│   │   └── ConsoleUIService.cs
│   ├── Program.cs
│   └── CybersecurityAwarenessBot.csproj
├── CybersecurityAwarenessBot.GUI/
│   ├── Media/
│   │   ├── cybersecurity-logo.txt
│   │   └── Welcome.wav
│   ├── Services/
│   │   ├── AudioService.cs
│   │   └── ChatbotService.cs
│   ├── App.xaml
│   ├── App.xaml.cs
│   ├── AssemblyInfo.cs
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   └── CybersecurityAwarenessBot.GUI.csproj
├── CybersecurityAwarenessBot.sln
└── README.md
```

## Application Architecture

### Console Application

**Program.cs**

Starts the application, initialises the required services and manages the main conversation loop.

**User model**

Stores the user's name for personalised interaction.

**AudioService**

Locates and plays the welcome WAV audio file. Audio failures are handled so they do not unnecessarily prevent the application from starting.

**ChatbotService**

Processes user input, recognises cybersecurity topics and generates relevant responses. It also handles unsupported questions.

**ConsoleUIService**

Manages console presentation, including colours, section headings, user prompts, response formatting and the typing effect.

### WPF GUI Application

**App.xaml and App.xaml.cs**

Define and initialise the WPF application.

**MainWindow.xaml**

Defines the graphical interface, including the header, chat area, suggested topic buttons and message input controls.

**MainWindow.xaml.cs**

Connects the interface to the chatbot service, handles user interaction and displays conversation messages.

**ChatbotService.cs**

Contains cybersecurity topic dictionaries, collections of tips, random response selection, conversation flow, session-based memory and sentiment detection.

**AudioService.cs**

Plays the welcome audio when the graphical application starts.

Separating the interface from the chatbot logic improves readability, maintainability and future development.

## Memory, Sentiment and Conversation Flow

The GUI chatbot supports several forms of interaction.

**Memory:** The chatbot can remember a user's name and favourite cybersecurity topic during the current session and recall that information when asked.

**Sentiment detection:** The chatbot recognises expressions associated with worry, frustration and curiosity. It responds with supportive language and provides relevant cybersecurity guidance where appropriate.

**Conversation flow:** Users can request another tip, ask for more information or explore a recognised cybersecurity topic without restarting the application.

**Random responses:** Multiple tips are stored for supported topics, allowing the chatbot to provide varied advice.

## Input Validation and Error Handling

Both applications include input validation and fallback handling to improve reliability.

Relevant cases include:

- Empty input.
- Unsupported questions.
- Invalid or unexpected user input.
- Missing audio or media files.
- Recoverable runtime errors.

The console application also validates excessively long input. The GUI handles empty message submissions and displays responses in the conversation area.

## Voice Greeting and Media Files

The WAV greeting and ASCII artwork are stored in each application's `Media` folder.

The project configurations include the required media resources so that the applications can locate them when running locally. Audio playback depends on the availability of the required files and a supported Windows environment.

## Continuous Integration

The repository includes a GitHub Actions workflow at:

`.github/workflows/dotnet-ci.yml`

The workflow runs automatically when changes are pushed to the `main` branch or when a pull request targets `main`.

The workflow:

1. Checks out the repository.
2. Sets up the .NET 8 SDK.
3. Restores solution dependencies.
4. Builds `CybersecurityAwarenessBot.sln` in Release configuration.

The solution includes both the console application and the WPF GUI application. A successful workflow run confirms that the automated build completed successfully for the projects included in the solution. It does not replace manual application testing.

## Version Control and Releases

GitHub is used to maintain the source code and track development progress through meaningful commits.

Development milestones include:

- Adding the GitHub Actions CI workflow.
- Improving cybersecurity responses.
- Strengthening input validation and error handling.
- Improving the console interface and keyword matching.
- Adding the typing effect.
- Developing the WPF graphical interface.
- Adding dynamic responses, memory and sentiment detection.
- Updating project documentation and the solution configuration.

GitHub tags and releases should identify significant project milestones and include clear version notes. Releases should accurately describe the features and changes included in each version.

## How to Run the Applications

### Requirements

- Windows operating system.
- Visual Studio 2022 with the .NET desktop development workload.
- .NET 8 SDK.
- Internet access when cloning the repository or restoring packages, if required.

### Run Part 1 – Console Application

1. Clone or download the repository.
2. Open `CybersecurityAwarenessBot.sln` in Visual Studio.
3. Select the `CybersecurityAwarenessBot` console project as the startup project.
4. Restore dependencies and build the solution.
5. Run the application.

### Run Part 2 – WPF GUI Application

1. Open `CybersecurityAwarenessBot.sln` in Visual Studio.
2. Select `CybersecurityAwarenessBot.GUI` as the startup project.
3. Restore dependencies and build the solution.
4. Run the application.
5. Enter a cybersecurity question or select one of the suggested topics.
6. Test follow-up questions, memory recall and sentiment detection.

## Testing

Testing during development included:

- Application startup and successful builds.
- Welcome audio playback.
- Cybersecurity topic recognition.
- Random tip generation.
- Follow-up requests for additional tips.
- Memory storage and recall during a session.
- Sentiment detection and empathetic responses.
- Unsupported questions and fallback handling.
- Empty input handling.
- GUI interaction through suggested buttons and message input.
- GitHub Actions workflow execution and successful solution builds.

These checks cover the main application features. Additional automated unit and integration tests could further improve test coverage.

## Demonstration Video

**Part 2 – Cybersecurity Awareness Bot GUI Demonstration**

YouTube link: **[Insert your unlisted YouTube video link here before submission]**

The demonstration should explain the application's purpose, interface, code structure, dynamic responses, conversation flow, memory recall, sentiment detection and voice greeting.

## Future Improvements

Potential future enhancements include:

- Persistent memory across application sessions.
- Additional cybersecurity topics and South African safety resources.
- Interactive cybersecurity quizzes.
- Automated unit and integration tests.
- Accessibility improvements.
- More advanced natural-language processing.

## Author

**Dennisious Moima**

C# Programming Portfolio of Evidence (POE)

Part 1 – Console Application

Part 2 – WPF GUI Application
