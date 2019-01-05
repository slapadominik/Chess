using System.Collections.Generic;
using System.ComponentModel;
using Chess.Logic.Consts;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
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

        [SetUp]
        public void SetUp()
        {
            _board = new Board();
        }

        [TestCase("d2", "d3")]
        [TestCase("d2", "d4")]
        public void MakeMove_WhenDestinationFieldDoesNotHaveChessman_IsFirstMove_MoveIsValid_ReturnsNormalMoveResult(string from, string to)
        {
            //Arrange
            //_sut = new Pawn(Color.White);

            //Act
            var result = _sut.Move(_board, to);

            //Assert
            result.Should().NotBeNull();
            //result.MoveStatus.Should().Be(MoveStatus.Normal);
            //result.Board.GetChessman(from).Should().Be(null);
            //result.Board.GetChessman(to).Should().Be(_sut);
        }

        [TestCase("d2", "d4")]
        public void MakeMoves_WhenDestinationFieldDoesNotHaveChessman_IsNotFirstMove_MoveIsInvalid_ThrowsInvalidMoveException(string from, string to)
        {
            //Arrange
            //_sut = new Pawn(Color.White);
            _sut.IsFirstMove = false;
            _board.SetChessman(from, _sut);
            _board.SetChessman(to, null);

            //Act //Assert
            Assert.Throws<InvalidMoveException>(() => _sut.Move(_board, to));
        }

        [TestCase("d2", "d4")]
        public void MakeMoves_WhenDestinationFieldDoesHaveChessmanWithTheSameColor_MoveIsInvalid_ThrowsInvalidMoveException(string from, string to)
        {
            //Arrange
            //_sut = new Pawn(Color.White);
            _sut.IsFirstMove = false;
            _board.SetChessman(from, _sut);
            //_board.SetChessman(to, new Bishop(Color.White));

            //Act //Assert
            Assert.Throws<InvalidMoveException>(() => _sut.Move(_board, to));
        }

        [TestCase("d2", "c3", Color.White, Color.Black)]
        [TestCase("d2", "c3", Color.Black, Color.White)]
        public void MakeMoves_WhenDestinationFieldDoesHaveChessmanWithOpponentColor_MoveIsValid_ReturnsCaptureMoveResult(string from, string to, Color myColor, Color opponentColor)
        {
            //Arrange
            //_sut = new Pawn(myColor);
            _sut.IsFirstMove = false;
            _board.SetChessman(from, _sut);
           // _board.SetChessman(to, new Bishop(opponentColor));

            //Act
            var result = _sut.Move(_board, to);

            //Assert
            result.Should().NotBeNull();
            //result.MoveStatus.Should().Be(MoveStatus.Capture);
            //result.Board.GetChessman(from).Should().Be(null);
            //.Board.GetChessman(to).Should().Be(_sut);
        }
    }
}