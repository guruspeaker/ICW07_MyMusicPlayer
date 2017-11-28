using Android.App;
using Android.Widget;
using Android.OS;
using Android.Media;


namespace ICW07_MyMusicPlayer
{
    [Activity(Label = "ICW07_MyMusicPlayer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        MediaPlayer myPlayer;
        public static int playerStatus;
        public static int track;
       

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            MainActivity.track = -1;
            MainActivity.playerStatus = 0;

            string[] soundFileList = new string[]
                {
                    "bensound_epic",
                    "bensound_goinghigher",
                    "bensound_littleplanet",
                    "bensound_scifi",
                    "bensound_slowmotion",
                    "bensound_straight"
                };
            string[] songList = new string[]
            {
                "Epic",
                "Going Higher",
                "Little Planet",
                "SciFi",
                "Slow Motion",
                "Straight"
            };
            int[] artFileList = new int[]
            {
                Resource.Drawable.art_epic,
                Resource.Drawable.art_goinghigher,
                Resource.Drawable.art_littleplanet,
                Resource.Drawable.art_scifi,
                Resource.Drawable.art_slowmotion,
                Resource.Drawable.art_straight
            };

            var songListView = FindViewById<ListView>(Resource.Id.lvSongList);
            var adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItemActivated1, songList);
            songListView.Adapter = adapter;
            songListView.ChoiceMode = Android.Widget.ChoiceMode.Single;
            var nowPlaying = FindViewById<TextView>(Resource.Id.txtNowPlaying);
            ImageView nowArt = FindViewById<ImageView>(Resource.Id.songArt);

            ImageButton buttonFirst = FindViewById<ImageButton>(Resource.Id.btnFirst);
            ImageButton buttonLast = FindViewById<ImageButton>(Resource.Id.btnLast);
            ImageButton buttonPrevious = FindViewById<ImageButton>(Resource.Id.btnPrevious);
            ImageButton buttonNext = FindViewById<ImageButton>(Resource.Id.btnNext);
            ImageButton buttonPlayPause = FindViewById<ImageButton>(Resource.Id.btnPlayPause);
            ImageButton buttonStop = FindViewById<ImageButton>(Resource.Id.btnStop);
            
            songListView.ItemClick += (sender, e) => {
                MainActivity.track = e.Position;
                //nowPlaying.Text = "Now Playing:\n" + songList[track] + "\nBy: www.bensound.com";
                //nowArt.SetBackgroundResource(artFileList[track]);
                //songListView.SetItemChecked(track, true);
                displayArt(songList[MainActivity.track]);

            };
            void displayArt(string name)
            {
                nowPlaying.Text = "Now Playing:\n" + songList[MainActivity.track] + "\nBy: www.bensound.com";
                nowArt.SetBackgroundResource(artFileList[MainActivity.track]);
                songListView.SetItemChecked(MainActivity.track, true);
            };

            buttonPlayPause.Click += delegate
            {
                if (MainActivity.track != -1)
                {
                     switch (MainActivity.playerStatus)
                    {
                        case 0: // Player is stopped (do play)
                            MainActivity.playerStatus = 1;
                            buttonPlayPause.SetImageResource(Resource.Drawable.mpPause);
                            PlaySong(soundFileList[MainActivity.track]);
                            //nowPlaying.Text = "Play" + MainActivity.playerStatus;
                            break;
                        case 1: // Player is Playing (do pause)
                            PauseSong(MainActivity.playerStatus);
                            MainActivity.playerStatus = 2;
                            buttonPlayPause.SetImageResource(Resource.Drawable.mpPlay);
                            nowPlaying.Text = "Track Paused";
                            break;
                        default: // Player is paused (do unpause)
                            PauseSong(MainActivity.playerStatus);
                            MainActivity.playerStatus = 1;
                            buttonPlayPause.SetImageResource(Resource.Drawable.mpPause);
                            //nowPlaying.Text = "Now Playing:\n" + songList[track] + "\nBy: www.bensound.com";
                            displayArt(songList[MainActivity.track]);
                            break;

                    }
                    

                }
            };
            buttonFirst.Click += delegate
            {
                MainActivity.track = goFirst();
                displayArt(songList[MainActivity.track]);
                PlaySong(songList[MainActivity.track]);
                //playerReset(playerStatus);
            };
            buttonLast.Click += delegate
            {
                MainActivity.track = goLast();
                displayArt(songList[MainActivity.track]);
                PlaySong(songList[MainActivity.track]);
                //playerReset(playerStatus);
            };
            buttonNext.Click += delegate
            {
                MainActivity.track = goNext(MainActivity.track);
                displayArt(songList[MainActivity.track]);
                PlaySong(songList[MainActivity.track]);
                //playerReset(playerStatus);
            };
            buttonPrevious.Click += delegate
            {
                MainActivity.track = goPrevious(MainActivity.track);
                displayArt(songList[MainActivity.track]);
                PlaySong(songList[MainActivity.track]);
                //playerReset(playerStatus);
            };

            buttonStop.Click += delegate
            {
                MainActivity.playerStatus = 0;
                buttonPlayPause.SetImageResource(Resource.Drawable.mpPlay);
                StopSong();

            };






       

        }

        private void SongListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            throw new System.NotImplementedException();
        }
        
        public void PlaySong(string name)
        {
            var resID = Resources.GetIdentifier(name, "raw", PackageName);
            myPlayer = MediaPlayer.Create(this, resID);
            this.myPlayer.Completion += delegate
            {
                goNext(track);
            };
            myPlayer.Start();

        }
        public void PauseSong(int playerstatus)
        {
            if (playerstatus == 1)
                myPlayer.Pause();
            else
                myPlayer.Start();
            
        }
        
        public void StopSong()
        {
            myPlayer.Stop();
        }
        public int goNext(int track)
        {
            if (track == 5)
                track = 0;
            else
                track++;
            return track;
        }
        public int goPrevious(int track)
        {
            if (track == 0)
                track = 5;
            else
                track--;
            return track;
        }
        public int goFirst()
        {
            return 0;
        }
        public int goLast()
        {
            return 5;
        }
    }
}

