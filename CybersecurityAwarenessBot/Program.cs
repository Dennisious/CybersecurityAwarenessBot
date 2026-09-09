using CybersecurityAwarenessBot.Models;
using CybersecurityAwarenessBot.Services;

namespace CybersecurityAwarenessBot
{
    internal class Program
    {
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

            user.Name = Console.ReadLine()?.Trim() ?? string.Empty;

            // Handle an empty name gracefully.
            if (string.IsNullOrWhiteSpace(user.Name))
            {
                consoleUIService.DisplayWarning(
                    "I didn't catch your name. That's okay — I'll call you there for now."
                );

                user.Name = "there";
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

                    string userInput = Console.ReadLine()?.Trim() ?? string.Empty;

                    // Allow the user to exit the chatbot.
                    if (string.Equals(
                        userInput,
                        "exit",
                        StringComparison.OrdinalIgnoreCase))
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
    }
}