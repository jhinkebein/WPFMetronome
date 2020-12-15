using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WPFMetronome.BackEnd;
using System.Text.RegularExpressions;


namespace WPFMetronome
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Metronome metronome = new Metronome();
        bpmDB db = new bpmDB();
        public MainWindow()
        {
            InitializeComponent();
            populateCB();
        }
        
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int bpm = Convert.ToInt16(BPMControl.Value);
            if (bpm > 0) //User can enter whatever they want into an intupdown, but it will only return a valid number or 0
            {
                metronome.playSound(bpm);
                btnStop.IsEnabled = true;
                btnStart.IsEnabled = false; //Otherwise you can start multiple metronomes. "I don't suck, it's polyrhythmic"
            } 
            else
            {
                MessageBox.Show("Enter a valid BPM");
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            metronome.stopSound();
            btnStop.IsEnabled = false;
            btnStart.IsEnabled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int bpm = Convert.ToInt16(BPMControl.Value);
            if (!string.IsNullOrEmpty(txtSongTitle.Text) && !string.IsNullOrEmpty(txtArtist.Text) && bpm != 0)
            {
                db.addSong(txtSongTitle.Text, txtArtist.Text, bpm);
                cbSongs.Items.Add($"{txtSongTitle.Text} by {txtArtist.Text} - {bpm} BPM"); //using populateCB adds ALL songs in db, not just new ones
                MessageBox.Show($"{txtSongTitle.Text} added to database!");
            }
            else
            {
                if (string.IsNullOrEmpty(txtSongTitle.Text))
                {
                    MessageBox.Show("Song title cannot be blank!");
                }
                else
                {
                    MessageBox.Show("Enter a valid BPM");
                }
            }
        }
        private void populateCB() //To populate song combo box
        {
            List<string[]> songs = db.selectSong();

            foreach (string[] song in songs)
            {
                cbSongs.Items.Add($"{song[0]} by {song[1]} - {song[2]} BPM");
            }
        }

        private void cbSongs_SelectionChanged(object sender, SelectionChangedEventArgs e) //puts bpm from combobox selection into intupdown
        {
            string s = cbSongs.SelectedItem.ToString();
            s = Regex.Match(s, "[^-]*$").ToString(); //Converts selected cb item to only # BPM, in case there are numbers in the song title
            s = Regex.Replace(s, "\\D+", ""); //Replaces all non digits in s with ""
            //I'm sure there's a way to do this without regex but I'm trying to learn it
            int bpm = Convert.ToInt16(s);
            BPMControl.Value = bpm;
        }
    }

}
