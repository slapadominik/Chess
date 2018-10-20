using FluentAssertions;
using NUnit.Framework;

namespace Chess.Common.Tests
{
    [TestFixture]
    public class TableTests
    {
        private Table _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Table();
        }

        [Test]
        public void ChooseSite_WhenTableHaveNoPlayersAndColorIsBlack_SetsBlackPlayer()
        {
            //Arrange
            User user = new User();

            //Act
            _sut.ChooseSite(user, Color.Black);
            
            //Assert
            _sut.PlayerBlack.Should().NotBeNull();
            _sut.PlayerBlack.Id.Should().Be(user.Id);
        }
    }
}