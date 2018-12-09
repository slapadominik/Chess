using System;
using Chess.Logic.Exceptions;
using Chess.Logic.Figures;
using Chess.Logic.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace Chess.Logic.Tests
{
    [TestFixture]
    public class GameTests
    {
        private Game _sut;
        private Mock<IBoard> _boardMock;
        private Player _playerWhite;
        private Player _playerBlack;

        [SetUp]
        public void SetUp()
        {
            _playerWhite = new Player(Guid.NewGuid(), Color.White);
            _playerBlack = new Player(Guid.NewGuid(), Color.Black);
            _boardMock = new Mock<IBoard>();
            _sut = new Game(_playerWhite.Id, _playerBlack.Id, _boardMock.Object);
        }


        [Test]
        public void MakeMove_WhenCurrentPlayerIsWhiteAndBlackPlayerMakeMove_ShouldThrowNotACurrentPlayerException()
        {          
            //Act, Assert
            Assert.Throws<NotACurrentPlayerException>(() => _sut.MakeMove(_playerBlack.Id, "b3", "b5"));
            _boardMock.Verify(x => x.GetChessman(It.IsAny<string>()), Times.Never());
            _sut.MovesCount().Should().Be(0);
        }

        [Test]
        public void MakeMove_WhenLocationFromDoesntHaveChessman_ShouldThrowNullChessmanException()
        {
            //Arrange
            string locationFrom = "b3";
            _boardMock.Setup(x => x.GetChessman(locationFrom)).Returns((Chessman) null);

            //Act, Assert
            Assert.Throws<EmptyLocationException>(() => _sut.MakeMove(_playerWhite.Id, "b3", "b4"));
            _boardMock.Verify(x => x.GetChessman(locationFrom), Times.Once);
            _sut.MovesCount().Should().Be(0);
        }

        [Test]
        public void MakeMove_WhenLocationFromHasBlackChessmanAndPlayerWhiteIsCurrentPlayer_ShouldThrowWrongPlayerChessmanException()
        {
            //Arrange
            string locationFrom = "b3";
            //_boardMock.Setup(x => x.GetChessman(locationFrom)).Returns(new Pawn(Color.Black));

            //Act, Assert
            Assert.Throws<WrongPlayerChessmanException>(() => _sut.MakeMove(_playerWhite.Id, "b3", "b4"));
            _boardMock.Verify(x => x.GetChessman(locationFrom), Times.Once);
            _sut.MovesCount().Should().Be(0);
        }


        [Test]
        public void MakeMove_WhenLocationFromHasFigureAndMoveIsValid_ShouldIncrementMovesAndNotThrowException()
        {
            //Arrange
            string locationFrom = "b3";
            string locationTo = "b4";
            Mock<Chessman> chessmanMock = new Mock<Chessman>(MockBehavior.Loose, Color.White);

            //Act, Assert
            _sut.MakeMove(_playerWhite.Id, locationFrom, locationTo);
            _boardMock.Verify(x => x.GetChessman(locationFrom), Times.Once);
            _sut.MovesCount().Should().Be(1);
            _sut.CurrentPlayer.Id.Should().Be(_playerBlack.Id);

        }
    }
}