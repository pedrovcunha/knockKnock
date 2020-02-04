using System.Threading.Tasks;

namespace knockKnock.API.Services.Contracts
{
    public interface IReverseWordService
    {
        public Task<string> SvrReverseWord(string sentence);
    }
}
