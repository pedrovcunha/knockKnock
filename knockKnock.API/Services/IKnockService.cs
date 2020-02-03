using System.Threading.Tasks;

namespace knockKnock.API.Services
{
    public interface IKnockService
    {
        public Task<long> SvrRecursiveFibonacci(long n1, long n2, long counter, long number);
        public Task<long> SvrFibonacci(long index);
        public Task<string> SvrReverseWord(string sentence);
        public Task<KnockService.TriangleType> SrvTriangleType(int a, int b, int c);
        public Task<string> SrvToken();
    }
}
