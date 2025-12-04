using System.Timers;

namespace TrafficEscapeSimulator;

public partial class GamePage : ContentPage
{
    private bool _gamePlaying = false, _ballMoving = false;
    private int _goals, _timeLeft;
    Random random;
    private string _goaliePosition;
    private int interval = 1000;
    private int topScore = 0;

    private System.Timers.Timer timer;

    public GamePage()
    {
        InitializeComponent();
        InitialiseTimers();
        random = new Random();
    }

    private void InitialiseTimers()
    {
        // Initialise your timer here

        timer = new System.Timers.Timer();
        timer.Interval = interval;
        timer.Elapsed += Timer_Elapsed;
    }

    private void Timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        Dispatcher.Dispatch(

                () =>
                {
                    TimerFunction();
                }
            );
    }

    // Make a TimerTick Method, when the _timeleft is 0, end the game
    private void TimerFunction()
    {
        --_timeLeft;
        timer_lbl.Text = _timeLeft.ToString();
        if (_timeLeft == 0)
        {
            timer.Stop();
            EndGame();
            _timeLeft = 30;
        }
    }

    private void EndGame()
    {
        // Set Gameplaying to false
        _gamePlaying = false;
        _ballMoving = false;

        // Hide the buttons to kick the ball

        string score = "Your score was: " + _goals;

        // Tell the player their results
        string message = $"Final Score: _goals";
        DisplayAlert("Game Over", score, "OK");

        //disable grid
        ControlGrid.IsVisible = false;
        //enable start button
        StartBtn.IsVisible = true;
        //reset goal score
        _goals = 0;
        ScoreLbl.Text = _goals.ToString();
    }

    private async void StartGame()
    {

        _gamePlaying = true;
        _timeLeft = 30;
        StartBtn.IsVisible = false;
        ControlGrid.IsVisible = true;

        // Start the goalie animating
        _goaliePosition = "centre";
        await AnimateGoalKeeper();

    }

    private async Task AnimateGoalKeeper()
    {
        while (_gamePlaying)
        {
            double goalkeeperDistance = 220;
            int leftorright = random.Next(2);
            int direction = 1;
            if (leftorright == 0)
            {
                direction = -1;
                _goaliePosition = "left";
            }
            else
            {
                direction = 1;
                _goaliePosition = "right";
            }

            // Goalkeeper should move a distance of above in the correct direction.
            await GoalKeeper.TranslateTo((goalkeeperDistance * direction), 0, 1000);

            // Now go back to the centre
            _goaliePosition = "centre";
            await GoalKeeper.TranslateTo(0, 0, 1000);
        }
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        StartGame();
        timer.Start();
    }

    /*private void ShootButton_Clicked(object sender, EventArgs e)
    {
        if (!_ballMoving)
        {
            Button btn = (Button)sender;
            if (btn.Text == "Shoot Left")
            {
                MoveBall("left");
            }
            else if (btn.Text == "Shoot Right")
            {
                MoveBall("right");
            }
            else if (btn.Text == "Shoot Centre")
            {
                MoveBall("centre");
            }
        }
    }*/

    /*private async void MoveBall(string direction)
    {
        ControlGrid.IsVisible = false;
        _ballMoving = true;
        // Finish These:
        FootballImg.ScaleTo(0.5, 1000); //scales to size
        FootballImg.RotateTo(180, 0); //350 is temp
        int moveX = 180, moveY = -350;
        if (direction == "left")
        {

            await FootballImg.TranslateTo((moveX * -1), moveY, 1000);
        }
        else if (direction == "right")
        {
            await FootballImg.TranslateTo(moveX, moveY, 1000);
        }
        else
        {
            await FootballImg.TranslateTo(0, moveY, 1000);
        }
        if (direction != _goaliePosition)
        {
            await ScoreGoal();
        }
        else
        {
            await SaveShot();
            await BestScore();
        }
        // Reset the ball's position, rotation, translation etc
        FootballImg.TranslateTo(0, 0, 1000);
        FootballImg.RotateTo(0, 0);
        FootballImg.ScaleTo(1, 1);
        _ballMoving = false;
        ControlGrid.IsVisible = true;
    }*/

    private async Task ScoreGoal()
    {
        // Increase goals and update label
        _goals += 1;
        ScoreLbl.Text = _goals.ToString();

        // Show the feedback label
        FeedbackLbl.Text = "Goal Scored";
        FeedbackLbl.IsVisible = true;

        //check if scorce is better than best score
        await BestScore();

        await Task.Delay(500);
        // Hide the feedback label
        FeedbackLbl.IsVisible = false;
    }

    private async Task SaveShot()
    {

        // Show the feedback label
        FeedbackLbl.Text = "Missed";
        FeedbackLbl.IsVisible = true;

        // Let's move the ball back a little bit, say to -200 on the Y while leaving the X alone
        FootballImg.TranslateTo(FootballImg.TranslationX, -200, 500);
        await FootballImg.ScaleTo(0.5, 500);

        // Hide the feedback label
        FeedbackLbl.IsVisible = false;
    }

    private async Task BestScore()
    {
        if (topScore < _goals)
        {
            topScore = _goals;
        }
        BestScoreLbl.Text = topScore.ToString();
    }
}
