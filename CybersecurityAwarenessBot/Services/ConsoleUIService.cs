using System;
using System.IO;

namespace CybersecurityAwarenessBot.Services
{
    /// <summary>
    /// Handles the visual presentation of the Cybersecurity Awareness Bot
    /// in the console application.
    /// </summary>
    public class ConsoleUIService
    {
        /// <summary>
        /// Displays the cybersecurity ASCII logo.
        /// </summary>
        public void DisplayLogo()
        {
            string logoPath = Path.Combine(
                AppContext.BaseDirectory,
                "Media",
                "cybersecurity-logo.txt"
            );

            if (File.Exists(logoPath))
            {
                string logo = File.ReadAllText(logoPath);

                Console.WriteLine(logo);
            }
            else
            {
                DisplayWarning(
                    "Cybersecurity logo could not be found."
                );
            }
        }

        /// <summary>
        /// Displays a formatted section heading.
        /// </summary>
        /// <param name="title">The title of the section.</param>
        public void DisplaySectionHeader(string title)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.WriteLine($"  {title}");

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the personalised welcome message.
        /// </summary>
        /// <param name="userName">The user's name.</param>
        public void DisplayPersonalisedWelcome(string userName)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(
                "┌──────────────────────────────────────────────────────────┐"
            );

            Console.WriteLine(
                $"│  Welcome, {userName}! 👋"
            );

            Console.WriteLine(
                "│                                                          │"
            );

            Console.WriteLine(
                "│  I'm your Cybersecurity Awareness Bot.                  │"
            );

            Console.WriteLine(
                "│  My goal is to help you stay safe and confident         │"
            );

            Console.WriteLine(
                "│  while using the digital world.                         │"
            );

            Console.WriteLine(
                "│                                                          │"
            );

            Console.WriteLine(
                "│  You can ask me about phishing, passwords, suspicious   │"
            );

            Console.WriteLine(
                "│  links, safe browsing, and other cybersecurity topics.  │"
            );

            Console.WriteLine(
                "└──────────────────────────────────────────────────────────┘"
            );

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the chatbot conversation header.
        /// </summary>
        public void DisplayConversationHeader()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.WriteLine(
                "  🔐 CYBERSECURITY CONVERSATION"
            );

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine(
                "Ask me a cybersecurity question."
            );

            Console.WriteLine(
                "Type 'exit' when you are finished."
            );

            Console.WriteLine();
        }

        /// <summary>
        /// Displays the user's input prompt.
        /// </summary>
        /// <param name="userName">The user's name.</param>
        public void DisplayUserPrompt(string userName)
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write($"{userName} > ");

            Console.ResetColor();
        }

        /// <summary>
        /// Displays a chatbot response.
        /// </summary>
        /// <param name="response">The chatbot response.</param>
        public void DisplayBotResponse(string response)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine("  🤖 BOT");

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(
                "  ┌─────────────────────────────────────────────────────────"
            );

            Console.WriteLine($"  │ {response}");

            Console.WriteLine(
                "  └─────────────────────────────────────────────────────────"
            );

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays a warning message.
        /// </summary>
        /// <param name="message">The warning message.</param>
        public void DisplayWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            Console.WriteLine($"  ⚠ {message}");

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays an error message.
        /// </summary>
        /// <param name="message">The error message.</param>
        public void DisplayError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            Console.WriteLine($"  ✖ {message}");

            Console.ResetColor();
            Console.WriteLine();
        }

        /// <summary>
        /// Displays the chatbot goodbye message.
        /// </summary>
        /// <param name="userName">The user's name.</param>
        public void DisplayGoodbye(string userName)
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.WriteLine(
                $"  Thank you for using the Cybersecurity Awareness Bot, {userName}!"
            );

            Console.WriteLine(
                "  Stay alert, stay informed, and stay safe online! 🛡"
            );

            Console.WriteLine(
                "══════════════════════════════════════════════════════════"
            );

            Console.ResetColor();
            Console.WriteLine();
        }
    }
}