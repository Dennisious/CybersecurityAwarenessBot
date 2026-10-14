
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using CybersecurityAwarenessBot.GUI.Services;

namespace CybersecurityAwarenessBot.GUI
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotService chatbot = new ChatbotService();
        private readonly AudioService audioService = new AudioService();

        public MainWindow()
        {
            InitializeComponent();

            // Load the original ASCII logo from the Media folder.
            LoadAsciiLogo();

            // Play the welcome greeting when CyberGuard opens.
            audioService.PlayWelcomeSound();

            // Connect the send button and Enter key.
            SendButton.Click += SendButton_Click;
            MessageInput.KeyDown += MessageInput_KeyDown;

            // Connect the suggested cybersecurity topic buttons.
            PasswordTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Tell me about password safety");

            PhishingTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Give me a phishing tip");

            ScamsTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Give me an online scam tip");

            PrivacyTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Tell me about privacy");

            // Receive chatbot responses through the delegate event.
            chatbot.ResponseGenerated += Chatbot_ResponseGenerated;
        }

        private void LoadAsciiLogo()
        {
            try
            {
                string logoPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Media",
                    "cybersecurity-logo.txt");

                if (File.Exists(logoPath))
                {
                    AsciiLogoText.Text = File.ReadAllText(
                        logoPath,
                        Encoding.UTF8).TrimEnd();
                }
                else
                {
                    AsciiLogoText.Text =
                        "CYBERGUARD\nCYBERSECURITY\nLEARN • PROTECT • STAY SAFE";
                }
            }
            catch (IOException)
            {
                AsciiLogoText.Text = "CYBERGUARD";
            }
            catch (UnauthorizedAccessException)
            {
                AsciiLogoText.Text = "CYBERGUARD";
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void MessageInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
                e.Handled = true;
            }
        }

        private void SendSuggestedMessage(string message)
        {
            MessageInput.Text = message;
            SendMessage();
        }

        private void SendMessage()
        {
            string message = MessageInput.Text.Trim();

            // Ignore empty messages and keep the input ready.
            if (string.IsNullOrWhiteSpace(message))
            {
                MessageInput.Focus();
                return;
            }

            // Display the user's message.
            AddChatMessage("YOU", message, true);

            MessageInput.Clear();

            try
            {
                // Generate a response using the chatbot service.
                chatbot.GenerateResponse(message);
            }
            catch (Exception)
            {
                // Handle unexpected errors without crashing the GUI.
                AddChatMessage(
                    "CYBERGUARD",
                    "Sorry, something went wrong while processing that message. Please try again.",
                    false);
            }

            MessageInput.Focus();
        }

        private void Chatbot_ResponseGenerated(string response)
        {
            AddChatMessage("CYBERGUARD", response, false);
        }

        private void AddChatMessage(
            string sender,
            string message,
            bool isUser)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(
                        isUser ? "#164E63" : "#153A42")),
                CornerRadius = new CornerRadius(10),
                Padding = new Thickness(14),
                Margin = new Thickness(0, 0, 0, 12),
                HorizontalAlignment = isUser
                    ? HorizontalAlignment.Right
                    : HorizontalAlignment.Left,
                MaxWidth = 680
            };

            StackPanel content = new StackPanel();

            TextBlock senderLabel = new TextBlock
            {
                Text = sender,
                Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString(
                        isUser ? "#67E8F9" : "#5EEAD4")),
                FontSize = 10,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(0, 0, 0, 6)
            };

            TextBlock messageText = new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap,
                Foreground = new SolidColorBrush(
                    (Color)ColorConverter.ConvertFromString("#E2E8F0")),
                FontSize = 13,
                LineHeight = 21
            };

            content.Children.Add(senderLabel);
            content.Children.Add(messageText);
            bubble.Child = content;

            ChatPanel.Children.Add(bubble);

            // Automatically scroll to the newest message.
            ChatScrollViewer.ScrollToEnd();
        }
    }
}
