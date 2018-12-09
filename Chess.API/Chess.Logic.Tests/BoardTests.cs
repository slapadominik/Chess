using Chess.Logic.Figures;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Chess.Logic.Tests
{
    [TestFixture]
    public class BoardTests
    {
        private Board _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Board();
        }

        [Test]
        public void GetChessman_WhenLocationIsValid_ReturnsChessman()
        {
            //Act
            var chessman = _sut.GetChessman(Locations.C1);
            //Assert
            chessman.Should().NotBeNull();
            chessman.GetType().Should().Be<Bishop>();
            chessman.GetColor().Should().Be(Color.White);
        }

        [Test]
        public void GetChessman_WhenLocationIsInvalid_ReturnsNull()
        {
            //Act
            var chessman = _sut.GetChessman("A9");
            //Assert
            chessman.Should().BeNull();
        }

    }
}
