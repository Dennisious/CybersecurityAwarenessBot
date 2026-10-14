
using System;
using System.IO;
using System.Media;

namespace CybersecurityAwarenessBot.GUI.Services
{
    public class AudioService
    {
        private SoundPlayer? player;

        public void PlayWelcomeSound()
        {
            try
            {
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Media",
                    "Welcome.wav");

                if (!File.Exists(audioPath))
                {
                    return;
                }

                player = new SoundPlayer(audioPath);
                player.Play();
            }
            catch (Exception)
            {
                // The chatbot should still open if audio is unavailable.
            }
        }
    }
}
