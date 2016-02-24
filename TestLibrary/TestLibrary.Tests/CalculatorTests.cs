using System;
using System.Collections.Generic;
using NUnit.Framework;
using NSubstitute;

namespace TestLibrary.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void CalculatorTest_NoPoints_Success()
        {
            const int result = 0;
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints("", 0).ReturnsForAnyArgs(info => new List<int>());

            CalculationResult expectedResult = new CalculationResult
            {
                TotalAmount = result
            };

            Calculator calc = new Calculator(dataReader, repository);
            var actualResult = calc.ProcessData(Arg.Any<string>(), 0);

            repository.Received().Save(actualResult);

            Assert.IsTrue(Comparer.Compare(actualResult, expectedResult));
        }

        [Test]
        public void CalculatorTest_SinglePoint_Success()
        {
            const int result = 10;
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { result });

            CalculationResult expectedResult = new CalculationResult
            {
                TotalAmount = result
            };

            Calculator calc = new Calculator(dataReader, repository);
            var actualResult = calc.ProcessData(Arg.Any<string>(), 1);

            repository.Received().Save(actualResult);

            Assert.IsTrue(Comparer.Compare(actualResult, expectedResult));
        }

        [Test]
        public void CalculatorTest_TwoPoints_Success()
        {
            const int result = 14;
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { 6, 8 });

            CalculationResult expectedResult = new CalculationResult
            {
                TotalAmount = result
            };

            Calculator calc = new Calculator(dataReader, repository);
            var actualResult = calc.ProcessData(Arg.Any<string>(), 2);

            repository.Received().Save(actualResult);

            Assert.IsTrue(Comparer.Compare(actualResult, expectedResult));
        }

        [Test]
        public void CalculatorTest_FivePoints_Success()
        {
            const int result = 15;
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints("", 1).ReturnsForAnyArgs(info => new List<int> { 1, 2, 3, 4, 5 });

            CalculationResult expectedResult = new CalculationResult
            {
                TotalAmount = result
            };

            Calculator calc = new Calculator(dataReader, repository);
            var actualResult = calc.ProcessData(Arg.Any<string>(), 5);

            repository.Received().Save(actualResult);

            Assert.IsTrue(Comparer.Compare(actualResult, expectedResult));
        }

        [Test]
        public void CalculatorTest_ThrowsFormatException_Fail()
        {
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints(Arg.Any<string>(), Arg.Any<int>()).Returns(info => new List<int> { 1, 2, 3, 4, 5 });

            Calculator calc = new Calculator(dataReader, repository);

            Assert.Throws<FormatException>(() => calc.ProcessData(Arg.Any<string>(), 3));

            repository.DidNotReceiveWithAnyArgs().Save(null);
        }

        [Test]
        public void CalculatorTest_ThrowsArgumentException_Fail()
        {
            var dataReader = Substitute.For<IDataReader>();
            var repository = Substitute.For<IRepository>();
            dataReader.ReadDataPoints(Arg.Any<string>(), Arg.Any<int>()).Returns(info => new List<int> { -1, -2, -3, -4, 5 });

            Calculator calc = new Calculator(dataReader, repository);

            Assert.Throws<ArgumentException>(() => calc.ProcessData(Arg.Any<string>(), 5));

            repository.DidNotReceiveWithAnyArgs().Save(null);
        }
    }
}
