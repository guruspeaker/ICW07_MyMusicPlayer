using Android.App;
using Android.Widget;
using Android.OS;

namespace ICW07_MyMusicPlayer
{
    [Activity(Label = "ICW07_MyMusicPlayer", MainLauncher = true)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
        }
    }
}

