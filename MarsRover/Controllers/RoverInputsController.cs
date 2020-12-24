using MarsRover.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MarsRover.Controllers
{
    public class RoverInputsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Execute(RoverInputsModel model)
        {
            if (ModelState.IsValid)
            {
                string roverOutputs = string.Empty;
                var marsRovers = CreateMarsRovers(model);
                foreach (var rover in marsRovers)
                {
                    rover.ExecuteMoveInstructions();
                    roverOutputs = AppendFinalRoverPositionAndHeading(rover, roverOutputs);
                }

                var result = new RoverOutputsModel(roverOutputs);
                return View("RoverOutputs", result);
            }
            
            return View("Index");
        }

        private List<MarsRoverModel> CreateMarsRovers(RoverInputsModel model)
        {
            var result = new List<MarsRoverModel>();

            using (StringReader reader = new StringReader(model.RoverInputs))
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
                        curRoverMoveInstructions = StripWhiteSpaceFromString(curRoverMoveInstructions);
                        int curInitialX = int.Parse(curRoverPositionAndHeading[0]);
                        int curInitialY = int.Parse(curRoverPositionAndHeading[1]);
                        char curInitialHeading = char.Parse(curRoverPositionAndHeading[2]); 
                        var newRover = new MarsRoverModel(model.MaxX, model.MaxY, curInitialX, curInitialY, curInitialHeading, curRoverMoveInstructions);
                        result.Add(newRover);
                    }
                }
            }

            return result;
        }

        private string AppendFinalRoverPositionAndHeading(MarsRoverModel rover, string roverOutputs)
        {
            var result = roverOutputs + String.Format("{0} {1} {2} \n", rover.CurrentPositionX, rover.CurrentPositionY, rover.CurrentHeading);
            return result;
        }

        private string StripWhiteSpaceFromString(string input)
        {
            return new string(input
                    .Where(c => !Char.IsWhiteSpace(c))
                    .ToArray());
        }
    }
}
