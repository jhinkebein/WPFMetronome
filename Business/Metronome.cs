using System.Threading;
using System.Media;
using WPFMetronome.Properties;

//Controls the sound output of the metronome
namespace WPFMetronome
{
    class Metronome
    {
        private SoundPlayer player = new SoundPlayer(Resources.beat); //Soundplayer for bpm click
        private System.Threading.Timer timer; //timer for bpm timing

        public void playSound(int bpm)
        {
            timer = new System.Threading.Timer(new TimerCallback(playBeat), player, 0, 60000 / bpm); //https://docs.microsoft.com/en-us/dotnet/api/system.threading.timer?view=net-5.0 for more info
            //60000 ms in 1 minute divided by bpm gives correct timing
            //Tested vs a metronome app on my phone, and the one google gives when you search 'metronome'. Behaves the same
        }
        private void playBeat(object soundToPlay)
        {
            SoundPlayer currentSound = (SoundPlayer)soundToPlay; 
            currentSound.Play();
        }
        public void stopSound()
        {
            timer.Dispose();
            timer = null;
        }
    }
    
}
