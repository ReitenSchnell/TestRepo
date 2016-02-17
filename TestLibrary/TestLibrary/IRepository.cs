using System.Security.Cryptography.X509Certificates;

namespace TestLibrary
{
    public interface IRepository
    {
        void Save(CalculationResult calculationResult);
    }
}