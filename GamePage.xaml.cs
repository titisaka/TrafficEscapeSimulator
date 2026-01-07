using Microsoft.Maui.Controls;
using System.Timers;

namespace TrafficEscapeSimulator;

public partial class GamePage : ContentPage
{
    private bool _gamePlaying = false, _ballMoving = false;
    private int _distance, _timeLeft;
    Random random;
    private string _carPosition;
    private int interval = 1000;
    private int topScore = 0;
    private int carInterval = 5000;

    private System.Timers.Timer timer;

    //note that source will be interchangeable for player car selection
    //this will be moved to the big global guy keep an eye out * moved to big global guy!
    //remembered that i can put multiple variables on the same line
    //nvm
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

    // Make a TimerTick Method, when the _timeleft is 0, end the game
    private void TimerFunction()
    {
        --_timeLeft;
        timer_lbl.Text = _timeLeft.ToString();
        if (_timeLeft == 0)
        {
            timer.Stop();
            EndGame();
            _timeLeft = 60;
        }
    }

    private void EndGame()
    {
        // Set Gameplaying to false
        _gamePlaying = false;
        _ballMoving = false;

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
        _timeLeft = 60;
        carInterval = 10000;
        StartBtn.IsVisible = false;
        ControlGrid.IsVisible = true;

        // Map Car Position for detecting collision
        _carPosition = "centre";

        //random moving cars
        await Task.Delay(500);
        await AnimateCars();

    }

    //for animating the moving cars
    private async Task AnimateCars()
    {
        while (_gamePlaying)
        {
            //double roadLength = 577; this is a fixed value so might just forgo having it saved as a variable
            int whichLane = random.Next(3), whichCar = random.Next(8);
            //await Cars();
            //int direction = 1;
            //string src;

            if (whichLane == 0)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane1Car1.IsVisible = true;
                await Lane1Car1.TranslateTo(0, -456, (uint)carInterval);
                Lane1Car1.IsVisible = false;
                await Lane1Car1.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane1Car1.Source = cars[whichCar];

                FeedbackLbl.Text = "Ran Car 1";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;
            }
            else if (whichLane == 1)
            {
                // cars will be moving up from bottom to top then disappearing
                // add a fadeto at the end later for aesthetics, focus on function for now
                Lane2Car1.IsVisible = true;
                await Lane2Car1.TranslateTo(0, -456, (uint)carInterval);
                Lane2Car1.IsVisible = false;
                await Lane2Car1.TranslateTo(0, 577, 1000);

                //changes the car each time to avoid repeats
                Lane2Car1.Source = cars[whichCar];

                FeedbackLbl.Text = "Ran Car 2";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;
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

                FeedbackLbl.Text = "Ran Car 3";
                FeedbackLbl.IsVisible = true;
                await Task.Delay(500);
                FeedbackLbl.IsVisible = false;
            }

            /*
             * if (whichLane == 0)
            {
                Lane1.Add(car1);
                await car1.TranslateTo(0, -456, (uint)carInterval);
                car1.IsVisible = false;
                await car1.TranslateTo(0, 577, 1000);

            }
            else if (whichLane == 1)
            {
                Lane2.Add(car2);
                await car2.TranslateTo(0, -456, (uint)carInterval);
                car2.IsVisible = false;
                await car2.TranslateTo(0, 577, 1000);
            }

            else
            {
                Lane3.Add(car3);
                await car3.TranslateTo(0, -456, (uint)carInterval);
                car3.IsVisible = false;
                await car3.TranslateTo(0, 577, 1000);
            }
             */

            /* tried this and it didn't work
             * maybe due to how i set the variables?
             * or it could just be that it can't read from the array, even when it's converted to a string
            else
            {
                src = cars[(whichCar + 1)].ToString();
                car3.Source = src;
                Lane3.Add(car3);
                await car3.TranslateTo(0, -456, (uint)carInterval);
                car3.IsVisible = false;
                await car3.TranslateTo(0, 577, 1000);
            }*/

            /*// cars will be moving up from bottom to top then disappearing
            // add a fadeto at the end later for aesthetics, focus on function for now
            Lane1Car1.IsVisible = true;
        await Lane1Car1.TranslateTo(0, -456, (uint)carInterval);
        Lane1Car1.IsVisible = false;
        await Lane1Car1.TranslateTo(0, 577, 1000);*/

            /*// Now go back to the centre
            _carPosition = "centre";
            await GoalKeeper.TranslateTo(0, 0, 1000);*/
        }
    }

    private void StartBtn_Clicked(object sender, EventArgs e)
    {
        StartGame();
        timer.Start();
    }

     private void ShootButton_Clicked(object sender, EventArgs e)
    {
        //annoying asf but I'll rewrite this, just need the visual for now
        //*made it less annoying already, yippee!
        Button btn = (Button)sender;


        //i hate nested if statements but what needs to be done needs to be done
        //this could probably be done with numbers for efficiency but i'm so tired man and this works just fine
        //this project has crashed so much i'm surprised i haven't completely given up already
        //moves can to left
        if (btn.Text == "Left")
        {
            if (_carPosition == "centre")
            {
                _carPosition = "left";
                //Lane2.Clear();
                Lane1.Add(playerCar);
            }

            else if (_carPosition == "right")
            {
                _carPosition = "centre";
                //Lane3.Clear();
                Lane2.Add(playerCar);
            }

            else { }
        }

        //moves car to the right
        else if (btn.Text == "Right")
        {
            if (_carPosition == "centre")
            {
                _carPosition = "right";
                // Lane2.Remove(playerCar); //why aren't you working :(
                //Lane2.Clear();
                Lane3.Add(playerCar);
            }

            else if (_carPosition == "left")
            {
                _carPosition = "centre";
                //Lane1.Clear();
                Lane2.Add(playerCar);
            }

            else { }
        }
        else if (btn.Text == "Centre")
        {
            //button to be removed; redundant
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

    //was for changing sources, decided to try an array instaed
    /*private async Task Cars()
    {
        int whichCar = random.Next(3);

        switch (whichCar)
        {
            case 0:
                car1.Source = "ambulance.png";
                car2.Source = "blackviper.png";
                car3.Source = "minivan.png";
                car4.Source = "taxi.png";
                car5.Source = "audi.png";
                car6.Source = "minitruck.png";
                break;

            case 1:
                car1.Source = "audi.png";
                car2.Source = "minitruck.png";
                car3.Source = "police.png";
                car4.Source = "truck.png";
                car5.Source = "minivan.png";
                car6.Source = "taxi.png";
                break;

            case 2:
                car1.Source = "taxi.png";
                car2.Source = "truck.png";
                car3.Source = "police.png";
                car4.Source = "minivan.png";
                car5.Source = "ambulance.png";
                car6.Source = "blackviper.png";
                break;

            default:
                car1.Source = "taxi.png";
                car2.Source = "truck.png";
                car3.Source = "police.png";
                car4.Source = "minitruck.png";
                car5.Source = "minivan.png";
                car6.Source = "ambulance.png";
                break;
        }
    }*/

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
