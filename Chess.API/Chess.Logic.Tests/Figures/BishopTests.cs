using System.Linq;
using Chess.Logic.Consts;
using Chess.Logic.Figures;
using FluentAssertions;
using NUnit.Framework;

namespace Chess.Logic.Tests.Figures
{
    [TestFixture]
    public class BishopTests
    {
        private Bishop _sut;
        private Board _board;

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
        }

        [TestCase(Color.White, "d5", 8)]
        public void GetPossibleMoves_WhenThereIsNoPossibleMove_And_BoardIsInitialized_ReturnsListOfPossibleMoves(Color color, string currentLocation, int expectedValidMoves)
        {
            //Arrange
            _sut = InitializeBishop(color, currentLocation);

            //Act
            var result = _sut.GetPossibleMoves(_board);

            //Assert
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Count().Should().Be(expectedValidMoves);
        }

        private Bishop InitializeBishop(Color color, string currentLocation)
        {
            Bishop bishop = new Bishop(color, currentLocation);
            _board.SetChessman(currentLocation, bishop);
            return bishop;
        }

    }
}