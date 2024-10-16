namespace Contorio.CharGraphics.Sound
{
    public class SoundPlayer
    {
        public static void Play(Note[] tune)
        {
            foreach (Note n in tune)
            {
                if (n.NoteTone == Tone.REST)
                {
                    Thread.Sleep((int)n.NoteDuration);
                }
                else
                {
                    Console.Beep((int)n.NoteTone, (int)n.NoteDuration);
                }
            }
        }
    }
}
