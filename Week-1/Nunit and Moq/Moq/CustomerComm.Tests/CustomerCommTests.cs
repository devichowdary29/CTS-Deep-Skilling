using NUnit.Framework;
using Moq;
using CustomerCommLib;

namespace CustomerComm.Tests
{
    [TestFixture]
    public class CustomerCommTests
    {
        private Mock<IMailSender> _mailSenderMock;
        private CustomerCommLib.CustomerComm _customerComm;

        [OneTimeSetUp]
        public void Init()
        {
            _mailSenderMock = new Mock<IMailSender>();
            
            // Configure the mock object so that SendMail() will accept any two string arguments 
            // and always return true.
            _mailSenderMock
                .Setup(m => m.SendMail(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
                
            _customerComm = new CustomerCommLib.CustomerComm(_mailSenderMock.Object);
        }

        [Test]
        public void SendMailToCustomer_ShouldReturnTrue()
        {
            // Act
            bool result = _customerComm.SendMailToCustomer();

            // Assert
            Assert.That(result, Is.True);
            
            // Verify that SendMail was indeed called with correct parameters
            _mailSenderMock.Verify(m => m.SendMail("cust123@abc.com", "Some Message"), Times.Once);
        }
    }
}
