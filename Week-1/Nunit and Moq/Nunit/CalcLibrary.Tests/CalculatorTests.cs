using NUnit.Framework;
using CalcLibrary;
using System;

namespace CalcLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private Calculator _calculator;

        // [SetUp] is called before EACH test method runs
        [SetUp]
        public void Setup()
        {
            Console.WriteLine("Setting up the test context...");
            _calculator = new Calculator();
        }

        // [TearDown] is called after EACH test method runs
        [TearDown]
        public void TearDown()
        {
            Console.WriteLine("Cleaning up the test context...");
            _calculator = null;
        }

        // Standard [Test] without parameters
        [Test]
        public void Add_WhenCalledWithTwoPositiveNumbers_ReturnsSum()
        {
            // Arrange
            int num1 = 5;
            int num2 = 10;

            // Act
            int result = _calculator.Add(num1, num2);

            // Assert
            Assert.That(result, Is.EqualTo(15), "Addition of 5 and 10 should be 15.");
        }

        // Parameterized Test using [TestCase]
        [TestCase(1, 2, 3)]
        [TestCase(-1, -1, -2)]
        [TestCase(100, 50, 150)]
        [TestCase(0, 0, 0)]
        public void Add_WhenCalledWithVariousInputs_ReturnsExpectedSum(int a, int b, int expectedResult)
        {
            // Act
            int result = _calculator.Add(a, b);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult), $"Addition of {a} and {b} should be {expectedResult}.");
        }

        // Test ignored using [Ignore]
        [Test]
        [Ignore("This feature is not fully implemented yet.")]
        public void Subtract_WhenCalled_ReturnsDifference()
        {
            int result = _calculator.Subtract(10, 5);
            Assert.That(result, Is.EqualTo(5));
        }
    }
}
