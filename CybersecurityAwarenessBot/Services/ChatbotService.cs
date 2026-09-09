using System;

namespace CybersecurityAwarenessBot.Services
{
    /// <summary>
    /// Provides cybersecurity awareness responses based on user input.
    /// </summary>
    public class ChatbotService
    {
        /// <summary>
        /// Generates an appropriate cybersecurity response
        /// based on the user's question.
        /// </summary>
        /// <param name="userInput">
        /// The question or message entered by the user.
        /// </param>
        /// <param name="userName">
        /// The name of the current user.
        /// </param>
        /// <returns>
        /// A suitable cybersecurity awareness response.
        /// </returns>
        public string GetResponse(string userInput, string userName)
        {
            string input = userInput.Trim().ToLowerInvariant();

            if (ContainsAny(
                input,
                "how are you",
                "how are things",
                "how do you feel"))
            {
                return $"I'm doing well, {userName}! " +
                       "I'm ready to help you learn how to stay safer online.";
            }

            if (ContainsAny(
                input,
                "purpose",
                "what is your purpose",
                "why were you created",
                "what do you do"))
            {
                return "My purpose is to provide practical cybersecurity " +
                       "awareness and help people recognise common online threats.";
            }

            if (ContainsAny(
                input,
                "what can i ask",
                "what can i ask you",
                "what do you know",
                "topics",
                "help"))
            {
                return "You can ask me about password safety, phishing, " +
                       "suspicious links, safe browsing, malware, social " +
                       "engineering, and general online safety.";
            }

            if (ContainsAny(
                input,
                "password",
                "passwords"))
            {
                return "For safer passwords, use a long and unique password " +
                       "for each important account. Avoid easily guessed " +
                       "information such as your name or birthday. A password " +
                       "manager can also help you create and store strong passwords.";
            }

            if (ContainsAny(
                input,
                "phishing",
                "phishing email",
                "phishing emails"))
            {
                return "Phishing is an attempt to trick you into revealing " +
                       "sensitive information, such as passwords or banking " +
                       "details. Be suspicious of unexpected messages, urgent " +
                       "requests, unusual links, and requests for confidential " +
                       "information. Verify the sender before taking action.";
            }

            if (ContainsAny(
                input,
                "suspicious link",
                "suspicious links",
                "unknown link",
                "strange link"))
            {
                return "Do not click a suspicious link immediately. Check the " +
                       "sender, inspect the destination carefully, and when " +
                       "possible, visit the organisation's official website " +
                       "directly instead of using the link in the message.";
            }

            if (ContainsAny(
                input,
                "safe browsing",
                "browse safely",
                "browsing"))
            {
                return "For safer browsing, keep your browser and operating " +
                       "system updated, use secure websites, avoid downloading " +
                       "files from untrusted sources, and be careful when " +
                       "entering personal information online.";
            }

            if (ContainsAny(
                input,
                "malware",
                "virus",
                "ransomware"))
            {
                return "Malware is malicious software designed to damage " +
                       "systems, steal information, or disrupt operations. " +
                       "Keep your software updated, avoid suspicious downloads, " +
                       "and use reputable security software to reduce your risk.";
            }

            if (ContainsAny(
                input,
                "social engineering",
                "manipulation",
                "scam",
                "scams"))
            {
                return "Social engineering involves manipulating people into " +
                       "giving away information or performing unsafe actions. " +
                       "Attackers may create urgency, fear, or trust to influence " +
                       "you. Stop, verify the request, and avoid sharing sensitive " +
                       "information without confirmation.";
            }

            if (ContainsAny(
                input,
                "safe online",
                "online safety",
                "stay safe",
                "protect myself"))
            {
                return "To stay safer online, use strong unique passwords, " +
                       "enable multi-factor authentication where available, " +
                       "keep your software updated, avoid suspicious links, " +
                       "and think carefully before sharing personal information.";
            }

            if (ContainsAny(
                input,
                "hack",
                "hacked",
                "account hacked"))
            {
                return "If you think an account has been hacked, change the " +
                       "password immediately from a trusted device, enable " +
                       "multi-factor authentication, review recent account " +
                       "activity, and contact the service provider if necessary.";
            }

            if (ContainsAny(
                input,
                "personal information",
                "private information",
                "personal details"))
            {
                return "Avoid sharing sensitive personal information unless " +
                       "you are certain who you are dealing with and why the " +
                       "information is required. Criminals can use personal " +
                       "details for identity theft and social engineering.";
            }

            if (ContainsAny(
                input,
                "thank you",
                "thanks",
                "thank"))
            {
                return $"You're welcome, {userName}! " +
                       "Staying informed is one of the best ways to improve " +
                       "your cybersecurity.";
            }

            if (ContainsAny(
                input,
                "hello",
                "hi",
                "hey",
                "good morning",
                "good afternoon",
                "good evening"))
            {
                return $"Hello, {userName}! " +
                       "What cybersecurity topic would you like to explore?";
            }

            return GetFallbackResponse();
        }

        /// <summary>
        /// Checks whether the user's input contains any of the supplied keywords.
        /// </summary>
        /// <param name="input">Normalised user input.</param>
        /// <param name="keywords">Keywords or phrases to search for.</param>
        /// <returns>
        /// True when a matching keyword or phrase is found; otherwise false.
        /// </returns>
        private bool ContainsAny(string input, params string[] keywords)
        {
            foreach (string keyword in keywords)
            {
                if (input.Contains(keyword))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Provides a helpful response when the chatbot does not recognise
        /// the user's question.
        /// </summary>
        /// <returns>A friendly fallback response.</returns>
        private string GetFallbackResponse()
        {
            return "I didn't quite understand that. Could you rephrase your " +
                   "question? You can ask about passwords, phishing, suspicious " +
                   "links, safe browsing, malware, social engineering, scams, " +
                   "or general online safety.";
        }
    }
}