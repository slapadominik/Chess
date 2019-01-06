using System.Linq;
using Chess.Logic.Consts;
using Chess.Logic.Figures;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Logic.Tests.Figures
{
    [TestFixture]
    public class RookTests
    {
        private Rook _sut;
        private Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
        }

        [TestCase(Color.White, "c4", 11)]
        [TestCase(Color.White, "a6", 11)]
        [TestCase(Color.Black, "a2", 6)]
        [TestCase(Color.Black, "d6", 11)]
        public void GetPossibleMoves_WhenThereArePossibleMoves_And_BoardIsInitialized_ReturnsListOfPossibleMoves(Color color, string currentLocation, int expectedValidMoves)
        {
            //Arrange
            _sut = InitializeRook(color, currentLocation);

            //Act
            var result = _sut.GetPossibleMoves(_board);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count().Should().Be(expectedValidMoves);
        }

        [TestCase(Color.White, "h1", 0)]
        [TestCase(Color.Black, "b8", 0)]
        public void GetPossibleMoves_WhenThereIsNoPossibleMove_And_BoardIsInitialized_ReturnsListOfPossibleMoves(Color color, string currentLocation, int expectedValidMoves)
        {
            //Arrange
            _sut = InitializeRook(color, currentLocation);

            //Act
            var result = _sut.GetPossibleMoves(_board);

            //Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }

        private Rook InitializeRook(Color color, string currentLocation)
        {
            Rook rook = new Rook(color, currentLocation);
            _board.SetChessman(currentLocation, rook);
            return rook;
        }
    }
}