using System.Threading.Tasks;
using knockKnock.API.Models;

namespace knockKnock.API.Services.Contracts
{
    public interface ITriangleTypeService
    {
        public Task<Triangle.TriangleType> SrvTriangleType(int a, int b, int c);
    }
}
