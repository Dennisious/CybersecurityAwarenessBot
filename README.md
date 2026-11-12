# Cybersecurity Awareness Bot

## Project Overview

The Cybersecurity Awareness Bot is a C# application developed to promote cybersecurity awareness among South African citizens. It provides practical guidance on common online threats and encourages safer digital behaviour.

The project was developed as part of the C# Programming Portfolio of Evidence (POE) and consists of three development parts:

- **Part 1 – Console Application:** A text-based chatbot featuring a voice greeting, personalised responses, cybersecurity advice and input validation.
- **Part 2 – WPF GUI Application:** A graphical chatbot featuring dynamic responses, random cybersecurity tips, conversation flow, session-based memory and sentiment detection.
- **Part 3 – Enhanced Cybersecurity Assistant:** An expanded WPF application with task management, database integration, automatic reminders, an interactive cybersecurity quiz, natural-language processing simulation and an activity log.

## Project Objectives

The objectives of the project are to:

- Educate users about common cybersecurity threats.
- Provide practical advice for safer online behaviour.
- Recognise cybersecurity keywords and user questions.
- Generate varied responses to encourage continued interaction.
- Personalise conversations using remembered information.
- Detect user sentiment and respond empathetically.
- Allow users to create and manage cybersecurity-related tasks.
- Store task information using a MySQL database.
- Provide automatic reminders for scheduled tasks.
- Test cybersecurity knowledge through an interactive quiz.
- Recognise different user commands and phrasings.
- Maintain an activity log of user actions.
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

### Part 3 – Enhanced WPF Application

**Task Assistant and Reminders**

- Create cybersecurity-related tasks.
- Add a task title and description.
- Set an optional reminder date and time.
- View saved tasks.
- Mark tasks as completed.
- Delete tasks.
- Store and retrieve task information using MySQL.
- Check scheduled reminders while the application is running.
- Display reminder notifications when tasks become due.

**Cybersecurity Mini-Game Quiz**

- 15 cybersecurity quiz questions.
- Multiple-choice and true-or-false questions.
- One question displayed at a time.
- Immediate feedback after answering.
- Explanations to help users understand correct answers.
- Score tracking.
- Final quiz results.
- Topics include phishing, password safety, safe browsing and social engineering.

**Natural-Language Processing Simulation**

- Recognises different ways of expressing supported commands.
- Uses C# string methods, collections and keyword matching.
- Recognises task and reminder requests.
- Recognises commands to start the cybersecurity quiz.
- Recognises cybersecurity topics such as passwords and phishing.
- Supports requests such as "What have you done for me?" to summarise recent actions.
- Processes commands locally without using an external AI API.

**Activity Log**

- Records important user actions.
- Stores action descriptions and timestamps.
- Tracks task-related activities.
- Records reminder notifications.
- Records quiz activity.
- Records supported chatbot actions.
- Displays recent activity.
- Provides a Show More option to view additional entries.

## Technologies Used

- C#
- .NET 8
- WPF (Windows Presentation Foundation)
- XAML
- Visual Studio 2022
- MySQL
- MySqlConnector
- MySQL Workbench
- Git and GitHub
- GitHub Actions
- WAV audio files

## Project Structure

The repository contains the original console application and the enhanced WPF GUI application.

```text
CybersecurityAwarenessBot/
├── .github/
│   └── workflows/
│       └── dotnet-ci.yml
├── CybersecurityAwarenessBot/
│   ├── Media/
│   ├── Models/
│   ├── Services/
│   ├── Program.cs
│   └── CybersecurityAwarenessBot.csproj
├── CybersecurityAwarenessBot.GUI/
│   ├── Media/
│   ├── Models/
│   │   └── CyberTask.cs
│   ├── Services/
│   │   ├── AudioServices.cs
│   │   ├── ChatbotServices.cs
│   │   └── TaskServices.cs
│   ├── DatabaseService.cs
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

Locates and plays the welcome WAV audio file.

**ChatbotService**

Processes user input, recognises cybersecurity topics and generates relevant responses.

**ConsoleUIService**

Manages console presentation, including colours, section headings, user prompts, response formatting and the typing effect.

### WPF GUI Application

**App.xaml and App.xaml.cs**

Define and initialise the WPF application.

**MainWindow.xaml**

Defines the graphical interface, including the chatbot, task assistant, quiz and activity log sections.

**MainWindow.xaml.cs**

Connects the graphical interface to the application services. It handles user interactions, task operations, quiz functionality, reminders and activity logging.

**ChatbotServices.cs**

Handles chatbot responses, cybersecurity topic recognition, conversation flow and supported natural-language commands.

**AudioServices.cs**

Handles welcome audio playback.

**DatabaseService.cs**

Creates MySQL database connections and tests database connectivity. The database password is obtained from a Windows environment variable rather than being stored directly in the source code.

**TaskServices.cs**

Handles task-related database operations, including adding, retrieving, completing and deleting tasks.

**CyberTask.cs**

Defines the task model and its properties, including task ID, title, description, reminder date, completion status and creation date.

Separating the interface, models and services improves readability, organisation and maintainability.

## Database Integration

Part 3 uses MySQL to store task information.

The database is named:

`cybersecurity_chatbot`

The task table is named:

`CyberTasks`

The table stores:

- TaskID
- Title
- Description
- ReminderAt
- IsCompleted
- CreatedAt
- CompletedAt

The application uses the MySqlConnector package to communicate with MySQL.

### Database Configuration

The application expects MySQL Server to be installed and running.

The database password is supplied through a Windows environment variable:

`CYBERGUARD_DB_PASSWORD`

The password is not included in the source code or GitHub repository.

Before running Part 3, the user must create the database and the required table in MySQL.

## Input Validation and Error Handling

The applications include input validation and fallback handling to improve reliability.

Relevant cases include:

- Empty input.
- Unsupported questions.
- Invalid or unexpected user input.
- Missing audio or media files.
- Database connection errors.
- Recoverable runtime errors.
- Invalid task information.

The application also provides feedback when users interact with the chatbot, task assistant and quiz.

## Voice Greeting and Media Files

The WAV greeting and ASCII artwork are stored in the applications' Media folders.

The project configurations include the required media resources so that the applications can locate them when running locally.

Audio playback depends on the availability of the required files and a supported Windows environment.

## Continuous Integration

The repository includes a GitHub Actions workflow at:

`.github/workflows/dotnet-ci.yml`

The workflow runs automatically when changes are pushed to the `main` branch or when a pull request targets `main`.

The workflow:

1. Checks out the repository.
2. Sets up the .NET 8 SDK.
3. Restores solution dependencies.
4. Builds `CybersecurityAwarenessBot.sln` in Release configuration.

A successful workflow run confirms that the automated build completed for the projects included in the solution. It does not replace manual application testing.

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
- Integrating MySQL database functionality.
- Adding task management and reminders.
- Implementing the cybersecurity quiz.
- Adding natural-language command recognition.
- Implementing the activity log.
- Updating project documentation and configuration.

GitHub tags and releases identify significant project milestones and should include clear version notes describing the features and changes in each version.

## How to Run the Applications

### Requirements

- Windows operating system.
- Visual Studio 2022 with the .NET desktop development workload.
- .NET 8 SDK.
- MySQL Server.
- MySQL Workbench (recommended for database management).
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

### Run Part 3 – Enhanced WPF Application

1. Install and start MySQL Server.
2. Create the `cybersecurity_chatbot` database and `CyberTasks` table.
3. Configure the `CYBERGUARD_DB_PASSWORD` Windows environment variable.
4. Open `CybersecurityAwarenessBot.sln` in Visual Studio.
5. Restore the NuGet packages.
6. Select `CybersecurityAwarenessBot.GUI` as the startup project.
7. Build and run the application.
8. Test the chatbot, task assistant, reminders, quiz and activity log.

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
- GUI interaction through buttons and message input.
- Successful MySQL database connection.
- Creating, viewing, completing and deleting tasks.
- Saving tasks to the database.
- Creating reminders and displaying reminder notifications.
- Starting the quiz and displaying questions.
- Multiple-choice and true-or-false answers.
- Immediate quiz feedback and score updates.
- Natural-language task and quiz commands.
- Activity log entries and action summaries.
- GitHub Actions workflow execution and successful solution builds.

These checks cover the main application features. Additional automated unit and integration tests could further improve test coverage.

## Demonstration Video

**Part 3 – Enhanced Cybersecurity Awareness Bot Demonstration**

YouTube link: **[Insert the unlisted Part 3 demonstration video link here before submission]**

The demonstration should explain the application's purpose, interface, project structure, database connection, task management, reminders, cybersecurity quiz, natural-language processing simulation and activity log.

## Future Improvements

Potential future enhancements include:

- Persistent chatbot memory across application sessions.
- Additional cybersecurity topics and South African safety resources.
- More advanced natural-language processing.
- Additional quiz questions and difficulty levels.
- Automated unit and integration tests.
- Accessibility improvements.
- More advanced reporting and activity filtering.

## Author

**Dennisious Moima**

C# Programming Portfolio of Evidence (POE)

- Part 1 – Console Application
- Part 2 – WPF GUI Application
- Part 3 – Enhanced Cybersecurity Awareness Application
