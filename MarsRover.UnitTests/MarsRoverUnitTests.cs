using MarsRover.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.UnitTests
{
    class MarsRoverUnitTests
    {
        private int maxX = 5;
        private int maxY = 5;

        [SetUp]
        public void Setup()
        {
        }

        public MarsRoverModel GetRoverModel()
        {
            var result = new MarsRoverModel();
            result.MaxX = maxX;
            result.MaxY = maxY;
            return result;
        }

        [Test]
        public void RoverIgnoreMovingOutofUpperBoundsTest()
        {
            //arrange
            var rover = GetRoverModel();
            rover.InitialPositionX = 3;
            rover.InitialPositionY = 3;
            rover.InitialHeading = 'E';
            rover.CurrentPositionX = 3;
            rover.CurrentPositionY = 3;
            rover.CurrentHeading = 'E';
            rover.MoveInstructions = "MMMMMMMMMMLMMMMMMMMMM";

            //act
            rover.ExecuteMoveInstructions();

            //assert
            Assert.IsTrue(rover.CurrentHeading == 'N');
            Assert.IsTrue(rover.CurrentPositionX == maxX);
            Assert.IsTrue(rover.CurrentPositionY == maxY);
        }

        [Test]
        public void RoverIgnoreMovingOutofLowerBoundsTest()
        {
            //arrange
            var rover = GetRoverModel();
            rover.InitialPositionX = 3;
            rover.InitialPositionY = 3;
            rover.InitialHeading = 'S';
            rover.CurrentPositionX = 3;
            rover.CurrentPositionY = 3;
            rover.CurrentHeading = 'S';
            rover.MoveInstructions = "MMMMMMMMMMRMMMMMMMMMM";

            //act
            rover.ExecuteMoveInstructions();

            //assert
            Assert.IsTrue(rover.CurrentHeading == 'W');
            Assert.IsTrue(rover.CurrentPositionX == 0);
            Assert.IsTrue(rover.CurrentPositionY == 0);
        }


    }
}
