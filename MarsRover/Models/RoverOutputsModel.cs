using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Models
{
    public class RoverOutputsModel
    {
        [DisplayName("Rover Outputs")]
        public string RoverOutputs { get; set; }

        public RoverOutputsModel(string outDisplayString)
        {
            RoverOutputs = outDisplayString;
        }
    }
}
