using System.Timers;

namespace TrafficEscapeSimulator
{
    public partial class MainPage : ContentPage
    {
        private bool _gamePlaying = false, _ballMoving = false;
        private int _goals, _timeLeft;
        Random random;
        private string _goaliePosition;
        private int interval = 1000;
        private int topScore = 0;

        private System.Timers.Timer timer;

        public MainPage()
        {
            InitializeComponent();
        }

        private async void StartButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GamePage());
        }

        private async void InstructionsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Instructions());
        }

        private async void OptionsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Options());
        }
    }
}
