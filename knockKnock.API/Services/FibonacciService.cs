using System;
using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;

namespace knockKnock.API.Services
{
    public class FibonacciService : IFibonacciService
    {
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
    }
}
