using System;
using System.Threading;

namespace TestLibrary
{
    public class Repository : IRepository
    {
        public void Save(CalculationResult calculationResult)
        {
            Console.WriteLine("Saving data");
            Thread.Sleep(10000);
            Console.WriteLine("Saved");
        }
    }
}