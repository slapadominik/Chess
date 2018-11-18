using System.Collections.Generic;
using Chess.Logic.Figures;
using Chess.Logic.Helpers.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Logic.Tests.Figures
{
    [TestFixture]
    public class PawnTests
    {
        private Pawn _sut;
        private Board _board;
        private Mock<IMoveValidator> _moveValidatorMock;

        [SetUp]
        public void SetUp()
        {
            _moveValidatorMock = new Mock<IMoveValidator>();
            _board = new Board(_moveValidatorMock.Object);
        }

        [Test]
        public void MakeMove_WhenDestinationFieldDoesNotHaveChessman_IsFirstMove_MoveIsValid_ReturnsNormalMoveResult()
        {
            //Arrange
            string from = Locations.D2;
            string to = Locations.D3;
            _sut = new Pawn(Color.White);
            _moveValidatorMock.Setup(x => x.IsMoveValid(It.IsAny<IEnumerable<int>>(), from, to)).Returns(true);

            //Act
            var result = _sut.MakeMove(_board, Locations.D2, Locations.D3);

            //Assert
            result.Should().NotBeNull();
            result.MoveStatus.Should().Be(MoveStatus.Normal);
            result.Board.GetChessman(from).Should().Be(null);
            result.Board.GetChessman(to).Should().Be(_sut);
        }
    }
}