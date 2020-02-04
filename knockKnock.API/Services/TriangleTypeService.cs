using System;
using System.Threading.Tasks;
using knockKnock.API.Models;
using knockKnock.API.Services.Contracts;

namespace knockKnock.API.Services
{
    public class TriangleTypeService : ITriangleTypeService
    {
        public Task<Triangle.TriangleType> SrvTriangleType(int a, int b, int c)
        {
            if (a <= 0 || b <= 0 || c <= 0)
                return Task.FromResult(Triangle.TriangleType.NotATriangle);
            if ((long)a + b <= c || (long)a + c <= b || (long)b + c <= a)
                return Task.FromResult(Triangle.TriangleType.NotATriangle);
            if (a == b && b == c)
                return Task.FromResult(Triangle.TriangleType.Equilateral);
            if ((a == b && b != c) || (a != b && b == c))
                return Task.FromResult(Triangle.TriangleType.Isosceles);

            return Task.FromResult(Triangle.TriangleType.Scalene);
        }
    }
}
