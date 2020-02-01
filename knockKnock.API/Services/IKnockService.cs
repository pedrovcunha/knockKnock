namespace knockKnock.API.Services
{
    public interface IKnockService
    {
        public long SvrRecursiveFibonacci(long n1, long n2, long counter, long number);
        public long SvrFibonacci(long index);
        public string SvrReverseWord(string sentence);
        public KnockService.TriangleType SrvTriangleType(int a, int b, int c);
        public string SrvToken();
    }
}
