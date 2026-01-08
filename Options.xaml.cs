namespace TrafficEscapeSimulator;

public partial class Options : ContentPage
{
	public Options()
	{
		InitializeComponent();
	}

    private void Difficulty(object sender, EventArgs e)
    {
		Button btn = (Button)sender;
        GamePage game = new GamePage();

        if (btn.Text == "Hard")
		{
			game.difficultySpeed = 30;
			HardBtn.IsEnabled = false;
			NormalBtn.IsEnabled = true;

			//HardBtn.Background = Color.Equals(DarkGreen);
		}

		else
		{
			game.difficultySpeed = 15;
            HardBtn.IsEnabled = true;
            NormalBtn.IsEnabled = false;
        }

		string _mode = btn.Text.ToString();
        // Tell the player their results
        string message = $"Game Mode is _mode";
        DisplayAlert("Game Mode", message, "OK");
    }
}