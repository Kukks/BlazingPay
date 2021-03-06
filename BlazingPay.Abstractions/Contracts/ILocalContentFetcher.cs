using System.IO;
using System.Threading.Tasks;

namespace BlazingPay.Abstractions.Contracts
{
    public interface ILocalContentFetcher
    {
        Task<Stream> Fetch(string path);
    }
}