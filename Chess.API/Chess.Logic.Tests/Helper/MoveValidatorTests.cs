using Chess.Logic.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Chess.Logic.Tests.Helper
{
    [TestFixture]
    public class MoveValidatorTests
    {
        private MoveValidator _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MoveValidator();
        }
        [TestCase(new int[] { 8, -8 }, "d2", "d3")]
        [TestCase(new int[] { 5, 6, 10, 11, 15, 17 }, "g1", "f3")]
        public void IsMoveValid_WhenLocationFromAndLocationToIsValidAndMoveIsValid_ReturnsTrue(int[] availableMoves, string @from, string @to)
        {

            //Act
            var result = _sut.IsMoveValid(availableMoves, @from, @to);

            //Assert
            Assert.IsTrue(result);
        }

        [TestCase(new int[] { 8, -8 }, "e2", "d2")]
        [TestCase(new int[] { 5, 6, 10, 11, 15, 17 }, "b7", "c5")]
        public void IsMoveValid_WhenLocationFromAndLocationToIsValidAndMoveIsInvalid_ReturnsFalse(int[] availableMoves, string @from, string @to)
        {

            //Act
            var result = _sut.IsMoveValid(availableMoves, @from, @to);

            //Assert
            Assert.IsFalse(result);
        }

    }
}