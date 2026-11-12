
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CybersecurityAwarenessBot.GUI.Models;
using CybersecurityAwarenessBot.GUI.Services;

namespace CybersecurityAwarenessBot.GUI
{
    public partial class MainWindow : Window
    {
        private readonly ChatbotService chatbot = new ChatbotService();
        private readonly AudioService audioService = new AudioService();
        private readonly DatabaseService databaseService = new DatabaseService();
        private readonly TaskService taskService = new TaskService();

        private readonly DispatcherTimer reminderTimer = new DispatcherTimer();
        private readonly HashSet<int> notifiedReminderTaskIds = new HashSet<int>();
        private bool isCheckingReminders = false;

        private class ActivityEntry
        {
            public string Timestamp { get; set; } = "";
            public string Action { get; set; } = "";
            public string Details { get; set; } = "";
        }

        private readonly List<ActivityEntry> activityEntries = new List<ActivityEntry>();
        private const int InitialActivityLimit = 10;
        private bool showAllActivities = false;
        private bool quizCompletionLogged = false;

        private class QuizQuestion
        {
            public string Question { get; set; } = "";
            public string[] Options { get; set; } = Array.Empty<string>();
            public int CorrectAnswerIndex { get; set; }
            public string Explanation { get; set; } = "";
            public bool IsTrueFalse { get; set; }
        }

        private readonly List<QuizQuestion> quizQuestions = new List<QuizQuestion>
        {
            new QuizQuestion
            {
                Question = "What is the main purpose of a strong password?",
                Options = new[]
                {
                    "To make your account harder to access",
                    "To protect your account from unauthorized access",
                    "To make your computer faster",
                    "To improve internet speed"
                },
                CorrectAnswerIndex = 1,
                Explanation = "A strong password makes it harder for attackers to access your accounts."
            },
            new QuizQuestion
            {
                Question = "Should you use the same password for all your accounts?",
                Options = new[]
                {
                    "Yes, because it is easier to remember",
                    "Yes, but only for social media",
                    "No, every important account should have a unique password",
                    "Only if the password is long"
                },
                CorrectAnswerIndex = 2,
                Explanation = "If one account is compromised, attackers may try the same password on your other accounts."
            },
            new QuizQuestion
            {
                Question = "What is phishing?",
                Options = new[]
                {
                    "A way of improving internet speed",
                    "A method criminals use to trick people into sharing sensitive information",
                    "A type of computer update",
                    "A safe online shopping method"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Phishing uses fake emails, messages or websites to steal information."
            },
            new QuizQuestion
            {
                Question = "You receive an unexpected email asking you to click a link and confirm your bank password. What should you do?",
                Options = new[]
                {
                    "Click the link immediately",
                    "Reply with your password",
                    "Ignore the warning signs and open the attachment",
                    "Avoid the link and contact your bank through its official channel"
                },
                CorrectAnswerIndex = 3,
                Explanation = "Never share your password through unexpected links. Contact the bank using its official website or phone number."
            },
            new QuizQuestion
            {
                Question = "True or False: It is safe to share your password with a friend if you trust them.",
                IsTrueFalse = true,
                Options = new[] { "True", "False" },
                CorrectAnswerIndex = 1,
                Explanation = "Passwords should remain private. Sharing them can put your account at risk."
            },
            new QuizQuestion
            {
                Question = "What does two-factor authentication (2FA) do?",
                Options = new[]
                {
                    "It adds an extra verification step when signing in",
                    "It removes the need for passwords everywhere",
                    "It makes your phone battery last longer",
                    "It deletes suspicious emails"
                },
                CorrectAnswerIndex = 0,
                Explanation = "2FA adds another layer of protection, such as an authentication app code."
            },
            new QuizQuestion
            {
                Question = "Which website address is generally safer for entering sensitive information?",
                Options = new[]
                {
                    "A website with a strange spelling",
                    "A random link sent by a stranger",
                    "The official website with HTTPS and a verified domain",
                    "Any website with colourful advertisements"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Check the exact domain and HTTPS. HTTPS alone does not prove a website is legitimate."
            },
            new QuizQuestion
            {
                Question = "True or False: Public Wi-Fi can expose your information if you are not careful.",
                IsTrueFalse = true,
                Options = new[] { "True", "False" },
                CorrectAnswerIndex = 0,
                Explanation = "Public networks can be risky. Avoid sensitive transactions on untrusted networks and use secure connections."
            },
            new QuizQuestion
            {
                Question = "What should you do if you receive an unexpected attachment from an unknown sender?",
                Options = new[]
                {
                    "Open it to see what it contains",
                    "Forward it to all your friends",
                    "Download it and disable antivirus",
                    "Do not open it and verify the sender"
                },
                CorrectAnswerIndex = 3,
                Explanation = "Unexpected attachments may contain malware. Verify the sender before opening anything."
            },
            new QuizQuestion
            {
                Question = "What is social engineering?",
                Options = new[]
                {
                    "Building social media applications",
                    "Tricking people into revealing information or performing actions",
                    "Improving computer hardware",
                    "Creating stronger Wi-Fi signals"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Social engineering manipulates human behaviour to gain access or information."
            },
            new QuizQuestion
            {
                Question = "True or False: You should install software updates when they are available from trusted sources.",
                IsTrueFalse = true,
                Options = new[] { "True", "False" },
                CorrectAnswerIndex = 0,
                Explanation = "Updates often fix security weaknesses and protect your devices."
            },
            new QuizQuestion
            {
                Question = "Which of these is an example of a strong password?",
                Options = new[]
                {
                    "123456",
                    "password",
                    "Dennis123",
                    "T!ger_River#82Cloud"
                },
                CorrectAnswerIndex = 3,
                Explanation = "Long, unique passwords or passphrases are harder to guess. Avoid using personal information."
            },
            new QuizQuestion
            {
                Question = "What should you do if you suspect your account has been hacked?",
                Options = new[]
                {
                    "Ignore it",
                    "Change your password using the official website and secure your account",
                    "Share your login details online",
                    "Delete all your security updates"
                },
                CorrectAnswerIndex = 1,
                Explanation = "Secure the account immediately, enable 2FA, and contact the service provider if necessary."
            },
            new QuizQuestion
            {
                Question = "True or False: Oversharing personal information on social media can create security risks.",
                IsTrueFalse = true,
                Options = new[] { "True", "False" },
                CorrectAnswerIndex = 0,
                Explanation = "Personal details can help criminals guess passwords, impersonate you or target you."
            },
            new QuizQuestion
            {
                Question = "What is the safest response to an online offer that promises a large prize but asks you to pay a fee first?",
                Options = new[]
                {
                    "Pay immediately",
                    "Send your banking details",
                    "Treat it as suspicious and verify it independently",
                    "Share it with everyone"
                },
                CorrectAnswerIndex = 2,
                Explanation = "Unexpected prize offers requesting upfront payments are a common scam warning sign."
            }
        };

        private int currentQuestionIndex = 0;
        private int quizScore = 0;
        private bool answerSelected = false;

        public MainWindow()
        {
            InitializeComponent();

            LoadAsciiLogo();
            audioService.PlayWelcomeSound();

            SendButton.Click += SendButton_Click;
            MessageInput.KeyDown += MessageInput_KeyDown;

            PasswordTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Tell me about password safety");

            PhishingTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Give me a phishing tip");

            ScamsTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Give me an online scam tip");

            PrivacyTopicButton.Click += (sender, e) =>
                SendSuggestedMessage("Tell me about privacy");

            chatbot.ResponseGenerated += Chatbot_ResponseGenerated;

            AddTaskButton.Click += AddTaskButton_Click;
            RefreshTasksButton.Click += RefreshTasksButton_Click;
            CompleteTaskButton.Click += CompleteTaskButton_Click;
            DeleteTaskButton.Click += DeleteTaskButton_Click;

            StartQuizButton.Click += StartQuizButton_Click;
            NextQuestionButton.Click += NextQuestionButton_Click;
            RestartQuizButton.Click += RestartQuizButton_Click;

            QuizOption1.Click += QuizOption_Click;
            QuizOption2.Click += QuizOption_Click;
            QuizOption3.Click += QuizOption_Click;
            QuizOption4.Click += QuizOption_Click;

            ShowMoreActivityButton.Click += ShowMoreActivityButton_Click;

            reminderTimer.Interval = TimeSpan.FromSeconds(30);
            reminderTimer.Tick += ReminderTimer_Tick;

            LogActivity("Application", "CyberGuard session started.");

            Loaded += async (sender, e) =>
            {
                await CheckDatabaseConnectionAsync();
            };

            Closed += (sender, e) =>
            {
                reminderTimer.Stop();
            };
        }

        // AUTOMATIC REMINDERS

        private async void ReminderTimer_Tick(object sender, EventArgs e)
        {
            await CheckDueRemindersAsync();
        }

        private async Task CheckDueRemindersAsync()
        {
            if (isCheckingReminders) return;

            isCheckingReminders = true;

            try
            {
                List<CyberTask> savedTasks = await taskService.GetAllTasksAsync();

                foreach (CyberTask task in savedTasks)
                {
                    if (task.IsCompleted || !task.ReminderAt.HasValue)
                        continue;

                    if (task.ReminderAt.Value > DateTime.Now)
                        continue;

                    if (notifiedReminderTaskIds.Contains(task.TaskID))
                        continue;

                    notifiedReminderTaskIds.Add(task.TaskID);

                    LogActivity("Reminder triggered",
                        $"Reminder due for task: {task.Title}");

                    MessageBox.Show(
                        "Your cybersecurity task reminder is due!\n\n" +
                        "Task: " + task.Title + "\n\n" +
                        "Description: " +
                        (string.IsNullOrWhiteSpace(task.Description)
                            ? "No description provided."
                            : task.Description) +
                        "\n\nPlease remember to complete this task.",
                        "CyberGuard Task Reminder",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                LogActivity("Reminder check error",
                    "Could not check reminders: " + ex.Message);
            }
            finally
            {
                isCheckingReminders = false;
            }
        }

        private void StartReminderChecking()
        {
            reminderTimer.Start();
            _ = CheckDueRemindersAsync();
        }

        // ACTIVITY LOG

        private void LogActivity(string action, string details)
        {
            ActivityEntry entry = new ActivityEntry
            {
                Timestamp = DateTime.Now.ToString(
                    "dd MMM yyyy HH:mm:ss",
                    CultureInfo.InvariantCulture),
                Action = action,
                Details = details
            };

            activityEntries.Insert(0, entry);
            RefreshActivityLog();
        }

        private void RefreshActivityLog()
        {
            int totalActivities = activityEntries.Count;

            int visibleCount = showAllActivities
                ? totalActivities
                : Math.Min(InitialActivityLimit, totalActivities);

            ActivityLogList.ItemsSource =
                activityEntries.Take(visibleCount).ToList();

            ActivityLogEmptyText.Visibility = totalActivities == 0
                ? Visibility.Visible : Visibility.Collapsed;

            ActivityLogList.Visibility = totalActivities == 0
                ? Visibility.Collapsed : Visibility.Visible;

            ActivityLogCountText.Text = totalActivities == 0
                ? "No activities recorded yet."
                : $"Showing {visibleCount} of {totalActivities} activities";

            ShowMoreActivityButton.Visibility =
                totalActivities > InitialActivityLimit
                    ? Visibility.Visible : Visibility.Collapsed;

            ShowMoreActivityButton.Content = showAllActivities
                ? "Show Recent Activities"
                : "Show More Activities";

            ActivityLogStatusText.Text = showAllActivities
                ? "ALL ACTIVITIES SHOWN"
                : "SESSION ACTIVITY";
        }

        private void ShowMoreActivityButton_Click(object sender, RoutedEventArgs e)
        {
            showAllActivities = !showAllActivities;
            RefreshActivityLog();
        }

        private string BuildActivitySummary()
        {
            if (activityEntries.Count == 0)
            {
                return "I have not recorded any activities yet. " +
                       "You can start by chatting with me, adding a task " +
                       "or playing the cybersecurity quiz.";
            }

            StringBuilder summary = new StringBuilder();

            summary.AppendLine(
                "Here is a summary of what I have done with you during this session:");
            summary.AppendLine();

            foreach (ActivityEntry entry in activityEntries.Take(10))
            {
                summary.AppendLine(
                    $"• {entry.Timestamp} | {entry.Action}: {entry.Details}");
            }

            summary.AppendLine();
            summary.Append(
                $"You have {activityEntries.Count} recorded activities in total. " +
                "You can view them in the Activity Log tab.");

            return summary.ToString();
        }

        // DATABASE

        private async Task CheckDatabaseConnectionAsync()
        {
            bool connected = await databaseService.TestConnectionAsync();

            if (connected)
            {
                MessageBox.Show(
                    "Successfully connected to the Cybersecurity database!",
                    "Database Connection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                LogActivity("Database",
                    "Successfully connected to the Cybersecurity database.");

                await LoadTasksAsync();
                StartReminderChecking();
            }
            else
            {
                LogActivity("Database",
                    "Database connection could not be established.");

                MessageBox.Show(
                    "Database connection failed. Please check your MySQL settings.",
                    "Database Connection",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // ASCII LOGO

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
                        logoPath, Encoding.UTF8).TrimEnd();
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

        // CHAT AND NLP COMMANDS

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

            if (string.IsNullOrWhiteSpace(message))
            {
                MessageInput.Focus();
                return;
            }

            AddChatMessage("YOU", message, true);
            MessageInput.Clear();

            _ = ProcessChatMessageAsync(message);

            MessageInput.Focus();
        }

        private async Task ProcessChatMessageAsync(string message)
        {
            try
            {
                string normalised = message.ToLowerInvariant();

                // Activity summary command
                if (IsActivitySummaryRequest(normalised))
                {
                    string summary = BuildActivitySummary();

                    LogActivity("Activity summary",
                        "User requested a summary of recent activities.");

                    AddChatMessage("CYBERGUARD", summary, false);
                    return;
                }

                // Start quiz command
                if (ContainsAnyPhrase(normalised,
                    "start quiz",
                    "start the quiz",
                    "begin quiz",
                    "play quiz",
                    "start cybersecurity quiz",
                    "start the cybersecurity quiz",
                    "open the quiz",
                    "test my cybersecurity knowledge"))
                {
                    LogActivity("NLP command",
                        "User started the cybersecurity quiz through chat.");

                    AddChatMessage("CYBERGUARD",
                        "Certainly! I am opening the cybersecurity quiz for you.",
                        false);

                    // Automatically switch to the Quiz tab
                    TabControl mainTabControl =
                        FindVisualChild<TabControl>(this);

                    if (mainTabControl != null)
                    {
                        mainTabControl.SelectedIndex = 2;
                    }

                    // Start the quiz
                    StartQuizButton_Click(this, new RoutedEventArgs());

                    return;
                }

                // Create a reminder or task from chat
                if (IsReminderRequest(normalised) || IsTaskRequest(normalised))
                {
                    await CreateTaskFromChatAsync(message);
                    return;
                }

                // Regular chatbot response
                LogActivity("Chat interaction", message);
                chatbot.GenerateResponse(message);
            }
            catch (Exception ex)
            {
                AddChatMessage("CYBERGUARD",
                    "Sorry, I could not complete that request. " +
                    "Please try again. Details: " + ex.Message,
                    false);
            }
        }

        private bool IsActivitySummaryRequest(string message)
        {
            return ContainsAnyPhrase(message,
                "what have you done for me",
                "what did you do for me",
                "show activity log",
                "show my activities",
                "activity summary",
                "show recent activities",
                "what have you done",
                "show my activity");
        }

        private bool IsReminderRequest(string message)
        {
            return ContainsAnyPhrase(message,
                "remind me",
                "set a reminder",
                "add a reminder",
                "schedule a reminder",
                "reminder to");
        }

        private bool IsTaskRequest(string message)
        {
            return ContainsAnyPhrase(message,
                "add a task",
                "create a task",
                "save a task",
                "remember to",
                "i need to");
        }

        private async Task CreateTaskFromChatAsync(string message)
        {
            string title = message.Trim();
            string lower = title.ToLowerInvariant();

            string[] prefixes =
            {
                "can you remind me to",
                "please remind me to",
                "remind me to",
                "remind me",
                "set a reminder to",
                "set a reminder",
                "add a reminder to",
                "add a reminder",
                "schedule a reminder to",
                "schedule a reminder",
                "add a task to",
                "add a task",
                "create a task to",
                "create a task",
                "save a task to",
                "save a task",
                "remember to",
                "i need to"
            };

            foreach (string prefix in prefixes)
            {
                if (lower.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                {
                    title = title.Substring(prefix.Length).Trim();
                    break;
                }
            }

            title = Regex.Replace(title,
                @"\b(today|tomorrow|tonight|next week)\b",
                "",
                RegexOptions.IgnoreCase).Trim();

            title = Regex.Replace(title,
                @"\s+at\s+\d{1,2}(:\d{2})?\s*(am|pm)?",
                "",
                RegexOptions.IgnoreCase).Trim();

            title = title.Trim(' ', '.', ',', '!', '?');

            if (string.IsNullOrWhiteSpace(title))
            {
                AddChatMessage("CYBERGUARD",
                    "Please tell me what task you would like me to save.",
                    false);
                return;
            }

            DateTime? reminderAt = null;

            if (IsReminderRequest(lower))
            {
                DateTime reminderDate = DateTime.Today;

                if (ContainsAnyPhrase(lower, "tomorrow"))
                {
                    reminderDate = DateTime.Today.AddDays(1);
                }
                else if (ContainsAnyPhrase(lower, "next week"))
                {
                    reminderDate = DateTime.Today.AddDays(7);
                }

                TimeSpan reminderTime = new TimeSpan(9, 0, 0);

                Match timeMatch = Regex.Match(
                    lower,
                    @"\bat\s+(\d{1,2})(?::(\d{2}))?\s*(am|pm)?\b",
                    RegexOptions.IgnoreCase);

                if (timeMatch.Success)
                {
                    int hour = int.Parse(timeMatch.Groups[1].Value);
                    int minute = timeMatch.Groups[2].Success
                        ? int.Parse(timeMatch.Groups[2].Value)
                        : 0;

                    string period = timeMatch.Groups[3].Value.ToLowerInvariant();

                    if (hour < 1 || hour > 12 && period != "")
                    {
                        AddChatMessage("CYBERGUARD",
                            "Please provide a valid reminder time, for example 9 AM or 14:30.",
                            false);
                        return;
                    }

                    if (period == "pm" && hour < 12) hour += 12;
                    if (period == "am" && hour == 12) hour = 0;

                    if (hour > 23 || minute > 59)
                    {
                        AddChatMessage("CYBERGUARD",
                            "That time is not valid. Please try again.",
                            false);
                        return;
                    }

                    reminderTime = new TimeSpan(hour, minute, 0);
                }

                reminderAt = reminderDate.Date.Add(reminderTime);

                if (reminderAt.Value <= DateTime.Now)
                {
                    AddChatMessage("CYBERGUARD",
                        "That reminder time has already passed. Please give me a future date or time.",
                        false);
                    return;
                }
            }

            int taskId = await taskService.AddTaskAsync(
                title,
                "Task created through the CyberGuard chat assistant.",
                reminderAt);

            LogActivity("Task added through chat",
                title);

            if (reminderAt.HasValue)
            {
                LogActivity("Reminder scheduled through chat",
                    $"{title} — {reminderAt.Value:dd MMM yyyy HH:mm}");
            }

            await LoadTasksAsync();

            string response = reminderAt.HasValue
                ? $"Your task has been saved successfully!\n\nTask: {title}\nReminder: {reminderAt.Value:dd MMM yyyy HH:mm}\n\nI will notify you when it is due, as long as CyberGuard is running."
                : $"Your task has been saved successfully!\n\nTask: {title}\n\nYou can view it in the My Tasks tab.";

            AddChatMessage("CYBERGUARD", response, false);
        }

        private static bool ContainsAnyPhrase(
            string message,
            params string[] phrases)
        {
            return phrases.Any(phrase =>
                Regex.IsMatch(
                    message,
                    $@"(?<!\w){Regex.Escape(phrase)}(?!\w)",
                    RegexOptions.IgnoreCase));
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
            ChatScrollViewer.ScrollToEnd();
        }

        // TASK ASSISTANT

        private async void AddTaskButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            string title = TaskTitleInput.Text.Trim();
            string description = TaskDescriptionInput.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show(
                    "Please enter a task title.",
                    "Missing Task Title",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                TaskTitleInput.Focus();
                return;
            }

            DateTime? reminderAt = null;

            if (ReminderDatePicker.SelectedDate.HasValue)
            {
                if (!TimeSpan.TryParseExact(
                    ReminderTimeInput.Text.Trim(),
                    @"hh\:mm",
                    CultureInfo.InvariantCulture,
                    out TimeSpan reminderTime))
                {
                    MessageBox.Show(
                        "Please enter the reminder time in HH:mm format, for example 09:00.",
                        "Invalid Reminder Time",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                reminderAt = ReminderDatePicker.SelectedDate.Value.Date.Add(reminderTime);

                if (reminderAt.Value <= DateTime.Now)
                {
                    MessageBox.Show(
                        "Please select a future reminder date and time.",
                        "Invalid Reminder",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }
            }

            try
            {
                await taskService.AddTaskAsync(title, description, reminderAt);

                LogActivity("Task added", title);

                if (reminderAt.HasValue)
                {
                    LogActivity("Reminder scheduled",
                        $"{title} — {reminderAt.Value:dd MMM yyyy HH:mm}");
                }

                MessageBox.Show(
                    "Your task was successfully saved!",
                    "Task Added",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                TaskTitleInput.Clear();
                TaskDescriptionInput.Clear();
                ReminderDatePicker.SelectedDate = null;
                ReminderTimeInput.Text = "09:00";

                await LoadTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not save your task.\n\n" + ex.Message,
                    "Task Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async Task LoadTasksAsync()
        {
            try
            {
                TasksDataGrid.ItemsSource =
                    await taskService.GetAllTasksAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not load your tasks.\n\n" + ex.Message,
                    "Task Loading Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void RefreshTasksButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            await LoadTasksAsync();

            LogActivity("Tasks refreshed",
                "The saved task list was refreshed from the database.");
        }

        private CyberTask GetSelectedTask()
        {
            return TasksDataGrid.SelectedItem as CyberTask
                   ?? TasksDataGrid.CurrentItem as CyberTask;
        }

        private async void CompleteTaskButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            CyberTask selectedTask = GetSelectedTask();

            if (selectedTask == null)
            {
                MessageBox.Show(
                    "Please select a task first.",
                    "No Task Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            if (selectedTask.IsCompleted)
            {
                MessageBox.Show(
                    "This task is already completed.",
                    "Task Completed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            try
            {
                await taskService.MarkTaskCompletedAsync(selectedTask.TaskID);

                LogActivity("Task completed", selectedTask.Title);

                await LoadTasksAsync();

                MessageBox.Show(
                    "Task marked as completed successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not complete the task.\n\n" + ex.Message,
                    "Task Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private async void DeleteTaskButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            CyberTask selectedTask = GetSelectedTask();

            if (selectedTask == null)
            {
                MessageBox.Show(
                    "Please select a task first.",
                    "No Task Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            MessageBoxResult result = MessageBox.Show(
                "Are you sure you want to delete this task?",
                "Confirm Deletion",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                string deletedTaskTitle = selectedTask.Title;

                await taskService.DeleteTaskAsync(selectedTask.TaskID);

                LogActivity("Task deleted", deletedTaskTitle);

                await LoadTasksAsync();

                MessageBox.Show(
                    "Task successfully deleted.",
                    "Task Deleted",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Could not delete the task.\n\n" + ex.Message,
                    "Task Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        // QUIZ

        private void StartQuizButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            currentQuestionIndex = 0;
            quizScore = 0;
            quizCompletionLogged = false;

            LogActivity("Quiz started",
                $"Cybersecurity quiz started with {quizQuestions.Count} questions.");

            QuizIntroPanel.Visibility = Visibility.Collapsed;
            QuizResultPanel.Visibility = Visibility.Collapsed;
            QuizQuestionPanel.Visibility = Visibility.Visible;

            DisplayQuizQuestion();
        }

        private void DisplayQuizQuestion()
        {
            if (currentQuestionIndex >= quizQuestions.Count)
            {
                ShowQuizResults();
                return;
            }

            answerSelected = false;

            QuizQuestion question = quizQuestions[currentQuestionIndex];

            QuizProgressText.Text =
                $"Question {currentQuestionIndex + 1} of {quizQuestions.Count}";

            QuizScoreText.Text = $"Score: {quizScore}";
            QuizProgressBar.Value = currentQuestionIndex;

            QuizQuestionTypeText.Text = question.IsTrueFalse
                ? "TRUE OR FALSE" : "MULTIPLE CHOICE";

            QuizQuestionText.Text = question.Question;

            Button[] optionButtons =
            {
                QuizOption1, QuizOption2, QuizOption3, QuizOption4
            };

            for (int i = 0; i < optionButtons.Length; i++)
            {
                if (i < question.Options.Length)
                {
                    optionButtons[i].Content = question.Options[i];
                    optionButtons[i].Visibility = Visibility.Visible;
                    optionButtons[i].IsEnabled = true;
                    optionButtons[i].Background =
                        new SolidColorBrush(Color.FromRgb(23, 38, 59));
                }
                else
                {
                    optionButtons[i].Visibility = Visibility.Collapsed;
                }
            }

            QuizFeedbackBorder.Visibility = Visibility.Collapsed;
            NextQuestionButton.Visibility = Visibility.Collapsed;
        }

        private void QuizOption_Click(
            object sender,
            RoutedEventArgs e)
        {
            if (answerSelected) return;
            if (sender is not Button selectedButton) return;

            QuizQuestion question = quizQuestions[currentQuestionIndex];

            Button[] optionButtons =
            {
                QuizOption1, QuizOption2, QuizOption3, QuizOption4
            };

            int selectedAnswerIndex =
                Array.IndexOf(optionButtons, selectedButton);

            answerSelected = true;

            bool isCorrect =
                selectedAnswerIndex == question.CorrectAnswerIndex;

            if (isCorrect)
            {
                quizScore++;

                QuizFeedbackText.Text =
                    "✓ CORRECT!\n\n" + question.Explanation;

                QuizFeedbackBorder.Background =
                    new SolidColorBrush(Color.FromRgb(21, 58, 50));
            }
            else
            {
                string correctAnswer =
                    question.Options[question.CorrectAnswerIndex];

                QuizFeedbackText.Text =
                    "✗ INCORRECT!\n\nCorrect answer: " +
                    correctAnswer + "\n\n" + question.Explanation;

                QuizFeedbackBorder.Background =
                    new SolidColorBrush(Color.FromRgb(91, 35, 42));
            }

            QuizFeedbackBorder.Visibility = Visibility.Visible;
            QuizScoreText.Text = $"Score: {quizScore}";

            foreach (Button button in optionButtons)
            {
                button.IsEnabled = false;
            }

            NextQuestionButton.Visibility = Visibility.Visible;

            NextQuestionButton.Content =
                currentQuestionIndex == quizQuestions.Count - 1
                    ? "View Final Results"
                    : "Next Question ➜";
        }

        private void NextQuestionButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            currentQuestionIndex++;
            DisplayQuizQuestion();
        }

        private void ShowQuizResults()
        {
            QuizQuestionPanel.Visibility = Visibility.Collapsed;
            QuizResultPanel.Visibility = Visibility.Visible;
            QuizProgressBar.Value = quizQuestions.Count;

            QuizFinalScoreText.Text =
                $"Your score: {quizScore} out of {quizQuestions.Count}";

            double percentage =
                (double)quizScore / quizQuestions.Count * 100;

            if (percentage >= 80)
            {
                QuizFinalMessageText.Text =
                    "Excellent work! You have a strong understanding of cybersecurity. Keep practising and stay safe online.";
            }
            else if (percentage >= 50)
            {
                QuizFinalMessageText.Text =
                    "Good effort! You understand some important cybersecurity principles. Review the explanations and keep learning.";
            }
            else
            {
                QuizFinalMessageText.Text =
                    "Keep going! Cybersecurity takes practice. Review the explanations, learn from your mistakes and try again.";
            }

            if (!quizCompletionLogged)
            {
                LogActivity("Quiz completed",
                    $"Final score: {quizScore} out of {quizQuestions.Count} ({percentage:F0}%).");

                quizCompletionLogged = true;
            }
        }

        private void RestartQuizButton_Click(
            object sender,
            RoutedEventArgs e)
        {
            currentQuestionIndex = 0;
            quizScore = 0;
            answerSelected = false;

            QuizResultPanel.Visibility = Visibility.Collapsed;
            QuizIntroPanel.Visibility = Visibility.Visible;
        }

        // FIND THE MAIN TAB CONTROL

        private T FindVisualChild<T>(DependencyObject parent)
            where T : DependencyObject
        {
            for (int i = 0;
                 i < VisualTreeHelper.GetChildrenCount(parent);
                 i++)
            {
                DependencyObject child =
                    VisualTreeHelper.GetChild(parent, i);

                if (child is T result)
                    return result;

                T childResult = FindVisualChild<T>(child);

                if (childResult != null)
                    return childResult;
            }

            return null;
        }
    }
}
