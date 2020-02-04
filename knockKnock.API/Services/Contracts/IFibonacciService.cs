using System.Threading.Tasks;

namespace knockKnock.API.Services.Contracts
{
    public interface IFibonacciService
    {
        public Task<long> SvrRecursiveFibonacci(long n1, long n2, long counter, long number);
        public Task<long> SvrFibonacci(long index);
    }
}
