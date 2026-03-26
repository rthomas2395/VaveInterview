using VaveInterview.Core.Models;
using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;

namespace VaveInterview.UnitTests
{
    [TestClass]
    public sealed class RoverServiceTests
    {
        [TestMethod]
        public void Execute_Valid()
        {
            //Arrange
            var rover = new Rover
            {
                BoundaryHeight = 10,
                BoundaryWidth = 10,
                FacingDirection = Direction.North,
                CurrentPosition = new Position(0, 0)
            };

            var expectedPosition = new Position(7, 2);
            var expectedFinalDirection = Direction.North;
            var expectedFinalString = "True, N, (7, 2)";

            //Act
            var result = rover.Execute("aAaRaAArALAAAAL");

            //Assert
            Assert.IsTrue(result.IsValid);
            Assert.AreEqual(expectedPosition, result.Position);
            Assert.AreEqual(expectedFinalDirection, result.Direction);
            Assert.AreEqual(expectedFinalString, result.ToString());
        }

        [TestMethod]
        public void Execute_OutOfBounds()
        {
            //Arrange
            var rover = new Rover
            {
                BoundaryHeight = 10,
                BoundaryWidth = 10,
                FacingDirection = Direction.North,
                CurrentPosition = new Position(2, 3)
            };

            var expectedPosition = new Position(-1, 5);
            var expectedFinalDirection = Direction.West;
            var expectedFinalString = "False, W, (-1, 5)";

            //Act
            var result = rover.Execute("AALAAA");

            //Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(expectedPosition, result.Position);
            Assert.AreEqual(expectedFinalDirection, result.Direction);
            Assert.AreEqual(expectedFinalString, result.ToString());
        }
    }
}
