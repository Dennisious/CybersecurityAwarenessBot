
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CybersecurityAwarenessBot.GUI.Services
{
    public delegate void ResponseGeneratedHandler(string response);

    public class ChatbotService
    {
        private readonly Random random = new Random();

        private readonly Dictionary<string, List<string>> topicTips =
            new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string>
                {
                    "Use a long, unique password for every important account. A password manager can help you manage them safely.",
                    "Never share your password through messages or email. Legitimate support staff should not ask you to reveal it.",
                    "Enable multi-factor authentication (MFA) to add another layer of protection to your accounts.",
                    "If you reuse a password and one website is breached, change it on every other account where you used it."
                },
                ["phishing"] = new List<string>
                {
                    "Check the sender's full email address before trusting a message. Attackers often use addresses that look almost genuine.",
                    "Be careful with urgent messages claiming your account will be blocked. Visit the official website directly instead of clicking the message link.",
                    "Look for unexpected attachments, spelling mistakes, suspicious links and requests for passwords or banking details.",
                    "If a message looks suspicious, verify it through a separate, trusted contact method before taking action."
                },
                ["scams"] = new List<string>
                {
                    "Never pay an unexpected fee to claim a prize you never entered to win. Verify the organisation independently first.",
                    "Do not share your banking PIN, password or one-time password (OTP) with anyone, even someone claiming to be from your bank.",
                    "Be cautious of offers promising guaranteed profits or asking you to pay urgently. Verify the company before sending money.",
                    "If someone claiming to be your bank contacts you unexpectedly, end the conversation and call the official number on your bank's website or card."
                },
                ["privacy"] = new List<string>
                {
                    "Review your social media privacy settings and limit who can see your personal information.",
                    "Avoid posting your home address, identity document details, live location or other sensitive information publicly.",
                    "Check app permissions regularly. Remove access to your camera, microphone or location when it is not needed.",
                    "Use multi-factor authentication and review which devices and applications can access your accounts."
                },
                ["malware"] = new List<string>
                {
                    "Install software from official stores or trusted websites. Avoid unknown downloads and cracked applications.",
                    "Keep your operating system, browser and security software updated to fix known vulnerabilities.",
                    "Do not open unexpected attachments, especially executable files, even if the message appears familiar.",
                    "If you suspect malware, disconnect the affected device from the network when appropriate and use trusted security software to investigate."
                },
                ["wifi"] = new List<string>
                {
                    "Avoid accessing sensitive accounts on public Wi-Fi when possible. Use mobile data or a trusted connection instead.",
                    "Confirm the correct Wi-Fi network name with the venue. Attackers can create networks with misleading names.",
                    "Keep your device updated and disable automatic connections to unfamiliar Wi-Fi networks.",
                    "Never ignore browser security warnings when accessing websites over public Wi-Fi."
                },
                ["mfa"] = new List<string>
                {
                    "Enable multi-factor authentication on your email, banking and social media accounts.",
                    "An authenticator app or passkey can offer stronger protection than relying only on a password.",
                    "Never approve an unexpected sign-in request. It could mean someone is trying to access your account.",
                    "Keep your recovery methods secure so you can regain access if you lose your device."
                }
            };

        private readonly Dictionary<string, string[]> topicKeywords =
            new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new[] { "password", "passphrase", "login credentials" },
                ["phishing"] = new[] { "phishing", "fake email", "suspicious link", "suspicious email" },
                ["scams"] = new[] { "scam", "scams", "fraud", "fraudulent", "online fraud" },
                ["privacy"] = new[] { "privacy", "personal information", "data protection" },
                ["malware"] = new[] { "malware", "virus", "ransomware", "spyware" },
                ["wifi"] = new[] { "public wi-fi", "public wifi", "wi-fi", "wifi" },
                ["mfa"] = new[] { "mfa", "multi-factor", "two-factor", "2fa" }
            };

        private string currentTopic = "password";
        private string userName = "";
        private string favoriteTopic = "";

        public event ResponseGeneratedHandler? ResponseGenerated;

        public string GenerateResponse(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return PublishResponse(
                    "Please type a message or choose one of the suggested topics.");
            }

            string message = input.Trim();
            string lowerMessage = message.ToLowerInvariant();

            RememberName(message);
            RememberInterest(lowerMessage);

            // Check memory questions BEFORE detecting ordinary topic keywords.
            if (ContainsAny(lowerMessage,
                "what is my name",
                "what's my name",
                "do you remember my name",
                "tell me my name"))
            {
                return PublishResponse(
                    userName == ""
                        ? "You haven't told me your name yet. Try saying, 'My name is Alex'."
                        : $"Your name is {userName}.");
            }

            if (ContainsAny(lowerMessage,
                "what is my favorite topic",
                "what is my favourite topic",
                "what's my favorite topic",
                "what's my favourite topic",
                "what is my favorite cybersecurity topic",
                "what is my favourite cybersecurity topic",
                "what topic am i interested in",
                "what topic do i like",
                "what am i interested in",
                "do you remember my favorite topic",
                "do you remember my favourite topic",
                "remember my favorite topic",
                "remember my favourite topic"))
            {
                return PublishResponse(
                    favoriteTopic == ""
                        ? "You haven't told me your favourite cybersecurity topic yet. Try saying, 'My favourite topic is phishing'."
                        : $"I remember that your favourite cybersecurity topic is {GetTopicName(favoriteTopic)}.");
            }

            if (ContainsAny(lowerMessage, "exit", "quit", "goodbye"))
            {
                return PublishResponse(
                    $"Goodbye{GetNameGreeting()}! Remember to stay alert and protect your information online.");
            }

            if (ContainsAny(lowerMessage, "help", "what can you do"))
            {
                return PublishResponse(
                    "I can help you learn about passwords, phishing, online scams, privacy, malware, public Wi-Fi and multi-factor authentication. You can also ask me to explain more or give you another tip.");
            }

            bool followUp = ContainsAny(
                lowerMessage,
                "another tip",
                "more tips",
                "give me another tip",
                "give me another",
                "tell me more",
                "explain more",
                "explain further",
                "go on",
                "continue",
                "more information",
                "more detail");

            string detectedTopic = DetectTopic(lowerMessage);

            if (followUp && detectedTopic == "")
            {
                detectedTopic = currentTopic;
            }

            if (detectedTopic != "")
            {
                currentTopic = detectedTopic;
            }

            string topic = detectedTopic != "" ? detectedTopic : currentTopic;
            bool hasTopic = detectedTopic != "" || followUp;

            string empathy = DetectSentiment(lowerMessage);

            if (hasTopic)
            {
                string tip = GetRandomTip(topic);
                string topicName = GetTopicName(topic);
                string response = "";

                if (empathy != "")
                {
                    response += empathy + "\n\n";
                }

                if (ContainsAny(lowerMessage,
                    "explain more", "explain further", "more detail"))
                {
                    if (topic == "phishing")
                    {
                        response += "Phishing is when someone tries to trick you into revealing information or clicking a dangerous link by pretending to be trustworthy. ";
                    }
                    else if (topic == "scams")
                    {
                        response += "An online scam is a dishonest attempt to steal your money, information or account access. Scammers may pretend to be banks, employers or people you know. ";
                    }
                    else if (topic == "password")
                    {
                        response += "A strong password is difficult to guess and is not reused across different accounts. A long passphrase made from unrelated words can be easier to remember. ";
                    }
                    else if (topic == "privacy")
                    {
                        response += "Online privacy means controlling how your personal information is collected, shared and used. Review privacy settings and think carefully before sharing sensitive details. ";
                    }
                    else if (topic == "malware")
                    {
                        response += "Malware is harmful software designed to damage devices, steal information or gain unauthorised access. Keep your software updated and avoid untrusted downloads. ";
                    }
                    else if (topic == "wifi")
                    {
                        response += "Public Wi-Fi can expose you to risks if a network is fake or poorly secured. Verify the network and avoid sensitive activities on untrusted connections. ";
                    }
                    else if (topic == "mfa")
                    {
                        response += "Multi-factor authentication adds another verification step beyond your password, making it harder for someone to access your account with a stolen password alone. ";
                    }
                }

                response += $"Here is a tip to help you with {topicName}: {tip}";

                if (favoriteTopic != "" &&
                    favoriteTopic.Equals(topic, StringComparison.OrdinalIgnoreCase))
                {
                    response += $"\n\nI remember that {topicName} is your favourite cybersecurity topic.";
                }

                return PublishResponse(response);
            }

            if (empathy != "")
            {
                return PublishResponse(
                    empathy + "\n\nYou can ask me about passwords, phishing, online scams, privacy, malware, public Wi-Fi or multi-factor authentication. I'll help you work through it.");
            }

            if (ContainsAny(lowerMessage,
                "my name is", "i am ", "i'm "))
            {
                if (userName != "")
                {
                    return PublishResponse(
                        $"Nice to meet you, {userName}! You can ask me a cybersecurity question whenever you're ready.");
                }
            }

            if (ContainsAny(lowerMessage,
                "hello", "hi", "hey", "good morning", "good afternoon"))
            {
                return PublishResponse(
                    $"Hello{GetNameGreeting()}! What would you like to learn about cybersecurity today?");
            }

            return PublishResponse(
                "I'm not sure I understand. Could you rephrase your question? You can ask me about passwords, phishing, scams, privacy, malware, public Wi-Fi or multi-factor authentication. Type 'help' to see what I can do.");
        }

        private string PublishResponse(string response)
        {
            ResponseGenerated?.Invoke(response);
            return response;
        }

        private string DetectTopic(string message)
        {
            foreach (var topic in topicKeywords)
            {
                foreach (string keyword in topic.Value)
                {
                    if (ContainsPhrase(message, keyword))
                    {
                        return topic.Key;
                    }
                }
            }

            return "";
        }

        private string GetRandomTip(string topic)
        {
            if (topicTips.TryGetValue(topic, out List<string>? tips))
            {
                return tips[random.Next(tips.Count)];
            }

            return "Keep your software updated, use unique passwords and be cautious when sharing personal information online.";
        }

        private string DetectSentiment(string message)
        {
            if (ContainsAny(message,
                "worried", "scared", "afraid", "anxious", "nervous"))
            {
                return "It's understandable to feel worried about online threats. You don't have to solve everything at once. Let's take one practical step together.";
            }

            if (ContainsAny(message,
                "frustrated", "confused", "upset", "overwhelmed"))
            {
                return "I understand this can feel frustrating. Let's break it down into simple steps and work through it together.";
            }

            if (ContainsAny(message,
                "curious", "interested", "want to learn", "excited"))
            {
                return "It's great that you're curious about cybersecurity! Learning a little at a time can make a real difference.";
            }

            return "";
        }

        private void RememberName(string message)
        {
            Match match = Regex.Match(
                message,
                @"\b(?:my name is|i am|i'm)\s+([a-zA-Z][a-zA-Z'-]{0,29})",
                RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string candidate = match.Groups[1].Value;

                if (!new[]
                {
                    "worried", "scared", "afraid", "curious",
                    "interested", "frustrated", "confused",
                    "happy", "sad", "interested"
                }.Contains(candidate, StringComparer.OrdinalIgnoreCase))
                {
                    userName = char.ToUpper(candidate[0]) +
                               candidate.Substring(1);
                }
            }
        }

        private void RememberInterest(string message)
        {
            bool statesInterest = ContainsAny(
                message,
                "i'm interested in",
                "i am interested in",
                "my favorite topic is",
                "my favourite topic is",
                "my favorite cybersecurity topic is",
                "my favourite cybersecurity topic is",
                "my favorite is",
                "my favourite is");

            if (statesInterest)
            {
                string topic = DetectTopic(message);

                if (topic != "")
                {
                    favoriteTopic = topic;
                }
            }
        }

        private string GetNameGreeting()
        {
            return userName == "" ? "" : $", {userName}";
        }

        private static string GetTopicName(string topic)
        {
            return topic switch
            {
                "password" => "password safety",
                "phishing" => "phishing",
                "scams" => "online scams",
                "privacy" => "privacy",
                "malware" => "malware",
                "wifi" => "public Wi-Fi safety",
                "mfa" => "multi-factor authentication",
                _ => "cybersecurity"
            };
        }

        private static bool ContainsAny(
            string message,
            params string[] phrases)
        {
            return phrases.Any(phrase => ContainsPhrase(message, phrase));
        }

        private static bool ContainsPhrase(string message, string phrase)
        {
            string pattern = $@"(?<!\w){Regex.Escape(phrase)}(?!\w)";
            return Regex.IsMatch(
                message,
                pattern,
                RegexOptions.IgnoreCase);
        }
    }
}
