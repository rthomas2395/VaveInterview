using VaveInterview.Core.Models.Enums;
using VaveInterview.Core.Models.Records;
using VaveInterview.Core.Services;

namespace VaveInterview.UnitTests
{
    [TestClass]
    public sealed class RoverServiceTests
    {
        [TestMethod]
        public void Execute_Valid()
        {
            //Arrange
            var service = new RoverService();
            var expectedPosition = new Position(7, 2);
            var expectedFinalDirection = Direction.North;
            var expectedFinalString = "True, N, (7, 2)";

            //Act
            var result = service.Execute(10, 10, Direction.North, new Position(0, 0), "aAaRaAArALAAAAL");

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
            var service = new RoverService();
            var expectedPosition = new Position(-1, 5);
            var expectedFinalDirection = Direction.West;
            var expectedFinalString = "False, W, (-1, 5)";

            //Act
            var result = service.Execute(10, 10, Direction.North, new Position(2, 3), "AALAAA");

            //Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(expectedPosition, result.Position);
            Assert.AreEqual(expectedFinalDirection, result.Direction);
            Assert.AreEqual(expectedFinalString, result.ToString());
        }
    }
}
