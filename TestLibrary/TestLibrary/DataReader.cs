using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TestLibrary
{
    public class DataReader:IDataReader
    {
        public List<int> ReadDataPoints(string url, int pointsCount)
        {
            var random = new Random();
            var data = Enumerable.Range(0, pointsCount).Select(i => random.Next(-500, 500)).ToList();
            Console.WriteLine("Reading data from Url: {0}", url);
            Thread.Sleep(10000);
            return data;
        }
    }
}