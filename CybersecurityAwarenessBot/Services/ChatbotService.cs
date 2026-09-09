using System;

namespace CybersecurityAwarenessBot.Services
{
    /// <summary>
    /// Provides cybersecurity awareness responses based on user input.
    /// </summary>
    public class ChatbotService
    {
        /// <summary>
        /// Generates an appropriate cybersecurity response based on user input.
        /// </summary>
        /// <param name="userInput">The question or message entered by the user.</param>
        /// <param name="userName">The name of the current user.</param>
        /// <returns>A suitable cybersecurity awareness response.</returns>
        public string GetResponse(string userInput, string userName)
        {
            string input = userInput.Trim().ToLowerInvariant();


        // General conversation
        if (ContainsAny(input,
            "how are you",
            "how are things",
            "how do you feel"))
            {
                return $"I'm doing well, {userName}! " +
                       "I'm ready to help you learn how to stay safer online.";
            }

            if (ContainsAny(input,
                "purpose",
                "what is your purpose",
                "why were you created",
                "what do you do"))
            {
                return "My purpose is to provide practical cybersecurity " +
                       "awareness and help people recognise common online threats.";
            }

            if (ContainsAny(input,
                "what can i ask",
                "what can i ask you",
                "what do you know",
                "topics",
                "help"))
            {
                return "You can ask me about passwords, phishing, suspicious " +
                       "links, multi-factor authentication, scams, malware, " +
                       "social engineering, privacy, safe browsing, public Wi-Fi, " +
                       "social media safety, or hacked accounts.";
            }

            if (ContainsAny(input,
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

            if (ContainsAny(input,
                "thank you",
                "thanks",
                "thank"))
            {
                return $"You're welcome, {userName}! " +
                       "Staying informed is one of the best ways to improve " +
                       "your cybersecurity.";
            }

            // Password security
            if (ContainsAny(input,
                "password",
                "passwords"))
            {
                return "For safer passwords, use a long and unique password " +
                       "for each important account. Avoid easily guessed " +
                       "information such as your name or birthday. A password " +
                       "manager can help you create and store strong passwords.";
            }

            if (ContainsAny(input,
                "multi factor",
                "multi-factor",
                "mfa",
                "two factor",
                "2fa",
                "authentication"))
            {
                return "Multi-factor authentication adds another layer of " +
                       "protection to your account. Even if someone obtains " +
                       "your password, they may still be unable to access the " +
                       "account without the second verification factor. Enable " +
                       "MFA whenever it is available.";
            }

            // Phishing and scams
            if (ContainsAny(input,
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

            if (ContainsAny(input,
                "scam",
                "scams",
                "online scam",
                "fraud"))
            {
                return "Online scams often use urgency, fear, attractive offers, " +
                       "or fake identities to manipulate people. Do not send money " +
                       "or sensitive information until you have independently " +
                       "verified who you are dealing with.";
            }

            if (ContainsAny(input,
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

            // Malware
            if (ContainsAny(input,
                "malware",
                "virus",
                "ransomware",
                "trojan",
                "spyware"))
            {
                return "Malware is malicious software designed to damage " +
                       "systems, steal information, or disrupt operations. " +
                       "Keep your software updated, avoid suspicious downloads, " +
                       "and use reputable security software to reduce your risk.";
            }

            // Safe browsing
            if (ContainsAny(input,
                "safe browsing",
                "browse safely",
                "browsing"))
            {
                return "For safer browsing, keep your browser and operating " +
                       "system updated, use secure websites, avoid downloading " +
                       "files from untrusted sources, and be careful when " +
                       "entering personal information online.";
            }

            // Public Wi-Fi
            if (ContainsAny(input,
                "public wifi",
                "public wi-fi",
                "wifi",
                "wi-fi"))
            {
                return "Be careful when using public Wi-Fi. Avoid accessing " +
                       "highly sensitive accounts on untrusted networks when " +
                       "possible. Make sure your device is updated and use " +
                       "secure connections such as HTTPS.";
            }

            // Social engineering
            if (ContainsAny(input,
                "social engineering",
                "manipulation"))
            {
                return "Social engineering involves manipulating people into " +
                       "giving away information or performing unsafe actions. " +
                       "Attackers may create urgency, fear, or trust to influence " +
                       "you. Stop, verify the request, and avoid sharing sensitive " +
                       "information without confirmation.";
            }

            // Personal information and privacy
            if (ContainsAny(input,
                "personal information",
                "private information",
                "personal details",
                "privacy"))
            {
                return "Avoid sharing sensitive personal information unless " +
                       "you are certain who you are dealing with and why the " +
                       "information is required. Criminals can use personal " +
                       "details for identity theft and social engineering.";
            }

            // Social media safety
            if (ContainsAny(input,
                "social media",
                "facebook",
                "instagram",
                "tiktok",
                "whatsapp"))
            {
                return "Protect your social media accounts by using strong " +
                       "unique passwords and multi-factor authentication. Review " +
                       "your privacy settings and avoid publicly sharing details " +
                       "such as your address, passwords, financial information, " +
                       "or other sensitive personal details.";
            }

            // Hacked accounts
            if (ContainsAny(input,
                "hack",
                "hacked",
                "account hacked",
                "my account was hacked"))
            {
                return "If you think an account has been hacked, change the " +
                       "password immediately from a trusted device, enable " +
                       "multi-factor authentication, review recent account " +
                       "activity, and contact the service provider if necessary.";
            }

            // General online safety
            if (ContainsAny(input,
                "safe online",
                "online safety",
                "stay safe",
                "protect myself",
                "cybersecurity"))
            {
                return "To stay safer online, use strong unique passwords, " +
                       "enable multi-factor authentication where available, " +
                       "keep your software updated, avoid suspicious links, " +
                       "and think carefully before sharing personal information.";
            }

            // South African cybersecurity awareness
            if (ContainsAny(input,
                "south africa",
                "south african",
                "sa scam",
                "south african scam"))
            {
                return "Cybersecurity awareness is especially important when " +
                       "using online banking, mobile services, social media, and " +
                       "digital government or business services. Always verify " +
                       "unexpected requests for money, passwords, PINs, or personal information.";
            }

            // Fallback response
            return GetFallbackResponse();
        }

        /// <summary>
        /// Checks whether the user's input contains any supplied keywords.
        /// </summary>
        /// <param name="input">Normalised user input.</param>
        /// <param name="keywords">Keywords or phrases to search for.</param>
        /// <returns>True when a matching keyword or phrase is found.</returns>
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
                   "links, multi-factor authentication, scams, malware, social " +
                   "engineering, privacy, public Wi-Fi, social media safety, " +
                   "hacked accounts, or general online safety.";
        }
    }


}
