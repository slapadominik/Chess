using System;
using System.Threading;
using System.Threading.Tasks;
using Chess.API.Entity;
using Chess.API.Hubs;
using Chess.API.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Moq;
using NUnit.Framework;

namespace Chess.API.Tests.Hubs
{
    [TestFixture]
    public class TableHubTests
    {
        private TableHub _sut;
        private Mock<ITableService> _tableServiceMock;
        private Mock<IUserService> _userServiceMock;
        private Mock<IHubCallerClients> _hubCallerClientsMock;
        private Mock<IClientProxy> _clientProxyMock;
        private Mock<IGroupManager> _groupManagerMock;
        private Mock<HubCallerContext> _callerContextMock;

        [SetUp]
        public void SetUp()
        {
            _tableServiceMock = new Mock<ITableService>();
            _userServiceMock = new Mock<IUserService>();
            _hubCallerClientsMock = new Mock<IHubCallerClients>();
            _clientProxyMock = new Mock<IClientProxy>();
            _groupManagerMock = new Mock<IGroupManager>();
            _callerContextMock = new Mock<HubCallerContext>();
            _sut = new TableHub(_tableServiceMock.Object, _userServiceMock.Object) { Clients = _hubCallerClientsMock.Object, Groups = _groupManagerMock.Object, Context = _callerContextMock.Object};


        }

        [Test]
        public async Task JoinTable_WhenGivenTableNumberExists_AddUserToGroupAndNotifyClients()
        {
            //Arrange
            var userId = Guid.NewGuid();
            var username = "jan12314";
            var tableNumber = 10;
            var groupName = "Table" + tableNumber;
            _userServiceMock.Setup(x => x.GetUserById(userId)).Returns(new User{Id = userId, Username = username});
            _groupManagerMock.Setup(x =>
                    x.AddToGroupAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            _callerContextMock.Setup(x => x.ConnectionId).Returns("12357724");
            _hubCallerClientsMock
                .Setup(x => x.Groups(groupName))
                .Returns(It.IsAny<IClientProxy>());
            _hubCallerClientsMock
                .Setup(x => x.Groups(It.IsAny<string>())
                    .SendAsync(It.IsAny<string>(), username, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            //Act
            await _sut.JoinTable(10, userId);

            //Assert
            _hubCallerClientsMock.Verify(x => x.Groups(It.IsAny<string>()), Times.Exactly(2));
            
        }
    }
}