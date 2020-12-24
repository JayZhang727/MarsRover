using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    /// <summary>
    /// Model for Mars Rovers input display.
    /// Assume MaxX and MaxY will always be an integer between 1 and 2147483647
    /// Assume the initial position of the Mars Rover data input is delimited by a single whitespace
    /// </summary>
    public class RoverInputsModel : IValidatableObject
    {
        private readonly char[] Headings = { 'N', 'E', 'S', 'W' };

        [Required]
        [DisplayName("Max X Position")]
        [Range(1, 2147483647)]
        public int MaxX { get; set; }

        [Required]
        [DisplayName("Max Y Position")]
        [Range(1, 2147483647)]
        public int MaxY { get; set; }

        [Required]
        [DisplayName("Rover Inputs")]
        public string RoverInputs { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            using (StringReader reader = new StringReader(RoverInputs))
            {
                string curRoverInitialPositionAndHeading;
                string curRoverMoveInstructions;

                var keepReading = true;
                var roverCount = 0;
                while (keepReading)
                {
                    curRoverInitialPositionAndHeading = reader.ReadLine();
                    curRoverMoveInstructions = reader.ReadLine();
                    if (curRoverInitialPositionAndHeading == null || curRoverMoveInstructions == null)
                    {
                        keepReading = false;
                    }
                    else
                    {
                        roverCount++;
                        string[] curRoverPositionAndHeading = curRoverInitialPositionAndHeading.Split(' ');
                        if ((!int.TryParse(curRoverPositionAndHeading[0], out int curInitialX))
                            || (curInitialX > MaxX)
                            || (curInitialX < 0))
                        {
                            var msg = "Initial Position X for Mars Rover " + roverCount.ToString() + " must be an integer between 0 and " + MaxX.ToString();
                            yield return new ValidationResult(msg, new[] { nameof(RoverInputs) });
                        }

                        if ((!int.TryParse(curRoverPositionAndHeading[1], out int curInitialY))
                            || (curInitialY > MaxY)
                            || (curInitialY < 0))
                        {
                            var msg = "Initial Position Y for Mars Rover " + roverCount.ToString() + " must be an integer between 0 and " + MaxY.ToString();
                            yield return new ValidationResult(msg, new[] { nameof(RoverInputs) });
                        }

                        if ((!char.TryParse(curRoverPositionAndHeading[2], out char curInitialHeading))
                            || !Headings.Contains(char.ToUpper(curInitialHeading)))
                        {
                            var msg = "Initial Headinng for Mars Rover " + roverCount.ToString() + " must be a single character from the following set: 'N' 'E' 'S' 'W', Not case sensitive.";
                            yield return new ValidationResult(msg, new[] { nameof(RoverInputs) });
                        }
                    }
                }
            }
        }
    }
}
