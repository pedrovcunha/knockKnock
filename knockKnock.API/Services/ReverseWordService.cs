using System;
using System.Linq;
using System.Threading.Tasks;
using knockKnock.API.Services.Contracts;

namespace knockKnock.API.Services
{
    public class ReverseWordService : IReverseWordService
    {
        public Task<string> SvrReverseWord(string sentence)
        {
            if (sentence == null)
                throw new ArgumentNullException(nameof(sentence), "The sequence can not be null.");

            return Task.FromResult<string>(string.Join(" ", sentence.Split(' ').Select(s => new string(s.Reverse().ToArray()))));
        }
    }
}
