using System.Collections.Generic;

namespace TestLibrary
{
    public interface IDataReader
    {
        List<int> ReadDataPoints(string url, int pointsCount);
    }
}