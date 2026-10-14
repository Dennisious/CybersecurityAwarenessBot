# Cybersecurity Awareness Bot

## Project Overview

The Cybersecurity Awareness Bot is a C# application developed to promote cybersecurity awareness among South African citizens. It provides practical guidance on common online threats and encourages safer digital behaviour.

The project was developed as part of the C# Programming Portfolio of Evidence (POE).

The project consists of two applications:

- **Part 1 – Console Application:** A text-based chatbot with voice greeting, personalised responses, cybersecurity advice and input validation.
- **Part 2 – WPF GUI Application:** A graphical chatbot with dynamic responses, random cybersecurity tips, conversation flow, memory and sentiment detection.

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
- Keyword recognition for passwords, phishing, scams, privacy, malware, public Wi-Fi and multi-factor authentication.
- Random cybersecurity tips stored in collections.
- Conversation flow supporting requests such as “Give me another tip” and “Explain more”.
- Memory for the user's name and favourite cybersecurity topic.
- Memory recall through follow-up questions.
- Sentiment detection for worried, curious and frustrated users.
- Empathetic responses combined with relevant cybersecurity guidance.
- Fallback responses for unsupported questions.
- Input validation and exception handling.

Memory in the current GUI implementation is maintained during the running application session and is not guaranteed to persist after the application closes.

## Technologies Used

- C#
- .NET 8
- WPF (Windows Presentation Foundation)
- XAML
- Visual Studio 2022
- Git and GitHub
- GitHub Desktop
- GitHub Actions
- WAV audio files

## Project Structure

The repository contains the original console application and the additional GUI application.

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
│   │   └── Welcome.wav
│   ├── Models/
│   │   └── User.cs
│   ├── Services/
│   │   ├── AudioService.cs
│   │   ├── ChatbotService.cs
│   │   └── ConsoleUIService.cs
│   ├── Program.cs
│   └── CybersecurityAwarenessBot.csproj
│
├── CybersecurityAwarenessBot.GUI/
│   ├── Media/
│   │   ├── cybersecurity-logo.txt
│   │   └── Welcome.wav
│   ├── Services/
│   │   ├── AudioService.cs
│   │   └── ChatbotService.cs
│   ├── App.xaml
│   ├── MainWindow.xaml
│   ├── MainWindow.xaml.cs
│   └── CybersecurityAwarenessBot.GUI.csproj
│
├── CybersecurityAwarenessBot.sln
└── README.md
```

*Note: File and folder names above describe the intended structure. Verify them against the actual repository before submitting.*

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

**MainWindow.xaml**

Defines the graphical interface, including the header, chat area, suggested topic buttons and message input controls.

**MainWindow.xaml.cs**

Connects the interface to the chatbot service, handles user interaction and displays conversation messages.

**ChatbotService.cs**

Contains the cybersecurity topic dictionaries, collections of tips, random response selection, conversation flow, memory recall and sentiment detection.

**AudioService.cs**

Plays the welcome audio when the graphical application starts.

Separating the interface from the chatbot logic improves readability, maintainability and future development.

## Memory, Sentiment and Conversation Flow

The GUI chatbot supports several forms of interaction.

**Memory:** The chatbot can remember a user's name and favourite cybersecurity topic during the current session and recall that information when asked.

**Sentiment detection:** The chatbot recognises expressions associated with worry, frustration and curiosity. It responds with supportive language and, where a cybersecurity topic is identified, provides a relevant practical tip.

**Conversation flow:** Users can request another tip, ask for more information or explore a recognised cybersecurity topic without restarting the application.

**Random responses:** Multiple tips are stored for supported topics, allowing the chatbot to provide varied advice.

## Input Validation and Error Handling

Both applications include input validation and fallback handling to improve reliability.

The applications handle relevant cases such as:

- Empty input.
- Unsupported questions.
- Invalid or unexpected user input.
- Missing audio or media files.
- Recoverable runtime errors.

The GUI also handles empty message submissions and displays responses in the conversation area.

## Voice Greeting and Media Files

The WAV greeting and ASCII artwork are stored in each application's `Media` folder.

The project configuration copies the required media files to the output directory when the application is built. This allows the applications to locate their resources when running locally.

## Continuous Integration

The repository includes a GitHub Actions workflow at:

`.github/workflows/dotnet-ci.yml`

The workflow checks out the repository, sets up the .NET SDK, restores dependencies and builds the project configured in the workflow.

Successful workflow runs help verify that the configured project builds correctly after changes are pushed. The GUI project should also be built and tested separately before submission.

## Version Control and Releases

GitHub and GitHub Desktop are used to track development and maintain the project's source code.

Meaningful commits document improvements made during development, including:

- Adding the GitHub Actions workflow.
- Improving cybersecurity responses.
- Strengthening input validation and error handling.
- Improving the console interface and keyword matching.
- Adding the typing effect.
- Documenting the project.
- Developing the WPF graphical interface.
- Adding dynamic responses, memory and sentiment detection.

Git tags and GitHub releases should identify significant project milestones and include clear version notes.

## How to Run the Applications

### Requirements

- Windows operating system.
- Visual Studio 2022.
- .NET 8 SDK.
- Internet access when cloning the repository or restoring packages, if required.

### Run Part 1 – Console Application

1. Clone or download the repository.
2. Open `CybersecurityAwarenessBot.sln` in Visual Studio.
3. Select the console project as the startup project.
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
- Memory storage and recall.
- Sentiment detection and empathetic responses.
- Unsupported questions and fallback handling.
- Empty input handling.
- GUI interaction through the suggested buttons and message input.
- GitHub Actions workflow checks for the configured project.

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
