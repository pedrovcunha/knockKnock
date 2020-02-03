using System;
using System.Linq;
using System.Threading.Tasks;

namespace knockKnock.API.Services
{
    public class KnockService : IKnockService
    {
        public enum TriangleType
        {
            Equilateral,
            Isosceles,
            Scalene,
            NotATriangle
        }

        // First call always start n1 = 0, n2 = 1, counter = 1, 
        public async Task<long> SvrRecursiveFibonacci(long n1, long n2, long counter, long number)
        {
            return number switch
            {
                0 => 0,
                1 => 1,
                _ => counter < number ? await SvrRecursiveFibonacci(n2, n1 + n2, counter++, number) : n2,
            };
        }

        public Task<long> SvrFibonacci(long index)
        {
            if (index < 0)
            {
                throw new ArgumentException(
                    $"The Fibonacci sequence starts from zero onwards. Therefore the value of {index} is not acceptable.",
                    nameof(index));
            }

            if (index == 0)
                return Task.FromResult<long>(0);

            long n1 = 0;
            long n2 = 1;
            long counter = 1;
            do
            {
                var temp = n2;
                n2 = n1 + n2;
                n1 = temp;

                counter++;

            } while (counter < index);

            return Task.FromResult<long>(n2);
        }

        public Task<string> SvrReverseWord(string sentence)
        {
            if (sentence == null)
                throw new ArgumentNullException(nameof(sentence), "The sequence can not be null.");

            return Task.FromResult<string>(string.Join(" ", sentence.Split(' ').Select(s => new string(s.Reverse().ToArray()))));
        }

        public Task<TriangleType> SrvTriangleType(int a, int b, int c)
        {
            if (a + b <= c || a + c <= b || b + c <= a)
                return Task.FromResult<TriangleType>(TriangleType.NotATriangle);
            if (a == b && b == c)
                return Task.FromResult<TriangleType>(TriangleType.Equilateral);
            if ((a == b && b != c) || (a != b && b == c))
                return Task.FromResult<TriangleType>(TriangleType.Isosceles);

            return Task.FromResult<TriangleType>(TriangleType.Scalene);
        }

        public Task<string> SrvToken()
        {
            return Task.FromResult<string>("e0a6432a-25b1-4a60-921e-23bd8c33f44d");
        }
    }
}
