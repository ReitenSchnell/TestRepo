using System;
using System.Linq;

namespace TestLibrary
{
    public class Calculator
    {
        private readonly IDataReader dataReader;
        private readonly IRepository repository;

        public Calculator(IDataReader dataReader, IRepository repository)
        {
            this.dataReader = dataReader;
            this.repository = repository;
        }

        public CalculationResult ProcessData(string url, int pointsCount)
        {
            var dataPoints = dataReader.ReadDataPoints(url, pointsCount);
            if (dataPoints.Count != pointsCount)
                throw new FormatException("Data format is strange");
            var sum = dataPoints.Sum();
            if (sum < 0)
                throw new ArgumentException("Sum should be positive");
            var calculationResult = new CalculationResult {TotalAmount = sum};
            repository.Save(calculationResult);
            return calculationResult;
        }
    }
}
