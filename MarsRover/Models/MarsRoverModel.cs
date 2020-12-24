using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    /// <summary>
    /// Model for a single Mars Rover.
    /// Assume Origin is Always 0,0
    /// Assume Mars Rover will ignore move commands that will take it outside the bounds established by Origin, MaxY, and MaxX
    /// </summary>
    public class MarsRoverModel
    {
        private const char TurnLeft = 'L';
        private const char TurnRight = 'R';
        private const char Move = 'M';
        private const char North = 'N';
        private const char East = 'E';
        private const char South = 'S';
        private const char West = 'W';

        public int InitialPositionX { get; set; }
        public int InitialPositionY { get; set; }
        public char InitialHeading { get; set; }
        public string MoveInstructions { get; set; }
        public int CurrentPositionX { get; set; }
        public int CurrentPositionY { get; set; }
        public char CurrentHeading { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }

        public MarsRoverModel()
        {
        }

        public MarsRoverModel(int maxX, int maxY, int initialX, int initialY, char initialHeading, string moveInstructions)
        {
            MaxX = maxX;
            MaxY = maxY;
            InitialPositionX = initialX;
            InitialPositionY = initialY;
            InitialHeading = initialHeading;
            MoveInstructions = moveInstructions;
            CurrentPositionX = initialX;
            CurrentPositionY = initialY;
            CurrentHeading = initialHeading;
        }

        public void ExecuteMoveInstructions()
        {
            List<char> instructions = (from ch in MoveInstructions select ch).ToList();

            foreach (char instruction in instructions)
            {
                switch (char.ToUpper(instruction))
                {
                    case TurnLeft:
                        {
                            ExecuteLeftTurn();
                            break;
                        }
                    case TurnRight:
                        {
                            ExecuteRightTurn();
                            break;
                        }
                    case Move:
                        {
                            ExecuteMove();
                            break;
                        }
                    default:
                        break;
                }
            }
        }


        private void ExecuteLeftTurn()
        {
            switch (char.ToUpper(CurrentHeading))
            {
                case North:
                    {
                        CurrentHeading = West;
                        break;
                    }
                case East:
                    {
                        CurrentHeading = North;
                        break;
                    }
                case South:
                    {
                        CurrentHeading = East;
                        break;
                    }
                case West:
                    {
                        CurrentHeading = South;
                        break;
                    }
                default:
                    break;
            }
        }

        private void ExecuteRightTurn()
        {
            switch (char.ToUpper(CurrentHeading))
            {
                case North:
                    {
                        CurrentHeading = East;
                        break;
                    }
                case East:
                    {
                        CurrentHeading = South;
                        break;
                    }
                case South:
                    {
                        CurrentHeading = West;
                        break;
                    }
                case West:
                    {
                        CurrentHeading = North;
                        break;
                    }
                default:
                    break;
            }
        }

        private void ExecuteMove()
        {
            switch (char.ToUpper(CurrentHeading))
            {
                case North:
                    {
                        if (CurrentPositionY < MaxY)
                        {
                            CurrentPositionY++;
                        }
                        break;
                    }
                case East:
                    {
                        if (CurrentPositionX < MaxX)
                        {
                            CurrentPositionX++;
                        }
                        break;
                    }
                case South:
                    {
                        if (CurrentPositionY > 0)
                        {
                            CurrentPositionY--;
                        }
                        break;
                    }
                case West:
                    {
                        if (CurrentPositionX > 0)
                        {
                            CurrentPositionX--;
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
