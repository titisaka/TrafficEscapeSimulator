using Microsoft.Maui.Controls;
using System.Timers;
using System.Xml.Linq;

namespace TrafficEscapeSimulator;

public partial class GamePage : ContentPage
{
    private bool _gamePlaying = false;
    private int _distance, _timePassed;
    Random random;
    private string _carPosition;
    private int interval = 1000;
    private int topScore = 0;
    public int carInterval, delay, speed, difficultySpeed;

    private System.Timers.Timer timer;

    //note that source will be interchangeable for player car selection
    Image playerCar = new Image
    {
        Source = "car.png",
        WidthRequest = 128,
        HeightRequest = 128,
        Aspect = Aspect.Fill,
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        ZIndex = 1
    };

    /* wwas used for the Cars() Task but it didn't work as expected
     * Image car1 = new Image
    {
        Source = "car.png",
        WidthRequest = 128,
        HeightRequest = 128,
        Aspect = Aspect.Fill,
        VerticalOptions = LayoutOptions.Center,
        HorizontalOptions = LayoutOptions.Center,
        ZIndex = 1
    };*/

    private string[] cars = { "ambulance.png", "audi.png", "blackviper.png", "minitruck.png", "minivan.png", "police.png", "taxi.png", "truck.png" };

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

    // Make a TimerTick Method, when the _timePassed is 0, end the game
    private void TimerFunction()
    {
        ++_timePassed;
        timer_lbl.Text = _timePassed.ToString();
        if (_gamePlaying == false)
        {
            timer.Stop();
        }
    }

    private void EndGame()
    {
        // Set Gameplaying to false
        _gamePlaying = false;

        // Hide the buttons to kick the ball

        string score = "Your score was: " + _distance;

        // Tell the player their results
        string message = $"Final Score: _distance";
        DisplayAlert("Game Over", score, "OK");

        //disable grid
        ControlGrid.IsVisible = false;
        //enable start button
        StartBtn.IsVisible = true;
        //reset goal score
        _distance = 0;
        ScoreLbl.Text = _distance.ToString();

        //note, delay this task by a few seconds
        //did not realise that this fully removes the grid, may need to adjust accordingly
        //GameArea.Clear();
    }

    private async void StartGame()
    {
        //setting and resetting values at the top of each game
        _gamePlaying = true;
        _timePassed = 0;
        speed = _timePassed;
        carInterval = 15000;
        delay = 1500;
        StartBtn.IsVisible = false;
        ControlGrid.IsVisible = true;

        // Map Car Position for detecting collision
        _carPosition = "centre";

        //random moving cars
        await AnimateCars();
        await Task.Delay(delay);
        await AnimateCars();

    }

    //!DONE - DO NOT ToUCH!
    //for animating the moving cars
    private async Task AnimateCars()
    {
        while (_gamePlaying)
        {
            int whichLane1 = random.Next(3), whichLane2 = random.Next(3), whichCar = random.Next(8);

            //to avoid multiple cars appearing in the same lane at the same time
            if(whichLane2 == whichLane1) { whichLane2 -= 1; }

            if (whichLane1 == 0)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane1Car1.IsVisible = true;
                await Lane1Car1.TranslateTo(0, -456, (uint)carInterval);
                Lane1Car1.IsVisible = false;
                await Lane1Car1.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane1Car1.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 1";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            else if (whichLane1 == 1)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane2Car1.IsVisible = true;
                await Lane2Car1.TranslateTo(0, -456, (uint)carInterval);
                Lane2Car1.IsVisible = false;
                await Lane2Car1.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane2Car1.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 2";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            else
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane3Car1.IsVisible = true;
                await Lane3Car1.TranslateTo(0, -456, (uint)carInterval);
                Lane3Car1.IsVisible = false;
                await Lane3Car1.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane3Car1.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 3";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            //await Task.Delay(3000);
            //second car movement
            whichCar -= 1;
            whichCar = Math.Abs(whichCar);

            if (whichLane2 == 0)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane1Car2.IsVisible = true;
                await Lane1Car2.TranslateTo(0, -456, (uint)carInterval);
                Lane1Car2.IsVisible = false;
                await Lane1Car2.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane1Car2.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 1";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            else if (whichLane2 == 1)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane2Car2.IsVisible = true;
                await Lane2Car2.TranslateTo(0, -456, (uint)carInterval);
                Lane2Car2.IsVisible = false;
                await Lane2Car2.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane2Car1.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 2";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            else
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane3Car2.IsVisible = true;
                await Lane3Car2.TranslateTo(0, -456, (uint)carInterval);
                Lane3Car2.IsVisible = false;
                await Lane3Car2.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane3Car2.Source = cars[whichCar];

                /*//testing label tbr
                FeedbackLbl.Text = "Ran Car 3";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;*/
            }

            //increase the car speed over time
            if (_timePassed > (speed + 15) && (carInterval != 5000 && delay != 500))
            {
                carInterval -= 2500;

                if (_timePassed > (speed + 20))
                {
                    delay -= 250;
                }

                speed = _timePassed;
            }
        }
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        StartGame();
        timer.Start();
    }

    //function for moving car from left to right
     private void ShootButton_Clicked(object sender, EventArgs e)
    {
        //to help read the information on the button
        //know which button was pressed
        Button btn = (Button)sender;

        //moves car to the left
        if (btn.Text == "Left")
        {
            if (_carPosition == "centre")
            {
                _carPosition = "left";
                playerCarMOVINGL1.IsVisible = true;
                playerCarMOVINGL2.IsVisible = false;
            }

            else if (_carPosition == "right")
            {
                _carPosition = "centre";
                playerCarMOVINGL3.IsVisible = false;
                playerCarMOVINGL2.IsVisible = true;
            }

            else { }
        }

        //moves car to the right
        else if (btn.Text == "Right")
        {
            if (_carPosition == "centre")
            {
                _carPosition = "right";
                playerCarMOVINGL2.IsVisible = false;
                playerCarMOVINGL3.IsVisible = true;
            }

            else if (_carPosition == "left")
            {
                _carPosition = "centre";
                playerCarMOVINGL1.IsVisible = false;
                playerCarMOVINGL2.IsVisible = true;
            }

            else { }
        }
        else if (btn.Text == "End")
        {
            //use as tester for ending game early
            //tbr
            EndGame();
        }

        /*Keeping for possible car animation, just using static movements for now
         * if (!_ballMoving)
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
        }*/
    }

    //generate game grid
    private async Task Grid()
    {
        Grid normal = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            }

        };
        
        
        Grid hard = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition()
            }
        };
    }

    /*Using
     * 
     * private async void MoveCar(string direction)
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
        if (direction != _carPosition)
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

    /*Using
     * 
     * private async Task ScoreGoal()
    {
        // Increase goals and update label
        _distance += 1;
        ScoreLbl.Text = _distance.ToString();

        // Show the feedback label
        FeedbackLbl.Text = "Goal Scored";
        FeedbackLbl.IsVisible = true;

        //check if scorce is better than best score
        await BestScore();

        await Task.Delay(500);
        // Hide the feedback label
        FeedbackLbl.IsVisible = false;
    }*/

    /* Not Using
     * 
     * 
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
    }*/

    /*Using
     * 
     * private async Task BestScore()
    {
        if (topScore < _distance)
        {
            topScore = _distance;
        }
        BestScoreLbl.Text = topScore.ToString();
    }*/
}
