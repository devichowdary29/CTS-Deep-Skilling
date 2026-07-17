using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using MagicFilesLib;

namespace DirectoryExplorer.Tests
{
    [TestFixture]
    public class DirectoryExplorerTests
    {
        private Mock<IDirectoryExplorer> _directoryExplorerMock;

        private readonly string _file1 = "file.txt";
        private readonly string _file2 = "file2.txt";

        [OneTimeSetUp]
        public void Init()
        {
            _directoryExplorerMock = new Mock<IDirectoryExplorer>();

            // Mock the DirectoryExplorer to return hardcoded file names
            _directoryExplorerMock
                .Setup(m => m.GetFiles(It.IsAny<string>()))
                .Returns(new List<string> { _file1, _file2 });
        }

        [Test]
        public void GetFiles_ShouldReturnMockedFiles()
        {
            // Arrange
            IDirectoryExplorer target = _directoryExplorerMock.Object;

            // Act
            ICollection<string> result = target.GetFiles("C:\\Some\\Dummy\\Path");

            // Assert
            Assert.That(result, Is.Not.Null, "The collection is null.");
            Assert.That(result.Count, Is.EqualTo(2), "The collection count is not equal to 2.");
            Assert.That(result, Does.Contain(_file1), "The collection does not contain _file1.");
        }
    }
}
