using System;
using System.IO;
using System.Media;

namespace CybersecurityAwarenessBot.Services
{
    /// <summary>
    /// Handles audio functionality for the Cybersecurity Awareness Bot.
    /// </summary>
    public class AudioService
    {
        /// <summary>
        /// Plays the welcome voice greeting when the application starts.
        /// </summary>
        public void PlayWelcomeGreeting()
        {
            try
            {
                string audioPath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Media",
                    "welcome.wav"
                );

                if (!File.Exists(audioPath))
                {
                    Console.WriteLine(
                        "[Audio Warning] Welcome audio file could not be found."
                    );

                    return;
                }

                using SoundPlayer player = new SoundPlayer(audioPath);
                player.PlaySync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(
                    $"[Audio Warning] The welcome greeting could not be played: {ex.Message}"
                );
            }
        }
    }
}