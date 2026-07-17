using System;
using NUnit.Framework;
using Moq;
using PlayersManagerLib;

namespace PlayerManager.Tests
{
    [TestFixture]
    public class PlayerTests
    {
        private Mock<IPlayerMapper> _playerMapperMock;

        [OneTimeSetUp]
        public void Init()
        {
            _playerMapperMock = new Mock<IPlayerMapper>();

            // When the RegisterNewPlayer function calls IsPlayerNameExistsInDb,
            // we make sure that the mock object returns "false".
            _playerMapperMock
                .Setup(m => m.IsPlayerNameExistsInDb(It.IsAny<string>()))
                .Returns(false);
                
            // Setup AddNewPlayerIntoDb to do nothing
            _playerMapperMock
                .Setup(m => m.AddNewPlayerIntoDb(It.IsAny<string>()));
        }

        [Test]
        public void RegisterNewPlayer_ShouldReturnPlayer_WhenNameDoesNotExist()
        {
            // Arrange
            string playerName = "Sachin";

            // Act
            Player result = Player.RegisterNewPlayer(playerName, _playerMapperMock.Object);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Name, Is.EqualTo(playerName));
            Assert.That(result.Age, Is.EqualTo(23));
            Assert.That(result.Country, Is.EqualTo("India"));
            Assert.That(result.NoOfMatches, Is.EqualTo(30));
        }

        [Test]
        public void RegisterNewPlayer_EmptyName_ThrowsArgumentException()
        {
            // Assert
            Assert.Throws<ArgumentException>(() => 
            {
                Player.RegisterNewPlayer("", _playerMapperMock.Object);
            }, "Player name can’t be empty.");
        }

        [Test]
        public void RegisterNewPlayer_NameExists_ThrowsArgumentException()
        {
            // Arrange
            var duplicateMock = new Mock<IPlayerMapper>();
            duplicateMock.Setup(m => m.IsPlayerNameExistsInDb("Duplicate")).Returns(true);

            // Assert
            Assert.Throws<ArgumentException>(() => 
            {
                Player.RegisterNewPlayer("Duplicate", duplicateMock.Object);
            }, "Player name already exists.");
        }
    }
}
