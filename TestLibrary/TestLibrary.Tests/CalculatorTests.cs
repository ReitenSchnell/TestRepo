using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;
using NUnit.Core;

namespace TestLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        private IDataReader dataReader;
        private IRepository repository;
        private Calculator calculator;

        [SetUp]
        public void Setup()
        {
            dataReader = Substitute.For<IDataReader>();
            repository = Substitute.For<IRepository>();
            calculator = new Calculator(dataReader, repository);
        }

        [Test]
        public void CalculatorTest_NoPoints_Success()
        {
            dataReader.ReadDataPoints("", 0).ReturnsForAnyArgs(info => new List<int>());

            var actualResult = calculator.ProcessData(Arg.Any<string>(), 0);

            repository.Received().Save(actualResult);
            Assert.AreEqual(actualResult.TotalAmount, 0);
        }

        [Test]
        public void CalculatorTest_SinglePoint_Success()
        {
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { 10 });

            var actualResult = calculator.ProcessData(Arg.Any<string>(), 1);

            repository.Received().Save(actualResult);
            Assert.AreEqual(actualResult.TotalAmount, 10);
        }

        [Test]
        public void CalculatorTest_TwoPoints_Success()
        {
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { 6, 8 });

            var actualResult = calculator.ProcessData(Arg.Any<string>(), 2);

            repository.Received().Save(actualResult);
            Assert.AreEqual(actualResult.TotalAmount, 14);
        }

        [Test]
        public void CalculatorTest_FivePoints_Success()
        {
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { 1, 2, 3, 4, 5 });

            var actualResult = calculator.ProcessData(Arg.Any<string>(), 5);

            repository.Received().Save(actualResult);
            Assert.AreEqual(actualResult.TotalAmount, 15);
        }

        [Test]
        public void CalculatorTest_ThrowsFormatException_Fail()
        {
            dataReader.ReadDataPoints(Arg.Any<string>(), Arg.Any<int>()).Returns(info => new List<int> { 1, 2, 3, 4, 5 });

            Assert.Throws<FormatException>(() => calculator.ProcessData(Arg.Any<string>(), 3));

            repository.DidNotReceiveWithAnyArgs().Save(null);
        }

        [Test]
        public void CalculatorTest_ThrowsArgumentException_Fail()
        {
            dataReader.ReadDataPoints(Arg.Any<string>(), Arg.Any<int>()).Returns(info => new List<int> { -1, -2, -3, -4, 5 });

            Assert.Throws<ArgumentException>(() => calculator.ProcessData(Arg.Any<string>(), 5));

            repository.DidNotReceiveWithAnyArgs().Save(null);
        }
    }
}
