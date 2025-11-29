namespace TrafficEscapeSimulator;
/*ideas
 * include save files like in sephiria to implement data binding
 * (maximum of three save files linked to a username)
 *
 */
public partial class WelcomePage : ContentPage
{
	public WelcomePage()
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