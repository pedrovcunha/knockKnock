using System;
using System.Threading.Tasks;

namespace knockKnock.API.Services.Contracts
{
    public interface ITokenService
    {
        public Task<Guid> SrvToken();
    }
}
