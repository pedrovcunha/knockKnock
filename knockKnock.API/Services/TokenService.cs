using System;
using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;

namespace knockKnock.API.Services
{
    public class TokenService : ITokenService
    {
        public Task<Guid> SrvToken()
        {
            return Task.FromResult<Guid>(new Guid("e0a6432a-25b1-4a60-921e-23bd8c33f44d"));
        }
    }
}
