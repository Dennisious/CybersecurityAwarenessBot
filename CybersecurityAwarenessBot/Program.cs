using CybersecurityAwarenessBot.Models;
using CybersecurityAwarenessBot.Services;

namespace CybersecurityAwarenessBot
{
    internal class Program
    {
        /// <summary>
        /// Main entry point for the Cybersecurity Awareness Bot.
        /// </summary>
        static void Main(string[] args)
        {
            // Create the application services.
            AudioService audioService = new AudioService();
            ConsoleUIService consoleUIService = new ConsoleUIService();
            ChatbotService chatbotService = new ChatbotService();


        // Create a User object to store the current user's information.
        User user = new User();

            // Play the welcome voice greeting.
            audioService.PlayWelcomeGreeting();

            // Display the cybersecurity ASCII logo.
            consoleUIService.DisplayLogo();

            // Display the main welcome section.
            consoleUIService.DisplaySectionHeader(
                "WELCOME TO CYBERSECURITY AWARENESS BOT"
            );

            // Ask the user for their name.
            Console.Write("What should I call you? ");

            string enteredName = Console.ReadLine()?.Trim() ?? string.Empty;

            // Validate the user's name before continuing.
            if (string.IsNullOrWhiteSpace(enteredName))
            {
                consoleUIService.DisplayWarning(
                    "I didn't catch your name. That's okay — I'll call you there for now."
                );

                user.Name = "there";
            }
            else if (enteredName.Length > 50)
            {
                consoleUIService.DisplayWarning(
                    "That name is quite long. I'll use a shorter version for this session."
                );

                user.Name = enteredName.Substring(0, 50);
            }
            else
            {
                user.Name = enteredName;
            }

            // Display a personalised welcome message.
            consoleUIService.DisplayPersonalisedWelcome(user.Name);

            // Start the cybersecurity conversation.
            StartChat(
                chatbotService,
                consoleUIService,
                user
            );
        }

        /// <summary>
        /// Starts and manages the chatbot conversation.
        /// </summary>
        /// <param name="chatbotService">
        /// Service responsible for generating chatbot responses.
        /// </param>
        /// <param name="consoleUIService">
        /// Service responsible for console presentation and formatting.
        /// </param>
        /// <param name="user">
        /// The current user interacting with the chatbot.
        /// </param>
        static void StartChat(
            ChatbotService chatbotService,
            ConsoleUIService consoleUIService,
            User user)
        {
            // Display the conversation section.
            consoleUIService.DisplayConversationHeader();

            while (true)
            {
                try
                {
                    // Display the user's personalised input prompt.
                    consoleUIService.DisplayUserPrompt(user.Name);

                    string? inputFromConsole = Console.ReadLine();

                    // Handle an unexpected end of console input.
                    if (inputFromConsole == null)
                    {
                        consoleUIService.DisplayWarning(
                            "No more input was received. The chatbot session will now close."
                        );

                        consoleUIService.DisplayGoodbye(user.Name);
                        break;
                    }

                    string userInput = inputFromConsole.Trim();

                    // Allow the user to exit the chatbot.
                    if (IsExitCommand(userInput))
                    {
                        consoleUIService.DisplayGoodbye(user.Name);
                        break;
                    }

                    // Handle empty questions.
                    if (string.IsNullOrWhiteSpace(userInput))
                    {
                        consoleUIService.DisplayWarning(
                            "Please enter a question so I can help you."
                        );

                        continue;
                    }

                    // Prevent excessively long input.
                    if (userInput.Length > 300)
                    {
                        consoleUIService.DisplayWarning(
                            "Your question is quite long. " +
                            "Please shorten it to 300 characters or fewer."
                        );

                        continue;
                    }

                    // Generate a response using the chatbot service.
                    string response = chatbotService.GetResponse(
                        userInput,
                        user.Name
                    );

                    // Display the chatbot response using the enhanced UI.
                    consoleUIService.DisplayBotResponse(response);
                }
                catch (Exception)
                {
                    // Handle unexpected errors without crashing the chatbot.
                    consoleUIService.DisplayError(
                        "Something unexpected happened while processing your request."
                    );

                    Console.WriteLine(
                        "  Please try again. If the problem continues, restart the application."
                    );

                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Determines whether the supplied input is an exit command.
        /// </summary>
        /// <param name="input">The user's input.</param>
        /// <returns>
        /// True when the user enters an accepted exit command; otherwise false.
        /// </returns>
        private static bool IsExitCommand(string input)
        {
            return string.Equals(
                       input,
                       "exit",
                       StringComparison.OrdinalIgnoreCase)
                   ||
                   string.Equals(
                       input,
                       "quit",
                       StringComparison.OrdinalIgnoreCase);
        }
    }


}
